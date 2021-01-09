using Telegram.Bot.Types;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.ForwardMessageAsync" /> method.
    /// </summary>
    public class ForwardMessageParameters : ParametersBase
    {
        /// <summary>
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        /// </summary>
        public ChatId FromChatId { get; set; }

        /// <summary>
        ///     Unique message identifier
        /// </summary>
        public int MessageId { get; set; }

        /// <summary>
        ///     Sends the message silently. iOS users will not receive a notification, Android users will receive a notification
        ///     with no sound.
        /// </summary>
        public bool DisableNotification { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId"></param>
        public ForwardMessageParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="FromChatId" /> property.
        /// </summary>
        /// <param name="fromChatId"></param>
        public ForwardMessageParameters WithFromChatId(ChatId fromChatId)
        {
            FromChatId = fromChatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="MessageId" /> property.
        /// </summary>
        /// <param name="messageId">Unique message identifier</param>
        public ForwardMessageParameters WithMessageId(int messageId)
        {
            MessageId = messageId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="DisableNotification" /> property.
        /// </summary>
        /// <param name="disableNotification">
        ///     Sends the message silently. iOS users will not receive a notification, Android users
        ///     will receive a notification with no sound.
        /// </param>
        public ForwardMessageParameters WithDisableNotification(bool disableNotification)
        {
            DisableNotification = disableNotification;
            return this;
        }
    }
}