using Telegram.Bot.Types;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Get data for high score tables. Will return the score of the specified user and several of his neighbors in a game. On success, returns an array of <see cref="GameHighScore"/>.
    /// </summary>
    public class GetGameHighScoresRequest : RequestBase<GameHighScore[]>
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
        /// Initializes a new request
        /// </summary>
        public GetGameHighScoresRequest()
            : base("getGameHighScores")
        { }

        /// <summary>
        /// Initializes a new request with userId, chatId and messageId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="chatId"></param>
        /// <param name="messageId"></param>
        public GetGameHighScoresRequest(int userId, long chatId, int messageId)
            : this()
        {
            UserId = userId;
            ChatId = chatId;
            MessageId = messageId;
        }
    }
}
