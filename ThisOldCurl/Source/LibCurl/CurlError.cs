using System;
using System.Collections.Generic;
using System.Text;

namespace ThisOldCurl.LibCurl
{
    /// <summary>
    /// All possible error codes from all sorts of curl functions. Future versions
    /// may return other values, stay prepared.
    /// 
    /// Always add new return codes last. Never *EVER* remove any. The return
    /// codes must remain the same!
    /// </summary>
    public enum CURLcode
    {
        CURLE_OK = 0,
        CURLE_UNSUPPORTED_PROTOCOL,    /* 1 */
        CURLE_FAILED_INIT,             /* 2 */
        CURLE_URL_MALFORMAT,           /* 3 */
        CURLE_NOT_BUILT_IN,
        CURLE_COULDNT_RESOLVE_PROXY,   /* 5 */
        CURLE_COULDNT_RESOLVE_HOST,    /* 6 */
        CURLE_COULDNT_CONNECT,         /* 7 */
        CURLE_FTP_WEIRD_SERVER_REPLY,  /* 8 */
        /// <summary>
        /// 9 - a service was denied by the server
        /// due to lack of access - when login fails
        /// this is not returned.
        /// </summary>
        CURLE_REMOTE_ACCESS_DENIED,
        CURLE_FTP_ACCEPT_FAILED,
        CURLE_FTP_WEIRD_PASS_REPLY,    /* 11 */
        /// <summary>
        /// 12 - timeout occurred accepting server
        /// </summary>
        CURLE_FTP_ACCEPT_TIMEOUT,
        CURLE_FTP_WEIRD_PASV_REPLY,    /* 13 */
        CURLE_FTP_WEIRD_227_FORMAT,    /* 14 */
        CURLE_FTP_CANT_GET_HOST,       /* 15 */
        /// <summary>
        /// 16 - A problem in the http2 framing layer.
        /// </summary>
        CURLE_HTTP2,
        CURLE_FTP_COULDNT_SET_TYPE,    /* 17 */
        CURLE_PARTIAL_FILE,            /* 18 */
        CURLE_FTP_COULDNT_RETR_FILE,   /* 19 */
        /// <summary>
        /// NOT USED
        /// </summary>
        CURLE_OBSOLETE20,              /* 20 - NOT USED */
        /// <summary>
        /// 21 - quote command failure
        /// </summary>
        CURLE_QUOTE_ERROR,             
        CURLE_HTTP_RETURNED_ERROR,     /* 22 */
        CURLE_WRITE_ERROR,             /* 23 */
        /// <summary>
        /// NOT USED
        /// </summary>
        CURLE_OBSOLETE24,              /* 24 - NOT USED */
        /// <summary>
        /// 25 - failed upload "command"
        /// </summary>
        CURLE_UPLOAD_FAILED,           /* 25 - failed upload "command" */
        /// <summary>
        /// 26 - couldn't open/read from file
        /// </summary>
        CURLE_READ_ERROR,              /* 26 - couldn't open/read from file */
        /// <summary>
        /// 27 - Note: CURLE_OUT_OF_MEMORY may sometimes indicate a conversion error
        /// instead of a memory allocation error if CURL_DOES_CONVERSIONS
        /// is defined
        /// </summary>
        CURLE_OUT_OF_MEMORY,           /* 27 */
        /// <summary>
        /// 28 - the timeout time was reached
        /// </summary>
        CURLE_OPERATION_TIMEDOUT,
        /// <summary>
        /// 29 - NOT USED
        /// </summary>
        CURLE_OBSOLETE29,              /* 29 - NOT USED */
        /// <summary>
        /// 30 - FTP PORT operation failed
        /// </summary>
        CURLE_FTP_PORT_FAILED,         /* 30 - FTP PORT operation failed */
        /// <summary>
        /// 31 - the REST command failed
        /// </summary>
        CURLE_FTP_COULDNT_USE_REST,    /* 31 - the REST command failed */
        /// <summary>
        /// NOT USED
        /// </summary>
        CURLE_OBSOLETE32,              /* 32 - NOT USED */
        /// <summary>
        /// 33 - RANGE "command" didn't work
        /// </summary>
        CURLE_RANGE_ERROR,             /* 33 - RANGE "command" didn't work */
        CURLE_HTTP_POST_ERROR,         /* 34 */
        /// <summary>
        /// 35 - wrong when connecting with SSL
        /// </summary>
        CURLE_SSL_CONNECT_ERROR,  
        /// <summary>
        /// 36 - couldn't resume download
        /// </summary>
        CURLE_BAD_DOWNLOAD_RESUME,   
        CURLE_FILE_COULDNT_READ_FILE,  /* 37 */
        CURLE_LDAP_CANNOT_BIND,        /* 38 */
        CURLE_LDAP_SEARCH_FAILED,      /* 39 */
        /// <summary>
        /// NOT USED
        /// </summary>
        CURLE_OBSOLETE40,              /* 40 - NOT USED */
        CURLE_FUNCTION_NOT_FOUND,      /* 41 */
        CURLE_ABORTED_BY_CALLBACK,     /* 42 */
        CURLE_BAD_FUNCTION_ARGUMENT,   /* 43 */
        /// <summary>
        /// NOT USED
        /// </summary>
        CURLE_OBSOLETE44,              /* 44 - NOT USED */
        /// <summary>
        /// 45 - CURLOPT_INTERFACE failed
        /// </summary>
        CURLE_INTERFACE_FAILED,        /* 45 - CURLOPT_INTERFACE failed */
        /// <summary>
        /// NOT USED
        /// </summary>
        CURLE_OBSOLETE46,              /* 46 - NOT USED */
        /// <summary>
        /// 47 - catch endless re-direct loops
        /// </summary>
        CURLE_TOO_MANY_REDIRECTS,     /* 47 - catch endless re-direct loops */
        /// <summary>
        /// 48 - User specified an unknown option
        /// </summary>
        CURLE_UNKNOWN_OPTION,          /* 48 - User specified an unknown option */
        /// <summary>
        /// 49 - Malformed telnet option
        /// </summary>
        CURLE_TELNET_OPTION_SYNTAX,   /* 49 - Malformed telnet option */
        /// <summary>
        /// NOT USED 
        /// </summary>
        CURLE_OBSOLETE50,              /* 50 - NOT USED */
        /// <summary>
        /// 51 - peer's certificate or fingerprint wasn't verified fine
        /// </summary>
        CURLE_PEER_FAILED_VERIFICATION,
        /// <summary>
        /// 52 - when this is a specific error
        /// </summary>
        CURLE_GOT_NOTHING,             /* 52 - when this is a specific error */
        /// <summary>
        /// 53 - SSL crypto engine not found
        /// </summary>
        CURLE_SSL_ENGINE_NOTFOUND,     /* 53 - SSL crypto engine not found */
        /// <summary>
        /// 54 - can not set SSL crypto engine as default
        /// </summary>
        CURLE_SSL_ENGINE_SETFAILED,    /* 54 - can not set SSL crypto engine as default */
        /// <summary>
        /// 55 - failed sending network data
        /// </summary>
        CURLE_SEND_ERROR,              /* 55 - failed sending network data */
        /// <summary>
        /// 56 - failure in receiving network data 
        /// </summary>
        CURLE_RECV_ERROR,              /* 56 - failure in receiving network data */
        /// <summary>
        /// NOT IN USE 
        /// </summary>
        CURLE_OBSOLETE57,              /* 57 - NOT IN USE */
        /// <summary>
        /// 58 - problem with the local certificate
        /// </summary>
        CURLE_SSL_CERTPROBLEM,         /* 58 - problem with the local certificate */
        /// <summary>
        /// 59 - couldn't use specified cipher
        /// </summary>
        CURLE_SSL_CIPHER,              /* 59 - couldn't use specified cipher */
        /// <summary>
        /// 60 - problem with the CA cert (path?)
        /// </summary>
        CURLE_SSL_CACERT,              /* 60 - problem with the CA cert (path?) */
        /// <summary>
        /// 61 - Unrecognized/bad encoding
        /// </summary>
        CURLE_BAD_CONTENT_ENCODING,    /* 61 - Unrecognized/bad encoding */
        /// <summary>
        /// 62 - Invalid LDAP URL
        /// </summary>
        CURLE_LDAP_INVALID_URL,        /* 62 - Invalid LDAP URL */
        /// <summary>
        /// 63 - Maximum file size exceeded
        /// </summary>
        CURLE_FILESIZE_EXCEEDED,       /* 63 - Maximum file size exceeded */
        /// <summary>
        /// 64 - Requested FTP SSL level failed
        /// </summary>
        CURLE_USE_SSL_FAILED,          /* 64 - Requested FTP SSL level failed */
        /// <summary>
        /// 65 - Sending the data requires a rewind that failed
        /// </summary>
        CURLE_SEND_FAIL_REWIND,        /* 65 - Sending the data requires a rewind that failed */
        /// <summary>
        /// 66 - failed to initialise ENGINE
        /// </summary>
        CURLE_SSL_ENGINE_INITFAILED,   /* 66 - failed to initialise ENGINE */
        /// <summary>
        /// 67 - user, password or similar was not accepted and we failed to login
        /// </summary>
        CURLE_LOGIN_DENIED,            /* 67 - user, password or similar was not accepted and we failed to login */
        /// <summary>
        /// 68 - file not found on server
        /// </summary>
        CURLE_TFTP_NOTFOUND,           /* 68 - file not found on server */
        /// <summary>
        /// 69 - permission problem on server
        /// </summary>
        CURLE_TFTP_PERM,               /* 69 - permission problem on server */
        /// <summary>
        /// 70 - out of disk space on server
        /// </summary>
        CURLE_REMOTE_DISK_FULL,        /* 70 - out of disk space on server */
        /// <summary>
        /// 71 - Illegal TFTP operation
        /// </summary>
        CURLE_TFTP_ILLEGAL,            /* 71 - Illegal TFTP operation */
        /// <summary>
        /// 72 - Unknown transfer ID
        /// </summary>
        CURLE_TFTP_UNKNOWNID,          /* 72 - Unknown transfer ID */
        /// <summary>
        /// 73 - File already exists
        /// </summary>
        CURLE_REMOTE_FILE_EXISTS,      /* 73 - File already exists */
        /// <summary>
        /// 74 - No such user
        /// </summary>
        CURLE_TFTP_NOSUCHUSER,         /* 74 - No such user */
        /// <summary>
        /// 75 - conversion failed
        /// </summary>
        CURLE_CONV_FAILED,             /* 75 - conversion failed */
        /// <summary>
        /// 76 - caller must register conversion
        /// callbacks using curl_easy_setopt options
        /// CURLOPT_CONV_FROM_NETWORK_FUNCTION,
        /// CURLOPT_CONV_TO_NETWORK_FUNCTION, and
        /// CURLOPT_CONV_FROM_UTF8_FUNCTION 
        /// </summary>
        CURLE_CONV_REQD,               
        /// <summary>
        /// 77 - could not load CACERT file, missing or wrong format
        /// </summary>
        CURLE_SSL_CACERT_BADFILE,      /* 77 - could not load CACERT file, missing or wrong format */
        /// <summary>
        /// 78 - remote file not found
        /// </summary>
        CURLE_REMOTE_FILE_NOT_FOUND,   /* 78 - remote file not found */
        /// <summary>
        /// 79 - error from the SSH layer, somewhat 
        /// generic so the error message will be of
        /// interest when this has happened 
        /// </summary>
        CURLE_SSH,
        /// <summary>
        /// 80 - failled to shut down the SSL connection
        /// </summary>
        CURLE_SSL_SHUTDOWN_FAILED,   
        /// <summary>
        /// 81 - socket is not ready for send/recv, wait till it's ready and try again
        /// </summary>
        CURLE_AGAIN, 
        /// <summary>
        /// 82 - could not load CRL file, missing or wrong format
        /// </summary>
        CURLE_SSL_CRL_BADFILE,         /* 82 - could not load CRL file, missing or wrong format (Added in 7.19.0) */
        /// <summary>
        /// 83 - Issuer check failed.
        /// </summary>
        CURLE_SSL_ISSUER_ERROR,  
        /// <summary>
        /// 84 - a PRET command failed 
        /// </summary>
        CURLE_FTP_PRET_FAILED,         /* 84 - a PRET command failed */
        /// <summary>
        /// 85 - mismatch of RTSP CSeq numbers
        /// </summary>
        CURLE_RTSP_CSEQ_ERROR,         /* 85 - mismatch of RTSP CSeq numbers */
        /// <summary>
        /// 86 - mismatch of RTSP Session Ids
        /// </summary>
        CURLE_RTSP_SESSION_ERROR,      /* 86 - mismatch of RTSP Session Ids */
        /// <summary>
        /// 87 - unable to parse FTP file list
        /// </summary>
        CURLE_FTP_BAD_FILE_LIST,       /* 87 - unable to parse FTP file list */
        /// <summary>
        /// 88 - chunk callback reported error
        /// </summary>
        CURLE_CHUNK_FAILED,            /* 88 - chunk callback reported error */
        /// <summary>
        /// 89 - No connection available, the session will be queued
        /// </summary>
        CURLE_NO_CONNECTION_AVAILABLE, /* 89 - No connection available, the session will be queued */
        /// <summary>
        /// 90 - specified pinned public key did not match
        /// </summary>
        CURLE_SSL_PINNEDPUBKEYNOTMATCH, /* 90 - specified pinned public key did not match */
        /// <summary>
        /// 91 - invalid certificate status
        /// </summary>
        CURLE_SSL_INVALIDCERTSTATUS,   /* 91 - invalid certificate status */
        /// <summary>
        /// NEVER USE (last in enum)
        /// </summary>
        CURL_LAST /* never use! */
    }
}
