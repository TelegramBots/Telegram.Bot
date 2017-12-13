using Newtonsoft.Json;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Send a game. On success, the sent <see cref="Message"/> is returned.
    /// </summary>
    public class SendGameRequest : RequestBase<Message>,
                                   INotifiableMessage,
                                   IReplyMessage,
                                   IInlineReplyMarkupMessage
    {
        /// <summary>
        /// Unique identifier for the target chat
        /// </summary>
        public long ChatId { get; set; }

        /// <summary>
        /// Short name of the game, serves as the unique identifier for the game
        /// </summary>
        public string GameShortName { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool DisableNotification { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ReplyToMessageId { get; set; }

        /// <inheritdoc cref="IInlineReplyMarkupMessage.ReplyMarkup" />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public InlineKeyboardMarkup ReplyMarkup { get; set; }

        /// <summary>
        /// Initializes a new request
        /// </summary>
        public SendGameRequest()
            : base("sendGame")
        { }

        /// <summary>
        /// Initializes a new request with chatId and gameShortName
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="gameShortName">Short name of the game, serves as the unique identifier for the game</param>
        public SendGameRequest(long chatId, string gameShortName)
            : this()
        {
            ChatId = chatId;
            GameShortName = gameShortName;
        }
    }
}
