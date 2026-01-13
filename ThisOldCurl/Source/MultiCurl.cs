using System;
using System.Collections.Generic;
using System.Text;
using ThisOldCurl.LibCurl;
using System.Runtime.InteropServices;

using CURLM = System.IntPtr;
using System.IO; // CURLM multi handle

namespace ThisOldCurl
{
    public delegate void MultiSingleCompleteHandler(EasyCurl handle);
    public delegate void MultiAllCompleteHandler(List<EasyCurl> handles);
    /// <summary>
    /// Wrapper around the curl_multi API
    /// 
    /// Creates a multi handle when instantiated and cleans it up when disposed.
    /// Takes ownership of any handles passed to it - if you call MultiCurl.Dispose(),
    /// all associated EasyCurl instances will also be disposed.
    /// 
    /// If you want to keep an EasyCurl handle for reuse, call MultiCurl.Remove
    /// before calling MultiCurl.Dispose to unlink the EasyCurl handle and keep 
    /// it alive after MultiCurl.Dispose has been called.
    /// 
    /// PerformAll will run all transfers and return when complete, emitting both
    /// TransferComplete and AllTransfersComplete along the way.
    /// 
    /// The MultiCurl class will enforce only 1 SharedCurl instance per MultiCurl.
    /// You can bypass this check by assigning the SharedCurl handle directly on each
    /// EasyCurl object using: 
    /// 
    /// ForEach(delegate(EasyCurl curl) { curl.SetOpt(CURLOPT_SHARE, IntPtr curlsh); }
    /// 
    /// or you can assign a share handle to each individual instance after adding it
    /// to the MultiCurl instance. 
    /// 
    /// If MultiCurl.ShareHandle is set prior to calling Add(), that handle will be added
    /// to the new EasyCurl handle. If an EasyCurl is passed to Add(easy) which already has 
    /// a ShareHandle, the existing handle will not overwritten. If it does not, 
    /// MultiCurl.ShareHandle will be added to it. 
    /// 
    /// Setting MultiCurl.ShareHandle after calling Add(easy) will attach that share handle to all
    /// existing EasyCurl handles. If you don't want to apply to all existing EasyCurl instances,
    /// use ForEach with a filter, or reattach your desired SharedCurl instance to each individual
    /// EasyCurl directly.
    /// 
    /// You can access the full API via the LibCurl.Curl static class if you need to.
    /// MultiCurl.Handle provides a pointer you can pass to those functions.
    /// 
    /// MultiCurl is designed, like the underlying libcurl API, to have one MultiCurl per thread.
    /// EasyCurl instances can normally be run on separate threads, but not if they're associated
    /// with a MultiCurl. I don't know how to enforce this - I also don't know how you would actually
    /// construct such a scenario. Again, one SharedCurl can be spread across threads, but EasyCurl
    /// instances must run on the same thead as their associated MutliCurl.
    /// 
    /// NOTICE: MultiCurl depends on the default functionality of its associated EasyCurl instances.
    /// That means that if you overwrite the default callback of a member EasyCurl 
    /// (for example, by calling EasyCurl.SetOpt(CURLoption.CURLOPT_WRITEFUNCTION, callback))
    /// that may interfere with the behavior of MultiCurl in unexpected ways. When overriding
    /// the default libcurl callbacks, it is best to replace them all entirely, and rely
    /// only on MultiCurl/EasyCurl/SharedCurl only for managing handles and lifecycles.
    /// 
    /// </summary>
    public sealed class MultiCurl : IDisposable
    {
        private CURLM multi;
        private bool disposed = false;
        private List<EasyCurl> handles = new List<EasyCurl>();
        private SharedCurl share = null;
        private bool debugLogging = false;

        public event MultiSingleCompleteHandler TransferComplete;
        public event MultiAllCompleteHandler AllTransfersComplete;

        public MultiCurl()
        {
            this.multi = Curl.curl_multi_init();
            if (this.multi == IntPtr.Zero)
                throw new ExternalException("[libcurl] curl_multi_init failed");
        }
        ~MultiCurl()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (this.disposed)
                return;

            // always clean up unmanaged
            if (multi != IntPtr.Zero)
            {
                Curl.curl_multi_cleanup(this.multi);
                this.multi = IntPtr.Zero;
            }
            this.share = null;

            // only clean up managed if called publicly
            if (disposing)
            {
                this.ForEach(delegate(EasyCurl handle) { handle.Dispose(); });
                this.handles.Clear();
                this.TransferComplete = null;
                this.AllTransfersComplete = null;
            }
        }

        public EasyCurl getEasyByHandle(IntPtr handle)
        {
            return this.handles.Find(delegate(EasyCurl easy)
            {
                return easy.Handle == handle;
            });
        }


        private CURLMcode handleCurlMcode(CURLMcode code)
        {
            if (code != CURLMcode.CURLM_OK)
                throw new ExternalException("[libcurl] [ERROR] Curl multi exception: " + ErrorString(code));
            return code;
        }

        private void notDisposed()
        {
            if (this.disposed)
                throw new ObjectDisposedException("MultiCurl");
        }

        /// <summary>
        /// The curl_multi_strerror function may be used to turn a CURLMcode
        /// value into the equivalent human readable error string.  This is
        /// useful for printing meaningful error messages.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string ErrorString(CURLMcode code)
        {
            return Curl.curl_multi_strerror(code);
        }

        /// <summary>
        /// add an EasyCurl handle to the multi stack.
        /// All added handles will be cleaned up with MultiCurl.Dispose()
        /// </summary>
        /// <param name="easy"></param>
        /// <returns></returns>
        public CURLMcode Add(EasyCurl easy)
        {
            this.notDisposed();
            if (easy == null)
                throw new ArgumentNullException("easy");
            if (this.handles.Contains(easy))
                throw new InvalidOperationException("EasyCurl already added to this MultiCurl");
            CURLMcode result = handleCurlMcode(Curl.curl_multi_add_handle(this.multi, easy.Handle));
            this.handles.Add(easy);
            if (this.share != null)
                easy.ShareHandle = this.ShareHandle;
            easy.TransferCompleteEvent += new EasyTransferCompleteHandler(delegate(EasyCurl handle)
            {
                if (this.TransferComplete != null)
                    this.TransferComplete(handle);
            });
            return result;
        }
        public EasyCurl Add() { return this.Add((string)null); }
        public EasyCurl Add(string url)
        {
            this.notDisposed();
            EasyCurl handle = new EasyCurl(this.debugLogging);
            if (url != null)
                handle.URL = url;
            this.Add(handle);
            return handle;
        }

        /// <summary>
        /// removes a curl handle from the multi stack.
        /// this returns ownership of the handle to the caller:
        /// MultiCurl will not clean it up when it is disposed
        /// (this way you can remove the handle and still re-use it)
        /// </summary>
        /// <param name="easy"></param>
        /// <returns></returns>
        public CURLMcode Remove(EasyCurl easy)
        {
            this.notDisposed();
            if (easy == null)
                throw new ArgumentNullException("easy");
            if (!this.handles.Contains(easy))
                throw new InvalidOperationException("EasyCurl not owned by this MultiCurl");
            CURLMcode result = handleCurlMcode(Curl.curl_multi_remove_handle(this.multi, easy.Handle));
            this.handles.Remove(easy);
            return result;
        }

        public void ForEach(Action<EasyCurl> action)
        {
            this.notDisposed();
            if (action == null)
                throw new ArgumentNullException("action");
            if (this.handles.Count < 1)
                return;
            foreach (EasyCurl handle in this.handles)
            {
                action(handle);
            }
        }

        /// <summary>
        /// Used in conjunction with Perform(),
        /// assumes you are using your own control loop to manage transfers.
        /// Wraps native curl_mutli_wait with a default timeout of 100ms
        /// </summary>
        /// <returns></returns>
        public CURLMcode Wait() { return Wait(100); }
        public CURLMcode Wait(int timeoutMillis)
        {
            this.notDisposed();
            return handleCurlMcode(
                Curl.curl_multi_wait(
                this.multi,
                new curl_waitfd[0],
                0,
                timeoutMillis,
                IntPtr.Zero));
        }

        /// <summary>
        /// NOTICE: You probably want PerformAll(). This maps to curl_multi_perform,
        /// which assumes you've set up your own messaging/retry loop.
        /// PerformAll will run all transfers and return when they are all complete.
        /// 
        /// If you are running your own Perform/Wait loop, you are responsible for
        /// calling EasyCurl.
        /// 
        /// libcurl docs:
        /// 
        /// When the app thinks there's data available for curl it calls this
        /// function to read/write whatever there is right now. This returns
        /// as soon as the reads and writes are done. This function does not
        /// require that there actually is data available for reading or that
        /// data can be written, it can be called just in case.
        /// </summary>
        /// <returns>number of handles still transferring</returns>
        public int Perform()
        {
            this.notDisposed();
            this.ForEach(delegate(EasyCurl handle) { handle.commitConfig(); });
            int runningHandles = 0;
            handleCurlMcode(Curl.curl_multi_perform(this.multi, ref runningHandles));
            return runningHandles;
        }

        public void PerformAll()
        {
            this.notDisposed();
            int runningTransfers;
            do
            {
                runningTransfers = this.Perform();
                IntPtr msgPtr;
                int msgsLeft;

                while ((msgPtr = Curl.curl_multi_info_read(this.multi, out msgsLeft)) != IntPtr.Zero)
                {
                    CURLMsg msg = (CURLMsg)Marshal.PtrToStructure(msgPtr, typeof(CURLMsg));
                    if (msg.msg == CURLMSG.CURLMSG_DONE)
                    {
                        EasyCurl easy = this.getEasyByHandle(msg.easy_handle);
                        if (easy != null)
                        {
                            if (easy.DownloadStream != null)
                                easy.DownloadStream.Position = 0;
                            easy.FinalizeTransfer();
                        }
                    }
                }

                if (runningTransfers > 0)
                {
                    this.Wait();
                }
            } while (runningTransfers > 0);
            if (AllTransfersComplete != null)
            {
                AllTransfersComplete.Invoke(this.handles);
            }
        }

        public CURLM Handle
        {
            get { return this.multi; }
        }

        public List<EasyCurl> Handles
        {
            get { return this.handles; }
        }

        public SharedCurl ShareHandle
        {
            get { return this.share; }
            set
            {
                this.ForEach(delegate(EasyCurl handle)
                {
                    handle.ShareHandle = value;
                });
                this.share = value;
            }
        }

        public bool DebugLogging
        {
            set { this.debugLogging = value; }
        }
    }
}
