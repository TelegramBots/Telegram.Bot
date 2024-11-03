namespace Telegram.Bot.Exceptions;

/// <summary>Parses unsuccessful responses from Telegram Bot API to make specific exceptions</summary>
public interface IExceptionParser
{
    /// <summary>Parses HTTP response and constructs a specific exception out of it</summary>
    /// <param name="apiResponse">ApiResponse with an error</param>
    ApiRequestException Parse(ApiResponse apiResponse);
}

/// <summary>Default implementation of <see cref="IExceptionParser"/> that always returns <see cref="ApiRequestException"/></summary>
public class DefaultExceptionParser : IExceptionParser
{
    /// <inheritdoc/>
    public ApiRequestException Parse(ApiResponse apiResponse)
    {
        if (apiResponse is null) throw new ArgumentNullException(nameof(apiResponse));
        return new(apiResponse.Description!, apiResponse.ErrorCode, apiResponse.Parameters);
    }
}
