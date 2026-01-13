using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ThisOldCurl.LibCurl;

using CURL = System.IntPtr; //CURL handle
using size_t = System.UInt32;
using curl_off_t = System.Int64;

namespace ThisOldCurl
{
    public delegate void EasyDataHandler(byte[] buffer);
    public delegate bool EasyOnRecvCallback(byte[] buffer);
    public delegate void EasyTransferCompleteHandler(EasyCurl curl);
    public delegate void EasyProgressHandler(
        curl_off_t dltotal,
        curl_off_t dlnow,
        curl_off_t ultotal,
        curl_off_t ulnow);

    public partial class EasyCurl : IDisposable
    {
        public event EasyDataHandler DebugEvent;
        public event EasyDataHandler WriteEvent;
        public event EasyDataHandler HeaderEvent;
        public event EasyProgressHandler ProgressEvent;

        public event EasyTransferCompleteHandler TransferCompleteEvent;

        private void assignDefaultCallbacks()
        {
            this.SetOpt(
                CURLoption.CURLOPT_DEBUGFUNCTION,
                new CurlDebugCallback(this.onDebugWrapper));
            this.SetOpt(
                CURLoption.CURLOPT_WRITEFUNCTION,
                new CurlWriteCallback(this.onWriteWrapper));
            this.SetOpt(
                CURLoption.CURLOPT_HEADERFUNCTION,
                new CurlWriteCallback(this.onHeaderWrapper));
            this.SetOpt(
                CURLoption.CURLOPT_XFERINFOFUNCTION,
                new CurlXferInfoCallback(this.onXferInfoWrapper));
            this.SetOpt(
                CURLoption.CURLOPT_READFUNCTION, 
                new CurlReadCallback(this.readCallback));
        }

        private size_t readCallback(
            IntPtr buffer, size_t size, size_t nmemb, IntPtr _user)
        {
            size_t max = size * nmemb;
            byte[] chunk = new byte[max];
            int read = this.uploadStream.Read(chunk, 0, (int)max);
            if (read > 0)
                Marshal.Copy(chunk, 0, buffer, read);
            return (uint)read;
        }

        private int onDebugWrapper(
            CURL handle,
            curl_infotype type,
            IntPtr data,
            size_t size,
            IntPtr userPtr)
        {
            if (size <= 0)
                return 0;
            byte[] buf = new byte[size];
            Marshal.Copy(data, buf, 0, (int)size);
            if (Array.IndexOf(printableTypes, type) >= 0)
            {
                string msg = Encoding.UTF8.GetString(buf);
                Console.Write("[libcurl] " + msg);
            }
            if (DebugEvent != null)
                DebugEvent.Invoke(buf);
            return 0;
        }

        private size_t onWriteWrapper(
            IntPtr buffer,
            size_t size,
            size_t nitems,
            IntPtr _user)
        {
            if (size <= 0)
                return 0;
            size_t len = size * nitems;
            if (this.downloadStream != null || this.WriteEvent != null)
            {
                byte[] chunk = new byte[len];
                Marshal.Copy(buffer, chunk, 0, (int)len);
                if (this.downloadStream != null)
                    this.downloadStream.Write(chunk, 0, (int)len);
                if (this.WriteEvent != null)
                    WriteEvent.Invoke(chunk);
            }
            if (this.pauseRequest)
                return (uint)CurlWriteCode.CURL_WRITEFUNC_PAUSE;
            return len;
        }

        private size_t onHeaderWrapper(
            IntPtr buffer,
            size_t size,
            size_t nitems,
            IntPtr _user)
        {
            if (size <= 0)
                return 0;
            size_t len = size * nitems;
            if (HeaderEvent != null)
            {
                byte[] chunk = new byte[len];
                Marshal.Copy(buffer, chunk, 0, (int)len);
                HeaderEvent.Invoke(chunk);
            }
            if (this.pauseRequest)
                return (uint)CurlWriteCode.CURL_WRITEFUNC_PAUSE;
            return len;
        }

        private int onXferInfoWrapper(
            IntPtr _user,
            curl_off_t dltotal,
            curl_off_t dlnow,
            curl_off_t ultotal,
            curl_off_t ulnow)
        {
            if (ProgressEvent != null)
                ProgressEvent.Invoke(dltotal, dlnow, ultotal, ulnow);
            return this.abortRequest ? 1 : 0;
        }

        internal void FireCompleteEvent()
        {
            if (this.TransferCompleteEvent != null)
                this.TransferCompleteEvent.Invoke(this);
        }
    }
}
