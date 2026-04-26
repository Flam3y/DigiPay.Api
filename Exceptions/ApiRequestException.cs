using System.Net;

namespace DigiPay.Api.Exceptions;

public class ApiRequestException : RequestException
{
    public HttpStatusCode ErrorCode { get; }

    public ApiRequestException(string message) : base(message) { }

    public ApiRequestException(string message, HttpStatusCode errorCode) : base(message) => ErrorCode = errorCode;

    public ApiRequestException(string message, Exception innerException) : base(message, innerException) { }

    public ApiRequestException(string message, HttpStatusCode errorCode, Exception innerException)
        : base(message, innerException) => ErrorCode = errorCode;

    public override string ToString()
    {
        return $"DigiPay API error {ErrorCode} {Message}";
    }
}
