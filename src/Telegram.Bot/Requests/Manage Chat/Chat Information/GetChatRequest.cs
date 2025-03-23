// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to get up-to-date information about the chat.<para>Returns: A <see cref="ChatFullInfo"/> object on success.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class GetChatRequest() : RequestBase<ChatFullInfo>("getChat"), IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target supergroup or channel (in the format <c>@channelusername</c>)</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }
}
