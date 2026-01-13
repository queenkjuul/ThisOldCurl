using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using CURLSH = System.IntPtr; // CURLSH share handle
using CURL = System.IntPtr; // CURL easy handle


namespace ThisOldCurl.LibCurl
{
    public enum CURLSHcode
    {
        /// <summary>
        /// all is fine
        /// </summary>
        CURLSHE_OK,
        CURLSHE_BAD_OPTION, /* 1 */
        CURLSHE_IN_USE,     /* 2 */
        CURLSHE_INVALID,    /* 3 */
        /// <summary>
        /// out of memory
        /// </summary>
        CURLSHE_NOMEM,
        /// <summary>
        /// feature not present in lib
        /// </summary>
        CURLSHE_NOT_BUILT_IN,
        /// <summary>
        /// never use (last in enum)
        /// </summary>
        CURLSHE_LAST
    }

    public enum CURLSHoption
    {
        /// <summary>
        /// DON'T USE
        /// </summary>
        CURLSHOPT_NONE,
        /// <summary>
        /// specify a data type to share
        /// </summary>
        CURLSHOPT_SHARE,
        /// <summary>
        /// specify which data type to stop sharing
        /// </summary>
        CURLSHOPT_UNSHARE,
        /// <summary>
        /// pass in a 'curl_lock_function' pointer
        /// </summary>
        CURLSHOPT_LOCKFUNC,
        /// <summary>
        /// pass in a 'curl_unlock_function' pointer
        /// </summary>
        CURLSHOPT_UNLOCKFUNC,
        /// <summary>
        /// pass in a user data pointer used in the lock/unlock callback functions
        /// </summary>
        CURLSHOPT_USERDATA,   
        /// <summary>
        /// NEVER USE (last in enum)
        /// </summary>
        CURLSHOPT_LAST
    }

    /// <summary>
    /// Different data locks for a single share
    /// </summary>
    public enum curl_lock_data
    {
        CURL_LOCK_DATA_NONE = 0,
        /// <summary>
        /// CURL_LOCK_DATA_SHARE is used internally to say that
        /// the locking is just made to change the internal state of the share
        /// itself.
        /// </summary>
        CURL_LOCK_DATA_SHARE,
        CURL_LOCK_DATA_COOKIE,
        CURL_LOCK_DATA_DNS,
        CURL_LOCK_DATA_SSL_SESSION,
        CURL_LOCK_DATA_CONNECT,
        CURL_LOCK_DATA_LAST
    }

    /// <summary>
    /// Different lock access types
    /// </summary>
    public enum curl_lock_access
    {
        /// <summary>
        /// unspecified action
        /// </summary>
        CURL_LOCK_ACCESS_NONE = 0,
        /// <summary>
        /// for read perhaps
        /// </summary>
        CURL_LOCK_ACCESS_SHARED = 1,
        /// <summary>
        /// for write perhaps
        /// </summary>
        CURL_LOCK_ACCESS_SINGLE = 2,
        /// <summary>
        /// never use
        /// </summary>
        CURL_LOCK_ACCESS_LAST
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void CurlLockCallback(
        CURL handle,
        curl_lock_data data,
        curl_lock_access locktype,
        IntPtr userptr);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void CurlUnlockCallback(
        CURL handle,
        curl_lock_data data,
        IntPtr userptr);

    public static partial class Curl
    {
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLSH curl_share_init();
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLSHcode curl_share_setopt(CURLSH curlsh, CURLSHoption option, curl_lock_data data);
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLSHcode curl_share_setopt(CURLSH curlsh, CURLSHoption option, IntPtr ptr);
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLSHcode curl_share_cleanup(CURLSH curlsh);
        /// <summary>
        /// The curl_share_strerror function may be used to turn a CURLSHcode value
        /// into the equivalent human readable error string.  This is useful
        /// for printing meaningful error messages.
        /// </summary>
        /// <param name="code"></param>
        /// <returns>pointer to a string</returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern string curl_share_strerror(CURLSHcode code);
    }
}
