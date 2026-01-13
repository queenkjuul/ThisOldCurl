using System;
using System.Collections.Generic;
using System.Text;
using ThisOldCurl;

namespace SampleApplication
{
    public class AdvancedMultiCurlExample: Example
    {
        public AdvancedMultiCurlExample()
        {
            this.name = "Advanced MultiCurl - Manual Perform()/Wait() Loop";
            this.description = @"
You can use MultiCurl.Perform() and MultiCurl.Wait() to implement
a manual read loop for a MultiCurl collection. This lets you drive 
libcurl directly, like you would do in C.";
        }
        public override void Run()
        {
            using (MultiCurl multi = new MultiCurl())
            {
                EasyCurl transfer1 = new EasyCurl(true);
                transfer1.URL = "https://api.queenkjuul.xyz/zerofile?size=102400";
                EasyCurl transfer2 = new EasyCurl(true);
                transfer2.URL = "https://api.queenkjuul.xyz/zerofile";
                multi.Add(transfer1);
                multi.Add(transfer2);
                int running = multi.Perform();
                do
                {
                    multi.Wait();
                    running = multi.Perform();
                } while (running > 0);
                multi.Remove(transfer1);
                multi.Remove(transfer2);
                transfer1.Dispose();
                transfer2.Dispose();
                multi.Dispose();
            }
        }
    }
}
