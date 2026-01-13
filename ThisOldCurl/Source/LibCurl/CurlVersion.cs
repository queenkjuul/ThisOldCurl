using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ThisOldCurl.LibCurl
{
    [Flags]
    public enum CurlVersionFlags : uint
    {
        /// <summary>IPv6-enabled</summary>
        IPV6 = 1 << 0,
        /// <summary>Kerberos V4 auth is supported (deprecated)</summary>
        KERBEROS4 = 1 << 1,
        /// <summary>SSL options are present</summary>
        SSL = 1 << 2,
        /// <summary>libz features are present</summary>
        LIBZ = 1 << 3,
        /// <summary>NTLM auth is supported</summary>
        NTLM = 1 << 4,
        /// <summary>Negotiate auth is supported (deprecated)</summary>
        GSSNEGOTIATE = 1 << 5,
        /// <summary>Built with debug capabilities</summary>
        DEBUG = 1 << 6,
        /// <summary>Asynchronous DNS resolves</summary>
        ASYNCHDNS = 1 << 7,
        /// <summary>SPNEGO auth is supported</summary>
        SPNEGO = 1 << 8,
        /// <summary>Supports files larger than 2GB</summary>
        LARGEFILE = 1 << 9,
        /// <summary>Internationized Domain Names are supported</summary>
        IDN = 1 << 10,
        /// <summary>Built against Windows SSPI</summary>
        SSPI = 1 << 11,
        /// <summary>Character conversions supported</summary>
        CONV = 1 << 12,
        /// <summary>Debug memory tracking supported</summary>
        CURLDEBUG = 1 << 13,
        /// <summary>TLS-SRP auth is supported</summary>
        TLSAUTH_SRP = 1 << 14,
        /// <summary>NTLM delegation to winbind helper is supported</summary>
        NTLM_WB = 1 << 15,
        /// <summary>HTTP2 support built-in</summary>
        HTTP2 = 1 << 16,
        /// <summary>Built against a GSS-API library</summary>
        GSSAPI = 1 << 17,
        /// <summary>Kerberos V5 auth is supported</summary>
        KERBEROS5 = 1 << 18,
        /// <summary>Unix domain sockets support</summary>
        UNIX_SOCKETS = 1 << 19
    }

    public enum CURLversion
    {
        CURLVERSION_FIRST,
        CURLVERSION_SECOND,
        CURLVERSION_THIRD,
        CURLVERSION_FOURTH,
        /// <summary>
        /// NEVER USE (last in enum)
        /// </summary>
        CURLVERSION_LAST
    }

#pragma warning disable 0169
    public struct curl_version_info_data
    {
        /// <summary>
        /// age of the returned struct
        /// </summary>
        public CURLversion age;
        /// <summary>
        /// LIBCURL_VERSION
        /// </summary>
        public IntPtr version; 
        /// <summary>
        /// LIBCURL_VERSION_NUM
        /// </summary>
        public UInt32 version_num;
        /// <summary>
        /// OS/host/cpu/machine when configured
        /// </summary>
        public IntPtr host;
        /// <summary>
        /// bitmask, see defines below
        /// </summary>
        public int features;         
        /// <summary>
        /// human readable string
        /// </summary>
        public IntPtr ssl_version;
        /// <summary>
        /// not used anymore, always 0
        /// </summary>
        public long ssl_version_num;
        /// <summary>
        /// human readable string
        /// </summary>
        public IntPtr libz_version;
        /// <summary>
        /// protocols is terminated by an entry with a NULL protoname
        /// </summary>
        public IntPtr protocols;
        /// <summary>
        /// The fields below this were added in CURLVERSION_SECOND
        /// </summary>
        public IntPtr ares;
        public int ares_num;
        /// <summary>
        /// This field was added in CURLVERSION_THIRD
        /// </summary>
        public IntPtr libidn;
        /* These field were added in CURLVERSION_FOURTH */
        /// <summary>
        /// Same as '_libiconv_version' if built with HAVE_ICONV
        /// </summary>
        public int iconv_ver_num;
        /// <summary>
        /// human readable string
        /// </summary>
        public IntPtr libssh_version;
    }
#pragma warning restore 0169

    public static partial class Curl
    {
        /// <summary>
        /// Returns a static ascii string of the libcurl version.
        /// </summary>
        /// <returns>pointer to a string</returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr curl_version();

        /// <summary>
        /// This function returns a pointer to a static copy of the version info
        /// struct. See above.
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern curl_version_info_data curl_version_info(CURLversion version);
    }
}
