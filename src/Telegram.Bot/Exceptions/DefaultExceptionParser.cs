namespace Telegram.Bot.Exceptions
{
    internal class DefaultExceptionParser : IExceptionParser
    {
        public ApiRequestException Parse(ApiResponse apiResponse) =>
            new(
                message: apiResponse.Description,
                errorCode: apiResponse.ErrorCode,
                parameters: apiResponse.Parameters
            );
    }
}
