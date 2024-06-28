namespace Telegram.Bot.Requests;

/// <summary>Use this method to remove webhook integration if you decide to switch back to <see cref="TelegramBotClientExtensions.GetUpdatesAsync">GetUpdates</see>.<para>Returns: </para></summary>
public partial class DeleteWebhookRequest : RequestBase<bool>
{
    /// <summary>Pass <see langword="true"/> to drop all pending updates</summary>
    public bool DropPendingUpdates { get; set; }

    /// <summary>Instantiates a new <see cref="DeleteWebhookRequest"/></summary>
    public DeleteWebhookRequest() : base("deleteWebhook") { }
}
