using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events.EventArguments
{
    public class TwitterExceptionEventArgs : EventArgs
    {
        public TwitterExceptionEventArgs(TwitterException ex)
        {
            TwitterException = ex;
        }

        public TwitterException TwitterException { get; private set; }
    }

    public class TwitterException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ResponseMessage { get; set; }

        public TwitterException(HttpStatusCode code, string message)
        {
            StatusCode = code;
            ResponseMessage = message;
        }
        public override string ToString()
        {
            return $"Twitter WebException: HttpStatusCode - { StatusCode} ResponseMessage - { ResponseMessage}";
        }
    }
}
