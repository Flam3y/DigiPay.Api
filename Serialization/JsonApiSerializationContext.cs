using DigiPay.Api.Requests.Abstractions;
using System.Text.Json.Serialization;

namespace DigiPay.Serialization;

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    UseStringEnumConverter = false)]
[JsonSerializable(typeof(IRequest))]
public partial class JsonApiSerializationContext : JsonSerializerContext;
