// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to set a new group sticker set for a supergroup. The bot must be an administrator in the chat for this to work and must have the appropriate administrator rights. Use the field <em>CanSetStickerSet</em> optionally returned in <see cref="TelegramBotClientExtensions.GetChat">GetChat</see> requests to check if the bot can use this method.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SetChatStickerSetRequest() : RequestBase<bool>("setChatStickerSet"), IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Name of the sticker set to be set as the group sticker set</summary>
    [JsonPropertyName("sticker_set_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string StickerSetName { get; set; }
}
