using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using size_t = System.UInt32;
using curl_httppost_ = System.IntPtr; // pointer to curl_httppost

namespace ThisOldCurl.LibCurl
{
    /// <summary>
    /// structure to be used as parameter for CURLFORM_ARRAY 
    /// </summary>
    public enum CURLformoption
    {
        CURLFORM_NOTHING,
        CURLFORM_COPYNAME,
        CURLFORM_PTRNAME,
        CURLFORM_NAMELENGTH,
        CURLFORM_COPYCONTENTS,
        CURLFORM_PTRCONTENTS,
        CURLFORM_CONTENTSLENGTH,
        CURLFORM_FILECONTENT,
        CURLFORM_ARRAY,
        CURLFORM_OBSOLETE,
        CURLFORM_FILE,
        CURLFORM_BUFFER,
        CURLFORM_BUFFERPTR,
        CURLFORM_BUFFERLENGTH,
        CURLFORM_CONTENTTYPE,
        CURLFORM_CONTENTHEADER,
        CURLFORM_FILENAME,
        CURLFORM_END,
        CURLFORM_OBSOLETE2,
        CURLFORM_STREAM,
        CURLFORM_LASTENTRY
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct curl_forms
    {
        public CURLformoption option;
        public IntPtr value;
    }

    /// <summary>
    /// use this for multipart formpost building */
    /// Returns code for curl_formadd()
    /// </summary>
    public enum CURLFORMcode
    {
        /// <summary>
        /// on success
        /// </summary>
        CURL_FORMADD_OK, /* first, no error */
        /// <summary>
        /// if the FormInfo allocation fails
        /// OR
        /// if the allocation of a FormInfo struct failed
        /// OR
        /// if a curl_httppost struct cannot be allocated
        /// OR
        /// if some allocation for string copying failed
        /// </summary>
        CURL_FORMADD_MEMORY,
        /// <summary>
        /// if one option is given twice for one Form
        /// </summary>
        CURL_FORMADD_OPTION_TWICE,
        /// <summary>
        /// if a null pointer was given for a char
        /// </summary>
        CURL_FORMADD_NULL,
        /// <summary>
        /// if an unknown option was used
        /// </summary>
        CURL_FORMADD_UNKNOWN_OPTION,
        /// <summary>
        /// if the some FormInfo is not complete (or error)
        /// </summary>
        CURL_FORMADD_INCOMPLETE,
        /// <summary>
        ///  if an illegal option is used in an array
        /// </summary>
        CURL_FORMADD_ILLEGAL_ARRAY,
        /// <summary>
        /// libcurl was built with this disabled
        /// </summary>
        CURL_FORMADD_DISABLED, /* libcurl was built with this disabled */

        CURL_FORMADD_LAST /* last */
    }


    /// <summary>
    /// callback function for curl_formget()
    /// The void *arg pointer will be the one passed as second argument to
    /// curl_formget().
    /// The character buffer passed to it must not be freed.
    /// Should return the buffer length passed to it as the argument "len" on
    /// success.
    /// </summary>
    /// <param name="arg"></param>
    /// <param name="buf"></param>
    /// <param name="len"></param>
    /// <returns></returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate size_t CurlFormGetCallback(
        IntPtr arg,
        IntPtr buf,
        size_t len);

    public static partial class Curl
    {
        /// <summary>
        /// Pretty advanced function for building multi-part formposts. Each invoke
        /// adds one part that together construct a full post. Then use
        /// CURLOPT_HTTPPOST to send it off to libcurl.
        /// </summary>
        /// <param name="httppost"></param>
        /// <param name="last_post"></param>
        /// <returns></returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLFORMcode curl_formadd(
            ref curl_httppost_ httppost,
            ref curl_httppost_ last_post,
            CURLformoption nameOpt, string name,
            CURLformoption valueOpt, string value,
            CURLformoption end);
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLFORMcode curl_formadd(
            ref curl_httppost_ httppost,
            ref curl_httppost_ last_post,
            CURLformoption nameOpt, string name,
            CURLformoption fileOpt, string filePath,
            CURLformoption typeOpt, string contentType,
            CURLformoption end);

        /// <summary>
        /// Serialize a curl_httppost struct built with curl_formadd().
        /// Accepts a void pointer as second argument which will be passed to
        /// the curl_formget_callback function.
        /// Returns 0 on success.
        /// </summary>
        /// <param name="form"></param>
        /// <param name="arg"></param>
        /// <param name="append"></param>
        /// <returns></returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int curl_formget(
            curl_httppost_ form,
            IntPtr arg,
            CurlFormGetCallback append);

        /// <summary>
        /// Free a multipart formpost previously built with curl_formadd().
        /// </summary>
        /// <param name="form"></param>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void curl_formfree(curl_httppost_ form);
    }
}
