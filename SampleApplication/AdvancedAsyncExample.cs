using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ThisOldCurl;
using System.IO;
using System.Net;

namespace SampleApplication
{
    class AdvancedAsyncExample : Example
    {
        public AdvancedAsyncExample()
        {
            this.name = "Asynchronous POST Message";
            this.description = @"
Use the WebRequest async API to send a POST request with a JSON payload.
This is somewhat less tested, as I am less familiar with 
old C# async patterns.

Note how the ""Demo Complete"" message prints before the response does.
The calling thread continues, and the asynchronous callback runs later.
Press Y after the response has printed to continue.";
        }
        public override void Run()
        {
            ManualResetEvent done = new ManualResetEvent(false); // so app doesn't exit before we're done
            string postData = "{\"message\":\"hello\"}";
            CurlWebRequest request = new CurlWebRequest("https://httpbin.org/post");
            request.Method = "POST";
            request.ContentType = "application/json";
            request.BeginGetRequestStream(delegate(IAsyncResult requestResult)
            {
                try
                {
                    Stream requestStream = request.EndGetRequestStream(requestResult);
                    byte[] bytes = Encoding.UTF8.GetBytes(postData);
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Flush();
                    request.BeginGetResponse(delegate(IAsyncResult responseResult)
                    {
                        try
                        {
                            WebResponse postResponse = request.EndGetResponse(responseResult);
                            using (StreamReader postResponseReader = new StreamReader(postResponse.GetResponseStream()))
                            {
                                string msg = postResponseReader.ReadToEnd();
                                Console.WriteLine(msg);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error getting response: " + ex.Message);
                        }
                        finally
                        {
                            done.Set();
                        }
                    }, null);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error getting request stream: " + ex.Message);
                }
                finally
                {
                    done.Set();
                }
            }, null);

            done.WaitOne();
        }
    }
}
