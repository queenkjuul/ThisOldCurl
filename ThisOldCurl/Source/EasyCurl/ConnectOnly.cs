using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ThisOldCurl.LibCurl;

using size_t = System.UInt32;
using System.IO;
using System.Threading;

namespace ThisOldCurl
{
    public partial class EasyCurl
    {
        /// <summary>
        /// This method will throw if CURLOPT_CONNECT_ONLY is not set.
        /// You must call Perform() before calling this method.
        /// Send the contents of the buffer to the socket.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int Send(byte[] buffer, int offset, int count)
        {
            if (!this.connectOnly || !this.performed)
                throw new InvalidOperationException("[EasyCurl] Cannot call Send unless handle is Connect-Only");
            UInt32 sent = 0;
            GCHandle sent_ = GCHandle.Alloc(sent, GCHandleType.Pinned);
            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                IntPtr ptr = new IntPtr(handle.AddrOfPinnedObject().ToInt32() + offset);
                CURLcode code = Curl.curl_easy_send(
                    this.curl,
                    ptr,
                    (size_t)count,
                    sent_.AddrOfPinnedObject());
                if (code == CURLcode.CURLE_AGAIN)
                    return 0;
                handleCurlCode(code);
                return Marshal.ReadInt32(sent_.AddrOfPinnedObject());
            }
            finally
            {
                sent_.Free();
                handle.Free();
            }
        }
        /// <summary>
        /// This method will throw if CURLOPT_CONNECTION_ONLY is not set.
        /// Continues sending data to the socket until 
        /// the entire buffer has been sent.
        /// </summary>
        /// <param name="buffer"></param>
        public void SendAll(byte[] buffer)
        {
            int offset = 0;
            while (offset < buffer.Length)
            {
                int sent = this.Send(buffer, offset, buffer.Length - offset);
                if (sent > 0)
                    offset += sent;
            }
        }

        /// <summary>
        /// This method will throw if CURLOPT_CONNECTION_ONLY is not set.
        /// You must call Perform() before calling this method.
        /// </summary>
        /// <param name="buffer">Buffer to fill with data from the socket</param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int Recv(byte[] buffer, int offset, int count)
        {
            if (!this.connectOnly || !this.performed)
                throw new InvalidOperationException("[EasyCurl] Cannot call Recv unless handle is Connect-Only");
            IntPtr recvd = Marshal.AllocHGlobal(sizeof(int));
            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                IntPtr ptr = new IntPtr(handle.AddrOfPinnedObject().ToInt32() + offset);
                CURLcode code = Curl.curl_easy_recv(
                    this.curl,
                    ptr,
                    (size_t)count,
                    recvd);
                if (code == CURLcode.CURLE_AGAIN)
                    return -1;
                handleCurlCode(code);
                return Marshal.ReadInt32(recvd);
            }
            finally
            {
                Marshal.FreeHGlobal(recvd);
                handle.Free();
            }
        }
        /// <summary>
        /// This method will throw if CURLOPT_CONNECT_ONLY is not set.
        /// This method will throw if Timeout, CURLOPT_TIMEOUT,
        /// or CURLOPT_TIMEOUT_MS is not set.
        /// You must call Perform() before calling this method.
        /// Continues listening until a timeout is reached
        /// or the provided callback returns false.
        /// </summary>
        /// <returns></returns>
        public byte[] RecvAll() { return this.RecvAll(null, 8192); }
        public byte[] RecvAll(EasyOnRecvCallback onRecv) { return this.RecvAll(onRecv, 8192); }
        public byte[] RecvAll(EasyOnRecvCallback onRecv, int bufferSize)
        {
            if (onRecv == null && this.timeout == null)
                throw new InvalidOperationException("[EasyCurl] Can't call RecvAll with no callback and no timeout");
            MemoryStream stream = new MemoryStream();
            byte[] buffer = new byte[bufferSize];
            DateTime startTime = DateTime.Now;
            while (true)
            {
                int recvd = this.Recv(buffer, 0, buffer.Length);
                if (recvd < 0)
                {
                    DateTime time = startTime.AddMilliseconds(Convert.ToDouble(this.timeout ?? 0));
                    Thread.Sleep(10);
                    if (this.timeout != null && DateTime.Now > time)
                        break;
                    continue;
                }
                if (recvd == 0)
                    break;
                stream.Write(buffer, 0, recvd);
                if (onRecv != null)
                {
                    byte[] chunk = new byte[recvd];
                    Array.Copy(buffer, 0, chunk, 0, recvd);
                    bool cont = onRecv(chunk);
                    if (!cont)
                        break;
                }
                startTime = DateTime.Now;
            }
            return stream.ToArray();
        }
    }
}
