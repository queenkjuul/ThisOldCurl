using System.Runtime.InteropServices;
using System;

namespace ThisOldCurl.LibCurl
{
    /// <summary>
    /// Below here follows defines for the CURLOPT_IPRESOLVE option. If a host
    /// name resolves addresses using more than one IP protocol version, this
    /// option might be handy to force libcurl to use a specific IP version.
    /// </summary>
    public enum CurlIpResolveMode
    {
        /// <summary>
        /// default, resolves addresses to all IP versions that your system allows
        /// </summary>
        CURL_IPRESOLVE_WHATEVER = 0,    
        /// <summary>
        /// IPv4
        /// </summary>
        CURL_IPRESOLVE_V4 = 1,
        /// <summary>
        /// IPv6 (untested)
        /// </summary>
        CURL_IPRESOLVE_V6 = 2
    }

    /// <summary>
    /// These enums are for use with the CURLOPT_HTTP_VERSION option.
    /// </summary>
    public enum CurlHttpVersion
    {
        /// <summary>
        /// auto
        /// </summary>
        CURL_HTTP_VERSION_NONE,
        CURL_HTTP_VERSION_1_0,
        CURL_HTTP_VERSION_1_1,
        CURL_HTTP_VERSION_2_0,
        /// <summary>
        /// invalid
        /// </summary>
        CURL_HTTP_VERSION_LAST
    }

    /// <summary>
    /// These enums are for use with the CURLOPT_NETRC option.
    /// </summary>
    public enum CURL_NETRC_OPTION
    {
        /// <summary>
        /// The .netrc will never be read.
        /// This is the default.
        /// </summary>
        CURL_NETRC_IGNORED,
        /// <summary>
        /// A user:password in the URL will be preferred to one in the .netrc.
        /// </summary>
        CURL_NETRC_OPTIONAL,
        /// <summary>
        /// A user:password in the URL will be ignored.
        /// Unless one is set programmatically, the .netrc
        /// will be queried.
        /// </summary>
        CURL_NETRC_REQUIRED,
        CURL_NETRC_LAST
    }

    public enum curl_TimeCond
    {
        CURL_TIMECOND_NONE,
        CURL_TIMECOND_IFMODSINCE,
        CURL_TIMECOND_IFUNMODSINCE,
        CURL_TIMECOND_LASTMOD,
        CURL_TIMECOND_LAST
    }

    public enum CurlOptType
    {
        CURLOPTTYPE_LONG = 0,
        CURLOPTTYPE_OBJECTPONT = 10000,
        CURLOPTTYPE_FUNCTIONPOINT = 20000,
        CURLOPTTYPE_OFF_T = 30000
    }

    /// <summary>
    /// Options to pass to curl_easy_setopt (EasyCurl.SetOpt)
    /// </summary>
    public enum CURLoption: int
    {
        /// <summary>
        /// This is the FILE * or void * the regular output should be written to.
        /// </summary>
        CURLOPT_WRITEDATA = 10000 + 1,
        /// <summary>
        /// The full URL to get/put
        /// </summary>
        CURLOPT_URL = 10000 + 2,
        /// <summary>
        /// Port number to connect to, if other than default.
        /// </summary>
        CURLOPT_PORT = 0 + 3,
        /// <summary>
        /// Name of proxy to use.
        /// </summary>
        CURLOPT_PROXY = 10000 + 4,
        /// <summary>
        /// "user:password;options" to use when fetching.
        /// </summary>
        CURLOPT_USERPWD = 10000 + 5,
        /// <summary>
        /// "user:password" to use with proxy.
        /// </summary>
        CURLOPT_PROXYUSERPWD = 10000 + 6,
        /// <summary>
        /// Range to get, specified as an ASCII string.
        /// </summary>
        CURLOPT_RANGE = 10000 + 7,
        /// <summary>
        /// Specified file stream to upload from (use as input):
        /// </summary>
        CURLOPT_READDATA = 10000 + 9,
        /// <summary>
        /// Buffer to receive error messages in, must be at least CURL_ERROR_SIZE
        /// count big. If this is not used, error messages go to stderr instead:
        /// </summary>
        CURLOPT_ERRORBUFFER = 10000 + 10,
        /// <summary>
        ///Function that will be called to store the output (instead of fwrite). The
        /// parameters will use fwrite() syntax, make sure to follow them.
        /// </summary>
        CURLOPT_WRITEFUNCTION = 20000 + 11,
        /// <summary>
        /// Function that will be called to read the input (instead of fread). The
        /// parameters will use fread() syntax, make sure to follow them.
        /// </summary>
        CURLOPT_READFUNCTION = 20000 + 12,
        /// <summary>
        /// Time-out the read operation after this amount of seconds
        /// </summary>
        CURLOPT_TIMEOUT = 0 + 13,
        /// <summary>
        /// If the CURLOPT_INFILE is used, this can be used to inform libcurl about
        /// how large the file being sent really is. That allows better error
        /// checking and better verifies that the upload was successful. -1 means
        /// unknown size.
        ///
        /// For large file support, there is also a _LARGE version of the key
        /// which takes an off_t type, allowing platforms with larger off_t
        /// sizes to handle larger files. See below for INFILESIZE_LARGE.
        /// </summary>
        CURLOPT_INFILESIZE = 0 + 14,
        /// <summary>
        /// POST static input fields.
        /// </summary>
        CURLOPT_POSTFIELDS = 10000 + 15,
        /// <summary>
        /// Set the referrer page (needed by some CGIs)
        /// </summary>
        CURLOPT_REFERER = 10000 + 16,
        /// <summary>
        /// Set the FTP PORT string (interface name, named or numerical IP address)
        /// Use i.e '-' to use default address.
        /// </summary>
        CURLOPT_FTPPORT = 10000 + 17,
        /// <summary>
        /// Set the User-Agent string (examined by some CGIs)
        /// </summary>
        CURLOPT_USERAGENT = 10000 + 18,
        /// <summary>
        /// If the download receives less than "low speed limit" count/second
        /// during "low speed time" seconds, the operations is aborted.
        /// You could i.e if you have a pretty high speed connection, abort if
        /// it is less than 2000 count/sec during 20 seconds.
        /// </summary>
        /// <summary>
        /// Set the "low speed limit"
        /// </summary>
        CURLOPT_LOW_SPEED_LIMIT = 0 + 19,
        /// <summary>
        /// Set the "low speed time"
        /// </summary>
        CURLOPT_LOW_SPEED_TIME = 0 + 20,
        /// <summary>
        /// Set the continuation offset.
        ///
        /// Note there is also a _LARGE version of this key which uses
        /// off_t types, allowing for large file offsets on platforms which
        /// use larger-than-32-bit off_t's. Look below for RESUME_FROM_LARGE.
        /// </summary>
        CURLOPT_RESUME_FROM = 0 + 21,
        /// <summary>
        /// Set cookie in request:
        /// </summary>
        CURLOPT_COOKIE = 10000 + 22,
        /// <summary>
        /// This points to a linked list of headers, struct curl_slist kind. This
        /// list is also used for RTSP (in spite of its name)
        /// </summary>
        CURLOPT_HTTPHEADER = 10000 + 23,
        /// <summary>
        /// This points to a linked list of post entries, struct curl_httppost
        /// </summary>
        CURLOPT_HTTPPOST = 10000 + 24,
        /// <summary>
        /// Name of the file keeping your private SSL-certificate
        /// </summary>
        CURLOPT_SSLCERT = 10000 + 25,
        /// <summary>
        /// Password for the SSL or SSH private key
        /// </summary>
        CURLOPT_KEYPASSWD = 10000 + 26,
        /// <summary>
        /// Send TYPE parameter?
        /// </summary>
        CURLOPT_CRLF = 0 + 27,
        /// <summary>
        /// Send linked-list of QUOTE commands
        /// </summary>
        CURLOPT_QUOTE = 10000 + 28,
        /// <summary>
        /// Send FILE * or void * to store headers to, if you use a callback it
        /// is simply passed to the callback unmodified
        /// </summary>
        CURLOPT_HEADERDATA = 10000 + 29,
        /// <summary>
        /// Point to a file to read the initial cookies from, also enables
        /// "cookie awareness"
        /// </summary>
        CURLOPT_COOKIEFILE = 10000 + 31,
        /// <summary>
        /// What version to specifically try to use.
        /// See CURL_SSLVERSION defines below.
        /// </summary>
        CURLOPT_SSLVERSION = 0 + 32,
        /// <summary>
        /// What kind of HTTP time condition to use, see defines
        /// </summary>
        CURLOPT_TIMECONDITION = 0 + 33,
        /// <summary>
        /// Time to use with the above condition. Specified in number of seconds
        /// since 1 Jan 1970
        /// </summary>
        CURLOPT_TIMEVALUE = 0 + 34,
        /// <summary>
        /// Custom request, for customizing the get command like
        /// HTTP: DELETE, TRACE and others
        /// FTP: to use a different list command
        /// </summary>
        CURLOPT_CUSTOMREQUEST = 10000 + 36,
        /// <summary>
        /// HTTP request, for odd commands like DELETE, TRACE and others
        /// </summary>
        CURLOPT_STDERR = 10000 + 37,
        /// <summary>
        /// Send linked-list of post-transfer QUOTE commands
        /// </summary>
        CURLOPT_POSTQUOTE = 10000 + 39,
        /// <summary>
        /// OBSOLETE, do not use!
        /// </summary>
        CURLOPT_OBSOLETE40 = 10000 + 40,
        /// <summary>
        /// Talk a lot
        /// </summary>
        CURLOPT_VERBOSE = 0 + 41,
        /// <summary>
        /// Throw the header out too
        /// </summary>
        CURLOPT_HEADER = 0 + 42,
        /// <summary>
        /// Shut off the progress meter
        /// </summary>
        CURLOPT_NOPROGRESS = 0 + 43,
        /// <summary>
        /// Use HEAD to get http document
        /// </summary>
        CURLOPT_NOBODY = 0 + 44,
        /// <summary>
        /// No output on http error codes >= 400
        /// </summary>
        CURLOPT_FAILONERROR = 0 + 45,
        /// <summary>
        /// This is an upload
        /// </summary>
        CURLOPT_UPLOAD = 0 + 46,
        /// <summary>
        /// HTTP POST method
        /// </summary>
        CURLOPT_POST = 0 + 47,
        /// <summary>
        /// Bare names when listing directories
        /// </summary>
        CURLOPT_DIRLISTONLY = 0 + 48,
        /// <summary>
        /// Append instead of overwrite on upload!
        /// </summary>
        CURLOPT_APPEND = 0 + 50,
        /// <summary>
        /// Specify whether to read the user+password from the .netrc or the URL.
        /// This must be one of the CURL_NETRC_* enums below.
        /// </summary>
        CURLOPT_NETRC = 0 + 51,
        /// <summary>
        /// Use Location: Luke!
        /// </summary>
        CURLOPT_FOLLOWLOCATION = 0 + 52,
        /// <summary>
        /// Transfer data in text/ASCII format
        /// </summary>
        CURLOPT_TRANSFERTEXT = 0 + 53,
        /// <summary>
        /// HTTP PUT
        /// </summary>
        CURLOPT_PUT = 0 + 54,
        /// <summary>
        /// DEPRECATED
        /// Function that will be called instead of the internal progress display
        /// function. This function should be defined as the curl_progress_callback
        /// prototype defines.
        /// </summary>
        CURLOPT_PROGRESSFUNCTION = 20000 + 56,
        /// <summary>
        /// Data passed to the CURLOPT_PROGRESSFUNCTION and CURLOPT_XFERINFOFUNCTION
        /// callbacks
        /// </summary>
        CURLOPT_PROGRESSDATA = 10000 + 57,
        /// <summary>
        /// We want the referrer field set automatically when following locations
        /// </summary>
        CURLOPT_AUTOREFERER = 0 + 58,
        /// <summary>
        /// Port of the proxy, can be set in the proxy string as well with:
        /// "[host]:[port]"
        /// </summary>
        CURLOPT_PROXYPORT = 0 + 59,
        /// <summary>
        /// Size of the POST input data, if strlen() is not good to use
        /// </summary>
        CURLOPT_POSTFIELDSIZE = 0 + 60,
        /// <summary>
        /// Tunnel non-http operations through a HTTP proxy
        /// </summary>
        CURLOPT_HTTPPROXYTUNNEL = 0 + 61,
        /// <summary>
        /// Set the interface string to use as outgoing network interface
        /// </summary>
        CURLOPT_INTERFACE = 10000 + 62,
        /// <summary>
        /// Set the krb4/5 security level, this also enables krb4/5 awareness. This
        /// is a string, 'clear', 'safe', 'confidential' or 'private'. If the string
        /// is set but doesn't match one of these, 'private' will be used.
        /// </summary>
        CURLOPT_KRBLEVEL = 10000 + 63,
        /// <summary>
        /// Set if we should verify the peer in ssl handshake, set 1 to verify.
        /// </summary>
        CURLOPT_SSL_VERIFYPEER = 0 + 64,
        /// <summary>
        /// The CApath or CAfile used to validate the peer certificate
        /// this option is used only if SSL_VERIFYPEER is true
        /// </summary>
        CURLOPT_CAINFO = 10000 + 65,
        /// <summary>
        /// Maximum number of http redirects to follow
        /// </summary>
        CURLOPT_MAXREDIRS = 0 + 68,
        /// <summary>
        /// Pass a long set to 1 to get the date of the requested document (if
        /// possible)! Pass a zero to shut it off.
        /// </summary>
        CURLOPT_FILETIME = 0 + 69,
        /// <summary>
        /// This points to a linked list of telnet options
        /// </summary>
        CURLOPT_TELNETOPTIONS = 10000 + 70,
        /// <summary>
        /// Max amount of cached alive connections
        /// </summary>
        CURLOPT_MAXCONNECTS = 0 + 71,
        /// <summary>
        /// OBSOLETE, do not use!
        /// </summary>
        CURLOPT_OBSOLETE72 = 0 + 72,
        /// <summary>
        /// Set to explicitly use a new connection for the upcoming transfer.
        /// Do not use this unless you're absolutely sure of this, as it makes the
        /// operation slower and is less friendly for the network.
        /// </summary>
        CURLOPT_FRESH_CONNECT = 0 + 74,
        /// <summary>
        /// Set to explicitly forbid the upcoming transfer's connection to be re-used
        /// when done. Do not use this unless you're absolutely sure of this, as it
        /// makes the operation slower and is less friendly for the network.
        /// </summary>
        CURLOPT_FORBID_REUSE = 0 + 75,
        /// <summary>
        /// Set to a file name that contains random data for libcurl to use to
        /// seed the random engine when doing SSL connects.
        /// </summary>
        CURLOPT_RANDOM_FILE = 10000 + 76,
        /// <summary>
        /// Set to the Entropy Gathering Daemon socket pathname
        /// </summary>
        CURLOPT_EGDSOCKET = 10000 + 77,
        /// <summary>
        /// Time-out connect operations after this amount of seconds, if connects are
        /// OK within this time, then fine... This only aborts the connect phase.
        /// </summary>
        CURLOPT_CONNECTTIMEOUT = 0 + 78,
        /// <summary>
        /// Function that will be called to store headers (instead of fwrite). The
        /// parameters will use fwrite() syntax, make sure to follow them.
        /// </summary>
        CURLOPT_HEADERFUNCTION = 20000 + 79,
        /// <summary>
        /// Set this to force the HTTP request to get back to GET. Only really usable
        /// if POST, PUT or a custom request have been used first.
        /// </summary>
        CURLOPT_HTTPGET = 0 + 80,
        /// <summary>
        /// Set if we should verify the Common name from the peer certificate in ssl
        /// handshake, set 1 to check existence, 2 to ensure that it matches the
        /// provided hostname.
        /// </summary>
        CURLOPT_SSL_VERIFYHOST = 0 + 81,
        /// <summary>
        /// Specify which file name to write all known cookies in after completed
        /// operation. Set file name to "-" (dash) to make it go to stdout.
        /// </summary>
        CURLOPT_COOKIEJAR = 10000 + 82,
        /// <summary>
        /// Specify which SSL ciphers to use
        /// </summary>
        CURLOPT_SSL_CIPHER_LIST = 10000 + 83,
        /// <summary>
        /// Specify which HTTP version to use! This must be set to one of the
        /// CURL_HTTP_VERSION* enums set below.
        /// </summary>
        CURLOPT_HTTP_VERSION = 0 + 84,
        /// <summary>
        /// Specifically switch on or off the FTP engine's use of the EPSV command. By
        /// default, that one will always be attempted before the more traditional
        /// PASV command.
        /// </summary>
        CURLOPT_FTP_USE_EPSV = 0 + 85,
        /// <summary>
        /// Type of the file keeping your SSL-certificate ("DER", "PEM", "ENG")
        /// </summary>
        CURLOPT_SSLCERTTYPE = 10000 + 86,
        /// <summary>
        /// Name of the file keeping your private SSL-key
        /// </summary>
        CURLOPT_SSLKEY = 10000 + 87,
        /// <summary>
        /// Type of the file keeping your private SSL-key ("DER", "PEM", "ENG")
        /// </summary>
        CURLOPT_SSLKEYTYPE = 10000 + 88,
        /// <summary>
        /// Crypto engine for the SSL-sub system
        /// </summary>
        CURLOPT_SSLENGINE = 10000 + 89,
        /// <summary>
        /// Set the crypto engine for the SSL-sub system as default
        /// the param has no meaning...
        /// </summary>
        CURLOPT_SSLENGINE_DEFAULT = 0 + 90,
        /// <summary>
        /// DEPRECATED: non-zero value means to use the global dns cache
        /// </summary>
        CURLOPT_DNS_USE_GLOBAL_CACHE = 0 + 91,
        /// <summary>
        /// DNS cache timeout
        /// </summary>
        CURLOPT_DNS_CACHE_TIMEOUT = 0 + 92,
        /// <summary>
        /// Send linked-list of pre-transfer QUOTE commands
        /// </summary>
        CURLOPT_PREQUOTE = 10000 + 93,
        /// <summary>
        /// Set the debug function
        /// </summary>
        CURLOPT_DEBUGFUNCTION = 20000 + 94,
        /// <summary>
        /// Set the data for the debug function
        /// </summary>
        CURLOPT_DEBUGDATA = 10000 + 95,
        /// <summary>
        /// Mark this as start of a cookie session
        /// </summary>
        CURLOPT_COOKIESESSION = 0 + 96,
        /// <summary>
        /// The CApath directory used to validate the peer certificate
        /// this option is used only if SSL_VERIFYPEER is true
        /// </summary>
        CURLOPT_CAPATH = 10000 + 97,
        /// <summary>
        /// Instruct libcurl to use a smaller receive buffer
        /// </summary>
        CURLOPT_BUFFERSIZE = 0 + 98,
        /// <summary>
        /// Instruct libcurl to not use any signal/alarm handlers, even when using
        /// timeouts. This option is useful for multi-threaded applications.
        /// See libcurl-the-guide for more background information.
        /// </summary>
        CURLOPT_NOSIGNAL = 0 + 99,
        /// <summary>
        /// Provide a CURLShare for mutexing non-ts data
        /// </summary>
        CURLOPT_SHARE = 10000 + 100,
        /// <summary>
        /// Indicates type of proxy. accepted values are CURLPROXY_HTTP (default),
        /// CURLPROXY_SOCKS4, CURLPROXY_SOCKS4A and CURLPROXY_SOCKS5.
        /// </summary>
        CURLOPT_PROXYTYPE = 0 + 101,
        /// <summary>
        /// Set the Accept-Encoding string. Use this to tell a server you would like
        /// the response to be compressed. Before 7.21.6, this was known as
        /// CURLOPT_ENCODING
        /// </summary>
        CURLOPT_ACCEPT_ENCODING = 10000 + 102,
        /// <summary>
        /// Set pointer to private data
        /// </summary>
        CURLOPT_PRIVATE = 10000 + 103,
        /// <summary>
        /// Set aliases for HTTP 200 in the HTTP Response header
        /// </summary>
        CURLOPT_HTTP200ALIASES = 10000 + 104,
        /// <summary>
        /// Continue to send authentication (user+password) when following locations,
        /// even when hostname changed. This can potentially send off the name
        /// and password to whatever host the server decides.
        /// </summary>
        CURLOPT_UNRESTRICTED_AUTH = 0 + 105,
        /// <summary>
        /// Specifically switch on or off the FTP engine's use of the EPRT command (
        /// it also disables the LPRT attempt). By default, those ones will always be
        /// attempted before the good old traditional PORT command.
        /// </summary>
        CURLOPT_FTP_USE_EPRT = 0 + 106,
        /// <summary>
        /// Set this to a bitmask value to enable the particular authentications
        /// methods you like. Use this in combination with CURLOPT_USERPWD.
        /// Note that setting multiple bits may cause extra network round-trips.
        /// </summary>
        CURLOPT_HTTPAUTH = 0 + 107,
        /// <summary>
        /// Set the ssl context callback function, currently only for OpenSSL ssl_ctx
        /// in second argument. The function must be matching the
        /// curl_ssl_ctx_callback proto.
        /// </summary>
        CURLOPT_SSL_CTX_FUNCTION = 20000 + 108,
        /// <summary>
        /// Set the userdata for the ssl context callback function's third
        /// argument
        /// </summary>
        CURLOPT_SSL_CTX_DATA = 10000 + 109,
        /// <summary>
        /// FTP Option that causes missing dirs to be created on the remote server.
        /// In 7.19.4 we introduced the convenience enums for this option using the
        /// CURLFTP_CREATE_DIR prefix.
        /// </summary>
        CURLOPT_FTP_CREATE_MISSING_DIRS = 0 + 110,
        /// <summary>
        /// Set this to a bitmask value to enable the particular authentications
        /// methods you like. Use this in combination with CURLOPT_PROXYUSERPWD.
        /// Note that setting multiple bits may cause extra network round-trips.
        /// </summary>
        CURLOPT_PROXYAUTH = 0 + 111,
        /// <summary>
        /// FTP option that changes the timeout, in seconds, associated with
        /// getting a response.  This is different from transfer timeout time and
        /// essentially places a demand on the FTP server to acknowledge commands
        /// in a timely manner.
        /// </summary>
        CURLOPT_FTP_RESPONSE_TIMEOUT = 0 + 112,
        /// <summary>
        /// Set this option to one of the CURL_IPRESOLVE_* defines (see below) to
        /// tell libcurl to resolve names to those IP versions only. This only has
        /// affect on systems with support for more than one, i.e IPv4 _and_ IPv6.
        /// </summary>
        CURLOPT_IPRESOLVE = 0 + 113,
        /// <summary>
        /// Set this option to limit the size of a file that will be downloaded from
        /// an HTTP or FTP server.
        ///
        /// Note there is also _LARGE version which adds large file support for
        /// platforms which have larger off_t sizes.  See MAXFILESIZE_LARGE below.
        /// </summary>
        CURLOPT_MAXFILESIZE = 0 + 114,
        /// <summary>
        /// See the comment for INFILESIZE above, but in short, specifies
        /// the size of the file being uploaded.  -1 means unknown.
        /// </summary>
        CURLOPT_INFILESIZE_LARGE = 30000 + 115,
        /// <summary>
        /// Sets the continuation offset.  There is also a LONG version of this;
        /// look above for RESUME_FROM.
        /// </summary>
        CURLOPT_RESUME_FROM_LARGE = 30000 + 116,
        /// <summary>
        /// Sets the maximum size of data that will be downloaded from
        /// an HTTP or FTP server.  See MAXFILESIZE above for the LONG version.
        /// </summary>
        CURLOPT_MAXFILESIZE_LARGE = 30000 + 117,
        /// <summary>
        /// Set this option to the file name of your .netrc file you want libcurl
        /// to parse (using the CURLOPT_NETRC option). If not set, libcurl will do
        /// a poor attempt to find the user's home directory and check for a .netrc
        /// file in there.
        /// </summary>
        CURLOPT_NETRC_FILE = 10000 + 118,
        /// <summary>
        /// Enable SSL/TLS for FTP, pick one of:
        /// CURLUSESSL_TRY     - try using SSL, proceed anyway otherwise
        /// CURLUSESSL_CONTROL - SSL for the control connection or fail
        /// CURLUSESSL_ALL     - SSL for all communication or fail
        /// </summary>
        CURLOPT_USE_SSL = 0 + 119,
        /// <summary>
        /// The _LARGE version of the standard POSTFIELDSIZE option
        /// </summary>
        CURLOPT_POSTFIELDSIZE_LARGE = 30000 + 120,
        /// <summary>
        /// Enable/disable the TCP Nagle algorithm
        /// </summary>
        CURLOPT_TCP_NODELAY = 0 + 121,
        /// <summary>
        /// When FTP over SSL/TLS is selected (with CURLOPT_USE_SSL), this option
        /// can be used to change libcurl's default action which is to first try
        /// "AUTH SSL" and then "AUTH TLS" in this order, and proceed when a OK
        /// response has been received.
        ///
        /// Available parameters are:
        /// CURLFTPAUTH_DEFAULT - let libcurl decide
        /// CURLFTPAUTH_SSL     - try "AUTH SSL" first, then TLS
        /// CURLFTPAUTH_TLS     - try "AUTH TLS" first, then SSL
        /// </summary>
        CURLOPT_FTPSSLAUTH = 0 + 129,
        CURLOPT_IOCTLFUNCTION = 20000 + 130,
        CURLOPT_IOCTLDATA = 10000 + 131,
        /// <summary>
        /// zero terminated string for pass on to the FTP server when asked for
        /// "account" info
        /// </summary>
        CURLOPT_FTP_ACCOUNT = 10000 + 134,
        /// <summary>
        /// feed cookies into cookie engine
        /// </summary>
        CURLOPT_COOKIELIST = 10000 + 135,
        /// <summary>
        /// ignore Content-Length
        /// </summary>
        CURLOPT_IGNORE_CONTENT_LENGTH = 0 + 136,
        /// <summary>
        /// Set to non-zero to skip the IP address received in a 227 PASV FTP server
        /// response. Typically used for FTP-SSL purposes but is not restricted to
        /// that. libcurl will then instead use the same IP address it used for the
        /// control connection.
        /// </summary>
        CURLOPT_FTP_SKIP_PASV_IP = 0 + 137,
        /// <summary>
        /// Select "file method" to use when doing FTP, see the curl_ftpmethod
        /// above.
        /// </summary>
        CURLOPT_FTP_FILEMETHOD = 0 + 138,
        /// <summary>
        /// Local port number to bind the socket to
        /// </summary>
        CURLOPT_LOCALPORT = 0 + 139,
        /// <summary>
        /// Number of ports to try, including the first one set with LOCALPORT.
        /// Thus, setting it to 1 will make no additional attempts but the first.
        /// </summary>
        CURLOPT_LOCALPORTRANGE = 0 + 140,
        /// <summary>
        /// no transfer, set up connection and let application use the socket by
        /// extracting it with CURLINFO_LASTSOCKET
        /// </summary>
        CURLOPT_CONNECT_ONLY = 0 + 141,
        /// <summary>
        /// Function that will be called to convert from the
        /// network encoding (instead of using the iconv calls in libcurl)
        /// </summary>
        CURLOPT_CONV_FROM_NETWORK_FUNCTION = 20000 + 142,
        /// <summary>
        /// Function that will be called to convert to the
        /// network encoding (instead of using the iconv calls in libcurl)
        /// </summary>
        CURLOPT_CONV_TO_NETWORK_FUNCTION = 20000 + 143,
        /// <summary>
        /// Function that will be called to convert from UTF8
        /// (instead of using the iconv calls in libcurl)
        /// Note that this is used only for SSL certificate processing
        /// </summary>
        CURLOPT_CONV_FROM_UTF8_FUNCTION = 20000 + 144,
        /// <summary>
        /// if the connection proceeds too quickly then need to slow it down
        /// limit-rate: maximum number of count per second to send or receive
        /// </summary>
        CURLOPT_MAX_SEND_SPEED_LARGE = 30000 + 145,
        CURLOPT_MAX_RECV_SPEED_LARGE = 30000 + 146,
        /// <summary>
        /// Pointer to command string to send if USER/PASS fails.
        /// </summary>
        CURLOPT_FTP_ALTERNATIVE_TO_USER = 10000 + 147,
        /// <summary>
        /// callback function for setting socket options
        /// </summary>
        CURLOPT_SOCKOPTFUNCTION = 20000 + 148,
        CURLOPT_SOCKOPTDATA = 10000 + 149,
        /// <summary>
        /// set to 0 to disable session ID re-use for this transfer, default is
        /// enabled (== 1)
        /// </summary>
        CURLOPT_SSL_SESSIONID_CACHE = 0 + 150,
        /// <summary>
        /// allowed SSH authentication methods
        /// </summary>
        CURLOPT_SSH_AUTH_TYPES = 0 + 151,
        /// <summary>
        /// Used by scp/sftp to do public/private key authentication
        /// </summary>
        CURLOPT_SSH_PUBLIC_KEYFILE = 10000 + 152,
        CURLOPT_SSH_PRIVATE_KEYFILE = 10000 + 153,
        /// <summary>
        /// Send CCC (Clear Command Channel) after authentication
        /// </summary>
        CURLOPT_FTP_SSL_CCC = 0 + 154,
        /// <summary>
        /// Same as TIMEOUT and CONNECTTIMEOUT, but with ms resolution
        /// </summary>
        CURLOPT_TIMEOUT_MS = 0 + 155,
        CURLOPT_CONNECTTIMEOUT_MS = 0 + 156,
        /// <summary>
        /// set to zero to disable the libcurl's decoding and thus pass the raw body
        /// data to the application even when it is encoded/compressed
        /// </summary>
        CURLOPT_HTTP_TRANSFER_DECODING = 0 + 157,
        CURLOPT_HTTP_CONTENT_DECODING = 0 + 158,
        /// <summary>
        /// Permission used when creating new files and directories on the remote
        /// server for protocols that support it, SFTP/SCP/FILE
        /// </summary>
        CURLOPT_NEW_FILE_PERMS = 0 + 159,
        CURLOPT_NEW_DIRECTORY_PERMS = 0 + 160,
        /// <summary>
        /// Set the behaviour of POST when redirecting. Values must be set to one
        /// of CURL_REDIR* defines below. This used to be called CURLOPT_POST301
        /// </summary>
        CURLOPT_POSTREDIR = 0 + 161,
        /// <summary>
        /// used by scp/sftp to verify the host's public key
        /// </summary>
        CURLOPT_SSH_HOST_PUBLIC_KEY_MD5 = 10000 + 162,
        CURLOPT_OPENSOCKETFUNCTION = 20000 + 163,
        /// <summary>
        /// Callback function for opening socket (instead of socket(2)). Optionally,
        /// callback is able change the address or refuse to connect returning
        /// CURL_SOCKET_BAD.  The callback should have type
        /// curl_opensocket_callback
        /// </summary>
        CURLOPT_OPENSOCKETDATA = 10000 + 164,
        /// <summary>
        /// POST volatile input fields.
        /// </summary>
        CURLOPT_COPYPOSTFIELDS = 10000 + 165,
        /// <summary>
        /// set transfer mode (;type=<a|i>) when doing FTP via an HTTP proxy
        /// </summary>
        CURLOPT_PROXY_TRANSFER_MODE = 0 + 166,
        /// <summary>
        /// Callback function for seeking in the input stream
        /// </summary>
        CURLOPT_SEEKFUNCTION = 20000 + 167,
        CURLOPT_SEEKDATA = 10000 + 168,
        /// <summary>
        /// CRL file
        /// </summary>
        CURLOPT_CRLFILE = 10000 + 169,
        /// <summary>
        /// Issuer certificate
        /// </summary>
        CURLOPT_ISSUERCERT = 10000 + 170,
        /// <summary>
        /// (IPv6) Address scope
        /// </summary>
        CURLOPT_ADDRESS_SCOPE = 0 + 171,
        /// <summary>
        /// Collect certificate chain info and allow it to get retrievable with
        /// CURLINFO_CERTINFO after the transfer is complete.
        /// </summary>
        CURLOPT_CERTINFO = 0 + 172,
        /// <summary>
        /// "name" and "pwd" to use when fetching.
        /// </summary>
        CURLOPT_USERNAME = 10000 + 173,
        CURLOPT_PASSWORD = 10000 + 174,
        /// <summary>
        /// "name" and "pwd" to use with Proxy when fetching.
        /// </summary>
        CURLOPT_PROXYUSERNAME = 10000 + 175,
        CURLOPT_PROXYPASSWORD = 10000 + 176,
        /// <summary>
        /// Comma separated list of hostnames defining no-proxy zones. These should
        /// match both hostnames directly, and hostnames within a domain. For
        /// example, local.com will match local.com and www.local.com, but NOT
        /// notlocal.com or www.notlocal.com. For compatibility with other
        /// implementations of this, .local.com will be considered to be the same as
        /// local.com. A single * is the only valid wildcard, and effectively
        /// disables the use of proxy.
        /// </summary>
        CURLOPT_NOPROXY = 10000 + 177,
        /// <summary>
        /// block size for TFTP transfers
        /// </summary>
        CURLOPT_TFTP_BLKSIZE = 0 + 178,
        /// <summary>
        /// Socks Service
        /// </summary>
        CURLOPT_SOCKS5_GSSAPI_SERVICE = 10000 + 179,
        /// <summary>
        /// Socks Service
        /// </summary>
        CURLOPT_SOCKS5_GSSAPI_NEC = 0 + 180,
        /// <summary>
        /// set the bitmask for the protocols that are allowed to be used for the
        /// transfer, which thus helps the app which takes URLs from users or other
        /// external inputs and want to restrict what protocol(s) to deal
        /// with. Defaults to CURLPROTO_ALL.
        /// </summary>
        CURLOPT_PROTOCOLS = 0 + 181,
        /// <summary>
        /// set the bitmask for the protocols that libcurl is allowed to follow to,
        /// as a subset of the CURLOPT_PROTOCOLS ones. That means the protocol needs
        /// to be set in both bitmasks to be allowed to get redirected to. Defaults
        /// to all protocols except FILE and SCP.
        /// </summary>
        CURLOPT_REDIR_PROTOCOLS = 0 + 182,
        /// <summary>
        /// set the SSH knownhost file name to use
        /// </summary>
        CURLOPT_SSH_KNOWNHOSTS = 10000 + 183,
        /// <summary>
        /// set the SSH host key callback, must point to a curl_sshkeycallback
        /// function
        /// </summary>
        CURLOPT_SSH_KEYFUNCTION = 20000 + 184,
        /// <summary>
        /// set the SSH host key callback custom pointer
        /// </summary>
        CURLOPT_SSH_KEYDATA = 10000 + 185,
        /// <summary>
        /// set the SMTP mail originator
        /// </summary>
        CURLOPT_MAIL_FROM = 10000 + 186,
        /// <summary>
        /// set the SMTP mail receiver(s)
        /// </summary>
        CURLOPT_MAIL_RCPT = 10000 + 187,
        /// <summary>
        /// FTP: send PRET before PASV
        /// </summary>
        CURLOPT_FTP_USE_PRET = 0 + 188,
        /// <summary>
        /// RTSP request method (OPTIONS, SETUP, PLAY, etc...)
        /// </summary>
        CURLOPT_RTSP_REQUEST = 0 + 189,
        /// <summary>
        /// The RTSP session identifier
        /// </summary>
        CURLOPT_RTSP_SESSION_ID = 10000 + 190,
        /// <summary>
        /// The RTSP stream URI
        /// </summary>
        CURLOPT_RTSP_STREAM_URI = 10000 + 191,
        /// <summary>
        /// The Transport: header to use in RTSP requests
        /// </summary>
        CURLOPT_RTSP_TRANSPORT = 10000 + 192,
        /// <summary>
        /// Manually initialize the client RTSP CSeq for this handle
        /// </summary>
        CURLOPT_RTSP_CLIENT_CSEQ = 0 + 193,
        /// <summary>
        /// Manually initialize the server RTSP CSeq for this handle
        /// </summary>
        CURLOPT_RTSP_SERVER_CSEQ = 0 + 194,
        /// <summary>
        /// The stream to pass to INTERLEAVEFUNCTION.
        /// </summary>
        CURLOPT_INTERLEAVEDATA = 10000 + 195,
        /// <summary>
        /// Let the application define a custom write method for RTP data
        /// </summary>
        CURLOPT_INTERLEAVEFUNCTION = 20000 + 196,
        /// <summary>
        /// Turn on wildcard matching
        /// </summary>
        CURLOPT_WILDCARDMATCH = 0 + 197,
        /// <summary>
        /// Directory matching callback called before downloading of an
        /// individual file (chunk) started
        /// </summary>
        CURLOPT_CHUNK_BGN_FUNCTION = 20000 + 198,
        /// <summary>
        /// Directory matching callback called after the file (chunk)
        /// was downloaded, or skipped
        /// </summary>
        CURLOPT_CHUNK_END_FUNCTION = 20000 + 199,
        /// <summary>
        /// Change match (fnmatch-like) callback for wildcard matching
        /// </summary>
        CURLOPT_FNMATCH_FUNCTION = 20000 + 200,
        /// <summary>
        /// Let the application define custom chunk data pointer
        /// </summary>
        CURLOPT_CHUNK_DATA = 10000 + 201,
        /// <summary>
        /// FNMATCH_FUNCTION user pointer
        /// </summary>
        CURLOPT_FNMATCH_DATA = 10000 + 202,
        /// <summary>
        /// send linked-list of name:port:address sets
        /// </summary>
        CURLOPT_RESOLVE = 10000 + 203,
        /// <summary>
        /// Set a username for authenticated TLS
        /// </summary>
        CURLOPT_TLSAUTH_USERNAME = 10000 + 204,
        /// <summary>
        /// Set a password for authenticated TLS
        /// </summary>
        CURLOPT_TLSAUTH_PASSWORD = 10000 + 205,
        /// <summary>
        /// Set authentication type for authenticated TLS
        /// </summary>
        CURLOPT_TLSAUTH_TYPE = 10000 + 206,
        /// <summary>
        /// Set to 1 to enable the "TE:" header in HTTP requests to ask for
        /// compressed transfer-encoded responses. Set to 0 to disable the use of TE:
        /// in outgoing requests. The current default is 0, but it might change in a
        /// future libcurl release.
        ///
        /// libcurl will ask for the compressed methods it knows of, and if that
        /// isn't any, it will not ask for transfer-encoding at all even if this
        /// option is set to 1.
        /// </summary>
        CURLOPT_TRANSFER_ENCODING = 0 + 207,
        /// <summary>
        /// Callback function for closing socket (instead of close(2)). The callback
        /// should have type curl_closesocket_callback
        /// </summary>
        CURLOPT_CLOSESOCKETFUNCTION = 20000 + 208,
        CURLOPT_CLOSESOCKETDATA = 10000 + 209,
        /// <summary>
        /// allow GSSAPI credential delegation
        /// </summary>
        CURLOPT_GSSAPI_DELEGATION = 0 + 210,
        /// <summary>
        /// Set the name servers to use for DNS resolution
        /// </summary>
        CURLOPT_DNS_SERVERS = 10000 + 211,
        /// <summary>
        /// Time-out accept operations (currently for FTP only) after this amount
        /// of miliseconds.
        /// </summary>
        CURLOPT_ACCEPTTIMEOUT_MS = 0 + 212,
        /// <summary>
        /// Set TCP keepalive
        /// </summary>
        CURLOPT_TCP_KEEPALIVE = 0 + 213,
        /// <summary>
        /// non-universal keepalive knobs (Linux, AIX, HP-UX, more)
        /// </summary>
        CURLOPT_TCP_KEEPIDLE = 0 + 214,
        CURLOPT_TCP_KEEPINTVL = 0 + 215,
        /// <summary>
        /// Enable/disable specific SSL features with a bitmask, see CURLSSLOPT_*
        /// </summary>
        CURLOPT_SSL_OPTIONS = 0 + 216,
        /// <summary>
        /// Set the SMTP auth originator
        /// </summary>
        CURLOPT_MAIL_AUTH = 10000 + 217,
        /// <summary>
        /// Enable/disable SASL initial response
        /// </summary>
        CURLOPT_SASL_IR = 0 + 218,
        /// <summary>
        /// Function that will be called instead of the internal progress display
        /// function. This function should be defined as the curl_xferinfo_callback
        /// prototype defines. (Deprecates CURLOPT_PROGRESSFUNCTION)
        /// </summary>
        CURLOPT_XFERINFOFUNCTION = 20000 + 219,
        /// <summary>
        /// The XOAUTH2 bearer token
        /// </summary>
        CURLOPT_XOAUTH2_BEARER = 10000 + 220,
        /// <summary>
        /// Set the interface string to use as outgoing network
        /// interface for DNS requests.
        /// Only supported by the c-ares DNS backend
        /// </summary>
        CURLOPT_DNS_INTERFACE = 10000 + 221,
        /// <summary>
        /// Set the local IPv4 address to use for outgoing DNS requests.
        /// Only supported by the c-ares DNS backend
        /// </summary>
        CURLOPT_DNS_LOCAL_IP4 = 10000 + 222,
        /// <summary>
        /// Set the local IPv6 address to use for outgoing DNS requests.
        /// Only supported by the c-ares DNS backend
        /// </summary>
        CURLOPT_DNS_LOCAL_IP6 = 10000 + 223,
        /// <summary>
        /// Set authentication options directly
        /// </summary>
        CURLOPT_LOGIN_OPTIONS = 10000 + 224,
        /// <summary>
        /// Enable/disable TLS NPN extension (http2 over ssl might fail without)
        /// </summary>
        CURLOPT_SSL_ENABLE_NPN = 0 + 225,
        /// <summary>
        /// Enable/disable TLS ALPN extension (http2 over ssl might fail without)
        /// </summary>
        CURLOPT_SSL_ENABLE_ALPN = 0 + 226,
        /// <summary>
        /// Time to wait for a response to a HTTP request containing an
        /// Expect: 100-continue header before sending the data anyway.
        /// </summary>
        CURLOPT_EXPECT_100_TIMEOUT_MS = 0 + 227,
        /// <summary>
        /// This points to a linked list of headers used for proxy requests only,
        /// struct curl_slist kind
        /// </summary>
        CURLOPT_PROXYHEADER = 10000 + 228,
        /// <summary>
        /// Pass in a bitmask of "header options"
        /// </summary>
        CURLOPT_HEADEROPT = 0 + 229,
        /// <summary>
        /// The public key in DER form used to validate the peer public key
        /// this option is used only if SSL_VERIFYPEER is true
        /// </summary>
        CURLOPT_PINNEDPUBLICKEY = 10000 + 230,
        /// <summary>
        /// Path to Unix domain socket
        /// </summary>
        CURLOPT_UNIX_SOCKET_PATH = 10000 + 231,
        /// <summary>
        /// Set if we should verify the certificate status.
        /// </summary>
        CURLOPT_SSL_VERIFYSTATUS = 0 + 232,
        /// <summary>
        /// Set if we should enable TLS false start.
        /// </summary>
        CURLOPT_SSL_FALSESTART = 0 + 233,
        /// <summary>
        /// Do not squash dot-dot sequences
        /// </summary>
        CURLOPT_PATH_AS_IS = 0 + 234,
        /// <summary>
        /// do not use (last in enum)
        /// </summary>
        CURLOPT_LASTENTRY
    }
}
