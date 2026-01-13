using System;
using System.Collections.Generic;
using System.Text;
using ThisOldCurl;
using System.IO;

namespace SampleApplication
{
    public class FormCurlExample : Example
    {
        public FormCurlExample()
        {
            this.name = "FormCurl Example - Send Multipart Form Data";
            this.description = @"
Uses the libcurl form-data utils to build a multipart/form-data request.
Note the ""form"" key in the response data.
(httpbin.org will return what you send)";
        }
        public override void Run()
        {
            EasyCurl formCurl = new EasyCurl(true);
            formCurl.URL = "https://httpbin.org/post";
            formCurl.Method ="POST";
            FormCurl form = new FormCurl();
            form.AddField("whatRocks", "ThisOldCurl");
            form.AddField("secondField", "secondValue");
            formCurl.Form = form;
            MemoryStream formResponseStream = new MemoryStream();
            formCurl.DownloadStream = formResponseStream;
            formCurl.Perform();
            Console.WriteLine(Encoding.UTF8.GetString(formResponseStream.ToArray()));
        }
    }
}
