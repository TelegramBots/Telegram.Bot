// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Changes the emoji status for a given user that previously allowed the bot to manage their emoji status via the Mini App method <a href="https://core.telegram.org/bots/webapps#initializing-mini-apps">requestEmojiStatusAccess</a>.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SetUserEmojiStatusRequest() : RequestBase<bool>("setUserEmojiStatus"), IUserTargetable
{
    /// <summary>Unique identifier of the target user</summary>
    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Custom emoji identifier of the emoji status to set. Pass an empty string to remove the status.</summary>
    [JsonPropertyName("emoji_status_custom_emoji_id")]
    public string? EmojiStatusCustomEmojiId { get; set; }

    /// <summary>Expiration date of the emoji status, if any</summary>
    [JsonPropertyName("emoji_status_expiration_date")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime? EmojiStatusExpirationDate { get; set; }
}
