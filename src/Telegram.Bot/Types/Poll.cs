using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object contains information about a poll.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Poll
    {
        /// <summary>
        /// Unique poll identifier
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Id { get; set; }

        /// <summary>
        /// Poll question, 1-255 characters
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Question { get; set; }

        /// <summary>
        /// List of poll options
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public PollOption[] Options { get; set; }

        /// <summary>
        /// Total number of users that voted in the poll
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public int TotalVoterCount { get; set; }

        /// <summary>
        /// True, if the poll is closed
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public bool IsClosed { get; set; }

        /// <summary>
        /// True, if the poll is anonymous
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public bool IsAnonymous { get; set; }

        /// <summary>
        /// Poll type, currently can be “regular” or “quiz”
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Type { get; set; }

        /// <summary>
        /// True, if the poll allows multiple answers
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public bool AllowsMultipleAnswers { get; set; }

        /// <summary>
        /// Optional. 0-based identifier of the correct answer option. Available only for polls in the quiz mode, which are closed, or was sent (not forwarded) by the bot or to the private chat with the bot.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? CorrectOptionId { get; set; }
    }
}
