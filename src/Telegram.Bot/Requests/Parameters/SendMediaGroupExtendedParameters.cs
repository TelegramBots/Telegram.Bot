using System.Collections.Generic;
using Telegram.Bot.Types;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.SendMediaGroupAsync" /> method.
    /// </summary>
    public class SendMediaGroupExtendedParameters : ParametersBase
    {
        /// <summary>
        ///     A JSON-serialized array describing photos and videos to be sent, must include 2–10 items
        /// </summary>
        public IEnumerable<IAlbumInputMedia> InputMedia { get; set; }

        /// <summary>
        ///     Unique identifier for the target chat or username of the target channel (in the format @channelusername)
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     Sends the messages silently. Users will receive a notification with no sound.
        /// </summary>
        public bool DisableNotification { get; set; }

        /// <summary>
        ///     If the message is a reply, ID of the original message
        /// </summary>
        public int ReplyToMessageId { get; set; }

        /// <summary>
        ///     Sets <see cref="InputMedia" /> property.
        /// </summary>
        /// <param name="inputMedia">A JSON-serialized array describing photos and videos to be sent, must include 2–10 items</param>
        public SendMediaGroupExtendedParameters WithInputMedia(IEnumerable<IAlbumInputMedia> inputMedia)
        {
            InputMedia = inputMedia;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId">
        ///     Unique identifier for the target chat or username of the target channel (in the format
        ///     @channelusername)
        /// </param>
        public SendMediaGroupExtendedParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="DisableNotification" /> property.
        /// </summary>
        /// <param name="disableNotification">Sends the messages silently. Users will receive a notification with no sound.</param>
        public SendMediaGroupExtendedParameters WithDisableNotification(bool disableNotification)
        {
            DisableNotification = disableNotification;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ReplyToMessageId" /> property.
        /// </summary>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        public SendMediaGroupExtendedParameters WithReplyToMessageId(int replyToMessageId)
        {
            ReplyToMessageId = replyToMessageId;
            return this;
        }
    }
}
