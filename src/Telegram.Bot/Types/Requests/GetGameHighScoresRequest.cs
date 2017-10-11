using System.Collections.Generic;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to get a game's highscores
    /// </summary>
    public class GetGameHighScoresRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetGameHighScoresRequest"/> class
        /// </summary>
        /// <param name="userId">Unique identifier of the target user.</param>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat.</param>
        /// <param name="messageId">Unique identifier of the sent message.</param>
        public GetGameHighScoresRequest(int userId, ChatId chatId, int messageId) : base("getGameHighScores",
            new Dictionary<string, object>
            {
                {"user_id", userId},
                {"chat_id", chatId},
                {"message_id", messageId}
            })
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetGameHighScoresRequest"/> class
        /// </summary>
        /// <param name="userId">Unique identifier of the target user.</param>
        /// <param name="inlineMessageId">Unique identifier of the inline message.</param>
        public GetGameHighScoresRequest(int userId, string inlineMessageId) : base("getGameHighScores",
            new Dictionary<string, object>
            {
                {"user_id", userId},
                {"inline_message_id", inlineMessageId}
            })
        {

        }
    }
}
