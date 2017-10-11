using System.Collections.Generic;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to send an invoice
    /// </summary>
    public class SendInvoiceRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendInvoiceRequest"/> class
        /// </summary>
        /// <param name="chatId">Unique identifier for the target private chat</param>
        /// <param name="title">Product name</param>
        /// <param name="description">Product description</param>
        /// <param name="payload">Bot-defined invoice payload, 1-128 bytes. This will not be displayed to the user, use for your internal processes.</param>
        /// <param name="providerToken">Payments provider token, obtained via Botfather</param>
        /// <param name="startParameter">Unique deep-linking parameter that can be used to generate this invoice when used as a start parameter</param>
        /// <param name="currency">Three-letter ISO 4217 currency code, see more on currencies</param>
        /// <param name="prices">Price breakdown, a list of components (e.g. product price, tax, discount, delivery cost, delivery tax, bonus, etc.)</param>
        /// <param name="photoUrl">URL of the product photo for the invoice. Can be a photo of the goods or a marketing image for a service.</param>
        /// <param name="photoSize">Photo size</param>
        /// <param name="photoWidth">Photo width</param>
        /// <param name="photoHeight">Photo height</param>
        /// <param name="needName">Pass True, if you require the user's full name to complete the order</param>
        /// <param name="needPhoneNumber">Pass True, if you require the user's phone number to complete the order</param>
        /// <param name="needEmail">Pass True, if you require the user's email to complete the order</param>
        /// <param name="needShippingAddress">Pass True, if you require the user's shipping address to complete the order</param>
        /// <param name="isFlexible">Pass True, if the final price depends on the shipping method</param>
        /// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        public SendInvoiceRequest(ChatId chatId, string title, string description,
            string payload, string providerToken, string startParameter, string currency,
            LabeledPrice[] prices, string photoUrl = null, int photoSize = 0, int photoWidth = 0,
            int photoHeight = 0, bool needName = false, bool needPhoneNumber = false,
            bool needEmail = false, bool needShippingAddress = false, bool isFlexible = false,
            bool disableNotification = false, int replyToMessageId = 0, InlineKeyboardMarkup replyMarkup = null) : base("sendInvoice",
                new Dictionary<string, object>
                {
                    {"description", description},
                    {"payload", payload},
                    {"provider_token", providerToken},
                    {"start_parameter", startParameter},
                    {"currency", currency},
                    {"prices", prices},
                })
        {
            if (photoUrl != null)
                Parameters.Add("photo_url", photoUrl);

            if (photoSize != 0)
                Parameters.Add("photo_size", photoSize);

            if (photoWidth != 0)
                Parameters.Add("photo_width", photoWidth);

            if (photoHeight != 0)
                Parameters.Add("photo_height", photoHeight);

            if (needName)
                Parameters.Add("need_name", true);

            if (needPhoneNumber)
                Parameters.Add("need_phone_number", true);

            if (needEmail)
                Parameters.Add("need_email", true);

            if (needShippingAddress)
                Parameters.Add("need_shipping_address", true);

            if (isFlexible)
                Parameters.Add("is_flexible", true);
        }
    }
}
