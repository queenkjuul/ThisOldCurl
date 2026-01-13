using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using CURL = System.IntPtr; // CURL handle

namespace ThisOldCurl.LibCurl
{
    public enum curlioerr
    {
        /// <summary>
        /// I/O operation successful
        /// </summary>
        CURLIOE_OK,
        /// <summary>
        /// command was unknown to callback
        /// </summary>
        CURLIOE_UNKNOWNCMD,
        /// <summary>
        /// failed to restart the read
        /// </summary>
        CURLIOE_FAILRESTART,
        /// <summary>
        /// NEVER USE (last in enum)
        /// </summary>
        CURLIOE_LAST
    }

    public enum curliocmd
    {
        /// <summary>
        /// no operation
        /// </summary>
        CURLIOCMD_NOP,
        /// <summary>
        /// restart th read stream from the start
        /// </summary>
        CURLIOCMD_RESTARTREAD,
        /// <summary>
        /// NEVER USE (last in enum)
        /// </summary>
        CURLIOCMD_LAST
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate curlioerr CurlIoctlCallback(CURL handle, curliocmd cmd, IntPtr clientp);
}
