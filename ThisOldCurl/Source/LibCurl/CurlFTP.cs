using System;
using System.Collections.Generic;
using System.Text;

namespace ThisOldCurl.LibCurl
{
    /// <summary>
    /// parameter for the CURLOPT_FTP_SSL_CCC option
    /// </summary>
    public enum curl_ftpccc
    {
        /// <summary>
        /// do not send CCC
        /// </summary>
        CURLFTPSSL_CCC_NONE,
        /// <summary>
        /// Let the server initiate the shutdown
        /// </summary>
        CURLFTPSSL_CCC_PASSIVE,
        /// <summary>
        /// Initiate the shutdown
        /// </summary>
        CURLFTPSSL_CCC_ACTIVE,
        /// <summary>
        /// not an option, never use
        /// </summary>
        CURLFTPSSL_CCC_LAST
    }

    /// <summary>
    /// parameter for the CURLOPT_FTPSSLAUTH option
    /// </summary>
    public enum curl_ftpauth
    {
        /// <summary>
        /// let libcurl decide
        /// </summary>
        CURLFTPAUTH_DEFAULT,
        /// <summary>
        /// use "AUTH SSL"
        /// </summary>
        CURLFTPAUTH_SSL,
        /// <summary>
        /// use "AUTH TLS"
        /// </summary>
        CURLFTPAUTH_TLS,
        /// <summary>
        /// not an option, never use
        /// </summary>
        CURLFTPAUTH_LAST
    }

    /// <summary>
    /// parameter for the CURLOPT_FTP_CREATE_MISSING_DIRS option
    /// </summary>
    public enum curl_ftpcreatedir
    {
        /// <summary>
        /// do NOT create missing dirs!
        /// </summary>
        CURLFTP_CREATE_DIR_NONE,
        /// <summary>
        /// (FTP/SFTP) if CWD fails, try MKD and then CWD
        /// again if MKD succeeded, for SFTP this does
        /// similar magic
        /// </summary>
        CURLFTP_CREATE_DIR,
        /// <summary>
        /// (FTP only) if CWD fails, try MKD and then CWD
        /// again even if MKD failed!
        /// </summary>
        CURLFTP_CREATE_DIR_RETRY,
        /// <summary>
        /// not an option, never use
        /// </summary>
        CURLFTP_CREATE_DIR_LAST
    }

    /// <summary>
    /// parameter for the CURLOPT_FTP_FILEMETHOD option
    /// </summary>
    public enum curl_ftpmethod
    {
        /// <summary>
        /// let libcurl pick
        /// </summary>
        CURLFTPMETHOD_DEFAULT,
        /// <summary>
        /// single CWD operation for each path part
        /// </summary>
        CURLFTPMETHOD_MULTICWD,
        /// <summary>
        /// no CWD at all
        /// </summary>
        CURLFTPMETHOD_NOCWD,
        /// <summary>
        /// one CWD to full dir, then work on file
        /// </summary>
        CURLFTPMETHOD_SINGLECWD,
        /// <summary>
        /// not an option, never use
        /// </summary>
        CURLFTPMETHOD_LAST
    }
}
