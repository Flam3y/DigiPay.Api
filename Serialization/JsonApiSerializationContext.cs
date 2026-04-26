using DigiPay.Api.Requests.Abstractions;
using DigiPay.Api.Types;
using System.Text.Json.Serialization;

namespace DigiPay.Serialization;

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    UseStringEnumConverter = false)]
[JsonSerializable(typeof(WebhookRequest))]
public partial class JsonApiSerializationContext : JsonSerializerContext;