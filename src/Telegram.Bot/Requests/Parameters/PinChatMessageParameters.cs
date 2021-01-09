using Telegram.Bot.Types;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.PinChatMessageAsync" /> method.
    /// </summary>
    public class PinChatMessageParameters : ParametersBase
    {
        /// <summary>
        ///     Unique identifier for the target chat or username of the target supergroup
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     Identifier of a message to pin
        /// </summary>
        public int MessageId { get; set; }

        /// <summary>
        ///     Pass True, if it is not necessary to send a notification to all group members about the new pinned message
        /// </summary>
        public bool DisableNotification { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup</param>
        public PinChatMessageParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="MessageId" /> property.
        /// </summary>
        /// <param name="messageId">Identifier of a message to pin</param>
        public PinChatMessageParameters WithMessageId(int messageId)
        {
            MessageId = messageId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="DisableNotification" /> property.
        /// </summary>
        /// <param name="disableNotification">
        ///     Pass True, if it is not necessary to send a notification to all group members about
        ///     the new pinned message
        /// </param>
        public PinChatMessageParameters WithDisableNotification(bool disableNotification)
        {
            DisableNotification = disableNotification;
            return this;
        }
    }
}