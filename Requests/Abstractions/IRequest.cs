namespace DigiPay.Api.Requests.Abstractions;

public interface IRequest
{
    HttpMethod HttpMethod { get; }

    string MethodPath { get; }

    bool IsWebhookResponse { get; set; }

    string ToQueryParameters();
}
