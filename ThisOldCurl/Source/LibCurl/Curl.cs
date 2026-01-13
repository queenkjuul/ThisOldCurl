using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using curl_httppost_ = System.IntPtr; // pointer to a curl_httppost
using curl_slist_ = System.IntPtr;  // pointer to a curl_slist
using curl_off_t = System.Int64;
using size_t = System.UInt32;
using time_t = System.Int64;

namespace ThisOldCurl.LibCurl
{
    // ==== Some structs and enums that didn't fit other places
    [StructLayout(LayoutKind.Sequential)]
    public struct curl_httppost
    {
        /// <summary>
        /// next entry in the list
        /// </summary>
        public curl_httppost_ next;
        /// <summary>
        /// pointer to allocated name
        /// </summary>
        public IntPtr name;
        /// <summary>
        /// length of name */
        /// </summary>
        public int namelength;
        /// <summary>
        /// pointer to allocated data contents */
        /// </summary>
        public IntPtr contents;
        /// <summary>
        /// length of contents field */
        /// </summary>
        public int contentslength;
        /// <summary>
        /// pointer to allocated buffer contents */
        /// </summary>
        public IntPtr buffer;
        /// <summary>
        /// length of buffer field */
        /// </summary>
        public int bufferlength;
        /// <summary>
        /// Content-Type (pointer to a string)
        /// </summary>
        public IntPtr contenttype;
        /// <summary>
        /// list of extra headers for this form */
        /// </summary>
        public curl_slist_ contentheader;
        /// <summary>
        /// if one field name has more than one file, this link should link to following files (pointer to curl_httppost)
        /// </summary>
        public curl_httppost_ more;
        public CurlHttpPostFlags flags; /* as defined below */

        /// <summary>
        /// The file name to show. If not set, the
        /// actual file name will be used (if this
        /// is a file part)
        /// </summary>
        public IntPtr showfilename;
        /// <summary>
        /// custom pointer used for HTTPPOST_CALLBACK posts
        /// </summary>
        public IntPtr userp;
    }

    [Flags]
    public enum CurlHttpPostFlags
    {
        /// <summary>
        /// specified content is a file name
        /// </summary>
        HTTPPOST_FILENAME = 1 << 0,
        /// <summary>
        /// specified content is a file name
        /// </summary>
        HTTPPOSE_READFILE = 1 << 1,
        /// <summary>
        /// name is only stored pointer do not free in formfree
        /// </summary>
        HTTPPOST_PTRNAME = 1 << 2,
        /// <summary>
        /// contents is only stored pointer do not free in formfree
        /// </summary>
        HTTPPOST_PTRCONTENTS = 1 << 3,
        /// <summary>
        /// upload file from buffer
        /// </summary>
        HTTPPOST_BUFFER = 1 << 4,
        /// <summary>
        /// upload file from pointer contents
        /// </summary>
        HTTPPOST_PTRBUFFER = 1 << 5,
        /// <summary>
        /// upload file contents by using the
        /// regular read callback to get the data
        /// and pass the given pointer as custom pointer
        /// </summary>
        HTTPPOST_CALLBACK = 1 << 6
    }

    /// <summary>
    /// bitmask defines for CURLOPT_HEADEROPT
    /// </summary>
    [Flags]
    public enum CurlHeaderFlags
    {
        CURLHEADER_UNIFIED = 0,
        CURLHEADER_SEPARATE = 1 << 0
    }

    /// <summary>
    /// linked-list structure for the CURLOPT_QUOTE option (and other)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct curl_slist
    {
        public IntPtr data;
        public curl_slist_ next;
    }

    public enum curl_closepolicy
    {
        /// <summary>
        /// NEVER USE (first in enum)
        /// </summary>
        CURLCLOSEPOLICY_NONE, /* first, never use this */
        CURLCLOSEPOLICY_OLDEST,
        CURLCLOSEPOLICY_LEAST_RECENTLY_USED,
        CURLCLOSEPOLICY_LEAST_TRAFFIC,
        CURLCLOSEPOLICY_SLOWEST,
        CURLCLOSEPOLICY_CALLBACK,
        /// <summary>
        /// NEVER USE (last in enum)
        /// </summary>
        CURLCLOSEPOLICY_LAST /* last, never use this */
    }

    /// <summary>
    /// This is a magic return code for the write callback that, when returned
    /// will signal libcurl to pause receiving on the current transfer.
    /// </summary>
    public enum CurlWriteCode : uint
    {
        CURL_WRITEFUNC_PAUSE = 0x10000001
    }

    /// <summary>
    /// Magic return values that can be returned from a CurlReadCallback to control an ongoing transfer
    /// </summary>
    public enum CurlReadCode : uint
    {
        /// <summary>
        /// This is a return code for the read callback that, when returned, will
        /// signal libcurl to immediately abort the current transfer.
        /// </summary>
        CURL_READFUNC_ABORT = 0x10000000,
        /// <summary>
        /// This is a return code for the read callback that, when returned, will
        /// signal libcurl to pause sending data on the current transfer.
        /// </summary>
        CURL_READFUNC_PAUSE = 0x10000001
    }

    public enum CurlMax
    {
        CURL_MAX_WRITE_SIZE = 16384,
        CURL_MAX_HTTP_HEADER = 100 * 1024
    }


    /// <summary>
    /// callback to handle writing data during a transfer
    /// can return a CurlWriteCallbackSignal to control the transfer
    /// </summary>
    /// <param name="buf">pointer to a buffer</param>
    /// <param name="size">size of the buffer (uint)</param>
    /// <param name="nitems">number of items (uint)</param>
    /// <param name="outstream">pointer</param>
    /// <returns>number of count handled by the callback (validated by libcurl)</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate size_t CurlWriteCallback(
        IntPtr buf,
        size_t size,
        size_t nitems,
        IntPtr outstream);

    /// <summary>
    /// callback to handle reading data during a transfer
    /// can return a CurlReadCallbackSignal to control the transfer
    /// </summary>
    /// <param name="buf">pointer to a buffer</param>
    /// <param name="size">size of the buffer (uint)</param>
    /// <param name="nitems">number of items (uint)</param>
    /// <param name="instream">pointer</param>
    /// <returns>number of count handled by the callback (validated by libcurl)</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate size_t CurlReadCallback(
        IntPtr buf,
        size_t size,
        size_t nitems,
        IntPtr instream);

    /// <summary>
    /// This is the CURLOPT_PROGRESSFUNCTION callback proto. It is now 
    /// deprecated but was the only choice up until 7.31.0
    /// </summary>
    /// <param name="clientp"></param>
    /// <param name="dltotal"></param>
    /// <param name="dlnow"></param>
    /// <param name="ultotal"></param>
    /// <param name="ulnow"></param>
    /// <returns></returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int CurlProgressCallback(
        IntPtr clientp,
        double dltotal,
        double dlnow,
        double ultotal,
        double ulnow);

    /// <summary>
    /// This is the CURLOPT_XFERINFOFUNCTION callback proto. It was introduced in
    /// 7.32.0, it avoids floating point and provides more detailed information.
    /// </summary>
    /// <param name="clientp"></param>
    /// <param name="dltotal"></param>
    /// <param name="dlnow"></param>
    /// <param name="ultotal"></param>
    /// <param name="ulnow"></param>
    /// <returns></returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int CurlXferInfoCallback(
        IntPtr clientp,
        curl_off_t dltotal,
        curl_off_t dlnow,
        curl_off_t ultotal,
        curl_off_t ulnow);

    /// <summary>
    /// this prototype applies to all conversion callbacks
    /// </summary>
    /// <param name="buf">pointer to a buffer</param>
    /// <param name="length">length of the buffer</param>
    /// <returns>CURLcode error code</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate CURLcode CurlConvCallback(IntPtr buf, size_t length);

    // ==== LibCurl.Curl
    /// <summary>
    /// Exposes all external DLL functions of libcurl directly.
    /// Partial class: related methods are grouped in their respective Curl*.cs files,
    /// but all are accessed statically via the Curl class.
    /// </summary>
    public static partial class Curl
    {
        internal const string CURLDLL = "libcurl.dll";
        public static CURLversion CURLVERSION_NOW = CURLversion.CURLVERSION_FOURTH;

        /// <summary>
        /// curl_global_init() should be invoked exactly once for each application that
        /// uses libcurl and before any call of other libcurl functions.
        /// 
        /// This function is not thread-safe!
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLcode curl_global_init(int flags);

        /// <summary>
        /// curl_global_cleanup() should be invoked exactly once for each application
        /// that uses libcurl
        /// </summary>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void curl_global_cleanup();

        /// <summary>
        /// Appends a string to a linked list. If no list exists, it will be created
        /// first. Returns the new list, after appending.
        /// </summary>
        /// <param name="curl_slist">pointer to a curl_slist</param>
        /// <param name="str">header in format 'Key: Value'</param>
        /// <returns>pointer to a curl_slist</returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern curl_slist_ curl_slist_append(curl_slist_ curl_slist, string str);

        /// <summary>
        /// free a previously built curl_slist.
        /// </summary>
        /// <param name="curl_slist">pointer to a curl_slist</param>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void curl_slist_free_all(curl_slist_ curl_slist);

        /// <summary>
        /// Returns the time, in seconds since 1 Jan 1970 of the time string given in
        /// the first argument. The time argument in the second parameter is unused
        /// and should be set to NULL.
        /// </summary>
        /// <param name="p">a time string</param>
        /// <param name="_unused">set to null</param>
        /// <returns>time_t (int64)</returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern time_t curl_getdate(string p, IntPtr _unused);
    }
}
