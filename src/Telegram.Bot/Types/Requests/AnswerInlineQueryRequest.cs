using System.Collections.Generic;
using Telegram.Bot.Types.InlineQueryResults;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to answer an inline query
    /// </summary>
    public class AnswerInlineQueryRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnswerInlineQueryRequest"/> class
        /// </summary>
        /// <param name="inlineQueryId">Unique identifier for answered query</param>
        /// <param name="results">A array of results for the inline query</param>
        /// <param name="cacheTime">The maximum amount of time in seconds the result of the inline query may be cached on the server</param>
        /// <param name="isPersonal">Pass <c>true</c>, if results may be cached on the server side only for the user that sent the query. By default, results may be returned to any user who sends the same query</param>
        /// <param name="nextOffset">Pass the offset that a client should send in the next query with the same text to receive more results. Pass an empty string if there are no more results or if you don't support pagination. Offset length can't exceed 64 bytes.</param>
        /// <param name="switchPmText">If passed, clients will display a button with specified text that switches the user to a private chat with the bot and sends the bot a start message with the parameter switch_pm_parameter</param>
        /// <param name="switchPmParameter">Parameter for the start message sent to the bot when user presses the switch button</param>
        public AnswerInlineQueryRequest(string inlineQueryId, InlineQueryResult[] results,
            int? cacheTime = null,
            bool isPersonal = false, string nextOffset = null,
            string switchPmText = null,
            string switchPmParameter = null) : base("answerInlineQuery",
                new Dictionary<string, object>
                {
                    {"inline_query_id", inlineQueryId},
                    {"results", results},
                    {"is_personal", isPersonal}
                })
        {
            if (cacheTime.HasValue)
                Parameters.Add("cache_time", cacheTime);

            if (!string.IsNullOrWhiteSpace(nextOffset))
                Parameters.Add("next_offset", nextOffset);

            if (!string.IsNullOrWhiteSpace(switchPmText))
                Parameters.Add("switch_pm_text", switchPmText);

            if (!string.IsNullOrWhiteSpace(switchPmParameter))
                Parameters.Add("switch_pm_parameter", switchPmParameter);
        }
    }
}
