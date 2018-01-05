using System;
using System.Net;

namespace JmPayApiServer
{
    public class ApiException : Exception
    {
        private readonly HttpStatusCode _statusCode;

        public ApiException(HttpStatusCode statusCode, string message, Exception ex)
            : base(message, ex)
        {
            _statusCode = statusCode;
        }

        public ApiException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            _statusCode = statusCode;
        }

        public ApiException(HttpStatusCode statusCode)
        {
            _statusCode = statusCode;
        }

        public HttpStatusCode StatusCode
        {
            get { return _statusCode; }
        }
    }
}