using DigiPay.Api.Types;

namespace DigiPay.Api.Exceptions;

public interface IExceptionParser
{
    ApiRequestException Parse(ApiResponse apiResponse);
}

public class DefaultExceptionParser : IExceptionParser
{
    public ApiRequestException Parse(ApiResponse apiResponse)
    {
        ArgumentNullException.ThrowIfNull(apiResponse);

        return new(apiResponse.Message!);
    }
}
