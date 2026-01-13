using System;
using System.Collections.Generic;
using System.Text;
using ThisOldCurl;

namespace SampleApplication
{
    public class SuperEasyCurlExample: Example
    {
        public SuperEasyCurlExample()
        {
            this.name = "SuperEasyCurl - One-Line requests";
            this.description = @"
SuperEasyCurl is the simplest way to make web requests, by far.
You need only to provide a URL, and get back either bytes or a string.
You can optionally provide a request body and a WebHeaderCollection.

Get() and Post() return strings. Set SuperEasyCurl.Encoding to use
a different charset (default UTF8).

GetBytes() and PostBytes() return byte[] instead.

Request() is the core of the class, and deals only in byte[] in and out.

This demo will run one line:

Console.WriteLine(SuperEasyCurl.Post(""https://httpbin.org/post"", ""ThisOldCurl""));
";
        }
        public override void Run()
        {
            Console.WriteLine(
                SuperEasyCurl.Post("https://httpbin.org/post", "ThisOldCurl"));           
        }
    }
}
