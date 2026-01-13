using System;
using System.Collections.Generic;
using System.Text;
using ThisOldCurl;
using System.IO;

namespace SampleApplication
{
    public class MultiCurlExample : Example
    {
        public MultiCurlExample()
        {
            this.name = "MultiCurl - Run multiple transfers on one thread";
            this.description = @"
This demo will run two transfers at once - one big one, and one small one.
Thanks to the ThisOldCurl events system, we can very easily print the contents
of the small transfer only after the big transfer completes.";
        }
        public override void Run()
        {
            using (MultiCurl multi = new MultiCurl())
            {
                EasyCurl transfer1 = new EasyCurl();
                transfer1.URL ="https://dn721909.ca.archive.org/0/items/bee-movie-2007_202405/Bee%20Movie%20Script_djvu.txt";
                transfer1.WriteEvent +=
                    delegate(byte[] data) { Console.WriteLine(Encoding.UTF8.GetString(data)); };
                EasyCurl transfer2 = new EasyCurl();
                transfer2.URL = "https://api.queenkjuul.xyz/ds9";
                MemoryStream results2 = new MemoryStream();
                transfer2.DownloadStream = results2;
                transfer1.TransferCompleteEvent +=
                    delegate(EasyCurl _curl)
                    {
                        Console.WriteLine("Big Transfer Complete");
                    };
                transfer2.TransferCompleteEvent +=
                    delegate(EasyCurl _curl)
                    {
                        Console.WriteLine("Small Transfer Complete");
                    };
                multi.Add(transfer1);
                multi.Add(transfer2);
                multi.AllTransfersComplete +=
                    delegate(List<EasyCurl> results)
                    {
                        Console.WriteLine("All Transfers complete!");
                        Console.WriteLine("Contents of small transfer response:");
                        transfer2.DownloadStream.Position = 0;
                        Console.WriteLine(
                            new StreamReader(transfer2.DownloadStream).ReadToEnd());
                    };
                multi.PerformAll();
                multi.Dispose();
            }
        }
    }
}
