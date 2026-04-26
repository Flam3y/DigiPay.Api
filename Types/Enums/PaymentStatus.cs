using System.Text.Json.Serialization;

namespace DigiPay.Api.Types.Enums;

[JsonConverter(typeof(JsonStringEnumConverter<PaymentStatus>))]
public enum PaymentStatus
{
    Created,
    Completed,
    Dispute,
    Canceled,
    CompletedOnDispute,
    Payed
}
