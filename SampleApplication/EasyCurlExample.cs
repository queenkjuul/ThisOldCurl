using System;
using System.Collections.Generic;
using System.Text;
using ThisOldCurl;
using System.IO;

namespace SampleApplication
{
    public class EasyCurlExample: Example
    {
        public EasyCurlExample()
        {
            this.name = "EasyCurl - The Easy Way";
            this.description = @"
This demo uses EasyCurl in its simplest form - assigning a Stream
to collect the response data (DownloadStream). EasyCurl exposes the 
full Curl ""easy API"" if you know what you're doing, but it also offers
simple C#-native Stream and Event interfaces for ease of use.";
        }
        public override void Run()
        {
            EasyCurl easyCurl = new EasyCurl(true); // optional bool DebugLogging
            easyCurl.URL = "https://api.queenkjuul.xyz/ds9";
            easyCurl.DownloadStream = new MemoryStream();
            easyCurl.TransferCompleteEvent += delegate(EasyCurl _completedEasyCurl)
            {
                Console.WriteLine("Transfer Complete!");
            };
            easyCurl.Perform();
            easyCurl.DownloadStream.Position = 0;
            string response = new StreamReader(easyCurl.DownloadStream).ReadToEnd();
            Console.WriteLine(response);
            easyCurl.Dispose();
        }
    }
}
