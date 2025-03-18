// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to remove webhook integration if you decide to switch back to <see cref="TelegramBotClientExtensions.GetUpdates">GetUpdates</see>.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class DeleteWebhookRequest() : RequestBase<bool>("deleteWebhook")
{
    /// <summary>Pass <see langword="true"/> to drop all pending updates</summary>
    [JsonPropertyName("drop_pending_updates")]
    public bool DropPendingUpdates { get; set; }
}
