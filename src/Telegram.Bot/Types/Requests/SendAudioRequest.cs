using System.Collections.Generic;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to send an audio message
    /// </summary>
    public class SendAudioRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new object of the <see cref="SendAudioRequest"/> class
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="audio">Audio file to send.</param>
        /// <param name="caption">Audio caption, 0-200 characters</param>
        /// <param name="duration">Duration of the audio in seconds</param>
        /// <param name="performer">Performer</param>
        /// <param name="title">Track name</param>
        /// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        public SendAudioRequest(ChatId chatId, FileToSend audio, string caption, 
            int duration, string performer, string title,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null) : base("sendAudio", new Dictionary<string, object>
            { { "chat_id", chatId }, { "caption", caption }, { "duration", duration }, { "performer", performer }, { "title", title } })
        {
            switch (audio.Type)
            {
                case FileType.Stream:
                    FileStream = audio.Content;
                    FileName = audio.Filename;
                    FileParameterName = "audio";
                    break;
                default:
                    Parameters.Add("audio", audio);
                    break;
            }
            if (disableNotification) Parameters.Add("disable_notification", true);
            if (replyToMessageId != 0) Parameters.Add("reply_to_message_id", replyToMessageId);
            if (replyMarkup != null) Parameters.Add("reply_markup", replyMarkup);
        }
    }
}
