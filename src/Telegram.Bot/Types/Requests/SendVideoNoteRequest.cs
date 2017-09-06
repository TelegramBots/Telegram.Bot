using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to send a video note
    /// </summary>
    public class SendVideoNoteRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new object of the <see cref="SendVideoNoteRequest"/> class
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="videoNote">Video note to send.</param>
        /// <param name="duration">Duration of sent video in seconds</param>
        /// <param name="length">Video width and height</param>
        /// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        public SendVideoNoteRequest(ChatId chatId, FileToSend videoNote,
            int duration = 0,
            int length = 0,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null) : base("sendVideoNote", new Dictionary<string, object> { { "chat_id", chatId } })
        {
            switch (videoNote.Type)
            {
                case FileType.Stream:
                    FileStream = videoNote.Content;
                    FileName = videoNote.Filename;
                    FileParameterName = "video_note";
                    break;
                default:
                    Parameters.Add("video_note", videoNote);
                    break;
            }

            if (duration != 0) Parameters.Add("duration", duration);
            if (length != 0) Parameters.Add("length", length);
            if (disableNotification) Parameters.Add("disable_notification", true);
            if (replyToMessageId != 0) Parameters.Add("reply_to_message_id", replyToMessageId);
            if (replyMarkup != null) Parameters.Add("reply_markup", replyMarkup);
        }
    }
}
