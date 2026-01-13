using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using CURL = System.IntPtr; // CURL handle
using curl_slist_ = System.IntPtr; // pointer to curl_slist

namespace ThisOldCurl.LibCurl
{
    /// <summary>
    /// parameter for the CURLOPT_USE_SSL option
    /// </summary>
    public enum curl_usessl
    {
        /// <summary>
        /// do not attempt to use SSL
        /// </summary>
        CURLUSESSL_NONE,
        /// <summary>
        /// try using SSL, proceed anyway otherwise
        /// </summary>
        CURLUSESSL_TRY,
        /// <summary>
        /// SSL for the control connection or fail
        /// </summary>
        CURLUSESSL_CONTROL,
        /// <summary>
        /// SSL for all communication or fail
        /// </summary>
        CURLUSESSL_ALL,
        /// <summary>
        /// not an option, never use
        /// </summary>
        CURLUSESSL_LAST
    }

    public enum CurlSslVersion
    {
        CURL_SSLVERSION_DEFAULT,
        CURL_SSLVERSION_TLSv1,
        CURL_SSLVERSION_SSLv2,
        CURL_SSLVERSION_SSLv3,
        CURL_SSLVERSION_TLSv1_0,
        CURL_SSLVERSION_TLSv1_1,
        CURL_SSLVERSION_TLSv1_2,
        CURL_SSLVERSION_LAST
    }

    public enum CURL_TLSAUTH
    {
        CURL_TLSAUTH_NONE,
        CURL_TLSAUTH_SRP,
        CURL_TLSAUTH_LAST
    };

    /// <summary>
    /// info about the certificate chain, only for OpenSSL builds. Asked
    /// for with CURLOPT_CERTINFO / CURLINFO_CERTINFO
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    struct curl_certinfo
    {
        /// <summary>
        /// number of certificates with information
        /// </summary>
        public int num_of_certs;
        /// <summary>
        /// pointer to curl_slist
        /// 
        /// for each index in this array, there's a
        /// linked list with textual information in the
        /// format "name: value"
        /// </summary>
        public curl_slist_ certinfo;
    };

    /// <summary>
    /// enum for the different supported SSL backends
    /// </summary>
    public enum curl_sslbackend
    {
        CURLSSLBACKEND_NONE = 0,
        CURLSSLBACKEND_OPENSSL = 1,
        CURLSSLBACKEND_GNUTLS = 2,
        CURLSSLBACKEND_NSS = 3,
        CURLSSLBACKEND_OBSOLETE4 = 4,  /* Was QSOSSL. */
        CURLSSLBACKEND_GSKIT = 5,
        CURLSSLBACKEND_POLARSSL = 6,
        CURLSSLBACKEND_CYASSL = 7,
        CURLSSLBACKEND_SCHANNEL = 8,
        CURLSSLBACKEND_DARWINSSL = 9,
        CURLSSLBACKEND_AXTLS = 10
    }

    /// <summary>
    /// Information about the SSL library used and the respective internal SSL
    /// handle, which can be used to obtain further information regarding the
    /// connection. Asked for with CURLINFO_TLS_SESSION.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct curl_tlssessioninfo
    {
        public curl_sslbackend backend;
        public IntPtr internals;
    }

    /// <param name="curl">CURL handle</param>
    /// <param name="ssl_ctx">pointer to an OpenSSL SSL_CTX</param>
    /// <param name="userptr"></param>
    /// <returns></returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate CURLcode CurlSslCtxCallback(CURL curl, IntPtr ssl_ctx, IntPtr userptr);
}
