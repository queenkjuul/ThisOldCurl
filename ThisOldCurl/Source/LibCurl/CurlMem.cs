using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using size_t = System.UInt32;

namespace ThisOldCurl.LibCurl
{
    /*
     * The following typedef's are signatures of malloc, free, realloc, strdup and
     * calloc respectively.  Function pointers of these types can be passed to the
     * curl_global_init_mem() function to set user defined memory management
     * callback routines.
     */
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr CurlMallocCallback(size_t size);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void CurlFreeCallback(IntPtr ptr);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr CurlReallocCallback(IntPtr ptr, size_t size);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr CurlStrdupCallback(IntPtr str);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr CurlCallocCallback(size_t nmemb, size_t size);

    public static partial class Curl
    {
        /// <summary>
        /// curl_global_init() or curl_global_init_mem() should be invoked exactly once
        /// for each application that uses libcurl.  This function can be used to
        /// initialize libcurl and set user defined memory management callback
        /// functions.  Users can implement memory management routines to check for
        /// memory leaks, check for mis-use of the curl library etc.  User registered
        /// callback routines with be invoked by this library instead of the system
        /// memory management routines like malloc, free etc.
        /// </summary>
        /// <param name="flags"></param>
        /// <param name="m"></param>
        /// <param name="f"></param>
        /// <param name="r"></param>
        /// <param name="s"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLcode curl_global_init_mem(
            int flags,
            CurlMallocCallback m,
            CurlFreeCallback f,
            CurlReadCallback r,
            CurlStrdupCallback s,
            CurlCallocCallback c);
    }
}
