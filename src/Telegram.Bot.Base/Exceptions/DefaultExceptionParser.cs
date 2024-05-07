namespace Telegram.Bot.Exceptions;

/// <summary>
/// Default implementation of <see cref="IExceptionParser"/> that always returns <see cref="ApiRequestException"/>
/// </summary>
public class DefaultExceptionParser : IExceptionParser
{
    /// <inheritdoc />
    public ApiRequestException Parse(ApiResponse apiResponse)
    {
        if (apiResponse is null)
        {
            throw new ArgumentNullException(nameof(apiResponse));
        }

        return new(
            message: apiResponse.Description,
            errorCode: apiResponse.ErrorCode,
            parameters: apiResponse.Parameters
        );
    }
}
