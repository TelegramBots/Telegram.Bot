// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Verifies a chat <a href="https://telegram.org/verify#third-party-verification">on behalf of the organization</a> which is represented by the bot.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class VerifyChatRequest() : RequestBase<bool>("verifyChat"), IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>). Channel direct messages chats can't be verified.</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Custom description for the verification; 0-70 characters. Must be empty if the organization isn't allowed to provide a custom verification description.</summary>
    [JsonPropertyName("custom_description")]
    public string? CustomDescription { get; set; }
}
