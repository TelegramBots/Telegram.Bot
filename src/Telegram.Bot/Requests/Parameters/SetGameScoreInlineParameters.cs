namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.SetGameScoreAsync" /> method.
    /// </summary>
    public class SetGameScoreInlineParameters : ParametersBase
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
        ///     Identifier of the inline message.
        /// </summary>
        public string InlineMessageId { get; set; }

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
        public SetGameScoreInlineParameters WithUserId(int userId)
        {
            UserId = userId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Score" /> property.
        /// </summary>
        /// <param name="score">The score.</param>
        public SetGameScoreInlineParameters WithScore(int score)
        {
            Score = score;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="InlineMessageId" /> property.
        /// </summary>
        /// <param name="inlineMessageId">Identifier of the inline message.</param>
        public SetGameScoreInlineParameters WithInlineMessageId(string inlineMessageId)
        {
            InlineMessageId = inlineMessageId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Force" /> property.
        /// </summary>
        /// <param name="force">
        ///     Pass True, if the high score is allowed to decrease. This can be useful when fixing mistakes or
        ///     banning cheaters
        /// </param>
        public SetGameScoreInlineParameters WithForce(bool force)
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
        public SetGameScoreInlineParameters WithDisableEditMessage(bool disableEditMessage)
        {
            DisableEditMessage = disableEditMessage;
            return this;
        }
    }
}