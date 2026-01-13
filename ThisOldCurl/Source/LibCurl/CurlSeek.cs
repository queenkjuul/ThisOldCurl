using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using curl_off_t = System.Int64;

namespace ThisOldCurl.LibCurl
{
    /// <summary>
    /// These are the return codes for the seek callbacks
    /// </summary>
    public enum CurlSeekCode : int
    {
        CURL_SEEKFUNC_OK = 0,
        /// <summary>
        /// fail the entire transfer
        /// </summary>
        CURL_SEEKFUNC_FAIL,
        /// <summary>
        /// tell libcurl seeking can't be done, so try something else
        /// </summary>
        CURL_SEEKFUNC_CANTSEEK
    }

    /// <param name="instream"></param>
    /// <param name="offset"></param>
    /// <param name="origin">" 'whence' "</param>
    /// <returns>CurlSeekCode (int 0..2)</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate CurlSeekCode CurlSeekCallback(
        IntPtr instream,
        curl_off_t offset,
        int origin);
}
