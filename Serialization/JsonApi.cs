using DigiPay.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DigiPay.Api.Serialization;

public static class JsonApi
{
    public static JsonSerializerOptions Options { get; } = new();

    static JsonApi()
    {
        Configure(Options);
    }

    public static void Configure(JsonSerializerOptions options)
    {
        options.PropertyNameCaseInsensitive = true;
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

        if (!JsonSerializer.IsReflectionEnabledByDefault) options.TypeInfoResolverChain.Add(JsonApiSerializationContext.Default);
    }
}