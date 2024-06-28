namespace Telegram.Bot.Requests;

/// <summary>Use this method to set a new group sticker set for a supergroup. The bot must be an administrator in the chat for this to work and must have the appropriate administrator rights. Use the field <em>CanSetStickerSet</em> optionally returned in <see cref="TelegramBotClientExtensions.GetChatAsync">GetChat</see> requests to check if the bot can use this method.<para>Returns: </para></summary>
public partial class SetChatStickerSetRequest : RequestBase<bool>, IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Name of the sticker set to be set as the group sticker set</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string StickerSetName { get; set; }

    /// <summary>Initializes an instance of <see cref="SetChatStickerSetRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</param>
    /// <param name="stickerSetName">Name of the sticker set to be set as the group sticker set</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public SetChatStickerSetRequest(ChatId chatId, string stickerSetName) : this()
    {
        ChatId = chatId;
        StickerSetName = stickerSetName;
    }

    /// <summary>Instantiates a new <see cref="SetChatStickerSetRequest"/></summary>
    public SetChatStickerSetRequest() : base("setChatStickerSet") { }
}
