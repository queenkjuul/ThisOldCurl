using System;
using System.Collections.Generic;
using System.Text;
using ThisOldCurl;
using ThisOldCurl.LibCurl;

namespace SampleApplication
{
    public class AdvancedConnectOnlyExample: Example
    {
        public AdvancedConnectOnlyExample()
        {
            this.name = "CONNECT_ONLY - Access the socket directly";
            this.description = @"
This is a small sampling of the power of libcurl. By setting CONNECT_ONLY = 1
we can access the underlying network socket directly using Send()/Recv().
This gives you total control over the protocol implementation.
";
        }
        public override void Run()
        {
            EasyCurl curl = new EasyCurl(true);
            curl.SetOpt(CURLoption.CURLOPT_URL, "https://httpbin.org/post");
            curl.SetOpt(CURLoption.CURLOPT_CONNECT_ONLY, 1);
            curl.SetOpt(CURLoption.CURLOPT_TIMEOUT, 2);
            curl.Perform();
            string content = "POST /post HTTP/1.1\r\nHost: httpbin.org\r\nContent-Type: text/plain\r\nContent-Length: 18\r\n\r\nThisOldCurl Rocks!";
            byte[] requestBytes = Encoding.ASCII.GetBytes(content);
            curl.SendAll(requestBytes);
            byte[] responseBytes = curl.RecvAll();
            string res = Encoding.ASCII.GetString(responseBytes);
            Console.WriteLine(res);
        }

    }
}
