using System;

namespace ErsteApi.Rest
{
    class RestException : Exception
    {
        public RestException()
        {
        }

        public RestException(string message) : base(message)
        {
        }

        public RestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
