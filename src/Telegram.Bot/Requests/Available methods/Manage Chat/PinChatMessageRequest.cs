using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to add a message to the list of pinned messages in a chat. If the chat is not a
/// private chat, the bot must be an administrator in the chat for this to work and must have the
/// '<see cref="ChatPermissions.CanPinMessages"/>' admin right in a supergroup or
/// '<see cref="ChatMemberAdministrator.CanEditMessages"/>' admin right in a channel.
/// Returns <see langword="true"/> on success.
/// </summary>
public class PinChatMessageRequest : RequestBase<bool>, IChatTargetable
{
    /// <inheritdoc />
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; init; }

    /// <summary>
    /// Identifier of a message to pin
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; init; }

    /// <inheritdoc cref="Abstractions.Documentation.DisableNotification"/>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? DisableNotification { get; set; }

    /// <summary>
    /// Initializes a new request with chatId and messageId
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageId">Identifier of a message to pin</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public PinChatMessageRequest(ChatId chatId, int messageId)
        : this()
    {
        ChatId = chatId;
        MessageId = messageId;
    }

    /// <summary>
    /// Initializes a new request with chatId and messageId
    /// </summary>
    public PinChatMessageRequest()
        : base("pinChatMessage")
    { }
}
