using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this request to decline a chat join request. The bot must be an administrator in the chat for this to
/// work and must have the <see cref="ChatPermissions.CanInviteUsers"/> administrator right.
/// Returns <see langword="true"/> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class DeclineChatJoinRequest : RequestBase<bool>, IChatTargetable, IUserTargetable
{
    /// <inheritdoc/>
    [JsonProperty(Required = Required.Always)]
    public ChatId ChatId { get; }

    /// <summary>
    /// Unique identifier of the target user
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public long UserId { get; }

    /// <summary>
    /// Initializes a new request with chatId and userId
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="userId">Unique identifier of the target user</param>
    public DeclineChatJoinRequest(ChatId chatId, long userId)
        : base("declineChatJoinRequest")
    {
        ChatId = chatId;
        UserId = userId;
    }
}
