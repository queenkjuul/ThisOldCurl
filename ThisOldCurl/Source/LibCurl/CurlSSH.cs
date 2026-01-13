using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using CURL = System.IntPtr; // CURL handle
using curl_khkey_ = System.IntPtr; // pointer to curl_khkey
using size_t = System.UInt32;

namespace ThisOldCurl.LibCurl
{
    public enum curl_khtype
    {
        CURLKHTYPE_UNKNOWN,
        CURLKHTYPE_RSA1,
        CURLKHTYPE_RSA,
        CURLKHTYPE_DSS
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct curl_khkey
    {
        /// <summary>
        /// points to a zero-terminated string encoded with base64 if len is zero, otherwise to the "raw" data 
        /// </summary>
        IntPtr key;     
        size_t len;
        curl_khtype keytype;
    }

    /// <summary>
    /// this is the set of return values expected from the curl_sshkeycallback
    /// callback
    /// </summary>
    public enum curl_khstat
    {
        CURLKHSTAT_FINE_ADD_TO_FILE,
        CURLKHSTAT_FINE,
        /// <summary>
        /// reject the connection, return an error
        /// </summary>
        CURLKHSTAT_REJECT,
        /// <summary>
        /// do not accept it, but we can't answer right now so
        /// this causes a CURLE_DEFER error but otherwise the
        /// connection will be left intact etc 
        /// </summary>
        CURLKHSTAT_DEFER, 
        /// <summary>
        /// not for use, only a marker for last-in-list
        /// </summary>
        CURLKHSTAT_LAST
    };

    /// <summary>
    /// this is the set of status codes pass in to the callback
    /// </summary>
    public enum curl_khmatch
    {
        /// <summary>
        /// match
        /// </summary>
        CURLKHMATCH_OK,
        /// <summary>
        /// host found, key mismatch!
        /// </summary>
        CURLKHMATCH_MISMATCH,
        /// <summary>
        /// no matching host/key found
        /// </summary>
        CURLKHMATCH_MISSING,
        /// <summary>
        /// not for use, only a marker for last-in-list
        /// </summary>
        CURLKHMATCH_LAST
    };

    /// <param name="easy">CURL handle</param>
    /// <param name="knownkey">pointer to curl_khkey</param>
    /// <param name="foundkey">pointer to curl_khkey</param>
    /// <param name="khmatch">libcurl's view on the keys</param>
    /// <param name="clientp">custom pointer passed from app</param>
    /// <returns></returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int CurlSshKeyCallback(
        CURL easy,
        curl_khkey_ knownkey,
        curl_khkey_ foundkey,
        curl_khmatch khmatch,
        IntPtr clientp);
}
