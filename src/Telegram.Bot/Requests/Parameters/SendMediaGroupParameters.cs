using System.Collections.Generic;
using Telegram.Bot.Types;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.SendMediaGroupAsync" /> method.
    /// </summary>
    public class SendMediaGroupParameters : ParametersBase
    {
        /// <summary>
        ///     Unique identifier for the target chat or username of the target channel (in the format @channelusername)
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        /// </summary>
        public IEnumerable<InputMediaBase> Media { get; set; }

        /// <summary>
        ///     Sends the messages silently. Users will receive a notification with no sound.
        /// </summary>
        public bool DisableNotification { get; set; }

        /// <summary>
        ///     If the message is a reply, ID of the original message
        /// </summary>
        public int ReplyToMessageId { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId">
        ///     Unique identifier for the target chat or username of the target channel (in the format
        ///     @channelusername)
        /// </param>
        public SendMediaGroupParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Media" /> property.
        /// </summary>
        public SendMediaGroupParameters WithMedia(IEnumerable<InputMediaBase> media)
        {
            Media = media;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="DisableNotification" /> property.
        /// </summary>
        /// <param name="disableNotification">Sends the messages silently. Users will receive a notification with no sound.</param>
        public SendMediaGroupParameters WithDisableNotification(bool disableNotification)
        {
            DisableNotification = disableNotification;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ReplyToMessageId" /> property.
        /// </summary>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        public SendMediaGroupParameters WithReplyToMessageId(int replyToMessageId)
        {
            ReplyToMessageId = replyToMessageId;
            return this;
        }
    }
}