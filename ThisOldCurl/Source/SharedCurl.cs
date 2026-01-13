using System;
using System.Collections.Generic;
using System.Text;
using ThisOldCurl.LibCurl;
using System.Runtime.InteropServices;

using CURLSH = System.IntPtr; // CURLSH handle

namespace ThisOldCurl
{
    /// <summary>
    /// Simple wrapper around the curl_share API
    /// 
    /// Allows you to share resources between requests - such as cookies, DNS cache, etc.
    /// One SharedCurl instance can be shared between many EasyCurl instances,
    /// even across multiple threads - as long as each EasyCurl has its own thread, 
    /// all can share one SharedCurl instance.
    /// 
    /// You can also use SharedCurl with MultiCurl - if MultiCurl.ShareHandle is defined,
    /// it will automatically be added to all attached EasyCurl instances - even if you 
    /// called MultiCurl.Add before MutliCurl.ShareHandle
    /// 
    /// WARNING: You must not Dispose SharedCurl until after all shared EasyCurl
    /// objects have also been Disposed. SharedCurl does not keep references to consumers;
    /// it cannot enforce the order in which you Dispose it.
    /// 
    /// </summary>
    public sealed class SharedCurl : IDisposable
    {
        private CURLSH curlsh;
        private List<Delegate> callbacks = new List<Delegate>();
        private bool disposed = false;

        public SharedCurl()
        {
            curlsh = Curl.curl_share_init();
            if (curlsh == IntPtr.Zero)
                throw new InvalidOperationException("[libcurl] [ERROR] curl_share_init failed");
        }
        ~SharedCurl()
        {
            this.Dispose(false);
        }
        private void Dispose(bool disposing)
        {
            if (this.disposed)
                return;
            if (disposing)
                this.callbacks.Clear();
            if (this.curlsh != IntPtr.Zero)
            {
                Curl.curl_share_cleanup(this.curlsh);
                this.curlsh = IntPtr.Zero;
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public static string ErrorString(CURLSHcode code)
        {
            return Curl.curl_share_strerror(code);
        }

        private void notDisposed()
        {
            if (this.disposed)
                throw new ObjectDisposedException("SharedCurl");
        }

        public CURLSHcode SetOpt(CURLSHoption option, curl_lock_data value)
        {
            notDisposed();
            if (option != CURLSHoption.CURLSHOPT_SHARE && option != CURLSHoption.CURLSHOPT_UNSHARE)
                throw new ArgumentException("[SharedCurl] Received curl_lock_data but option was not CURLSHOPT_SHARE or CURLSHOPT_UNSHARE");
            return Curl.curl_share_setopt(curlsh, option, value);
        }
        public CURLSHcode SetOpt(CURLSHoption option, IntPtr value)
        {
            notDisposed();
            if (option != CURLSHoption.CURLSHOPT_USERDATA)
                throw new ArgumentException("[SharedCurl] Received IntPtr but option was not CURLSHOPT_USERDATA");
            return Curl.curl_share_setopt(curlsh, option, value);
        }
        public CURLSHcode SetOpt(CURLSHoption option, CurlLockCallback callback)
        {
            notDisposed();
            if (option != CURLSHoption.CURLSHOPT_LOCKFUNC)
                throw new ArgumentException("[SharedCurl] Received CurlLockCallback but was not CURLSHOPT_LOCKFUNC");
            IntPtr ptr = Marshal.GetFunctionPointerForDelegate(callback);
            CURLSHcode result = Curl.curl_share_setopt(curlsh, option, ptr);
            this.callbacks.Add(callback);
            return result;
        }
        public CURLSHcode SetOpt(CURLSHoption option, CurlUnlockCallback callback)
        {
            notDisposed();
            if (option != CURLSHoption.CURLSHOPT_UNLOCKFUNC)
                throw new ArgumentException("[SharedCurl] Received CurlUnlockCallback but was not CURLSHOPT_UNLOCKFUNC");
            IntPtr ptr = Marshal.GetFunctionPointerForDelegate(callback);
            CURLSHcode result = Curl.curl_share_setopt(curlsh, option, ptr);
            this.callbacks.Add(callback);
            return result;
        }

        public CURLSH Handle
        {
            get { notDisposed(); return curlsh; }
        }
    }
}
