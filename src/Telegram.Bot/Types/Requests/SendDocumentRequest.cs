using System.Collections.Generic;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to send a document
    /// </summary>
    public class SendDocumentRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendDocumentRequest"/> class
        /// </summary>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="document">File to send.</param>
        /// <param name="caption">Document caption</param>
        /// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        public SendDocumentRequest(ChatId chatId, FileToSend document,
            string caption = "",
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null) : base("sendDocument", new Dictionary<string, object> { { "chat_id", chatId } })
        {
            switch (document.Type)
            {
                case FileType.Stream:
                    FileStream = document.Content;
                    FileName = document.Filename;
                    FileParameterName = "document";
                    break;
                default:
                    Parameters.Add("document", document);
                    break;
            }

            if (!string.IsNullOrWhiteSpace(caption)) Parameters.Add("caption", caption);
            if (disableNotification) Parameters.Add("disable_notification", true);
            if (replyToMessageId != 0) Parameters.Add("reply_to_message_id", replyToMessageId);
            if (replyMarkup != null) Parameters.Add("reply_markup", replyMarkup);
        }
    }
}
