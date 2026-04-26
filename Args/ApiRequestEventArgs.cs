using DigiPay.Api.Requests.Abstractions;

namespace DigiPay.Api.Args;

public class ApiRequestEventArgs(IRequest request, HttpRequestMessage? httpRequestMessage = default) : EventArgs
{
    public IRequest Request { get; } = request;

    public HttpRequestMessage? HttpRequestMessage { get; } = httpRequestMessage;
}
