using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Telegram.Bot.Types;

namespace Telegram.Bot.Exceptions
{
    internal static class ApiExceptionParser
    {
        private static readonly IApiExceptionInfo<ApiRequestException>[] BadRequestExceptionInfos =
        {
            #region BadRequest Exceptions

            new BadRequestExceptionInfo<ChatNotFoundException>("chat not found"),
            new BadRequestExceptionInfo<UserNotFoundException>("user not found"),
            new BadRequestExceptionInfo<WrongChatTypeException>("method is available (for supergroup and channel chats only|only for supergroups)"),
            new BadRequestExceptionInfo<ChatNotModifiedException>("CHAT_NOT_MODIFIED"),
            new BadRequestExceptionInfo<StickerSetNameExistsException>("sticker set name is already occupied"),
            new BadRequestExceptionInfo<StickerSetNotModifiedException>("STICKERSET_NOT_MODIFIED"),
            new BadRequestExceptionInfo<ContactRequestException>("phone number can be requested in a private chats only"),
            new BadRequestExceptionInfo<MessageIsNotModifiedException>("message is not modified"),
            new BadRequestExceptionInfo<ChatDescriptionIsNotModifiedException>("chat description is not modified"),
            // ToDo: BotRestrictedException test case
            new BadRequestExceptionInfo<BotRestrictedException>("have no rights to send a message"),
            // ToDo: NotEnoughRightsException test case
            new BadRequestExceptionInfo<NotEnoughRightsException>("not enough rights to restrict/unrestrict chat member"),

            #endregion

            #region InvalidParameter Exceptions

            new BadRequestExceptionInfo<InvalidUserIdException>("USER_ID_INVALID"),
            new BadRequestExceptionInfo<InvalidQueryIdException>("QUERY_ID_INVALID"),
            new BadRequestExceptionInfo<InvalidStickerSetNameException>("sticker set name invalid"),
            new BadRequestExceptionInfo<InvalidStickerEmojisException>("invalid sticker emojis"),
            new BadRequestExceptionInfo<InvalidStickerDimensionsException>("STICKER_PNG_DIMENSIONS"),
            new BadRequestExceptionInfo<InvalidParameterException>($@"\w{{3,}} Request: invalid (?<{InvalidParameterException.ParamGroupName}>[\w|\s]+)$"),
            new BadRequestExceptionInfo<InvalidParameterException>($@"\w{{3,}} Request: (?<{InvalidParameterException.ParamGroupName}>[\w|\s]+) invalid$"),
            // ToDo: rename MissingParameterException to EmptyParameterException
            new BadRequestExceptionInfo<MissingParameterException>($@"\w{{3,}} Request: (?<{InvalidParameterException.ParamGroupName}>[\w|\s]+) is empty"),

            #endregion
        };

        private static readonly IApiExceptionInfo<ApiRequestException>[] ForibddenExceptionInfos =
        {
            #region Forbidden Exceptions
            new ForbiddenExceptionInfo<ChatNotInitiatedException>("bot can't initiate conversation with a user"),
            new ForbiddenExceptionInfo<SendMessageToBotException>("bot can't send messages to bots"),
            new ForbiddenExceptionInfo<BotIsNotMemberException>("bot is not a member of the (supergroup|channel) chat"),
            // ToDo: BotBlockedException test case
            new ForbiddenExceptionInfo<BotBlockedException>("bot was blocked by the user"),
            #endregion
        };

        public static ApiRequestException Parse<T>(ApiResponse<T> apiResponse)
        {
            var errorMessage = string.Empty;
            IApiExceptionInfo<ApiRequestException> typeInfo = default;

            switch (apiResponse.ErrorCode)
            {
                case BadRequestException.BadRequestErrorCode:
                    errorMessage = TruncateBadRequestErrorDescription(apiResponse.Description);

                    typeInfo =
                        BadRequestExceptionInfos.FirstOrDefault(info => Regex.IsMatch(apiResponse.Description, info.ErrorMessageRegex))
                        ?? new BadRequestExceptionInfo<BadRequestException>(errorMessage);

                    if (IsAssignableFrom<InvalidParameterException>(typeInfo.Type))
                    {
                        string paramName = Regex.Match(apiResponse.Description, typeInfo.ErrorMessageRegex)
                            .Groups[InvalidParameterException.ParamGroupName]
                            .Value;

                        return string.IsNullOrEmpty(paramName)
                            ? Activator.CreateInstance(typeInfo.Type, errorMessage) as ApiRequestException
                            : Activator.CreateInstance(typeInfo.Type, paramName, errorMessage) as ApiRequestException;
                    }

                    return Activator.CreateInstance(typeInfo.Type, errorMessage) as ApiRequestException;

                case ForbiddenException.ForbiddenErrorCode:
                    errorMessage = TruncateForbiddenErrorDescription(apiResponse.Description);

                    typeInfo =
                        ForibddenExceptionInfos.FirstOrDefault(info => Regex.IsMatch(apiResponse.Description, info.ErrorMessageRegex))
                        ?? new ForbiddenExceptionInfo<ForbiddenException>(errorMessage);

                    return Activator.CreateInstance(typeInfo.Type, errorMessage) as ApiRequestException;

                case TooManyRequestsException.TooManyRequestsErrorCode:
                    errorMessage = TruncateTooManyRequestsErrorDescription(apiResponse.Description);

                    return new TooManyRequestsException(errorMessage, apiResponse.Parameters);

                default:
                    return new ApiRequestException(apiResponse.Description, apiResponse.ErrorCode, apiResponse.Parameters);
            }

            bool IsAssignableFrom<parentT>(Type derivedT) =>
                typeof(parentT).GetTypeInfo().IsAssignableFrom(derivedT.GetTypeInfo());
        }

        private static string TruncateBadRequestErrorDescription(string message) =>
            TryTruncateErrorDescription(message, BadRequestException.BadRequestErrorDescription);

        private static string TruncateForbiddenErrorDescription(string message) =>
            TryTruncateErrorDescription(message, ForbiddenException.ForbiddenErrorDescription);

        private static string TruncateTooManyRequestsErrorDescription(string message) =>
            TryTruncateErrorDescription(message, TooManyRequestsException.TooManyRequestsErrorDescription);

        private static string TryTruncateErrorDescription(string message, string description)
        {
            bool hasErrorTypeDescription = message?.IndexOf(description) == 0;
            if (hasErrorTypeDescription)
                message = message.Substring(description.Length);
            return message;
        }
    }
}
