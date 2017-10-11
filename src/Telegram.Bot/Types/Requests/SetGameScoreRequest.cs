using System.Collections.Generic;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to set a game score
    /// </summary>
    public class SetGameScoreRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetGameScoreRequest"/> class
        /// </summary>
        /// <param name="userId">Unique identifier of the target user.</param>
        /// <param name="score">The score.</param>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat.</param>
        /// <param name="messageId">Unique identifier of the sent message.</param>
        /// <param name="force">Pass True, if the high score is allowed to decrease. This can be useful when fixing mistakes or banning cheaters</param>
        /// <param name="disableEditMessage">Pass True, if the game message should not be automatically edited to include the current scoreboard</param>
        /// <param name="editMessage">Pass True, if the game message should be automatically edited to include the current scoreboard.</param>
        public SetGameScoreRequest(int userId, int score, 
            ChatId chatId, int messageId,
            bool force = false,
            bool disableEditMessage = false,
            bool editMessage = false) : base("setGameScore",
                new Dictionary<string, object>
                {
                    {"user_id", userId},
                    {"score", score},
                    {"force", force },
                    {"disable_edit_message", disableEditMessage},
                    {"chat_id", chatId},
                    {"message_id", messageId},
                    {"edit_message", editMessage}
                })
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SetGameScoreRequest"/> class
        /// </summary>
        /// <param name="userId">Unique identifier of the target user.</param>
        /// <param name="score">The score.</param>
        /// <param name="inlineMessageId">Identifier of the inline message.</param>
        /// <param name="force">Pass True, if the high score is allowed to decrease. This can be useful when fixing mistakes or banning cheaters</param>
        /// <param name="disableEditMessage">Pass True, if the game message should not be automatically edited to include the current scoreboard</param>
        /// <param name="editMessage">Pass True, if the game message should be automatically edited to include the current scoreboard.</param>
        public SetGameScoreRequest(int userId, int score, 
            string inlineMessageId,
            bool force = false,
            bool disableEditMessage = false,
            bool editMessage = false) : base("setGameScore",
                new Dictionary<string, object>
                {
                    {"user_id", userId},
                    {"score", score},
                    {"force", force},
                    {"disable_edit_message", disableEditMessage},
                    {"inline_message_id", inlineMessageId},
                    {"edit_message", editMessage}
                })
        {

        }
    }
}
