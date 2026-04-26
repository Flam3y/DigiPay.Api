using DigiPay.Api.Types.Enums;
using System.Text.Json.Serialization;

namespace DigiPay.Api.Types;

public class WebhookRequest
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string PaymentId { get; set; } = default!;

    public string? PartnerPaymentId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public decimal Amount { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public decimal ExchangeRate { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [JsonConverter(typeof(JsonNumberEnumConverter<PaymentStatus>))]
    public PaymentStatus Status { get; set; }

    [JsonConverter(typeof(JsonNumberEnumConverter<PaymentType>))]
    public PaymentType Type { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [JsonConverter(typeof(JsonNumberEnumConverter<Currency>))]
    public Currency Currency { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public DateTime Created { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public DateTime Expiry { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public decimal Rate { get; set; }

    public string? Payload { get; set; }
}
