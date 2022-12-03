using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to delete a group sticker set from a supergroup. The bot must be an administrator
/// in the chat for this to work and must have the appropriate admin rights. Use the field
/// <see cref="Types.Chat.CanSetStickerSet"/> optionally returned in <see cref="GetChatRequest"/>
/// requests to check if the bot can use this method. Returns <see langword="true"/> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class DeleteChatStickerSetRequest : RequestBase<bool>, IChatTargetable
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public ChatId ChatId { get; }

    /// <summary>
    /// Initializes a new request with chatId
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    public DeleteChatStickerSetRequest(ChatId chatId)
        : base("deleteChatStickerSet")
    {
        ChatId = chatId;
    }
}
