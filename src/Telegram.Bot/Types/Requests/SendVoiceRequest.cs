using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to send a voice message
    /// </summary>
    public class SendVoiceRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendVoiceRequest"/> class
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="voice">Audio file to send.</param>
        /// <param name="caption">Voice message caption, 0-200 characters</param>
        /// <param name="duration">Duration of sent audio in seconds</param>
        /// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        public SendVoiceRequest(ChatId chatId, FileToSend voice,
            string caption = "",
            int duration = 0,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null) : base("sendVoice", new Dictionary<string, object> { { "chat_id", chatId } })
        {
            switch (voice.Type)
            {
                case FileType.Stream:
                    FileStream = voice.Content;
                    FileName = voice.Filename;
                    FileParameterName = "voice";
                    break;
                default:
                    Parameters.Add("voice", voice);
                    break;
            }

            if (!string.IsNullOrWhiteSpace(caption)) Parameters.Add("caption", caption);
            if (duration != 0) Parameters.Add("duration", duration);
            if (disableNotification) Parameters.Add("disable_notification", true);
            if (replyToMessageId != 0) Parameters.Add("reply_to_message_id", replyToMessageId);
            if (replyMarkup != null) Parameters.Add("reply_markup", replyMarkup);
        }
    }
}
