using System;
using System.Collections.Generic;
using System.Text;

namespace ThisOldCurl.LibCurl
{
    public static class CurlInfoConstants
    {
        public const int CURLINFO_STRING = 0x100000;
        public const int CURLINFO_LONG = 0x200000;
        public const int CURLINFO_DOUBLE = 0x300000;
        public const int CURLINFO_SLIST = 0x400000;
        public const int CURLINFO_MASK = 0x0fffff;
        public const int CURLINFO_TYPEMASK = 0xf00000;
    }

    public enum CURLINFO : int
    {
        CURLINFO_NONE = 0, // first, never use this

        CURLINFO_EFFECTIVE_URL = CurlInfoConstants.CURLINFO_STRING + 1,
        CURLINFO_RESPONSE_CODE = CurlInfoConstants.CURLINFO_LONG + 2,
        CURLINFO_TOTAL_TIME = CurlInfoConstants.CURLINFO_DOUBLE + 3,
        CURLINFO_NAMELOOKUP_TIME = CurlInfoConstants.CURLINFO_DOUBLE + 4,
        CURLINFO_CONNECT_TIME = CurlInfoConstants.CURLINFO_DOUBLE + 5,
        CURLINFO_PRETRANSFER_TIME = CurlInfoConstants.CURLINFO_DOUBLE + 6,
        CURLINFO_SIZE_UPLOAD = CurlInfoConstants.CURLINFO_DOUBLE + 7,
        CURLINFO_SIZE_DOWNLOAD = CurlInfoConstants.CURLINFO_DOUBLE + 8,
        CURLINFO_SPEED_DOWNLOAD = CurlInfoConstants.CURLINFO_DOUBLE + 9,
        CURLINFO_SPEED_UPLOAD = CurlInfoConstants.CURLINFO_DOUBLE + 10,
        CURLINFO_HEADER_SIZE = CurlInfoConstants.CURLINFO_LONG + 11,
        CURLINFO_REQUEST_SIZE = CurlInfoConstants.CURLINFO_LONG + 12,
        CURLINFO_SSL_VERIFYRESULT = CurlInfoConstants.CURLINFO_LONG + 13,
        CURLINFO_FILETIME = CurlInfoConstants.CURLINFO_LONG + 14,
        CURLINFO_CONTENT_LENGTH_DOWNLOAD = CurlInfoConstants.CURLINFO_DOUBLE + 15,
        CURLINFO_CONTENT_LENGTH_UPLOAD = CurlInfoConstants.CURLINFO_DOUBLE + 16,
        CURLINFO_STARTTRANSFER_TIME = CurlInfoConstants.CURLINFO_DOUBLE + 17,
        CURLINFO_CONTENT_TYPE = CurlInfoConstants.CURLINFO_STRING + 18,
        CURLINFO_REDIRECT_TIME = CurlInfoConstants.CURLINFO_DOUBLE + 19,
        CURLINFO_REDIRECT_COUNT = CurlInfoConstants.CURLINFO_LONG + 20,
        CURLINFO_PRIVATE = CurlInfoConstants.CURLINFO_STRING + 21,
        CURLINFO_HTTP_CONNECTCODE = CurlInfoConstants.CURLINFO_LONG + 22,
        CURLINFO_HTTPAUTH_AVAIL = CurlInfoConstants.CURLINFO_LONG + 23,
        CURLINFO_PROXYAUTH_AVAIL = CurlInfoConstants.CURLINFO_LONG + 24,
        CURLINFO_OS_ERRNO = CurlInfoConstants.CURLINFO_LONG + 25,
        CURLINFO_NUM_CONNECTS = CurlInfoConstants.CURLINFO_LONG + 26,
        CURLINFO_SSL_ENGINES = CurlInfoConstants.CURLINFO_SLIST + 27,
        CURLINFO_COOKIELIST = CurlInfoConstants.CURLINFO_SLIST + 28,
        CURLINFO_LASTSOCKET = CurlInfoConstants.CURLINFO_LONG + 29,
        CURLINFO_FTP_ENTRY_PATH = CurlInfoConstants.CURLINFO_STRING + 30,
        CURLINFO_REDIRECT_URL = CurlInfoConstants.CURLINFO_STRING + 31,
        CURLINFO_PRIMARY_IP = CurlInfoConstants.CURLINFO_STRING + 32,
        CURLINFO_APPCONNECT_TIME = CurlInfoConstants.CURLINFO_DOUBLE + 33,
        CURLINFO_CERTINFO = CurlInfoConstants.CURLINFO_SLIST + 34,
        CURLINFO_CONDITION_UNMET = CurlInfoConstants.CURLINFO_LONG + 35,
        CURLINFO_RTSP_SESSION_ID = CurlInfoConstants.CURLINFO_STRING + 36,
        CURLINFO_RTSP_CLIENT_CSEQ = CurlInfoConstants.CURLINFO_LONG + 37,
        CURLINFO_RTSP_SERVER_CSEQ = CurlInfoConstants.CURLINFO_LONG + 38,
        CURLINFO_RTSP_CSEQ_RECV = CurlInfoConstants.CURLINFO_LONG + 39,
        CURLINFO_PRIMARY_PORT = CurlInfoConstants.CURLINFO_LONG + 40,
        CURLINFO_LOCAL_IP = CurlInfoConstants.CURLINFO_STRING + 41,
        CURLINFO_LOCAL_PORT = CurlInfoConstants.CURLINFO_LONG + 42,
        CURLINFO_TLS_SESSION = CurlInfoConstants.CURLINFO_SLIST + 43,

        CURLINFO_LASTONE = 43
    }
}
