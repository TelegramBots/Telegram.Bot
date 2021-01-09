using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.SendContactAsync" /> method.
    /// </summary>
    public class SendContactParameters : ParametersBase
    {
        /// <summary>
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     Contact's phone number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     Contact's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Contact's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     Sends the message silently. iOS users will not receive a notification, Android users will receive a notification
        ///     with no sound.
        /// </summary>
        public bool DisableNotification { get; set; }

        /// <summary>
        ///     If the message is a reply, ID of the original message
        /// </summary>
        public int ReplyToMessageId { get; set; }

        /// <summary>
        ///     Additional interface options. A JSON-serialized object for an inline keyboard, custom reply keyboard, instructions
        ///     to hide keyboard or to force a reply from the user.
        /// </summary>
        public IReplyMarkup ReplyMarkup { get; set; }

        /// <summary>
        ///     Additional data about the contact in the form of a vCard, 0-2048 bytes
        /// </summary>
        public string VCard { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId"></param>
        public SendContactParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="PhoneNumber" /> property.
        /// </summary>
        /// <param name="phoneNumber">Contact's phone number</param>
        public SendContactParameters WithPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="FirstName" /> property.
        /// </summary>
        /// <param name="firstName">Contact's first name</param>
        public SendContactParameters WithFirstName(string firstName)
        {
            FirstName = firstName;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="LastName" /> property.
        /// </summary>
        /// <param name="lastName">Contact's last name</param>
        public SendContactParameters WithLastName(string lastName)
        {
            LastName = lastName;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="DisableNotification" /> property.
        /// </summary>
        /// <param name="disableNotification">
        ///     Sends the message silently. iOS users will not receive a notification, Android users
        ///     will receive a notification with no sound.
        /// </param>
        public SendContactParameters WithDisableNotification(bool disableNotification)
        {
            DisableNotification = disableNotification;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ReplyToMessageId" /> property.
        /// </summary>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        public SendContactParameters WithReplyToMessageId(int replyToMessageId)
        {
            ReplyToMessageId = replyToMessageId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ReplyMarkup" /> property.
        /// </summary>
        /// <param name="replyMarkup">
        ///     Additional interface options. A JSON-serialized object for an inline keyboard, custom reply
        ///     keyboard, instructions to hide keyboard or to force a reply from the user.
        /// </param>
        public SendContactParameters WithReplyMarkup(IReplyMarkup replyMarkup)
        {
            ReplyMarkup = replyMarkup;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="VCard" /> property.
        /// </summary>
        /// <param name="vCard">Additional data about the contact in the form of a vCard, 0-2048 bytes</param>
        public SendContactParameters WithVCard(string vCard)
        {
            VCard = vCard;
            return this;
        }
    }
}