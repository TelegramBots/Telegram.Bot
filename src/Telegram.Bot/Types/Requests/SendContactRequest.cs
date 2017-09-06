using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types.Requests
{
    public class SendContactRequest : ApiRequest
    {
        public SendContactRequest(ChatId chatId, string phoneNumber, 
            string firstName, string lastName = null,
            bool disableNotification = false,
            int replyToMessageId = 0,
            IReplyMarkup replyMarkup = null) : base("sendContact", new Dictionary<string, object>
            { { "chat_id", chatId }, { "phone_number", phoneNumber }, { "first_name", firstName } })
        {
            if (lastName != null) Parameters.Add("last_name", lastName);
            if (disableNotification) Parameters.Add("disable_notification", true);
            if (replyToMessageId != 0) Parameters.Add("reply_to_message_id", replyToMessageId);
            if (replyMarkup != null) Parameters.Add("reply_markup", replyMarkup);
        }
    }
}
