// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to revoke the current token of a managed bot and generate a new one.<para>Returns: The new token as <em>String</em> on success.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class ReplaceManagedBotTokenRequest() : RequestBase<string>("replaceManagedBotToken"), IUserTargetable
{
    /// <summary>User identifier of the managed bot whose token will be replaced</summary>
    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }
}
