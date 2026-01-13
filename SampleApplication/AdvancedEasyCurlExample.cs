using System;
using System.Collections.Generic;
using System.Text;
using ThisOldCurl;
using System.Runtime.InteropServices;
using ThisOldCurl.LibCurl;

namespace SampleApplication
{
    class AdvancedEasyCurlExample : Example
    {
        public AdvancedEasyCurlExample()
        {
            this.name = "Advanced EasyCurl - Overriding Default Callbacks";
            this.description = @"
This is not advised, but it demonstrates the low-level access provided by
EasyCurl. 

Normally, EasyCurl attaches a CurlWriteCallback which copies data
to DownloadStream (if provided).

Here, we instead attach our own CurlWriteCallback - this disables
DownloadStream as well as WriteEvent, so we need to manually copy
the data out of unmanaged libcurl memory and into managed C# memory.";
        }
        public override void Run()
        {
            EasyCurl easyCurl = new EasyCurl(true); // debug = true
            CurlWriteCallback cb = delegate(IntPtr data, UInt32 size, UInt32 nitems, IntPtr outstream)
            {
                UInt32 bytes = size * nitems;
                byte[] buffer = new byte[bytes];
                Marshal.Copy(data, buffer, 0, (int)bytes);
                Console.WriteLine(Encoding.ASCII.GetString(buffer));
                return bytes;
            };
            easyCurl.SetOpt(CURLoption.CURLOPT_URL, "https://api.queenkjuul.xyz/ferengi");
            easyCurl.SetOpt(CURLoption.CURLOPT_WRITEFUNCTION, cb);
            easyCurl.Perform();
            easyCurl.Dispose();
        }
    }
}
