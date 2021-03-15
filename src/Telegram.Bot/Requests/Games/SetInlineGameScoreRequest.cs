﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Set the score of the specified user in a game. On success returns True. Returns an error, if the new score is not greater than the user's current score in the chat and force is False.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class SetInlineGameScoreRequest : RequestBase<bool>,
                                             IInlineMessage
    {
        /// <summary>
        /// User identifier
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public long UserId { get; }

        /// <summary>
        /// New score, must be non-negative
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public int Score { get; }

        /// <inheritdoc />
        [JsonProperty(Required = Required.Always)]
        public string InlineMessageId { get; }

        /// <summary>
        /// Pass True, if the high score is allowed to decrease. This can be useful when fixing mistakes or banning cheaters.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Force { get; set; }

        /// <summary>
        /// Pass True, if the game message should not be automatically edited to include the current scoreboard
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool DisableEditMessage { get; set; }

        /// <summary>
        /// Initializes a new request with userId, inlineMessageId and new score
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <param name="inlineMessageId">Identifier of the inline message</param>
        /// <param name="score">New score, must be non-negative</param>
        public SetInlineGameScoreRequest(long userId, int score, string inlineMessageId)
            : base("setGameScore")
        {
            UserId = userId;
            Score = score;
            InlineMessageId = inlineMessageId;
        }
    }
}
