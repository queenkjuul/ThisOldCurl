using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ThisOldCurl.LibCurl
{
    /// <summary>
    /// return codes for CURLOPT_CHUNK_BGN_FUNCTION 
    /// </summary>
    public enum CurlChunkBgnCode : int
    {
        CURL_CHUNK_BGN_FUNC_OK = 0,
        /// <summary>
        /// tell lib to end task
        /// </summary>
        CURL_CHUNK_BGN_FUNC_FAIL = 1,
        /// <summary>
        /// skip this chunk
        /// </summary>
        CURL_CHUNK_BGN_FUNC_SKIP = 2
    }

    /// <summary>
    /// if splitting of data transfer is enabled, this callback is called before
    /// download of an individual chunk started. Note that parameter "remains" works
    /// only for FTP wildcard downloading (for now), otherwise is not used
    /// </summary>
    /// <param name="transfer_info"></param>
    /// <param name="ptr"></param>
    /// <param name="remains"></param>
    /// <returns></returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate CurlChunkBgnCode CurlChunkBgnCallback(
        IntPtr transfer_info,
        IntPtr ptr,
        int remains);

    /// <summary>
    /// return codes for CURLOPT_CHUNK_END_FUNCTION
    /// </summary>
    public enum CurlChunkEndCode : int
    {
        CURL_CHUNK_END_FUNC_OK = 0,
        /// <summary>
        /// end the task
        /// </summary>
        CURL_CHUNK_END_FUNC_FAIL = 1
    }

    /// <summary>
    /// If splitting of data transfer is enabled this callback is called after
    /// download of an individual chunk finished.
    /// Note! After this callback was set then it have to be called FOR ALL chunks.
    /// Even if downloading of this chunk was skipped in CHUNK_BGN_FUNC.
    /// This is the reason why we don't need "transfer_info" parameter in this
    /// callback and we are not interested in "remains" parameter too.
    /// </summary>
    /// <param name="ptr"></param>
    /// <returns></returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate CurlChunkEndCode CurlChunkEndCallback(IntPtr ptr);
}
