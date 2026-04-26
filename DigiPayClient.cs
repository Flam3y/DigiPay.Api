using DigiPay.Api.Args;
using DigiPay.Api.Exceptions;
using DigiPay.Api.Requests.Abstractions;
using DigiPay.Api.Serialization;
using DigiPay.Api.Types;
using System.Net;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace DigiPay.Api;

public class DigiPayClient : IDigiPayClient
{
    private readonly DigiPayClientOptions _options;

    private readonly HttpClient _httpClient;

    public string ApiKey => _options.ApiKey;

    public TimeSpan Timeout
    {
        get => _httpClient.Timeout;
        set => _httpClient.Timeout = value;
    }

    public CancellationToken GlobalCancellationToken { get; }

    public IExceptionParser ExceptionsParser { get; set; } = new DefaultExceptionParser();

    public event AsyncEventHandler<ApiRequestEventArgs>? OnMakingApiRequest;
    public event AsyncEventHandler<ApiResponseEventArgs>? OnApiResponseReceived;

    public DigiPayClient(DigiPayClientOptions options, HttpClient? httpClient = default, CancellationToken cancellationToken = default)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));

        _httpClient = httpClient ?? new HttpClient(new SocketsHttpHandler { PooledConnectionLifetime = TimeSpan.FromMinutes(3) });
        _httpClient.DefaultRequestHeaders.Add("apikey", ApiKey);

        GlobalCancellationToken = cancellationToken;
    }

    public DigiPayClient(string apiKey, HttpClient? httpClient = null, CancellationToken cancellationToken = default)
        : this(new DigiPayClientOptions(apiKey), httpClient, cancellationToken)
    { }

    public virtual async Task<TResponse> SendRequest<TResponse>(IRequest request, CancellationToken cancellationToken = default)
        where TResponse : class
    {
        ArgumentNullException.ThrowIfNull(request);

        using CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(GlobalCancellationToken, cancellationToken);
        cancellationToken = cts.Token;

        string url = $"{_options.BaseRequestUrl}/{request.MethodPath}?{request.ToQueryParameters()}";

        for (int attempt = 1; ; attempt++)
        {
            HttpRequestMessage httpRequest = new(request.HttpMethod, url);

            ApiRequestEventArgs? requestEventArgs = null;

            if (OnMakingApiRequest is not null)
            {
                requestEventArgs ??= new(request, httpRequest);
                await OnMakingApiRequest.Invoke(this, requestEventArgs, cancellationToken).ConfigureAwait(false);
            }

            HttpResponseMessage httpResponse;

            try
            {
                httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
            }
            catch (TaskCanceledException exception)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    throw;
                }

                throw new RequestException("DigiPay API Request timed out", exception);
            }
            catch (Exception exception)
            {
                throw new RequestException($"DigiPay API Service Failure: {exception.GetType().Name}: {exception.Message}", exception);
            }

            using (httpResponse)
            {
                if (OnApiResponseReceived is not null)
                {
                    requestEventArgs ??= new(request, httpRequest);
                    ApiResponseEventArgs responseEventArgs = new(httpResponse, requestEventArgs);

                    await OnApiResponseReceived.Invoke(this, responseEventArgs, cancellationToken).ConfigureAwait(false);
                }

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    ApiResponse<TResponse> apiResponse = await DeserializeContent<ApiResponse<TResponse>>(
                        httpResponse,
                        response => response.Ok && response.Data is not null, cancellationToken).ConfigureAwait(false);
                    return apiResponse.Data!;
                }

                ApiResponse failedApiResponse = await DeserializeContent<ApiResponse>(httpResponse,
                       response => response.Message != null, cancellationToken).ConfigureAwait(false);

                if (attempt >= _options.RetryCount)
                {
                    throw ExceptionsParser.Parse(failedApiResponse);
                }
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static async Task<T> DeserializeContent<T>(HttpResponseMessage httpResponse, Func<T, bool> validate,
        CancellationToken cancellationToken = default) where T : class
    {
        if (httpResponse.Content is null)
        {
            throw new RequestException("Response doesn't contain any content", httpResponse.StatusCode);
        }

        T? deserializedObject;

        try
        {
            deserializedObject = await httpResponse.Content.ReadFromJsonAsync<T>(JsonApi.Options, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            throw new RequestException("There was an exception during deserialization of the response", httpResponse.StatusCode, exception);
        }

        if (deserializedObject is null || !validate(deserializedObject))
        {
            throw new RequestException("Required properties not found in response", httpResponse.StatusCode);
        }

        return deserializedObject;
    }
}
