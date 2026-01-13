using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using curl_socket_t = System.Int32; // fd?
using CURLM = System.IntPtr;    // pointer to a curl_multi handle
using CURL = System.IntPtr;     // pointer to a single curl handle
using fd_set_ = System.IntPtr;  // pointer to fd_set
using CURLMsg_ = System.IntPtr; // pointer to CURLMsg

namespace ThisOldCurl.LibCurl
{
    public enum CURLMcode
    {
        /// <summary>
        /// Please call curl_multi_perform() or curl_multi_socket*() soon.
        /// </summary>
        CURLM_CALL_MULTI_PERFORM = -1,
        /// <summary>
        /// The operation was successful.
        /// </summary>
        CURLM_OK,
        /// <summary>
        /// The passed-in handle is not a valid CURLM handle.
        /// </summary>
        CURLM_BAD_HANDLE,
        /// <summary>
        /// An easy handle was not good/valid.
        /// </summary>
        CURLM_BAD_EASY_HANDLE,
        /// <summary>
        /// There is not enough memory available.
        /// </summary>
        CURLM_OUT_OF_MEMORY,   /* if you ever get this, you're in deep sh*t */ /* editor's note: this is in the libcurl source */
        /// <summary>
        /// This indicates a bug in the libcurl library.
        /// </summary>
        CURLM_INTERNAL_ERROR,
        /// <summary>
        /// The passed-in socket argument did not match.
        /// </summary>
        CURLM_BAD_SOCKET,
        /// <summary>
        /// An unsupported option was used with curl_multi_setopt().
        /// </summary>
        CURLM_UNKNOWN_OPTION,
        /// <summary>
        /// The easy handle has already been added to a multi handle.
        /// </summary>
        CURLM_ADDED_ALREADY,
        /// <summary>
        /// Represents the last error code in this enumeration.
        /// </summary>
        CURLM_LAST
    }

    public enum CURLMoption
    {
        /// <summary>
        /// This is the socket callback function pointer
        /// </summary>
        CURLMOPT_SOCKETFUNCTION = 20000 + 1,
        /// <summary>
        /// This is the argument passed to the socket callback
        /// </summary>
        CURLMOPT_SOCKETDATA = 10000 + 2,
        /// <summary>
        /// set to 1 to enable pipelining for this multi handle
        /// </summary>
        CURLMOPT_PIPELINING = 0 + 3,
        /// <summary>
        /// This is the timer callback function pointer
        /// </summary>
        CURLMOPT_TIMERFUNCTION = 20000 + 4,
        /// <summary>
        /// This is the argument passed to the timer callback
        /// </summary>
        CURLMOPT_TIMERDATA = 10000 + 5,
        /// <summary>
        /// maximum number of entries in the connection cache
        /// </summary>
        CURLMOPT_MAXCONNECTS = 0 + 6,
        /// <summary>
        /// maximum number of (pipelining) connections to one host
        /// </summary>
        CURLMOPT_MAX_HOST_CONNECTIONS = 0 + 7,
        /// <summary>
        /// maximum number of requests in a pipeline
        /// </summary>
        CURLMOPT_MAX_PIPELINE_LENGTH = 0 + 8,
        /// <summary>
        /// a connection with a content-length longer than this
        /// will not be considered for pipelining
        /// </summary>
        CURLMOPT_CONTENT_LENGTH_PENALTY_SIZE = 30000 + 9,
        /// <summary>
        /// a connection with a chunk length longer than this 
        /// will not be considered for pipelining
        /// </summary>
        CURLMOPT_CHUNK_LENGTH_PENALTY_SIZE = 30000 + 10,
        /// <summary>
        /// a list of site names(+port) that are blacklisted from pipelining
        /// </summary>
        CURLMOPT_PIPELINING_SITE_BL = 10000 + 11,
        /// <summary>
        /// a list of server types that are blacklisted from pipelining
        /// </summary>
        CURLMOPT_PIPELINING_SERVER_BL = 10000 + 12,
        /// <summary>
        /// maximum number of open connections in total
        /// </summary>
        CURLMOPT_MAX_TOTAL_CONNECTIONS = 0 + 13,
        /// <summary>
        /// last, unused
        /// </summary>
        CURLMOPT_LASTENTRY
    }

    public enum CURLMSG
    {
        /// <summary>
        /// first, not used
        /// </summary>
        CURLMSG_NONE,
        /// <summary>
        /// This easy handle has completed. 'result' contains the CURLcode of the transfer
        /// </summary>
        CURLMSG_DONE,
        /// <summary>
        /// last, not used
        /// </summary>
        CURLMSG_LAST
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CURLMsg
    {
        /// <summary>
        /// what this message means
        /// </summary>
        public CURLMSG msg;
        /// <summary>
        /// handle the message concerns
        /// </summary>
        public IntPtr easy_handle;

        public DataUnion data;

        [StructLayout(LayoutKind.Explicit)]
        public struct DataUnion
        {
            /// <summary>
            /// message-specific data
            /// </summary>
            [FieldOffset(0)]
            public IntPtr whatever;

            /// <summary>
            /// return code for transfer
            /// </summary>
            [FieldOffset(0)]
            public CURLcode result;
        }
    }

    /// <summary>
    /// Based on poll(2) structure and values.
    /// We don't use pollfd and POLL* constants explicitly
    /// to cover platforms without poll(). */
    /// </summary>
    public enum CurlWaitPoll
    {
        CURL_WAIT_POLLIN = 0x0001,
        CURL_WAIT_POLLPRI = 0x0002,
        CURL_WAIT_POLLOUT = 0x0004
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct curl_waitfd
    {
        public IntPtr fd;
        public short events;
        /// <summary>
        /// NOT SUPPORTED
        /// </summary>
        public short revents;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct fd_set
    {
        /// <summary>
        /// number of sockets
        /// </summary>
        public uint fd_count;
        public IntPtr fd_array;
    }

    /// <summary>
    /// callback which receives the file descriptor for a socket.
    /// </summary>
    /// <param name="easy">
    /// easy handle (CURL*) */
    /// </param>
    /// <param name="s">
    /// socket (curl_socket_t) */
    /// </param>
    /// <param name="what">
    /// /* see above */
    /// </param>
    /// <param name="userp">
    /// private callback pointer */
    /// </param>
    /// <param name="socketp">
    /// private socket pointer */
    /// </param>
    /// <returns></returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int CurlSocketCallback(
        CURL easy,       
        curl_socket_t s,          
        int what,          
        IntPtr userp,      
        IntPtr socketp     
    );

    public enum CURLPoll
    {
        CURL_POLL_NONE,
        CURL_POLL_IN,
        CURL_POLL_OUT,
        CURL_POLL_INOUT,
        CURL_POLL_REMOVE
    }

    public enum CURLCSelect
    {
        CURL_CSELECT_IN = 0x01,
        CURL_CSELECT_OUT = 0x02,
        CURL_CSELECT_ERR = 0x04
    }

    /// <summary>
    /// Called by libcurl whenever the library detects a change in the
    /// maximum number of milliseconds the app is allowed to wait before
    /// curl_multi_socket() or curl_multi_perform() must be called
    /// (to allow libcurl's timed events to take place).
    /// </summary>
    /// <param name="multi">CURLM handle</param>
    /// <param name="timeout_ms"></param>
    /// <param name="userp"></param>
    /// <returns>
    /// The callback should return zero.
    /// </returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int CurlMultiTimerCallback(
        CURLM multi,
        Int32 timeout_ms,
        IntPtr userp);

    public static partial class Curl
    {
        /// <summary>
        /// inititalize multi-style curl usage
        /// </summary>
        /// <returns>
        /// a new CURLM handle to use in all 'curl_multi' functions.
        /// </returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLM curl_multi_init();

        /// <summary>
        /// add a standard curl handle to the multi stack
        /// </summary>
        /// <param name="multi_handle"></param>
        /// <param name="curl_handle"></param>
        /// <returns>
        /// CURLMcode type, general multi error code.
        /// </returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLMcode curl_multi_add_handle(CURLM multi_handle, CURL curl_handle);

        /// <summary>
        /// removes a curl handle from the multi stack again
        /// </summary>
        /// <param name="multi_handle">CURLM pointer to curl_multi handle</param>
        /// <param name="curl_handle">CURL pointer to single curl handle</param>
        /// <returns>
        /// CURLMcode type, general multi error code.
        /// </returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLMcode curl_multi_remove_handle(CURLM multi_handle, CURL curl_handle);

        /// <summary>
        /// Ask curl for its fd_set sets. The app can use these to select() or
        /// poll() on. We want curl_multi_perform() called as soon as one of
        /// them are ready.
        /// </summary>
        /// <param name="mutli_handle">CURLM handle</param>
        /// <param name="read_fd_set">pointer to an fd_set</param>
        /// <param name="write_fd_set">pointer to an fd_set</param>
        /// <param name="exc_fd_set">pointer to an fd_set</param>
        /// <param name="max_fd">pointer to an int</param>
        /// <returns>
        /// CURLMcode type, general multi error code.
        /// </returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLMcode curl_multi_fdset(
            CURLM mutli_handle,
            fd_set_ read_fd_set,
            fd_set_ write_fd_set,
            fd_set_ exc_fd_set,
            IntPtr max_fd);

        /// <summary>
        /// Poll on all fds within a CURLM set as well as any
        /// additional fds passed to the function. 
        /// </summary>
        /// <param name="multi_handle">CURLM handle</param>
        /// <param name="extra_fds"></param>
        /// <param name="extra_nfds"></param>
        /// <param name="timeout_ms"></param>
        /// <param name="ret">IntPtr to an int</param>
        /// <returns>
        /// CURLMcode type, general multi error code.
        /// </returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLMcode curl_multi_wait(
            CURLM multi_handle, 
            curl_waitfd[] extra_fds, 
            uint extra_nfds, 
            int timeout_ms, 
            IntPtr ret);

        /// <summary>
        /// When the app thinks there's data available for curl it calls this
        /// function to read/write whatever there is right now. This returns
        /// as soon as the reads and writes are done. This function does not
        /// require that there actually is data available for reading or that
        /// data can be written, it can be called just in case. It returns
        /// the number of handles that still transfer data in the second
        /// argument's integer-pointer.
        /// </summary>
        /// <param name="multi_handle">CURLM handle</param>
        /// <param name="running_handles">pointer to an int</param>
        /// <returns>
        /// CURLMcode type, general multi error code. *NOTE* that this only
        /// returns errors etc regarding the whole multi stack. There might
        /// still have occurred problems on invidual transfers even when this
        /// returns OK.
        /// </returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLMcode curl_multi_perform(CURLM multi_handle, ref int running_handles);

        /// <summary>
        /// Cleans up and removes a whole multi stack. It does not free or
        /// touch any individual easy handles in any way. We need to define
        /// in what state those handles will be if this function is called
        /// in the middle of a transfer.
        /// </summary>
        /// <param name="multi_handle">CURLM handle</param>
        /// <returns>
        /// CURLMcode type, general multi error code.
        /// </returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLMcode curl_multi_cleanup(CURLM multi_handle);

        /// <summary>
        /// Ask the multi handle if there's any messages/informationals from
        /// the individual transfers. Messages include informationals such as
        /// error code from the transfer or just the fact that a transfer is
        /// completed. More details on these should be written down as well.
        /// 
        /// Repeated calls to this function will return a new struct each
        /// time, until a special "end of msgs" struct is returned as a signal
        /// that there is no more to get at this point.
        /// 
        /// The data the returned pointer points to will not survive calling
        /// curl_multi_cleanup().
        ///         
        /// The 'CURLMsg' struct is meant to be very simple and only contain
        /// very basic informations. If more involved information is wanted,
        /// we will provide the particular "transfer handle" in that struct
        /// and that should/could/would be used in subsequent
        /// curl_easy_getinfo() calls (or similar). The point being that we
        /// must never expose complex structs to applications, as then we'll
        /// undoubtably get backwards compatibility problems in the future.
        /// </summary>
        /// <param name="multi_handle">CURLM handle</param>
        /// <param name="msgs_in_queue">pointer to an int</param>
        /// <returns>
        /// A pointer to a filled-in struct, or NULL if it failed or ran out
        /// of structs. It also writes the number of messages left in the
        /// queue (after this read) in the integer the second argument points to.
        /// </returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLMsg_ curl_multi_info_read(CURLM multi_handle, out int msgs_in_queue);


        /// <summary>
        /// The curl_multi_strerror function may be used to turn a 
        /// value into the equivalent human readable error string.  
        /// This useful for printing meaningful error messages.
        /// </summary>
        /// <param name="code">CURLMcode</param>
        /// <returns>
        /// A pointer to a zero-terminated error message.
        /// </returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern string curl_multi_strerror(CURLMcode code);

        /// <summary>
        /// An alternative version of curl_multi_perform() that allows the
        /// application to pass in one of the file descriptors that have been
        /// detected to have "action" on them and let libcurl perform.
        /// See man page for details.
        /// </summary>
        /// <param name="multi_handle">CURLM handle</param>
        /// <param name="s"></param>
        /// <param name="running_handles">pointer to an int</param>
        /// <returns></returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLMcode curl_mutli_socket(
            CURLM multi_handle,
            curl_socket_t s,
            IntPtr running_handles);

        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLMcode curl_multi_socket_action(
            CURLM multi_handle,
            curl_socket_t s,
            int ev_bitmask,
            IntPtr running_handles);
        
        /// <summary>
        /// An alternative version of curl_multi_perform() that allows the
        /// application to pass in one of the file descriptors that have been
        /// detected to have "action" on them and let libcurl perform.
        /// See man page for details.
        /// </summary>
        /// <param name="multi_handle">CURLM handle</param>
        /// <param name="running_handles">pointer to an int</param>
        /// <returns></returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLMcode curl_multi_socket_all(
            CURLM multi_handle,
            IntPtr running_handles);

        /// <summary>
        /// Returns the maximum number of milliseconds the app is allowed to
        /// wait before curl_multi_socket() or curl_multi_perform() must be
        /// called (to allow libcurl's timed events to take place).
        /// </summary>
        /// <param name="multi_handle">CURLM handle</param>
        /// <param name="milliseconds">pointer to a long (Int32)</param>
        /// <returns>CURLMcode error code</returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLMcode curl_multi_timeout(CURLM multi_handle, IntPtr milliseconds);

        /// <summary>
        /// Sets options for the multi handle.
        /// </summary>
        /// <param name="multi_handle">CURLM handle</param>
        /// <param name="option"></param>
        /// <returns>
        /// CURLM error code.
        /// </returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLMcode curl_multi_setopt(CURLM multi_handle, CURLMoption option);

        /// <summary>
        /// This function sets an association in the multi handle 
        /// between the given socket and a private pointer of the application. 
        /// This is (only) useful for curl_multi_socket uses.
        /// </summary>
        /// <param name="multi_handle">
        /// CURLM handle
        /// </param>
        /// <param name="sockfd"></param>
        /// <param name="sockp"></param>
        /// <returns>
        /// CURLMcode error code.
        /// </returns>
        [DllImport(CURLDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern CURLMcode curl_multi_assign(
            CURLM multi_handle,
            curl_socket_t sockfd,
            IntPtr sockp);
    }
}
