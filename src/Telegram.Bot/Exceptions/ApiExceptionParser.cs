using System;
using System.Diagnostics;
using System.Linq;
using Telegram.Bot.Types;

namespace Telegram.Bot.Exceptions
{
    internal static class ApiExceptionParser
    {
        private static readonly IApiExceptionInfo<ApiRequestException>[] ExceptionInfos = {
            new BadRequestExceptionInfo<ChatNotFoundException>("chat not found"),
            new BadRequestExceptionInfo<UserNotFoundException>("user not found"),

            new ForbiddenExceptionInfo<ChatNotInitiatedException>("bot can't initiate conversation with a user"),
        };

        public static ApiRequestException Parse<T>(ApiResponse<T> apiResponse)
        {
            ApiRequestException exception;

            var typeInfo = ExceptionInfos
                .SingleOrDefault(info => apiResponse.Message.Contains(info.ErrorMessage));

            if (typeInfo is null)
            {
                Debug.WriteLine($"Exception type info not found. {apiResponse.Code} - {apiResponse.Message}");
                exception = new ApiRequestException(apiResponse.Message, apiResponse.Code, apiResponse.Parameters);
            }
            else
            {
                string errorMessage;
                bool isBadRequestError = typeInfo.ErrorCode == BadRequestException.BadRequestErrorCode;

                if (isBadRequestError)
                {
                    errorMessage = TruncateBadRequestErrorDescription(apiResponse.Message);
                }
                else
                {
                    errorMessage = TruncateForbiddenErrorDescription(apiResponse.Message);
                }

                exception = Activator.CreateInstance(typeInfo.Type, errorMessage) as ApiRequestException;
            }
            return exception;
        }

        private static string TruncateBadRequestErrorDescription(string message) =>
            TryTruncateErrorDescription(message, BadRequestException.BadRequestErrorDescription);

        private static string TruncateForbiddenErrorDescription(string message) =>
            TryTruncateErrorDescription(message, ForbiddenException.ForbiddenErrorDescription);

        private static string TryTruncateErrorDescription(string message, string description)
        {
            bool hasErrorTypeDescription = message?.IndexOf(description) == 0;
            if (hasErrorTypeDescription)
                message = message.Substring(description.Length);
            return message;
        }
    }
}
