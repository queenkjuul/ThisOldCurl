using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using curl_socket_t = System.Int32; // fd?
using curl_sockaddr_ = System.IntPtr; // pointer to curl_sockaddr

namespace ThisOldCurl.LibCurl
{
    public enum curlsocktype
    {
        /// <summary>
        /// socket created for a specific IP connectio
        /// </summary>
        CURLSOCKTYPE_IPCXN,
        /// <summary>
        /// socket created by accept() call
        /// </summary>
        CURLSOCKTYPE_ACCEPT,
        /// <summary>
        /// never use
        /// </summary>
        CURLSOCKTYPE_LAST
    }

    /// <summary>
    /// The return code from the sockopt_callback can signal information 
    /// to libcurl:
    /// </summary>
    public enum CurlSockoptCode
    {
        CURL_SOCKOPT_OK = 0,
        /// <summary>
        /// causes libcurl to abort and return CURLE_ABORTED_BY_CALLBACK
        /// </summary>
        CURL_SOCKOPT_ERROR = 1,
        CURL_SOCKOPT_ALREADY_CONNECTED = 2
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate CurlSockoptCode CurlSockoptCallback(
        IntPtr clientp,
        curl_socket_t curlfd,
        curlsocktype purpose);

    [StructLayout(LayoutKind.Sequential)]
    public struct sockaddr
    {
        public ushort sa_family;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
        public byte[] sa_data;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct curl_sockaddr
    {
        public int family;
        public int socktype;
        public int protocol;
        public UInt32 addrlen;
        public sockaddr addr;
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate curl_socket_t CurlOpenSocketCallback(
        IntPtr clientp,
        curlsocktype purpose,
        curl_sockaddr_ address);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int CurlCloseSocketCallback(
        IntPtr clientp,
        curl_socket_t item);
}
