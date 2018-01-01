using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.InputMessageContents;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Types.InlineQueryResults
{
    /// <summary>
    /// Base Class for inline results send in response to an <see cref="InlineQuery"/>
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public abstract class InlineQueryResult
    {
        /// <summary>
        /// Unique identifier of this result
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Id { get; set; }

        /// <summary>
        /// Type of the result
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public InlineQueryResultType Type { get; protected set; }

        /// <summary>
        /// Content of the message to be sent
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public InputMessageContent InputMessageContent { get; set; }

        /// <summary>
        /// Inline keyboard attached to the message
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public InlineKeyboardMarkup ReplyMarkup { get; set; }
    }
}
