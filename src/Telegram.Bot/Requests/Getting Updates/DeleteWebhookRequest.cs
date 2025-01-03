namespace Telegram.Bot.Requests;

/// <summary>Use this method to remove webhook integration if you decide to switch back to <see cref="TelegramBotClientExtensions.GetUpdates">GetUpdates</see>.<para>Returns: </para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class DeleteWebhookRequest() : RequestBase<bool>("deleteWebhook")
{
    /// <summary>Pass <see langword="true"/> to drop all pending updates</summary>
    public bool DropPendingUpdates { get; set; }
}
