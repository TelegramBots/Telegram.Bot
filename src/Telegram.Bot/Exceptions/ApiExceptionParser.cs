using System;
using System.Linq;
using System.Text.RegularExpressions;
using Telegram.Bot.Types;

namespace Telegram.Bot.Exceptions
{
    internal static class ApiExceptionParser
    {
        private static readonly IApiExceptionInfo<ApiRequestException>[] ExceptionInfos = {
            new BadRequestExceptionInfo<ChatNotFoundException>("chat not found"),
            new BadRequestExceptionInfo<UserNotFoundException>("user not found"),
            new BadRequestExceptionInfo<InvalidUserIdException>("USER_ID_INVALID"),
            new BadRequestExceptionInfo<InvalidQueryIdException>("QUERY_ID_INVALID"),

            #region Stickers

            new BadRequestExceptionInfo<InvalidStickerSetNameException>("sticker set name invalid"),
            new BadRequestExceptionInfo<InvalidStickerEmojisException>("invalid sticker emojis"),
            new BadRequestExceptionInfo<InvalidStickerDimensionsException>("STICKER_PNG_DIMENSIONS"),
            new BadRequestExceptionInfo<StickerSetNameExistsException>("sticker set name is already occupied"),
            new BadRequestExceptionInfo<StickerSetNotModifiedException>("STICKERSET_NOT_MODIFIED"),

            #endregion

            new BadRequestExceptionInfo<ContactRequestException>("phone number can be requested in a private chats only"),

            new ForbiddenExceptionInfo<ChatNotInitiatedException>("bot can't initiate conversation with a user"),

            new BadRequestExceptionInfo<InvalidParameterException>($@"\w{{3,}} Request: invalid (?<{InvalidParameterException.ParamGroupName}>[\w|\s]+)$"),
            new BadRequestExceptionInfo<InvalidParameterException>($@"\w{{3,}} Request: (?<{InvalidParameterException.ParamGroupName}>[\w|\s]+) invalid$"),

            new BadRequestExceptionInfo<MessageIsNotModifiedException>("message is not modified"),
        };

        public static ApiRequestException Parse<T>(ApiResponse<T> apiResponse)
        {
            var errorMessage = string.Empty;

            var typeInfo = ExceptionInfos
                .FirstOrDefault(info => Regex.IsMatch(apiResponse.Description, info.ErrorMessageRegex));

            switch (apiResponse.ErrorCode)
            {
                case BadRequestException.BadRequestErrorCode:
                    errorMessage = TruncateBadRequestErrorDescription(apiResponse.Description);

                    switch (typeInfo?.Type)
                    {
                        case var ex when ex == typeof(InvalidParameterException):
                            string paramName = Regex.Match(apiResponse.Description, typeInfo.ErrorMessageRegex)
                                .Groups[InvalidParameterException.ParamGroupName]
                                .Value;
                            return new InvalidParameterException(paramName, errorMessage);

                        case null:
                            return new BadRequestException(errorMessage);

                        default:
                            return Activator.CreateInstance(typeInfo.Type, errorMessage) as ApiRequestException;

                    }

                case ForbiddenException.ForbiddenErrorCode:
                    errorMessage = TruncateForbiddenErrorDescription(apiResponse.Description);

                    if (typeInfo is null)
                        return new BadRequestException(errorMessage);

                    return Activator.CreateInstance(typeInfo.Type, errorMessage) as ApiRequestException;

                default:
                    return new ApiRequestException(apiResponse.Description, apiResponse.ErrorCode, apiResponse.Parameters);
            }
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
