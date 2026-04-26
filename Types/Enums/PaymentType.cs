using DigiPay.Api.Serialization;
using System.Text.Json.Serialization;

namespace DigiPay.Api.Types.Enums;

[JsonConverter(typeof(ToUpperConverter<PaymentType>))]
public enum PaymentType
{
    Card,
    SBP,
    Bill,
    Transgran,
    Acq_SBP,
    Acq_Card,
    Acq_Hybrid
}
