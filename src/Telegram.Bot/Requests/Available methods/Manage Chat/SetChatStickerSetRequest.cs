using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to set a new group sticker set for a supergroup. The bot must be an administrator in
/// the chat for this to work and must have the appropriate admin rights. Use the field
/// <see cref="Chat.CanSetStickerSet"/> optionally returned in <see cref="GetChatRequest"/> requests to
/// check if the bot can use this method. Returns <see langword="true"/> on success.
/// </summary>
public class SetChatStickerSetRequest : RequestBase<bool>, IChatTargetable
{
    /// <inheritdoc />
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; init; }

    /// <summary>
    /// Name of the sticker set to be set as the group sticker set
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string StickerSetName { get; init; }

    /// <summary>
    /// Initializes a new request with chatId and new stickerSetName
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="stickerSetName">Name of the sticker set to be set as the group sticker set</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public SetChatStickerSetRequest(ChatId chatId, string stickerSetName)
        : this()
    {
        ChatId = chatId;
        StickerSetName = stickerSetName;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public SetChatStickerSetRequest()
        : base("setChatStickerSet")
    { }
}
