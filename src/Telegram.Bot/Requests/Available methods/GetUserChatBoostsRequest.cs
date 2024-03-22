using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to get the list of boosts added to a chat by a user.
/// Requires administrator rights in the chat.
/// Returns a <see cref="UserChatBoosts"/> object.
/// </summary>
public class GetUserChatBoostsRequest : RequestBase<UserChatBoosts>
{
    /// <summary>
    /// Unique identifier for the chat or username of the channel (in the format <c>@channelusername</c>)
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId  { get; init; }

    /// <summary>
    /// Unique identifier of the target user
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; init; }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public GetUserChatBoostsRequest()
        : base("getUserChatBoosts")
    { }

    /// <summary>
    /// Initializes a new request with chatId and userId
    /// </summary>
    /// <param name="chatId">
    /// Unique identifier for the chat or username of the channel (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="userId">Unique identifier of the target user</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public GetUserChatBoostsRequest(ChatId chatId, long userId)
        : this()
    {
        ChatId = chatId;
        UserId = userId;
    }
}
