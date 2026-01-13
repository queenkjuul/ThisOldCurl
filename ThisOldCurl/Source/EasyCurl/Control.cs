using System;
using System.Collections.Generic;
using System.Text;
using ThisOldCurl.LibCurl;

namespace ThisOldCurl
{
    public partial class EasyCurl
    {
        /// <summary>
        /// used only within ThisOldCurl, because it MUST be called before 
        /// MultiCurl.Perform. If called in the wrong
        /// order, things could go wrong, so public use is not permitted
        /// (despite the library generally preferring to expose all its internals)
        /// </summary>
        internal void commitConfig()
        {
            if (this.url != null)
                this.SetOpt(CURLoption.CURLOPT_URL, this.url);

            if (this.headers != null && this.headers.Count > 0)
            {
                foreach (string key in headers.AllKeys)
                {
                    slist = Curl.curl_slist_append(
                        slist,
                        key + ": " + headers[key]);
                }
                this.SetOpt(CURLoption.CURLOPT_HTTPHEADER, slist);
            }
            if (this.uploadStream != null)
            {
                this.uploadStream.Position = 0;
                this.SetOpt(CURLoption.CURLOPT_UPLOAD, 1);
            }
            if (this.form != null)
                this.SetOpt(CURLoption.CURLOPT_HTTPPOST, this.form.First);
            if (this.method != null)
                this.SetOpt(CURLoption.CURLOPT_CUSTOMREQUEST, this.Method);
            if (this.share != null)
                this.SetOpt(CURLoption.CURLOPT_SHARE, this.share.Handle);
        }
            
        public CURLcode Perform()
        {
            this.notDisposed();
            this.commitConfig();

            CURLcode result = handleCurlCode(Curl.curl_easy_perform(this.curl));

            this.FinalizeTransfer();
            return result;
        }

        /// <summary>
        /// NOTICE: This method is unnecessary for normal use cases
        /// This method should be called only once, after the transfer has been performed.
        /// EasyCurl.Perform() will take care of this for you; there is no need to call it
        /// on your own. This is only necessary when using a custom Perform/Wait loop
        /// in conjunction with MultiCurl, or potentially when using Send/Recv/CONNECT_ONLY
        /// should you want to combine that with TransferCompleteEvent.
        /// </summary>
        public void FinalizeTransfer()
        {
            this.performed = true;
            this.captureInfo();
            if (this.TransferCompleteEvent != null)
                this.TransferCompleteEvent.Invoke(this);
        }

        public void Pause()
        {
            this.notDisposed();
            this.pauseRequest = true;
            handleCurlCode(Curl.curl_easy_pause(this.curl, CurlPauseFlags.CURLPAUSE_ALL));
        }

        public void Resume()
        {
            this.notDisposed();
            this.pauseRequest = false;
            handleCurlCode(Curl.curl_easy_pause(this.curl, CurlPauseFlags.CURLPAUSE_CONT));
        }

        public void Abort()
        {
            this.notDisposed();
            this.abortRequest = true;
        }

        /// <summary>
        /// Re-initializes a CURL handle to the default values. 
        /// This puts back the handle to the same state as it 
        /// was in when it was just created.
        /// 
        /// It does keep: live connections, the Session ID cache, 
        /// the DNS cache and the cookies.
        /// </summary>
        public void Reset()
        {
            this.notDisposed();
            this.performed = false;
            this.pauseRequest = false;
            this.abortRequest = false;
            this.connectOnly = false;
            this.timeout = null;
            this.headers = null;
            this.callbacks.Clear();
            this.info.Clear();

            Curl.curl_easy_reset(this.curl);
            
            this.assignDefaultCallbacks();
        }
    }
}
