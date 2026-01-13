using System;
using System.Collections.Generic;
using System.Text;

namespace ThisOldCurl.LibCurl
{
    /// <summary>
    /// CURLPROTO_ defines are for the CURLOPT_*PROTOCOLS options
    /// </summary>
    [Flags]
    public enum CurlProtoFlags
    {
        /// <summary>
        /// HTTP protocol.
        /// </summary>
        CURLPROTO_HTTP = 1 << 0,
        /// <summary>
        /// HTTPS protocol.
        /// </summary>
        CURLPROTO_HTTPS = 1 << 1,
        /// <summary>
        /// FTP protocol.
        /// </summary>
        CURLPROTO_FTP = 1 << 2,
        /// <summary>
        /// FTPS protocol.
        /// </summary>
        CURLPROTO_FTPS = 1 << 3,
        /// <summary>
        /// SCP protocol.
        /// </summary>
        CURLPROTO_SCP = 1 << 4,
        /// <summary>
        /// SFTP protocol.
        /// </summary>
        CURLPROTO_SFTP = 1 << 5,
        /// <summary>
        /// Telnet protocol.
        /// </summary>
        CURLPROTO_TELNET = 1 << 6,
        /// <summary>
        /// LDAP protocol.
        /// </summary>
        CURLPROTO_LDAP = 1 << 7,
        /// <summary>
        /// LDAPS protocol.
        /// </summary>
        CURLPROTO_LDAPS = 1 << 8,
        /// <summary>
        /// DICT protocol.
        /// </summary>
        CURLPROTO_DICT = 1 << 9,
        /// <summary>
        /// FILE protocol.
        /// </summary>
        CURLPROTO_FILE = 1 << 10,
        /// <summary>
        /// TFTP protocol.
        /// </summary>
        CURLPROTO_TFTP = 1 << 11,
        /// <summary>
        /// IMAP protocol.
        /// </summary>
        CURLPROTO_IMAP = 1 << 12,
        /// <summary>
        /// IMAPS protocol.
        /// </summary>
        CURLPROTO_IMAPS = 1 << 13,
        /// <summary>
        /// POP3 protocol.
        /// </summary>
        CURLPROTO_POP3 = 1 << 14,
        /// <summary>
        /// POP3S protocol.
        /// </summary>
        CURLPROTO_POP3S = 1 << 15,
        /// <summary>
        /// SMTP protocol.
        /// </summary>
        CURLPROTO_SMTP = 1 << 16,
        /// <summary>
        /// SMTPS protocol.
        /// </summary>
        CURLPROTO_SMTPS = 1 << 17,
        /// <summary>
        /// RTSP protocol.
        /// </summary>
        CURLPROTO_RTSP = 1 << 18,
        /// <summary>
        /// RTMP protocol.
        /// </summary>
        CURLPROTO_RTMP = 1 << 19,
        /// <summary>
        /// RTMPT protocol.
        /// </summary>
        CURLPROTO_RTMPT = 1 << 20,
        /// <summary>
        /// RTMPE protocol.
        /// </summary>
        CURLPROTO_RTMPE = 1 << 21,
        /// <summary>
        /// RTMPTE protocol.
        /// </summary>
        CURLPROTO_RTMPTE = 1 << 22,
        /// <summary>
        /// RTMPS protocol.
        /// </summary>
        CURLPROTO_RTMPS = 1 << 23,
        /// <summary>
        /// RTMPTS protocol.
        /// </summary>
        CURLPROTO_RTMPTS = 1 << 24,
        /// <summary>
        /// Gopher protocol.
        /// </summary>
        CURLPROTO_GOPHER = 1 << 25,
        /// <summary>
        /// SMB protocol.
        /// </summary>
        CURLPROTO_SMB = 1 << 26,
        /// <summary>
        /// SMBS protocol.
        /// </summary>
        CURLPROTO_SMBS = 1 << 27,
        /// <summary>
        /// Enable all protocols.
        /// </summary>
        CURLPROTO_ALL = ~0
    }
}
