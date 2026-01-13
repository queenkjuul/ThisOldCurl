using System;
using System.Collections.Generic;
using System.Text;

namespace ThisOldCurl.LibCurl
{
    /// <summary>
    /// Public API enums for RTSP requests
    /// </summary>
    public enum CurlRtspEq
    {
        CURL_RTSPREQ_NONE,
        CURL_RTSPREQ_OPTIONS,
        CURL_RTSPREQ_DESCRIBE,
        CURL_RTSPREQ_ANNOUNCE,
        CURL_RTSPREQ_SETUP,
        CURL_RTSPREQ_PLAY,
        CURL_RTSPREQ_PAUSE,
        CURL_RTSPREQ_TEARDOWN,
        CURL_RTSPREQ_GET_PARAMETER,
        CURL_RTSPREQ_SET_PARAMETER,
        CURL_RTSPREQ_RECORD,
        CURL_RTSPREQ_RECEIVE,
        CURL_RTSPREQ_LAST
    }
}
