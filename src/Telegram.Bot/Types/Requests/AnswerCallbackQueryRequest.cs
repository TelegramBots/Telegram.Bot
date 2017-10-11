using System.Collections.Generic;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to answer a callback query
    /// </summary>
    public class AnswerCallbackQueryRequest : ApiRequest
    {
        /// <summary>
        /// Initiaizes a new instance of the <see cref="AnswerCallbackQueryRequest"/> class
        /// </summary>
        /// <param name="callbackQueryId">Unique identifier for the query to be answered</param>
        /// <param name="text">Text of the notification. If not specified, nothing will be shown to the user</param>
        /// <param name="showAlert">If true, an alert will be shown by the client instead of a notification at the top of the chat screen. Defaults to false.</param>
        /// <param name="url">
        /// URL that will be opened by the user's client. If you have created a Game and accepted the conditions via @Botfather, specify the URL that opens your game – note that this will only work if the query comes from a callback_game button.
        /// Otherwise, you may use links like telegram.me/your_bot? start = XXXX that open your bot with a parameter.
        /// </param>
        /// <param name="cacheTime">The maximum amount of time in seconds that the result of the callback query may be cached client-side. Telegram apps will support caching starting in version 3.14.</param>
        public AnswerCallbackQueryRequest(string callbackQueryId, 
            string text = null,
            bool showAlert = false,
            string url = null,
            int cacheTime = 0) : base("answerCallbackQuery",
                new Dictionary<string, object>
                {
                    {"callback_query_id", callbackQueryId},
                    {"show_alert", showAlert},
                })
        {
            if (!string.IsNullOrEmpty(text))
                Parameters.Add("text", text);

            if (!string.IsNullOrEmpty(url))
                Parameters.Add("url", url);

            if (cacheTime != 0)
                Parameters.Add("cache_time", cacheTime);
        }
    }
}
