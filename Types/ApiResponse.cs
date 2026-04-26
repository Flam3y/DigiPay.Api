namespace DigiPay.Api.Types;

public class ApiResponse
{
    public bool Ok { get; set; }
    public string? Message { get; set; }
}


public class ApiResponse<TData> : ApiResponse
{
    public TData? Data { get; set; }
}
