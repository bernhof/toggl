using System;
using System.Net.Http;

namespace Toggl
{
    /// <summary>
    /// Reprents an error reported by the Toggl API
    /// </summary>
    public class TogglApiException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="TogglApiException"/>
        /// </summary>
        public TogglApiException()
        {
        }

        /// <summary>
        /// Creates a new <see cref="TogglApiException"/>
        /// </summary>
        /// <param name="message">Message to include in exception</param>
        /// <param name="response">Response that caused this exception to be thrown</param>
        public TogglApiException(string message, HttpResponseMessage response) : base(message)
        {
            this.Response = response;
        }

        /// <summary>
        /// Response that caused exception to be thrown
        /// </summary>
        public HttpResponseMessage Response { get; private set; }
    }
}