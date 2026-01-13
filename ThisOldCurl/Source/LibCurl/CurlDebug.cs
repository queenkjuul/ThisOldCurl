using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using CURL = System.IntPtr; // CURL handle
using size_t = System.UInt32;

namespace ThisOldCurl.LibCurl
{
    /// <summary>
    /// the kind of data that is passed to information_callback*/
    /// </summary>
    public enum curl_infotype
    {
        CURLINFO_TEXT = 0,
        CURLINFO_HEADER_IN,
        CURLINFO_HEADER_OUT,
        CURLINFO_DATA_IN,
        CURLINFO_DATA_OUT,
        CURLINFO_SSL_DATA_IN,
        CURLINFO_SSL_DATA_OUT,
        CURLINFO_END
    }

    /// <param name="handle">the handle/transfer this concerns</param>
    /// <param name="type">type of data</param>
    /// <param name="data">points to data</param>
    /// <param name="size">size of data</param>
    /// <param name="userPtr"></param>
    /// <returns></returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int CurlDebugCallback(
        IntPtr handle,
        curl_infotype type,
        IntPtr data,
        size_t size,
        IntPtr userPtr);        
}
