using DigiPay.Api.Types.Enums;
using System.Text.Json.Serialization;

namespace DigiPay.Api.Types;

public class Payment<TCredentials>
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string PaymentId { get; set; } = default!;

    public string? PartnerPaymentId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public decimal Amount { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public decimal ExchangeRate { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public PaymentStatus Status { get; set; }

    public PaymentType? Type { get; set; }

    public PaymentType? PaymentType { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Currency Currency { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public DateTime Created { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public DateTime Expiry { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public decimal Rate { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public TCredentials Credentials { get; set; } = default!;

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int CompletedIn { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public decimal Predict { get; set; }
}
