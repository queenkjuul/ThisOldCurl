using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;
using ThisOldCurl.LibCurl;

using size_t = System.UInt32;
using curl_off_t = System.Int64;
using System.Threading;

namespace ThisOldCurl
{
    /// <summary>
    /// CurlWebRequest - Complete WebRequest API backed by libcurl
    /// Instantiate requests directly - new CurlWebRequest(uri) or CurlWebrRequest.Create(url)
    /// Underlying EasyCurl options can be set with the EasyConfig callback:
    ///     request.EasyConfig = delegate(EasyCurl curl) { curl.SetOpt(CURLoption, value); }
    /// 
    /// NOTICE: HttpWebRequest uses a special ConnectStream class to handle pulling data from 
    /// the network socket. We don't have that - so instead we expose the underlying EasyCurl
    /// functionality, which is similar.
    /// 
    /// You can set RequestStream and ResponseStream to your own Stream - typically a FileStream,
    /// to upload or download a file. If these are not set, the class defaults to a MemoryStream.
    /// 
    /// If you request a large file, writing it entirely to memory will cause problems - be sure
    /// to set an appropriate stream if you're dealing with large responses (and remember that in
    /// a Windows 98 context, a "large file" may be as small as 4MB)
    /// 
    /// WARNING: If you call GetRequestStream() before setting RequestStream, you will end up with
    /// the default MemoryStream.
    /// </summary>
    public class CurlWebRequest : WebRequest
    {
        private readonly Uri uri;
        private string method = "GET";
        private WebHeaderCollection headers = new WebHeaderCollection();
        private int timeout = 0; // millis
        private bool allowAutoRedirect = true;
        private bool debugLogging = false;
        
        private Stream requestStream;
        private Stream responseStream;
        private long? contentLength;
        private IWebProxy proxy;
        private ICredentials creds;
        private Action<EasyCurl> easyConfig;

        private bool abort = false;

        public CurlWebRequest(string uri) : this(new Uri(uri)) { }
        public CurlWebRequest(Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException("No URI provided!");
            this.uri = uri;
        }

        /// <summary>
        /// You can't actually register a CurlWebRequest for "https://" (at least not in VS2005)
        /// so you can't use the conventional WebRequest.Create() syntax.
        /// However, such a method is provided for those who find it familiar.
        /// </summary>
        /// <returns></returns>
        public new static CurlWebRequest Create(string url) { return new CurlWebRequest(url); }
        public new static CurlWebRequest Create(Uri uri) { return new CurlWebRequest(uri); }

        public override IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
        {
            CurlRequestStreamAsyncResult result = new CurlRequestStreamAsyncResult(state);
            ThreadPool.QueueUserWorkItem(delegate
            {
                try
                {
                    result.SetCompleted(this.RequestStream, false);
                }
                catch (Exception ex)
                {
                    result.SetFailed(ex);
                }
                if (callback != null)
                    callback(result);
            });
            return result;
        }
        /// <summary>
        /// NOTICE: Unlike HttpWebRequest, you can set RequestStream with your own stream.
        /// Calling GetRequestStream before setting RequestStream will return a default MemoryStream
        /// </summary>
        /// <returns></returns>
        public override Stream GetRequestStream()
        {
            if (this.requestStream == null)
                this.requestStream = new MemoryStream();
            return this.requestStream;
        }
        public override Stream EndGetRequestStream(IAsyncResult asyncResult)
        {
            CurlRequestStreamAsyncResult result = asyncResult as CurlRequestStreamAsyncResult;
            if (result == null)
                throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
            if (!result.IsCompleted)
                result.AsyncWaitHandle.WaitOne();
            if (result.Error != null)
                throw result.Error;
            return result.Stream;
        }

        public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
        {
            CurlResponseAsyncResult result = new CurlResponseAsyncResult(state);

            ThreadPool.QueueUserWorkItem(delegate
            {
                try
                {
                    WebResponse response = this.GetResponse();
                    result.SetCompleted(response, false);
                }
                catch (Exception ex)
                {
                    result.SetFailed(ex);
                }
                if (callback != null)
                    callback(result);
            });
            return result;
        }
        public override WebResponse GetResponse()
        {
            // TODO: Much of this was later implemented directly into EasyCurl.
            // It might make sense to refactor this to rely on the newer EasyCurl
            // implementation.
            EasyCurl curl = new EasyCurl(this.debugLogging);
            Console.WriteLine(uri.OriginalString);
            curl.URL = this.uri.OriginalString;
            curl.Method = this.Method;
            curl.Headers = this.Headers;
            if (this.proxy != null && !proxy.IsBypassed(this.uri))
            {
                Uri proxyUri = this.proxy.GetProxy(this.uri);
                if (proxyUri != null)
                    curl.SetOpt(CURLoption.CURLOPT_PROXY, proxyUri.ToString());
                if (proxy.Credentials != null)
                {
                    NetworkCredential proxycred = proxy.Credentials.GetCredential(this.uri, "Basic");
                    if (proxycred != null)
                        curl.SetOpt(
                            CURLoption.CURLOPT_PROXYUSERPWD,
                            proxycred.UserName + ":" + proxycred.Password);
                }
            }

            if (this.creds != null)
            {
                NetworkCredential cred = this.creds.GetCredential(this.uri, "Basic");
                if (cred != null)
                    curl.SetOpt(CURLoption.CURLOPT_USERPWD, cred.UserName + ":" + cred.Password);
            }

            curl.SetOpt(CURLoption.CURLOPT_FOLLOWLOCATION, this.allowAutoRedirect ? 1 : 0);
            curl.SetOpt(CURLoption.CURLOPT_TIMEOUT_MS, this.timeout);

            WebHeaderCollection responseHeaders = new WebHeaderCollection();
            CurlWriteCallback callback = delegate(IntPtr buffer, size_t size, size_t nitems, IntPtr user)
            {
                size_t len = size * nitems;
                string line = Marshal.PtrToStringAnsi(buffer, (int)len).TrimEnd('\r', '\n');
                if (line.StartsWith("HTTP/"))
                {
                    responseHeaders.Clear();
                    return len;
                }
                int divider = line.IndexOf(':');
                if (divider > 0)
                    responseHeaders.Add(
                        line.Substring(0, divider),
                        line.Substring(divider + 1).Trim());
                return len;
            };
            curl.SetOpt(CURLoption.CURLOPT_HEADERFUNCTION, callback);

            if (this.easyConfig != null)
                this.easyConfig(curl);

            CurlXferInfoCallback xfcb = delegate(
                IntPtr client,
                curl_off_t dltotal,
                curl_off_t dlnow,
                curl_off_t ultotal,
                curl_off_t ulnow)
            {
                return abort ? 1 : 0;
            };
            curl.SetOpt(CURLoption.CURLOPT_NOPROGRESS, 0);
            curl.SetOpt(CURLoption.CURLOPT_XFERINFOFUNCTION, xfcb);

            Stream responseStream = this.responseStream ?? new MemoryStream();
            curl.DownloadStream = responseStream;

            curl.UploadStream = this.RequestStream;
            curl.Perform();

            responseStream.Position = 0;

            int status = (int)curl.GetInfo(CURLINFO.CURLINFO_RESPONSE_CODE);
            string responseUrl = (string)curl.GetInfo(CURLINFO.CURLINFO_EFFECTIVE_URL);

            return new CurlWebResponse(responseStream, status, responseHeaders, new Uri(responseUrl));
        }
        public override WebResponse EndGetResponse(IAsyncResult asyncResult)
        {
            CurlResponseAsyncResult result = asyncResult as CurlResponseAsyncResult;
            if (result == null)
                throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
            if (!result.IsCompleted)
                result.AsyncWaitHandle.WaitOne();
            if (result.Error != null)
                throw result.Error;
            return result.Response;
        }

        /// <summary>
        /// Configure the EasyCurl instance that gets created by GetResponse()
        /// Callback receives one argument, the EasyCurl instance.
        /// </summary>
        public Action<EasyCurl> EasyConfig
        {
            get { return this.easyConfig; }
            set { this.easyConfig = value; }
        }

        /// <summary>
        /// Optional Stream with data to send with the request
        /// </summary>
        public Stream RequestStream
        {
            get { return this.GetRequestStream(); }
            set { this.requestStream = value; }
        }

        /// <summary>
        /// If unset when Perform() is called, response data will be written direct to memory.
        /// This may be problematic with large responses; set this property as appropriate beforehand.
        /// </summary>
        public Stream ResponseStream
        {
            get { return this.responseStream; }
            set { this.responseStream = value; }
        }

        public override Uri RequestUri
        {
            get { return this.uri; }
        }

        public override string Method
        {
            get { return this.method.ToUpperInvariant(); }
            set { this.method = value; }
        }

        public override WebHeaderCollection Headers
        {
            get { return this.headers; }
            set { this.headers = value; }
        }

        /// <summary>
        /// if this is set manually, it will be sent instead of the actual length of the assigned requestStream
        /// </summary>
        public override long ContentLength
        {
            get
            {
                return this.contentLength == null
                    ? this.requestStream.Length
                    : this.contentLength ?? 0;
            }
            set { this.contentLength = value; }
        }

        public override string ContentType
        {
            get { return this.headers.Get(HttpHeaders.ContentType); }
            set { this.headers.Set(HttpHeaders.ContentType, value); }
        }

        /// <summary>
        /// Timeout value in milliseconds.
        /// </summary>
        public override int Timeout
        {
            get { return this.timeout; }
            set { this.timeout = value; }
        }

        public override ICredentials Credentials
        {
            get { return this.creds; }
            set { this.creds = value; }
        }

        public override bool PreAuthenticate
        {
            get { return false; }
        }

        public override IWebProxy Proxy
        {
            get { return this.proxy; }
            set { this.proxy = value; }
        }

        public override void Abort()
        {
            this.abort = true;
        }

        public bool AllowAutoRedirect
        {
            get { return this.allowAutoRedirect; }
            set { this.allowAutoRedirect = value; }
        }

        public bool DebugLogging
        {
            get { return this.debugLogging; }
            set { this.debugLogging = value; }
        }
    }

    internal static class HttpHeaders
    {
        public static string ContentType = "Content-Type";
        public static string ContentLength = "Content-Length";
    }
}
