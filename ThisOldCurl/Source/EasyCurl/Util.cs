using System;
using System.Collections.Generic;
using System.Text;
using ThisOldCurl.LibCurl;
using System.Runtime.InteropServices;

namespace ThisOldCurl
{
    public partial class EasyCurl
    {
        public readonly static curl_infotype[] printableTypes = {
            curl_infotype.CURLINFO_TEXT,
            curl_infotype.CURLINFO_HEADER_IN,
            curl_infotype.CURLINFO_HEADER_OUT
        };

        public static string ErrorString(CURLcode code)
        {
            return Curl.curl_easy_strerror(code);
        }

        private void notDisposed()
        {
            if (this.disposed)
                throw new ObjectDisposedException("EasyCurl");
        }

        private CURLcode handleCurlCode(CURLcode code)
        {
            if (code == CURLcode.CURLE_OK)
            {
                return code;
            }
            throw new ExternalException("[libcurl] [ERROR] " + ErrorString(code));
        }

        /// <summary>
        /// Escapes URL strings (converts all letters consider illegal in URLs to their
        /// %XX versions). This function returns a new allocated string or NULL if an
        /// error occurred.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string Escape(string str)
        {
            if (this.disposed)
                throw new ObjectDisposedException("EasyCurl");
            return Curl.curl_easy_escape(this.curl, str, str.Length);
        }

        /// <summary>
        /// Unescapes URL encoding in strings (converts all %XX codes to their 8bit
        /// versions). This function returns a new allocated string or NULL if an error
        /// occurred.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string UnEscape(string str)
        {
            if (this.disposed)
                throw new ObjectDisposedException("EasyCurl");
            return Curl.curl_easy_unescape(this.curl, str, str.Length, IntPtr.Zero);
        }
    }
}
