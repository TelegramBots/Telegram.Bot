using System.Collections.Generic;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to send a video
    /// </summary>
    public class SendVideoRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendVideoRequest"/> class
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="video">Video to send.</param>
        /// <param name="duration">Duration of sent video in seconds</param>
        /// <param name="width">Video width</param>
        /// <param name="height">Video height</param>
        /// <param name="caption">Video caption</param>
        /// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        public SendVideoRequest(ChatId chatId, FileToSend video, int duration = 0,
            int width = 0,
            int height = 0,
            string caption = "",
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null) : base("sendVideo", new Dictionary<string, object> { { "chat_id", chatId } })
        {
            switch (video.Type)
            {
                case FileType.Stream:
                    FileStream = video.Content;
                    FileName = video.Filename;
                    FileParameterName = "video";
                    break;
                default:
                    Parameters.Add("video", video);
                    break;
            }

            if (duration != 0) Parameters.Add("duration", duration);
            if (width != 0) Parameters.Add("width", width);
            if (height != 0) Parameters.Add("height", height);
            if (!string.IsNullOrWhiteSpace(caption)) Parameters.Add("caption", caption);
            if (disableNotification) Parameters.Add("disable_notification", true);
            if (replyToMessageId != 0) Parameters.Add("reply_to_message_id", replyToMessageId);
            if (replyMarkup != null) Parameters.Add("reply_markup", replyMarkup);
        }
    }
}
