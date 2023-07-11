//using Microsoft.AspNetCore.Http;

namespace EventService.Common.Exceptions
{
    public class EventException : Exception
    {
        public int StatusCode { get; }
        public EventException(string message, int statusCode)
        : base(message)
        {
            StatusCode = statusCode;
        }

        public EventException(string message, int statusCode, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
