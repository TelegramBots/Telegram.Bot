using System.Collections.Generic;
using Telegram.Bot.Types.InlineQueryResults;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.AnswerInlineQueryAsync" /> method.
    /// </summary>
    public class AnswerInlineQueryParameters : ParametersBase
    {
        /// <summary>
        ///     Unique identifier for answered query
        /// </summary>
        public string InlineQueryId { get; set; }

        /// <summary>
        ///     A array of results for the inline query
        /// </summary>
        public IEnumerable<InlineQueryResultBase> Results { get; set; }

        /// <summary>
        ///     The maximum amount of time in seconds the result of the inline query may be cached on the server
        /// </summary>
        public int? CacheTime { get; set; }

        /// <summary>
        ///     Pass
        /// </summary>
        public bool IsPersonal { get; set; }

        /// <summary>
        ///     Pass the offset that a client should send in the next query with the same text to receive more results. Pass an
        ///     empty string if there are no more results or if you don't support pagination. Offset length can't exceed 64 bytes.
        /// </summary>
        public string NextOffset { get; set; }

        /// <summary>
        ///     If passed, clients will display a button with specified text that switches the user to a private chat with the bot
        ///     and sends the bot a start message with the parameter switch_pm_parameter
        /// </summary>
        public string SwitchPmText { get; set; }

        /// <summary>
        ///     Parameter for the start message sent to the bot when user presses the switch button
        /// </summary>
        public string SwitchPmParameter { get; set; }

        /// <summary>
        ///     Sets <see cref="InlineQueryId" /> property.
        /// </summary>
        /// <param name="inlineQueryId">Unique identifier for answered query</param>
        public AnswerInlineQueryParameters WithInlineQueryId(string inlineQueryId)
        {
            InlineQueryId = inlineQueryId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Results" /> property.
        /// </summary>
        /// <param name="results">A array of results for the inline query</param>
        public AnswerInlineQueryParameters WithResults(IEnumerable<InlineQueryResultBase> results)
        {
            Results = results;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="CacheTime" /> property.
        /// </summary>
        /// <param name="cacheTime">
        ///     The maximum amount of time in seconds the result of the inline query may be cached on the
        ///     server
        /// </param>
        public AnswerInlineQueryParameters WithCacheTime(int? cacheTime)
        {
            CacheTime = cacheTime;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="IsPersonal" /> property.
        /// </summary>
        /// <param name="isPersonal">Pass </param>
        public AnswerInlineQueryParameters WithIsPersonal(bool isPersonal)
        {
            IsPersonal = isPersonal;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="NextOffset" /> property.
        /// </summary>
        /// <param name="nextOffset">
        ///     Pass the offset that a client should send in the next query with the same text to receive more
        ///     results. Pass an empty string if there are no more results or if you don't support pagination. Offset length can't
        ///     exceed 64 bytes.
        /// </param>
        public AnswerInlineQueryParameters WithNextOffset(string nextOffset)
        {
            NextOffset = nextOffset;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="SwitchPmText" /> property.
        /// </summary>
        /// <param name="switchPmText">
        ///     If passed, clients will display a button with specified text that switches the user to a
        ///     private chat with the bot and sends the bot a start message with the parameter switch_pm_parameter
        /// </param>
        public AnswerInlineQueryParameters WithSwitchPmText(string switchPmText)
        {
            SwitchPmText = switchPmText;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="SwitchPmParameter" /> property.
        /// </summary>
        /// <param name="switchPmParameter">Parameter for the start message sent to the bot when user presses the switch button</param>
        public AnswerInlineQueryParameters WithSwitchPmParameter(string switchPmParameter)
        {
            SwitchPmParameter = switchPmParameter;
            return this;
        }
    }
}