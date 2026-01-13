using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net;
using System.IO;

namespace ThisOldCurl
{
    public abstract class CurlAsyncResult : IAsyncResult
    {
        private ManualResetEvent asyncWaitHandle;
        private bool isCompleted;
        private bool completedSynchronously;
        private object asyncState;
        private Exception error;

        protected CurlAsyncResult(object state)
        {
            this.asyncState = state;
            this.asyncWaitHandle = new ManualResetEvent(false);
            this.isCompleted = false;
            this.completedSynchronously = false;
        }

        public object AsyncState
        {
            get { return asyncState; }
        }

        public WaitHandle AsyncWaitHandle
        {
            get { return asyncWaitHandle; }
        }

        public bool CompletedSynchronously
        {
            get { return completedSynchronously; }
            protected set { completedSynchronously = value; }
        }

        public bool IsCompleted
        {
            get { return isCompleted; }
            protected set { isCompleted = value; }
        }

        public Exception Error
        {
            get { return this.error; }
            protected set { this.error = value; }
        }

        protected void SignalCompleted()
        {
            this.IsCompleted = true;
            asyncWaitHandle.Set();
        }

        public void SetFailed(Exception ex)
        {
            this.Error = ex;
            SignalCompleted();
        }
    }

    public class CurlResponseAsyncResult : CurlAsyncResult
    {
        private WebResponse response;
        public WebResponse Response { get { return this.response; } }

        public CurlResponseAsyncResult(object state)
            : base(state)
        {
        }

        public void SetCompleted(WebResponse response, bool completedSync)
        {
            this.response = response;
            this.CompletedSynchronously = completedSync;
            SignalCompleted();
        }
    }

    public class CurlRequestStreamAsyncResult : CurlAsyncResult
    {
        private Stream stream;
        public Stream Stream { get { return this.stream; } }

        public CurlRequestStreamAsyncResult(object state)
            : base(state)
        {
        }

        public void SetCompleted(Stream stream, bool completedSync)
        {
            this.stream = stream;
            this.CompletedSynchronously = completedSync;
            SignalCompleted();
        }
    }
}
