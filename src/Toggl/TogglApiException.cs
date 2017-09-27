using System;
using System.Net.Http;

namespace Toggl
{
    public class TogglApiException : Exception
    {
        public TogglApiException()
        {
        }

        public TogglApiException(string message, HttpResponseMessage response) : base(message)
        {
            this.Response = response;
        }

        public HttpResponseMessage Response { get; private set; }
    }
}