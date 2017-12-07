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
            new BadRequestExceptionInfo<InvalidUserIdException>("USER_ID_INVALID"),

            #region Stickers

            new BadRequestExceptionInfo<InvalidStickerSetNameException>("sticker set name invalid"),
            new BadRequestExceptionInfo<InvalidStickerEmojisException>("invalid sticker emojis"),
            new BadRequestExceptionInfo<InvalidStickerDimensionsException>("STICKER_PNG_DIMENSIONS"),
            new BadRequestExceptionInfo<StickerSetNameExistsException>("sticker set name is already occupied"),
            new BadRequestExceptionInfo<StickerSetNotModifiedException>("STICKERSET_NOT_MODIFIED"),

            #endregion

            new BadRequestExceptionInfo<ContactRequestException>("phone number can be requested in a private chats only"),

            new ForbiddenExceptionInfo<ChatNotInitiatedException>("bot can't initiate conversation with a user"),
        };

        public static ApiRequestException Parse<T>(ApiResponse<T> apiResponse)
        {
            ApiRequestException exception;

            var typeInfo = ExceptionInfos
                .SingleOrDefault(info => apiResponse.Description.Contains(info.ErrorMessage));

            if (typeInfo is null)
            {
                Debug.WriteLine($"Exception type info not found. {apiResponse.ErrorCode} - {apiResponse.Description}");
                exception = new ApiRequestException(apiResponse.Description, apiResponse.ErrorCode, apiResponse.Parameters);
            }
            else
            {
                string errorMessage;
                bool isBadRequestError = typeInfo.ErrorCode == BadRequestException.BadRequestErrorCode;

                if (isBadRequestError)
                {
                    errorMessage = TruncateBadRequestErrorDescription(apiResponse.Description);
                }
                else
                {
                    errorMessage = TruncateForbiddenErrorDescription(apiResponse.Description);
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
