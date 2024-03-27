using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this request to approve a chat join request. The bot must be an administrator in the chat for this to
/// work and must have the <see cref="ChatPermissions.CanInviteUsers"/> administrator right.
/// Returns <see langword="true"/> on success.
/// </summary>
public class ApproveChatJoinRequest : RequestBase<bool>, IChatTargetable, IUserTargetable
{
    /// <inheritdoc/>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; init; }

    /// <summary>
    /// Unique identifier of the target user
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; init; }

    /// <summary>
    /// Initializes a new request with chatId and userId
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="userId">Unique identifier of the target user</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public ApproveChatJoinRequest(ChatId chatId, long userId)
        : this()
    {
        ChatId = chatId;
        UserId = userId;
    }

    /// <summary>
    /// Initializes a new request with chatId and userId
    /// </summary>
    public ApproveChatJoinRequest()
        : base("approveChatJoinRequest")
    { }
}
