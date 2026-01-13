using System;
using System.Collections.Generic;
using System.Text;
using ThisOldCurl.LibCurl;

namespace ThisOldCurl
{
    public class FormCurl : IDisposable
    {
        internal IntPtr First;
        internal IntPtr Last;

        public CURLFORMcode AddField(string name, string value)
        {
            return Curl.curl_formadd(
                ref First, 
                ref Last, 
                CURLformoption.CURLFORM_COPYNAME, 
                name, 
                CURLformoption.CURLFORM_COPYCONTENTS, 
                value, 
                CURLformoption.CURLFORM_END);
        }
        public CURLFORMcode AddFile(string name, string filePath, string contentType)
        {
            return Curl.curl_formadd(
                ref First, 
                ref Last, 
                CURLformoption.CURLFORM_COPYNAME, 
                name, 
                CURLformoption.CURLFORM_FILE, 
                filePath, 
                CURLformoption.CURLFORM_CONTENTTYPE, 
                contentType, 
                CURLformoption.CURLFORM_END);
        }
        public void Dispose()
        {
            if (First != IntPtr.Zero)
            {
                Curl.curl_formfree(First);
                First = Last = IntPtr.Zero;
            }
        }
    }
}
