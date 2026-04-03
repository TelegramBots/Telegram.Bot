// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to get the token of a managed bot.<para>Returns: The token as <em>String</em> on success.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class GetManagedBotTokenRequest() : RequestBase<string>("getManagedBotToken"), IUserTargetable
{
    /// <summary>User identifier of the managed bot whose token will be returned</summary>
    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }
}
