using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ThisOldCurl.LibCurl
{
    /// <summary>
    /// return codes for FNMATCHFUNCTION
    /// </summary>
    public enum CurlFnMatchCode
    {
        /// <summary>
        /// string corresponds to the pattern
        /// </summary>
        CURL_FNMATCHFUNC_MATCH = 0,
        /// <summary>
        /// pattern doesn't match the string
        /// </summary>
        CURL_FNMATCHFUNC_NOMATCH = 1,
        /// <summary>
        /// error
        /// </summary>
        CURL_FNMATCHFUNC_FAIL = 2
    }

    /// <summary>
    /// callback type for wildcard downloading pattern matching. If the
    /// string matches the pattern, return CURL_FNMATCHFUNC_MATCH value, etc.
    /// </summary>
    /// <param name="ptr"></param>
    /// <param name="pattern"></param>
    /// <param name="str"></param>
    /// <returns></returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate CurlFnMatchCode CurlFnMatchCallback(
        IntPtr ptr,
        IntPtr pattern,
        IntPtr str);
}
