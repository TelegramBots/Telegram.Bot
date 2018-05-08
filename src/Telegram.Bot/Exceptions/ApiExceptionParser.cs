using System;
using Telegram.Bot.Exceptions.Abstractions;
using Telegram.Bot.Types;

namespace Telegram.Bot.Exceptions
{
    /// <summary>
    /// Telegram API response parser class.
    /// </summary>
    public class ApiExceptionParser : IExceptionParser
    {
        /// <inheritdoc />
        public ApiRequestException Parse<T>(ApiResponse<T> apiResponse)
        {
            string errorMessage = apiResponse.Description;
            Type exceptionType = typeof(ApiRequestException);

            switch (apiResponse.ErrorCode)
            {
                case UnauthorizedException.UnauthorizedErrorCode:
                    exceptionType = typeof(UnauthorizedException);
                    break;

                case BadRequestException.BadRequestErrorCode:
                    errorMessage = TruncateBadRequestErrorDescription(apiResponse.Description);
                    exceptionType = typeof(BadRequestException);
                    break;

                case ForbiddenException.ForbiddenErrorCode:
                    errorMessage = TruncateForbiddenErrorDescription(apiResponse.Description);
                    exceptionType = typeof(ForbiddenException);
                    break;

                case TooManyRequestsException.TooManyRequestsErrorCode:
                    errorMessage = TruncateTooManyRequestsErrorDescription(apiResponse.Description);
                    exceptionType = typeof(TooManyRequestsException);
                    break;

                case ConflictException.ConflictErrorCode:
                    errorMessage = TruncateConflictErrorDescription(apiResponse.Description);
                    exceptionType = typeof(ConflictException);
                    break;

                default:
                    return new ApiRequestException(errorMessage, apiResponse.ErrorCode, apiResponse.Parameters);
            }

            return apiResponse.Parameters == null
                ? Activator.CreateInstance(exceptionType, errorMessage) as ApiRequestException
                : Activator.CreateInstance(exceptionType, errorMessage, apiResponse.Parameters) as ApiRequestException;
        }

        private static string TruncateBadRequestErrorDescription(string message) =>
            TryTruncateErrorDescription(message, BadRequestException.BadRequestErrorDescription);

        private static string TruncateForbiddenErrorDescription(string message) =>
            TryTruncateErrorDescription(message, ForbiddenException.ForbiddenErrorDescription);

        private static string TruncateTooManyRequestsErrorDescription(string message) =>
            TryTruncateErrorDescription(message, TooManyRequestsException.TooManyRequestsErrorDescription);

        private static string TruncateConflictErrorDescription(string message) =>
            TryTruncateErrorDescription(message, ConflictException.BadRequestErrorDescription);

        private static string TryTruncateErrorDescription(string message, string description)
        {
            bool hasErrorTypeDescription = message?.IndexOf(description) == 0;
            if (hasErrorTypeDescription)
                message = message.Substring(description.Length);
            return message;
        }
    }
}
