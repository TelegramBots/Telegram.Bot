namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.SetGameScoreAsync" /> method.
    /// </summary>
    public class SetGameScoreParameters : ParametersBase
    {
        /// <summary>
        ///     Unique identifier of the target user.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///     The score.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// </summary>
        public long ChatId { get; set; }

        /// <summary>
        /// </summary>
        public int MessageId { get; set; }

        /// <summary>
        ///     Pass True, if the high score is allowed to decrease. This can be useful when fixing mistakes or banning cheaters
        /// </summary>
        public bool Force { get; set; }

        /// <summary>
        ///     Pass True, if the game message should not be automatically edited to include the current scoreboard
        /// </summary>
        public bool DisableEditMessage { get; set; }

        /// <summary>
        ///     Sets <see cref="UserId" /> property.
        /// </summary>
        /// <param name="userId">Unique identifier of the target user.</param>
        public SetGameScoreParameters WithUserId(int userId)
        {
            UserId = userId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Score" /> property.
        /// </summary>
        /// <param name="score">The score.</param>
        public SetGameScoreParameters WithScore(int score)
        {
            Score = score;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        public SetGameScoreParameters WithChatId(long chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="MessageId" /> property.
        /// </summary>
        public SetGameScoreParameters WithMessageId(int messageId)
        {
            MessageId = messageId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Force" /> property.
        /// </summary>
        /// <param name="force">
        ///     Pass True, if the high score is allowed to decrease. This can be useful when fixing mistakes or
        ///     banning cheaters
        /// </param>
        public SetGameScoreParameters WithForce(bool force)
        {
            Force = force;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="DisableEditMessage" /> property.
        /// </summary>
        /// <param name="disableEditMessage">
        ///     Pass True, if the game message should not be automatically edited to include the
        ///     current scoreboard
        /// </param>
        public SetGameScoreParameters WithDisableEditMessage(bool disableEditMessage)
        {
            DisableEditMessage = disableEditMessage;
            return this;
        }
    }
}