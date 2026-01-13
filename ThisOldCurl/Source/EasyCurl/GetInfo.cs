using System;
using System.Collections.Generic;
using System.Text;
using ThisOldCurl.LibCurl;
using System.Runtime.InteropServices;

namespace ThisOldCurl
{
    public partial class EasyCurl
    {
        private object getInfoValue(CURLINFO property)
        {
            notDisposed();
            if (!this.performed)
                throw new InvalidOperationException("[EasyCurl] Cannot call GetInfo before Perform()");

            int type = (int)property & CurlInfoConstants.CURLINFO_TYPEMASK;
            switch (type)
            {
                case CurlInfoConstants.CURLINFO_LONG:
                    IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
                    handleCurlCode(Curl.curl_easy_getinfo(curl, property, ptr));
                    int value = Marshal.ReadInt32(ptr);
                    Marshal.FreeHGlobal(ptr);
                    return value;
                case CurlInfoConstants.CURLINFO_DOUBLE:
                    double val = 0;
                    handleCurlCode(Curl.curl_easy_getinfo(curl, property, ref val));
                    return val;
                case CurlInfoConstants.CURLINFO_STRING:
                    IntPtr str_ = IntPtr.Zero;
                    handleCurlCode(Curl.curl_easy_getinfo(curl, property, ref str_));
                    if (str_ == IntPtr.Zero)
                        return null;
                    string str = Marshal.PtrToStringAnsi(str_);
                    return str;
                case CurlInfoConstants.CURLINFO_SLIST:
                    List<string> result = new List<string>();
                    IntPtr current = IntPtr.Zero;
                    Curl.curl_easy_getinfo(curl, property, ref current);
                    while (current != IntPtr.Zero)
                    {
                        IntPtr data = Marshal.ReadIntPtr(current);
                        string str2 = Marshal.PtrToStringAnsi(data);
                        result.Add(str2);
                        current = Marshal.ReadIntPtr(current, IntPtr.Size);
                    }
                    return result;
                default:
                    Console.WriteLine("Invalid Property: " + property);
                    throw new ArgumentException("[libcurl] [ERROR] Invalid CURLINFO property: " + property);
            }
        }
        private void captureInfo()
        {
            foreach (CURLINFO field in Enum.GetValues(typeof(CURLINFO)))
            {
                CURLINFO property = (CURLINFO)field;
                if (property == CURLINFO.CURLINFO_LASTONE || property == CURLINFO.CURLINFO_NONE)
                    return;
                this.Info.Add(property, this.getInfoValue(property));
            }
        }
        /// <summary>
        /// Get information about a completed transfer.
        /// Cannot be called before a transfer has been performed.
        /// Alternatively, can use EasyCurl.Info.Get(CURLINFO)
        /// </summary>
        /// <param name="property">property to get</param>
        /// <returns>"string | List<string> | int | double"</returns>
        public object GetInfo(CURLINFO property)
        {
            return this.getInfoValue(property);
        }
    }
}
