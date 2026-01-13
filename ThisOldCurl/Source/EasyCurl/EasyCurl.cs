using System;
using System.Collections.Generic;
using System.Text;
using ThisOldCurl.LibCurl;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;
using System.Net;

using CURL = System.IntPtr;     // CURL handle
using CURLSH = System.IntPtr;   // CURL share handle
using curl_off_t = System.Int64;
using size_t = System.UInt32;

namespace ThisOldCurl
{
    /// <summary>
    /// The heart of ThisOldCurl: The EasyCurl class both exposes 
    /// the underlying libcurl "easy API"
    /// and provides an easy-to-use C# wrapper around said API.
    /// By default, an EasyCurl instance emits WriteEvent when receiving
    /// data, ReadEvent when sending data, HeaderEvent when receiving headers,
    /// ProgressEvent as a transfer is ongoing, and DebugEvent when 
    /// receiving debug information.
    /// 
    /// If you assign a Stream to DownloadStream, response data will be written
    /// to that stream.
    /// If you assign a Stream to UploadStream, data in the stream will be 
    /// read by libcurl and sent with the request.
    /// 
    /// After making a transfer, the Info field will be populated with a
    /// dictionary containing transfer metadata (like status code, IP, etc).
    /// 
    /// Setting DebugLogging = true (either via constructor or property)
    /// will log all "printable" libcurl debug messages to the console.
    /// Your event listener will receive unfiltered debug data, including 
    /// binary data unfit for console printing - use EasyCurl.PrintableTypes
    /// to filter input.
    /// 
    /// </summary>
    public partial class EasyCurl : IDisposable
    {
        private CURL curl = IntPtr.Zero;
        private SharedCurl share;
        private FormCurl form;

        // Options
        private string caCertPath = "cacert.pem";
        private bool debugLogging = false;
        private bool connectOnly = false; // leave socket open, use Send/Recv
        private int? timeout;
        private string url;
        private string method;

        // State
        private bool disposed = false;
        private bool performed = false;
        private bool pauseRequest = false;
        private bool abortRequest = false;
        private IntPtr slist = IntPtr.Zero;
        private WebHeaderCollection headers;
        private List<Delegate> callbacks = new List<Delegate>();
        private Dictionary<CURLINFO, object> info =
            new Dictionary<CURLINFO, object>();
        private Stream uploadStream;
        private Stream downloadStream;

        public string URL
        {
            get { return this.url; }
            set
            {
                notDisposed();
                this.url = value;
            }
        }

        public string Method
        {
            get { return this.method == null ? "GET" : this.method.ToUpperInvariant(); }
            set
            {
                notDisposed();
                this.method = value;
            }
        }

        public SharedCurl ShareHandle
        {
            get { return this.share; }
            set { notDisposed(); this.share = value; }
        }

        public WebHeaderCollection Headers
        {
            get { return this.headers; }
            set { notDisposed(); this.headers = value; }
        }

        public int Timeout
        {
            get { return this.timeout ?? 0; }
            set
            {
                handleCurlCode(this.SetOpt(CURLoption.CURLOPT_TIMEOUT, value));
                this.timeout = value;
            }
        }

        public int TimeoutMs
        {
            get { return this.timeout == null ? 0 : (int)this.timeout * 1000; }
            set
            {
                handleCurlCode(
                    this.SetOpt(CURLoption.CURLOPT_TIMEOUT_MS, value));
                this.timeout = value;
            }
        }

        public CURL Handle
        {
            get { return this.curl; }
        }

        public FormCurl Form
        {
            get { return this.form; }
            set { this.form = value; }
        }

        public string CaCertPath
        {
            get { return this.caCertPath; }
            set
            {
                this.caCertPath = value;
                this.SetOpt(CURLoption.CURLOPT_CAINFO, this.CaCertPath);
            }
        }

        public Dictionary<CURLINFO, object> Info
        {
            get
            {
                if (!this.performed)
                    throw new InvalidOperationException
                        ("[libcurl] Cannot access Info before calling Perform()");
                return this.info;
            }
        }

        public Stream UploadStream
        {
            get { return this.uploadStream; }
            set { this.uploadStream = value; }
        }

        public Stream DownloadStream
        {
            get { return this.downloadStream; }
            set { this.downloadStream = value; }
        }

        public bool DebugLogging
        {
            get { return this.debugLogging; }
            set
            {
                this.debugLogging = value;
                if (value)
                {
                    this.SetOpt(CURLoption.CURLOPT_VERBOSE, 1);
                }
                else
                {
                    this.SetOpt(CURLoption.CURLOPT_VERBOSE, 0);
                }
            }
        }
    }
}
