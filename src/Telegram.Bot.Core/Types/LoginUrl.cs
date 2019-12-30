namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a parameter of the inline keyboard button used to automatically authorize a user
    /// </summary>
    public class LoginUrl
    {
        /// <summary>
        /// An HTTP URL to be opened with user authorization data added to the query string when the button is pressed
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Optional. New text of the button in forwarded messages
        /// </summary>
        public string ForwardText { get; set; }

        /// <summary>
        /// Optional. Username of a bot, which will be used for user authorization
        /// </summary>
        public string BotUsername { get; set; }

        /// <summary>
        /// Optional. Pass True to request the permission for your bot to send messages to the user
        /// </summary>
        public bool RequestWriteAccess { get; set; }
    }
}
