using System.Text.Json.Serialization;

namespace DigiPay.Api.Types;

public class NSPKCredentials
{
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string PaymentUrl { get; set; } = default!;
}
