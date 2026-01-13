using System;
using System.Collections.Generic;
using System.Text;
using ThisOldCurl;
using System.IO;

namespace SampleApplication
{
    public class CurlWebRequestExample: Example
    {
        public CurlWebRequestExample()
        {
            this.name = "CurlWebRequest - HttpWebRequest-like API";
            this.description = @"
CurlWebRequest is used mostly like an HttpWebRequest, with one 
major exception: CurlWebRequest does not have a ConnectStream,
so you must provide your own Stream in which to write the 
response data. 

If you do not provide a ResponseStream, a default MemoryStream
will be used, but be careful - on Windows 98, a small-by-modern-standards
file download could cause issues if written straight to memory.

In such a case, you'd do this instead:

    CurlWebRequest request = CurlWebRequest.Create(""https://example.com"");
    request.ResponseStream = new FileStream(""targetFile"");
    request.GetResponse();
";
        }
        public override void Run()
        {
            CurlWebRequest req = new CurlWebRequest("https://api.queenkjuul.xyz/ferengi");
            /*
             * you can customize the EasyCurl options with a config callback.
             * For example:
             * 
             * response.EasyConfig = 
             *      delegate(EasyCurl curl) 
             *      { 
             *          curl.SetOpt(CURLoption, value); 
             *      };
             * 
             */
            CurlWebResponse response = (CurlWebResponse)req.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string body = reader.ReadToEnd();
            Console.WriteLine(body);
        }
    }
}
