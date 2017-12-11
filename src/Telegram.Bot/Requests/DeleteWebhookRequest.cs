namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Remove webhook integration if you decide to switch back to getUpdates.
    /// </summary>
    public class DeleteWebhookRequest : RequestBase<bool>
    {
        /// <summary>
        /// Initializes a new request
        /// </summary>
        public DeleteWebhookRequest()
            : base("deleteWebhook")
        { }
    }
}
