using System;
using System.Collections.Generic;
using System.Text;
using ThisOldCurl;
using ThisOldCurl.LibCurl;
using System.Runtime.InteropServices;

namespace SampleApplication
{
    public class SharedCurlExample: Example
    {
        public SharedCurlExample()
        {
            this.name = "SharedCurl - Share resources between EasyCurls";
            this.description = @"
SharedCurl is used to tell libcurl to share certain resources between several
different EasyCurl transfers. These are things like DNS cache, SSL context,
and ""cookie jars."" 

This example uses a shared cookie jar. 
Note the second transfer has the cookies from the first.";
        }
        public override void Run()
        {
            // print each only once
            bool lockCalled = false;
            bool unlockCalled = false;
            using (SharedCurl shared = new SharedCurl())
            {
                shared.SetOpt(
                    CURLSHoption.CURLSHOPT_LOCKFUNC,
                    new CurlLockCallback(delegate(
                        IntPtr share_handle,
                        curl_lock_data data,
                        curl_lock_access locktype,
                        IntPtr user)
                    {
                        if (lockCalled)
                            return;
                        lockCalled = true;
                        Console.WriteLine("Lock Requested: " + data);
                    }));
                shared.SetOpt(
                    CURLSHoption.CURLSHOPT_UNLOCKFUNC,
                    new CurlUnlockCallback(delegate(
                        IntPtr share_handle,
                        curl_lock_data data,
                        IntPtr user)
                        {
                            if (unlockCalled)
                                return;
                            unlockCalled = true;
                            Console.WriteLine("Unlock Requested: " + data);
                        }));
                shared.SetOpt(CURLSHoption.CURLSHOPT_SHARE, curl_lock_data.CURL_LOCK_DATA_COOKIE);
                shared.SetOpt(CURLSHoption.CURLSHOPT_SHARE, curl_lock_data.CURL_LOCK_DATA_DNS);
                shared.SetOpt(CURLSHoption.CURLSHOPT_SHARE, curl_lock_data.CURL_LOCK_DATA_SSL_SESSION);
                using (EasyCurl curl1 = new EasyCurl(true))
                using (EasyCurl curl2 = new EasyCurl(true))
                {
                    curl1.SetOpt(CURLoption.CURLOPT_URL, "https://httpbin.org/cookies/set?name1=value1");
                    curl1.SetOpt(CURLoption.CURLOPT_SHARE, shared.Handle);
                    curl1.Perform();

                    curl2.SetOpt(CURLoption.CURLOPT_URL, "https://httpbin.org/cookies");
                    curl2.SetOpt(CURLoption.CURLOPT_SHARE, shared.Handle);
                    curl2.SetOpt(
                        CURLoption.CURLOPT_WRITEFUNCTION,
                        new CurlWriteCallback(delegate(IntPtr data, UInt32 size, UInt32 nitems, IntPtr outstream)
                        {
                            UInt32 len = size * nitems;
                            byte[] buffer = new byte[len];
                            Marshal.Copy(data, buffer, 0, (int)len);
                            Console.WriteLine("curl2: " + Encoding.UTF8.GetString(buffer));
                            return len;
                        }));
                    curl2.Perform();
                }
            }            
        }
    }
}
