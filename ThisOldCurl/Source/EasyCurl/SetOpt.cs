using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Net;
using ThisOldCurl.LibCurl;

using CURLSH = System.IntPtr; //CURLSH share handle
using curl_off_t = System.Int64;

namespace ThisOldCurl
{
    public partial class EasyCurl
    {
        private string optionString(CURLoption option)
        {
            switch ((int)option / 10000)
            {
                case 0:
                    return "int";
                case 1:
                    return "IntPtr/string";
                case 2:
                    return "IntPtr (callback)";
                case 3:
                    return "Int64/long";
                default:
                    throw new ArgumentOutOfRangeException("[MultiCurl] Provided option didn't map cleanly?");
            }
        }

        /// <summary>
        /// Set a CURLoption for the transfer handled by this EasyCurl instance.
        /// Overloads exist for all possible values; 
        /// values are type-checked against their option flag
        /// 
        /// If you call SetOpt(CURLOPT_READFUNCTION, x) 
        /// [or WRITEFUNCTION, HEADERFUNCTION, DEBUGFUNCTION, etc) 
        /// where x is a valid function pointer, 
        /// you will disable all default behavior for that action,
        /// and events will not be emitted. You can use this for 
        /// low-level flow control of libcurl, should you want to directly
        /// control your own transfers (look at Send/Recv/CONNECT_ONLY).
        /// 
        /// EVENTS, UPLOAD AND DOWNLOAD STREAMS, GETINFO, AND MORE,
        /// COULD ALL BE BROKEN BY MANUALLY SETTING LIBCURL CALLBACKS.
        /// IF YOU ARE MANUALLY SETTING THE MAIN CALLBACKS, EITHER
        /// HANDLE ALL CALLBACKS YOURSELF OR VERIFY YOU ARE NOT RELYING
        /// ON BROKEN FUNCTIONALITY.
        /// </summary>
        /// <param name="option">CURLoption option</param>
        /// <param name="value">int / Int64 / string / IntPtr / Delegate</param>
        /// <returns>0 unless it throws</returns>
        public CURLcode SetOpt(CURLoption option, int value)
        {
            this.notDisposed();
            if ((int)option / 10000 != 0)
                throw new ArgumentException(
                    "[EasyCurl] Wrong type for option! Received int, expected " 
                    + optionString(option));
            if (option == CURLoption.CURLOPT_CONNECT_ONLY && value == 1)
                this.connectOnly = true;
            if (option == CURLoption.CURLOPT_CONNECT_ONLY && value == 0)
                this.connectOnly = false;
            if (option == CURLoption.CURLOPT_TIMEOUT)
                this.timeout = value * 1000;
            if (option == CURLoption.CURLOPT_TIMEOUT_MS)
                this.timeout = value;
            return 
                handleCurlCode(Curl.curl_easy_setopt(this.curl, option, value));
        }
        public CURLcode SetOpt(CURLoption option, curl_off_t value)
        {
            this.notDisposed();
            if ((int)option / 10000 != 3)
                throw new ArgumentException(
                    "[EasyCurl] Wrong type for option! Received Int64, expected " 
                    + optionString(option));
            return handleCurlCode(
                Curl.curl_easy_setopt(this.curl, option, value));
        }
        public CURLcode SetOpt(CURLoption option, string value)
        {
            this.notDisposed();
            if ((int)option / 10000 != 1)
                throw new ArgumentException("[EasyCurl] Wrong type for option! Received string, expected " + optionString(option));
            return 
                handleCurlCode(Curl.curl_easy_setopt(this.curl, option, value));
        }
        public CURLcode SetOpt(CURLoption option, IntPtr value)
        {
            this.notDisposed();
            if ((int)option / 10000 != 1)
                throw new ArgumentException("[EasyCurl] Wrong type for option! Received IntPtr, expected " + optionString(option));
            CURLcode result;
            result = handleCurlCode(
                Curl.curl_easy_setopt(this.curl, option, value));
            return result;
        }
        public CURLcode SetOpt(CURLoption option, Delegate callback)
        {
            this.notDisposed();
            if ((int)option / 10000 != 2)
                throw new ArgumentException("[EasyCurl] Wrong type for option! Received Delegate, expected " + optionString(option));
            IntPtr callbackPointer = Marshal.GetFunctionPointerForDelegate(callback);
            this.callbacks.Add(callback);
            return handleCurlCode(Curl.curl_easy_setopt(this.curl, option, callbackPointer));
        }
    }
}
