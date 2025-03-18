// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to set a custom title for an administrator in a supergroup promoted by the bot.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SetChatAdministratorCustomTitleRequest() : RequestBase<bool>("setChatAdministratorCustomTitle"), IChatTargetable, IUserTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Unique identifier of the target user</summary>
    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>New custom title for the administrator; 0-16 characters, emoji are not allowed</summary>
    [JsonPropertyName("custom_title")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string CustomTitle { get; set; }
}
