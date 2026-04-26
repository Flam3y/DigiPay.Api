using DigiPay.Api.Args;
using DigiPay.Api.Requests.Abstractions;

namespace DigiPay.Api;

public delegate ValueTask AsyncEventHandler<in TArgs>(IDigiPayClient apiClient, TArgs args, CancellationToken cancellationToken = default);

public interface IDigiPayClient
{
    TimeSpan Timeout { get; set; }

    event AsyncEventHandler<ApiRequestEventArgs>? OnMakingApiRequest;

    event AsyncEventHandler<ApiResponseEventArgs>? OnApiResponseReceived;

    Task<TResponse> SendRequest<TResponse>(IRequest request, CancellationToken cancellationToken = default)
        where TResponse : class;
}
