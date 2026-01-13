using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using CURL = System.IntPtr; // CURL handle
using curl_off_t = System.Int64;

namespace ThisOldCurl.LibCurl
{
    [Flags]
    public enum CurlPauseFlags
    {
        CURLPAUSE_RECV = 1 << 0,
        CURLPAUSE_RECV_CONT = 0,

        CURLPAUSE_SEND = 1 << 2,
        CURLPAUSE_SEND_CONT = 0,

        CURLPAUSE_ALL = CURLPAUSE_RECV | CURLPAUSE_SEND,
        CURLPAUSE_CONT = CURLPAUSE_RECV_CONT | CURLPAUSE_SEND_CONT
    }
 
    public static partial class Curl
    {
        /// <summary>
        /// Initializes libcurl, returns a CURL handle
        /// </summary>
        /// <returns>CURL handle</returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURL curl_easy_init();

        /// <summary>
        /// Set CURL options before performing a transfer
        /// </summary>
        /// <param name="curl">CURL handle</param>
        /// <param name="option">CURLoption flag</param>
        /// <param name="value">pointer, string, int, or Int64 (type depends on flag)</param>
        /// <returns>CURLcode error code</returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLcode curl_easy_setopt(CURL curl, CURLoption option, IntPtr value);
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLcode curl_easy_setopt(CURL curl, CURLoption option, string value);
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLcode curl_easy_setopt(CURL curl, CURLoption option, int value);
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLcode curl_easy_setopt(CURL curl, CURLoption option, curl_off_t value);

        /// <summary>
        /// Performs a transfer with the provided CURL handle
        /// </summary>
        /// <param name="curl">CURL handle</param>
        /// <returns>CURLcode error code</returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLcode curl_easy_perform(CURL curl);

        /// <summary>
        /// Tear down libcurl and free handle
        /// </summary>
        /// <param name="curl">CURL handle</param>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void curl_easy_cleanup(CURL curl);

        /// <summary>
        /// The curl_easy_strerror function may be used to turn a CURLcode value
        /// into the equivalent human readable error string.  This is useful
        /// for printing meaningful error messages.
        /// </summary>
        /// <param name="code">CURLcode error code</param>
        /// <returns>pointer to a string</returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern string curl_easy_strerror(CURLcode code);

        /// <summary>
        /// The curl_easy_pause function pauses or unpauses transfers. Select the new
        /// state by setting the bitmask, use the convenience enum CurlPauseMask.
        /// </summary>
        /// <param name="curl">CURL handle</param>
        /// <param name="bitmask">CurlPauseMask bitwise flags</param>
        /// <returns>CURLcode error code</returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLcode curl_easy_pause(CURL curl, CurlPauseFlags bitmask);

        /// <summary>
        /// Escapes URL strings (converts all letters consider illegal in URLs to their
        /// %XX versions). This function returns a new allocated string or NULL if an
        /// error occurred.
        /// </summary>
        /// <param name="handle">CURL handle</param>
        /// <param name="str">string</param>
        /// <param name="length">length of the string</param>
        /// <returns>string</returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern string curl_easy_escape(CURL handle, string str, int length);

        /// <summary>
        /// Unescapes URL encoding in strings (converts all %XX codes to their 8bit
        /// versions). This function returns a new allocated string or NULL if an error
        /// occurred.
        /// 
        /// Conversion Note: On non-ASCII platforms the ASCII %XX codes are
        /// converted into the host encoding.
        /// </summary>
        /// <param name="handle">CURL handle</param>
        /// <param name="str">string</param>
        /// <param name="length">length of the string</param>
        /// <param name="outlength">pointer to an int</param>
        /// <returns>string</returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern string curl_easy_unescape(CURL handle, string str, int length, IntPtr outlength);

        /// <summary>
        /// Request internal information from the curl session with this function.  The
        /// third argument MUST be a pointer to a long, a pointer to a char * or a
        /// pointer to a double (as the documentation describes elsewhere).  The data
        /// pointed to will be filled in accordingly and can be relied upon only if the
        /// function returns CURLE_OK.  This function is intended to get used *AFTER* a
        /// performed transfer, all results from this function are undefined until the
        /// transfer is completed
        /// </summary>
        /// <param name="curl">CURL handle</param>
        /// <param name="info"></param>
        /// <returns>CURLcode error code</returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLcode curl_easy_getinfo(CURL curl, CURLINFO info, IntPtr dest);
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLcode curl_easy_getinfo(CURL curl, CURLINFO info, ref double val);
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLcode curl_easy_getinfo(CURL curl, CURLINFO info, ref IntPtr string_pointer);

        /// <summary>
        /// Creates a new curl session handle with the same options set for the handle
        /// passed in. Duplicating a handle could only be a matter of cloning data and
        /// options, internal state info and things like persistent connections cannot
        /// be transferred. It is useful in multithreaded applications when you can run
        /// curl_easy_duphandle() for each new thread to avoid a series of identical
        /// curl_easy_setopt() invokes in every thread.
        /// </summary>
        /// <param name="curl">CURL handle</param>
        /// <returns>CURL handle</returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURL curl_easy_duphandle(CURL curl);

        /// <summary>
        /// Re-initializes a CURL handle to the default values. This puts back the
        /// handle to the same state as it was in when it was just created.
        /// 
        /// It does keep: live connections, the Session ID cache, the DNS cache and the cookies.
        /// </summary>
        /// <param name="curl">CURL handle</param>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void curl_easy_reset(CURL curl);

        /// <summary>
        /// Receives data from the connected socket. Use after successful
        /// curl_easy_perform() with CURLOPT_CONNECT_ONLY option.
        /// </summary>
        /// <param name="curl">CURL handle</param>
        /// <param name="buf">pointer to a buffer</param>
        /// <param name="buflen">length of the buffer</param>
        /// <param name="n">pointer to an int</param>
        /// <returns>CURLcode error code</returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLcode curl_easy_recv(CURL curl, IntPtr buf, uint buflen, IntPtr n);

        /// <summary>
        /// Sends data over the connected socket. Use after successful
        /// curl_easy_perform() with CURLOPT_CONNECT_ONLY option.
        /// </summary>
        /// <param name="curl">CURL handle</param>
        /// <param name="buf">pointer to a buffer</param>
        /// <param name="buflen">length of the buffer</param>
        /// <param name="n">pointer to an int</param>
        /// <returns>CURLcode error code</returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLcode curl_easy_send(CURL curl, IntPtr buf, uint buflen, IntPtr n);
    }
}
