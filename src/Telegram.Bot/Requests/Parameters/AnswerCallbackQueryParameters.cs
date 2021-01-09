namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.AnswerCallbackQueryAsync" /> method.
    /// </summary>
    public class AnswerCallbackQueryParameters : ParametersBase
    {
        /// <summary>
        ///     Unique identifier for the query to be answered
        /// </summary>
        public string CallbackQueryId { get; set; }

        /// <summary>
        ///     Text of the notification. If not specified, nothing will be shown to the user
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///     If true, an alert will be shown by the client instead of a notification at the top of the chat screen. Defaults to
        ///     false.
        /// </summary>
        public bool ShowAlert { get; set; }

        /// <summary>
        ///     URL that will be opened by the user's client. If you have created a Game and accepted the conditions via
        ///     @Botfather, specify the URL that opens your game — note that this will only work if the query comes from a
        ///     callback_game button.
        ///     Otherwise, you may use links like telegram.me/your_bot? start = XXXX that open your bot with a parameter.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///     The maximum amount of time in seconds that the result of the callback query may be cached client-side. Telegram
        ///     apps will support caching starting in version 3.14.
        /// </summary>
        public int CacheTime { get; set; }

        /// <summary>
        ///     Sets <see cref="CallbackQueryId" /> property.
        /// </summary>
        /// <param name="callbackQueryId">Unique identifier for the query to be answered</param>
        public AnswerCallbackQueryParameters WithCallbackQueryId(string callbackQueryId)
        {
            CallbackQueryId = callbackQueryId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Text" /> property.
        /// </summary>
        /// <param name="text">Text of the notification. If not specified, nothing will be shown to the user</param>
        public AnswerCallbackQueryParameters WithText(string text)
        {
            Text = text;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ShowAlert" /> property.
        /// </summary>
        /// <param name="showAlert">
        ///     If true, an alert will be shown by the client instead of a notification at the top of the chat
        ///     screen. Defaults to false.
        /// </param>
        public AnswerCallbackQueryParameters WithShowAlert(bool showAlert)
        {
            ShowAlert = showAlert;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Url" /> property.
        /// </summary>
        /// <param name="url">
        ///     URL that will be opened by the user's client. If you have created a Game and accepted the conditions via
        ///     @Botfather, specify the URL that opens your game — note that this will only work if the query comes from a
        ///     callback_game button.
        ///     Otherwise, you may use links like telegram.me/your_bot? start = XXXX that open your bot with a parameter.
        /// </param>
        public AnswerCallbackQueryParameters WithUrl(string url)
        {
            Url = url;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="CacheTime" /> property.
        /// </summary>
        /// <param name="cacheTime">
        ///     The maximum amount of time in seconds that the result of the callback query may be cached
        ///     client-side. Telegram apps will support caching starting in version 3.14.
        /// </param>
        public AnswerCallbackQueryParameters WithCacheTime(int cacheTime)
        {
            CacheTime = cacheTime;
            return this;
        }
    }
}
