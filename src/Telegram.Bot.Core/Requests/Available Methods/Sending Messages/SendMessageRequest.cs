using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Send text messages
    /// </summary>
    public class SendMessageRequest : RequestBase<Message>,
                                      INotifiableMessage,
                                      IReplyMessage,
                                      IFormattableMessage,
                                      IReplyMarkupMessage<IReplyMarkup>
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel
        /// </summary>
        public ChatId ChatId { get; }

        /// <summary>
        /// Text of the message to be sent
        /// </summary>
        public string Text { get; }

        /// <inheritdoc />
        public ParseMode ParseMode { get; set; }

        /// <summary>
        /// Disables link previews for links in this message
        /// </summary>
        public bool DisableWebPagePreview { get; set; }

        /// <inheritdoc />
        public bool DisableNotification { get; set; }

        /// <inheritdoc />
        public int ReplyToMessageId { get; set; }

        /// <inheritdoc />
        public IReplyMarkup ReplyMarkup { get; set; }

        /// <summary>
        /// Initializes a new request with chatId and text
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="text">Text of the message to be sent</param>
        public SendMessageRequest(ChatId chatId, string text)
            : base("sendMessage")
        {
            ChatId = chatId;
            Text = text;
        }
    }
}
