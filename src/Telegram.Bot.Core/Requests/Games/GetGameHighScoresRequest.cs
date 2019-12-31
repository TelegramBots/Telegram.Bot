﻿using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
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
        public long ChatId { get; }

        /// <summary>
        /// User identifier
        /// </summary>
        public int UserId { get; }

        /// <summary>
        /// Identifier of the sent message
        /// </summary>
        public int MessageId { get; }

        /// <summary>
        /// Initializes a new request with userId, chatId and messageId
        /// </summary>
        /// <param name="userId">Target user id</param>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="messageId">Identifier of the sent message</param>
        public GetGameHighScoresRequest(int userId, long chatId, int messageId)
            : base("getGameHighScores")
        {
            UserId = userId;
            ChatId = chatId;
            MessageId = messageId;
        }
    }
}
