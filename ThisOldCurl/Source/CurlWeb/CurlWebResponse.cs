using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace ThisOldCurl
{
    public class CurlWebResponse : WebResponse
    {
        private readonly Stream responseStream;
        private readonly long status = 0;
        private WebHeaderCollection headers;
        private Uri uri;

        public CurlWebResponse(Stream responseStream, long status, WebHeaderCollection headers, Uri uri)
        {
            if (responseStream == null)
                throw new ArgumentNullException("No response stream provided to CurlWebResponse");
            this.responseStream = responseStream;
            this.status = status;
            this.headers = headers;
            this.uri = uri;
        }

        public override long ContentLength
        {
            get
            {
                string header = this.headers.Get(HttpHeaders.ContentLength);
                if (header == null)
                    return -1;
                long n;
                return long.TryParse(header, out n) ? n : -1;
            }
        }

        public override string ContentType
        {
            get { return this.headers.Get(HttpHeaders.ContentType); }
        }

        public override Stream GetResponseStream()
        {
            return this.responseStream;
        }

        public override WebHeaderCollection Headers
        {
            get { return this.headers; }
        }
    }
}
