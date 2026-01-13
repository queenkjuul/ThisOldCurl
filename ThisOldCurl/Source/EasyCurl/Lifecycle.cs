using System;
using System.Collections.Generic;
using System.Text;
using ThisOldCurl.LibCurl;
using System.Runtime.InteropServices; 

using CURL = System.IntPtr; // CURL handle

namespace ThisOldCurl
{
    public partial class EasyCurl
    {
        public EasyCurl() : this(false) { }
        public EasyCurl(CURL curl) : this(false, curl) { }
        public EasyCurl(bool debug) : this(debug, null) { }
        public EasyCurl(bool debug, CURL? curl)
        {
            this.curl = curl ?? Curl.curl_easy_init();
            if (this.curl == IntPtr.Zero)
                throw new ExternalException("[libcurl] [ERROR] curl_easy_init failed!");
            if (debug)
            {
                this.DebugLogging = true;
            }
            this.SetOpt(CURLoption.CURLOPT_CAINFO, this.CaCertPath);
            this.SetOpt(CURLoption.CURLOPT_NOPROGRESS, 0);
            this.assignDefaultCallbacks();
        }
        ~EasyCurl()
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

            // clean up unmanaged resources no matter what
            if (curl != IntPtr.Zero)
            {
                Curl.curl_easy_cleanup(this.curl);
                this.curl = IntPtr.Zero;
            }
            this.share = null;

            if (this.form != null)
                this.form.Dispose();
            if (slist != IntPtr.Zero)
                Curl.curl_slist_free_all(slist);

            // clean up managed resources only if called publicly
            if (disposing)
            {
                this.callbacks.Clear();
                this.timeout = null;
                this.headers = null;
                this.WriteEvent = null;
                this.DebugEvent = null;
                this.HeaderEvent = null;
                this.TransferCompleteEvent = null;
            }

            this.disposed = true;
        }
        /// <summary>
        /// Duplicates an EasyCurl instance by using curl_easy_duphandle.
        /// DebugLogging state is reproduced and cannot be overridden.
        /// </summary>
        /// <returns></returns>
        public EasyCurl Clone()
        {
            if (this.disposed)
                throw new ObjectDisposedException("EasyCurl");
            return new EasyCurl(
                this.DebugLogging,
                Curl.curl_easy_duphandle(this.curl));
        }
    }
}
