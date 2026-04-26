namespace DigiPay.Api;

public class DigiPayClientOptions(string apiKey, string? baseRqeustUrl = null)
{
    private const string MainDigiPayUrl = "https://digitalpay.cc/api";

    public string ApiKey { get; } = apiKey ?? throw new ArgumentNullException(nameof(apiKey));

    public string BaseRequestUrl { get; } = baseRqeustUrl ?? MainDigiPayUrl;

    public TimeSpan RetryDelay { get; set; } = TimeSpan.FromSeconds(5);

    public int RetryCount { get; set; } = 3;
}
