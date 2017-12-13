using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Set the score of the specified user in a game. On success returns the edited <see cref="Message"/>. Returns an error, if the new score is not greater than the user's current score in the chat and force is False.
    /// </summary>
    public class SetGameScoreRequest : RequestBase<Message>
    {
        /// <summary>
        /// Unique identifier for the target chat
        /// </summary>
        public long ChatId { get; set; }

        /// <summary>
        /// User identifier
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Identifier of the sent message
        /// </summary>
        public int MessageId { get; set; }

        /// <summary>
        /// New score, must be non-negative
        /// </summary>
        public int Score { get; set; }

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
        /// Initializes a new request
        /// </summary>
        public SetGameScoreRequest()
            : base("setGameScore")
        { }

        /// <summary>
        /// Initializes a new request
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="userId">User identifier</param>
        /// <param name="messageId">Identifier of the sent message</param>
        /// <param name="score">New score, must be non-negative</param>
        public SetGameScoreRequest(long chatId, int userId, int messageId, int score)
            : this()
        {
            ChatId = chatId;
            UserId = userId;
            MessageId = messageId;
            Score = score;
        }
    }
}
