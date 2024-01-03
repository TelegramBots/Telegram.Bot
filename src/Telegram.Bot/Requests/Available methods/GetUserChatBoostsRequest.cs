// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to get the list of boosts added to a chat by a user.
/// Requires administrator rights in the chat.
/// Returns a <see cref="UserChatBoosts"/> object.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class GetUserChatBoostsRequest : RequestBase<UserChatBoosts>
{
    /// <summary>
    /// Unique identifier for the chat or username of the channel (in the format <c>@channelusername</c>)
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public ChatId ChatId  { get; }

    /// <summary>
    /// Unique identifier of the target user
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public long UserId { get; }

    /// <summary>
    /// Initializes a new request with chatId and userId
    /// </summary>
    /// <param name="chatId">
    /// Unique identifier for the chat or username of the channel (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="userId">Unique identifier of the target user</param>
    public GetUserChatBoostsRequest(ChatId chatId, long userId)
        : base("getUserChatBoosts")
    {
        ChatId = chatId;
        UserId = userId;
    }
}
