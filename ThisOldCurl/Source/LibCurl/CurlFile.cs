using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using time_t = System.Int64;
using curl_off_t = System.Int64;

namespace ThisOldCurl.LibCurl
{
    /// <summary>
    /// enumeration of file types
    /// </summary>
    public enum curlfiletype
    {
        CURLFILETYPE_FILE = 0,
        CURLFILETYPE_DIRECTORY,
        CURLFILETYPE_SYMLINK,
        CURLFILETYPE_DEVICE_BLOCK,
        CURLFILETYPE_DEVICE_CHAR,
        CURLFILETYPE_NAMEDPIPE,
        CURLFILETYPE_SOCKET,
        /// <summary>
        /// is only possible on Sun Solaris now
        /// </summary>
        CURLFILETYPE_DOOR,
        /// <summary>
        /// should never happen!
        /// </summary>
        CURLFILETYPE_UNKNOWN
    }

    [Flags]
    public enum CurlFileInfoFlag : uint
    {
        CURLINFOFLAG_KNOWN_FILENAME = 1 << 0,
        CURLINFOFLAG_KNOWN_FILETYPE = 1 << 1,
        CURLINFOFLAG_KNOWN_TIME = 1 << 2,
        CURLINFOFLAG_KNOWN_PERM = 1 << 3,
        CURLINFOFLAG_KNOWN_UID = 1 << 4,
        CURLINFOFLAG_KNOWN_GID = 1 << 5,
        CURLINFOFLAG_KNOWN_SIZE = 1 << 6,
        CURLINFOFLAG_KNOWN_HLINKCOUNT = 1 << 7
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct curl_fileinfo_strings
    {
        /// <summary>
        /// If some of these fields is not NULL, it is a pointer to b_data.
        /// </summary>
        public IntPtr time;
        public IntPtr perm;
        public IntPtr user;
        public IntPtr group;
        /// <summary>
        /// target filename of a symlink
        /// </summary>
        public IntPtr target;
    }

    /// <summary>
    /// Content of this structure depends on information which is known and 
    /// achievable (e.g. by FTP LIST parsing). Please see the url_easy_setopt(3) 
    /// page for callbacks returning this structure -- some fields are mandatory
    /// some others are optional. The FLAG field has special meaning.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct curl_fileinfo
    {
        /// <summary>
        /// pointer to string (filename)
        /// </summary>
        public IntPtr filename;
        public curlfiletype filetype;
        public time_t time;
        public uint perm;
        public int uid;
        public int gid;
        public curl_off_t size;
        public UInt64 hardlinks;
        public curl_fileinfo_strings strings;
        public CurlFileInfoFlag flags;
        /// <summary>
        /// used internally
        /// </summary>
        public IntPtr b_data;
        /// <summary>
        /// used internally
        /// </summary>
        public UIntPtr b_size;
        /// <summary>
        /// used internally
        /// </summary>
        public UIntPtr b_used;
    }



}
