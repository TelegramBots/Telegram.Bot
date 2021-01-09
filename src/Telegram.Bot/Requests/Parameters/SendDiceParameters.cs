using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.SendDiceAsync" /> method.
    /// </summary>
    public class SendDiceParameters : ParametersBase
    {
        /// <summary>
        ///     Unique identifier for the target chat or username of the target channel
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     Sends the message silently. iOS users will not receive a notification, Android users will receive a notification
        ///     with no sound.
        /// </summary>
        public bool DisableNotification { get; set; }

        /// <summary>
        ///     If the message is a reply, ID of the original message
        /// </summary>
        public int ReplyToMessageId { get; set; }

        /// <summary>
        ///     Additional interface options. A JSON-serialized object for an inline keyboard, custom reply keyboard, instructions
        ///     to hide keyboard or to force a reply from the user.
        /// </summary>
        public IReplyMarkup ReplyMarkup { get; set; }

        /// <summary>
        ///     Emoji on which the dice throw animation is based
        /// </summary>
        public Emoji? Emoji { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        public SendDiceParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="DisableNotification" /> property.
        /// </summary>
        /// <param name="disableNotification">
        ///     Sends the message silently. iOS users will not receive a notification, Android users
        ///     will receive a notification with no sound.
        /// </param>
        public SendDiceParameters WithDisableNotification(bool disableNotification)
        {
            DisableNotification = disableNotification;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ReplyToMessageId" /> property.
        /// </summary>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        public SendDiceParameters WithReplyToMessageId(int replyToMessageId)
        {
            ReplyToMessageId = replyToMessageId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ReplyMarkup" /> property.
        /// </summary>
        /// <param name="replyMarkup">
        ///     Additional interface options. A JSON-serialized object for an inline keyboard, custom reply
        ///     keyboard, instructions to hide keyboard or to force a reply from the user.
        /// </param>
        public SendDiceParameters WithReplyMarkup(IReplyMarkup replyMarkup)
        {
            ReplyMarkup = replyMarkup;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Emoji" /> property.
        /// </summary>
        /// <param name="emoji">Emoji on which the dice throw animation is based</param>
        public SendDiceParameters WithEmoji(Emoji? emoji)
        {
            Emoji = emoji;
            return this;
        }
    }
}
