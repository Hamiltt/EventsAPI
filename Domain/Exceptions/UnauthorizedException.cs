using System.Net;

namespace Domain.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        public UnauthorizedException()
            : base("Unauthorized access.")
        {
            StatusCode = HttpStatusCode.Unauthorized;
        }

        public UnauthorizedException(string message)
            : base(message)
        {
            StatusCode = HttpStatusCode.Unauthorized;
        }

        public UnauthorizedException(string message, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = HttpStatusCode.Unauthorized;
        }

        public UnauthorizedException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public UnauthorizedException(HttpStatusCode statusCode, string message, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
