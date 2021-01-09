using System.Collections.Generic;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.SendInvoiceAsync" /> method.
    /// </summary>
    public class SendInvoiceParameters : ParametersBase
    {
        /// <summary>
        ///     Unique identifier for the target private chat
        /// </summary>
        public int ChatId { get; set; }

        /// <summary>
        ///     Product name
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Product description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Bot-defined invoice payload, 1-128 bytes. This will not be displayed to the user, use for your internal processes.
        /// </summary>
        public string Payload { get; set; }

        /// <summary>
        ///     Payments provider token, obtained via Botfather
        /// </summary>
        public string ProviderToken { get; set; }

        /// <summary>
        ///     Unique deep-linking parameter that can be used to generate this invoice when used as a start parameter
        /// </summary>
        public string StartParameter { get; set; }

        /// <summary>
        ///     Three-letter ISO 4217 currency code, see more on currencies
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        ///     Price breakdown, a list of components (e.g. product price, tax, discount, delivery cost, delivery tax, bonus, etc.)
        /// </summary>
        public IEnumerable<LabeledPrice> Prices { get; set; }

        /// <summary>
        ///     JSON-encoded data about the invoice, which will be shared with the payment provider. A detailed description of
        ///     required fields should be provided by the payment provider.
        /// </summary>
        public string ProviderData { get; set; }

        /// <summary>
        ///     URL of the product photo for the invoice. Can be a photo of the goods or a marketing image for a service.
        /// </summary>
        public string PhotoUrl { get; set; }

        /// <summary>
        ///     Photo size
        /// </summary>
        public int PhotoSize { get; set; }

        /// <summary>
        ///     Photo width
        /// </summary>
        public int PhotoWidth { get; set; }

        /// <summary>
        ///     Photo height
        /// </summary>
        public int PhotoHeight { get; set; }

        /// <summary>
        ///     Pass True, if you require the user's full name to complete the order
        /// </summary>
        public bool NeedName { get; set; }

        /// <summary>
        ///     Pass True, if you require the user's phone number to complete the order
        /// </summary>
        public bool NeedPhoneNumber { get; set; }

        /// <summary>
        ///     Pass True, if you require the user's email to complete the order
        /// </summary>
        public bool NeedEmail { get; set; }

        /// <summary>
        ///     Pass True, if you require the user's shipping address to complete the order
        /// </summary>
        public bool NeedShippingAddress { get; set; }

        /// <summary>
        ///     Pass True, if the final price depends on the shipping method
        /// </summary>
        public bool IsFlexible { get; set; }

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
        ///     Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard
        ///     or to force a reply from the user.
        /// </summary>
        public InlineKeyboardMarkup ReplyMarkup { get; set; }

        /// <summary>
        ///     Pass True, if user's phone number should be sent to provider
        /// </summary>
        public bool SendPhoneNumberToProvider { get; set; }

        /// <summary>
        ///     Pass True, if user's email address should be sent to provider
        /// </summary>
        public bool SendEmailToProvider { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target private chat</param>
        public SendInvoiceParameters WithChatId(int chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Title" /> property.
        /// </summary>
        /// <param name="title">Product name</param>
        public SendInvoiceParameters WithTitle(string title)
        {
            Title = title;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Description" /> property.
        /// </summary>
        /// <param name="description">Product description</param>
        public SendInvoiceParameters WithDescription(string description)
        {
            Description = description;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Payload" /> property.
        /// </summary>
        /// <param name="payload">
        ///     Bot-defined invoice payload, 1-128 bytes. This will not be displayed to the user, use for your
        ///     internal processes.
        /// </param>
        public SendInvoiceParameters WithPayload(string payload)
        {
            Payload = payload;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ProviderToken" /> property.
        /// </summary>
        /// <param name="providerToken">Payments provider token, obtained via Botfather</param>
        public SendInvoiceParameters WithProviderToken(string providerToken)
        {
            ProviderToken = providerToken;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="StartParameter" /> property.
        /// </summary>
        /// <param name="startParameter">
        ///     Unique deep-linking parameter that can be used to generate this invoice when used as a
        ///     start parameter
        /// </param>
        public SendInvoiceParameters WithStartParameter(string startParameter)
        {
            StartParameter = startParameter;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Currency" /> property.
        /// </summary>
        /// <param name="currency">Three-letter ISO 4217 currency code, see more on currencies</param>
        public SendInvoiceParameters WithCurrency(string currency)
        {
            Currency = currency;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Prices" /> property.
        /// </summary>
        /// <param name="prices">
        ///     Price breakdown, a list of components (e.g. product price, tax, discount, delivery cost, delivery
        ///     tax, bonus, etc.)
        /// </param>
        public SendInvoiceParameters WithPrices(IEnumerable<LabeledPrice> prices)
        {
            Prices = prices;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ProviderData" /> property.
        /// </summary>
        /// <param name="providerData">
        ///     JSON-encoded data about the invoice, which will be shared with the payment provider. A
        ///     detailed description of required fields should be provided by the payment provider.
        /// </param>
        public SendInvoiceParameters WithProviderData(string providerData)
        {
            ProviderData = providerData;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="PhotoUrl" /> property.
        /// </summary>
        /// <param name="photoUrl">
        ///     URL of the product photo for the invoice. Can be a photo of the goods or a marketing image for a
        ///     service.
        /// </param>
        public SendInvoiceParameters WithPhotoUrl(string photoUrl)
        {
            PhotoUrl = photoUrl;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="PhotoSize" /> property.
        /// </summary>
        /// <param name="photoSize">Photo size</param>
        public SendInvoiceParameters WithPhotoSize(int photoSize)
        {
            PhotoSize = photoSize;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="PhotoWidth" /> property.
        /// </summary>
        /// <param name="photoWidth">Photo width</param>
        public SendInvoiceParameters WithPhotoWidth(int photoWidth)
        {
            PhotoWidth = photoWidth;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="PhotoHeight" /> property.
        /// </summary>
        /// <param name="photoHeight">Photo height</param>
        public SendInvoiceParameters WithPhotoHeight(int photoHeight)
        {
            PhotoHeight = photoHeight;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="NeedName" /> property.
        /// </summary>
        /// <param name="needName">Pass True, if you require the user's full name to complete the order</param>
        public SendInvoiceParameters WithNeedName(bool needName)
        {
            NeedName = needName;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="NeedPhoneNumber" /> property.
        /// </summary>
        /// <param name="needPhoneNumber">Pass True, if you require the user's phone number to complete the order</param>
        public SendInvoiceParameters WithNeedPhoneNumber(bool needPhoneNumber)
        {
            NeedPhoneNumber = needPhoneNumber;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="NeedEmail" /> property.
        /// </summary>
        /// <param name="needEmail">Pass True, if you require the user's email to complete the order</param>
        public SendInvoiceParameters WithNeedEmail(bool needEmail)
        {
            NeedEmail = needEmail;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="NeedShippingAddress" /> property.
        /// </summary>
        /// <param name="needShippingAddress">Pass True, if you require the user's shipping address to complete the order</param>
        public SendInvoiceParameters WithNeedShippingAddress(bool needShippingAddress)
        {
            NeedShippingAddress = needShippingAddress;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="IsFlexible" /> property.
        /// </summary>
        /// <param name="isFlexible">Pass True, if the final price depends on the shipping method</param>
        public SendInvoiceParameters WithIsFlexible(bool isFlexible)
        {
            IsFlexible = isFlexible;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="DisableNotification" /> property.
        /// </summary>
        /// <param name="disableNotification">
        ///     Sends the message silently. iOS users will not receive a notification, Android users
        ///     will receive a notification with no sound.
        /// </param>
        public SendInvoiceParameters WithDisableNotification(bool disableNotification)
        {
            DisableNotification = disableNotification;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ReplyToMessageId" /> property.
        /// </summary>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        public SendInvoiceParameters WithReplyToMessageId(int replyToMessageId)
        {
            ReplyToMessageId = replyToMessageId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ReplyMarkup" /> property.
        /// </summary>
        /// <param name="replyMarkup">
        ///     Additional interface options. A JSON-serialized object for a custom reply keyboard,
        ///     instructions to hide keyboard or to force a reply from the user.
        /// </param>
        public SendInvoiceParameters WithReplyMarkup(InlineKeyboardMarkup replyMarkup)
        {
            ReplyMarkup = replyMarkup;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="SendPhoneNumberToProvider" /> property.
        /// </summary>
        /// <param name="sendPhoneNumberToProvider">Pass True, if user's phone number should be sent to provider</param>
        public SendInvoiceParameters WithSendPhoneNumberToProvider(bool sendPhoneNumberToProvider)
        {
            SendPhoneNumberToProvider = sendPhoneNumberToProvider;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="SendEmailToProvider" /> property.
        /// </summary>
        /// <param name="sendEmailToProvider">Pass True, if user's email address should be sent to provider</param>
        public SendInvoiceParameters WithSendEmailToProvider(bool sendEmailToProvider)
        {
            SendEmailToProvider = sendEmailToProvider;
            return this;
        }
    }
}
