using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using ThisOldCurl.LibCurl;
using System.Runtime.InteropServices;
using System.IO;

namespace ThisOldCurl
{
    /// <summary>
    /// The simplest way to use ThisOldCurl. Each method simply returns
    /// the response in the form of a byte array.
    /// 
    /// If you need additional info - response headers, etc, then you 
    /// must use EasyCurl instead (it's still pretty easy)
    /// </summary>
    public static class SuperEasyCurl
    {
        private static bool debugLogging = false;
        private static Encoding encoding = Encoding.UTF8;

        /*
         * GET
         * 
         */
        /// <summary>
        /// Submit an HTTP GET request.
        /// Set SuperEasyCurl.Encoding to set string encoding; UTF8 is default.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string Get(string url)
        { return encoding.GetString(GetBytes(url, null)); }
        public static string Get(string url, WebHeaderCollection headers)
        { return encoding.GetString(GetBytes(url, headers)); }

        public static byte[] GetBytes(string url)
        { return GetBytes(url, null); }
        public static byte[] GetBytes(string url, WebHeaderCollection headers)
        { return Request("GET", url, null, headers); }

        /*
         * POST
         * a Post overload for every occasion
         * 
         */
        /// <summary>
        /// Submit an HTTP POST request with a string-encoded request body
        /// Set SuperEasyCurl.Encoding to set string encoding; UTF8 by default.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public static string Post(string url, string body)
        { return encoding.GetString(PostBytes(url, body)); }
        public static string Post(string url, string body, string contentType)
        { return encoding.GetString(PostBytes(url, body, contentType)); }
        public static string Post(
            string url,
            string body,
            string contentType,
            WebHeaderCollection headers)
        {
            return encoding.GetString(
                PostBytes(url, body, contentType, headers));
        }
        public static string Post(string url, byte[] body)
        { return encoding.GetString(PostBytes(url, body)); }
        public static string Post(string url, byte[] body, string contentType)
        { return encoding.GetString(PostBytes(url, body, contentType)); }
        public static string Post(
            string url,
            byte[] body,
            string contentType,
            WebHeaderCollection headers)
        {
            return encoding.GetString(
                PostBytes(url, body, contentType, headers));
        }

        // PostBytes - don't convert to string after receiving data
        public static byte[] PostBytes(string url, string body)
        {
            return PostBytes(url, encoding.GetBytes(body), "text/plain");
        }
        public static byte[] PostBytes(string url, string body, string contentType)
        {
            return PostBytes(url, encoding.GetBytes(body), contentType, null);
        }
        public static byte[] PostBytes(
            string url,
            string body,
            string contentType,
            WebHeaderCollection headers)
        {
            return 
                PostBytes(url, encoding.GetBytes(body), contentType, headers);
        }
        public static byte[] PostBytes(string url, byte[] body)
        { return PostBytes(url, body, "text/plain"); }
        public static byte[] PostBytes(
            string url, byte[] body, string contentType)
        { return PostBytes(url, body, contentType, null); }
        public static byte[] PostBytes(
            string url,
            byte[] body,
            string contentType,
            WebHeaderCollection headers)
        {
            if (headers == null)
                headers = new WebHeaderCollection();
            headers[HttpHeaders.ContentType] = contentType;
            return Request("POST", url, body, headers);
        }



        /// <summary>
        /// Send a request, get back bytes.
        /// If you need more control than just method/url/body/headers,
        /// or need to read response headers, you need to use EasyCurl.
        /// </summary>
        /// <param name="method">GET, POST, PUT, etc (protocol dependent)</param>
        /// <param name="url">Required.</param>
        /// <param name="body">Request body (optional)</param>
        /// <param name="headers">Request headers (optional)</param>
        /// <returns></returns>
        public static byte[] Request(
            string method,
            string url,
            byte[] body,
            WebHeaderCollection headers)
        {
            if (url == null)
                throw new ArgumentNullException("url");
            if (method == null)
                throw new ArgumentNullException("method");
            if (headers == null)
                headers = new WebHeaderCollection();

            CurlWebRequest request = CurlWebRequest.Create(url);
            request.Method = method;
            request.Headers = headers;
            if (body != null && body.Length > 0)
            {
                Stream requestBody = request.GetRequestStream();
                requestBody.Write(body, 0, body.Length);
                requestBody.Position = 0;
            }
            if (debugLogging)
                request.DebugLogging = true;
            CurlWebResponse response = (CurlWebResponse)request.GetResponse();
            MemoryStream stream = (MemoryStream)response.GetResponseStream();
            return stream.ToArray();
        }

        public static bool DebugLogging
        {
            get { return debugLogging; }
            set { debugLogging = value; }
        }

        public static Encoding Encoding
        {
            get { return encoding; }
            set { encoding = value; }
        }
    }
}
