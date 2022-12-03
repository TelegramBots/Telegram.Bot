using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to get the number of members in a chat. Returns <c>int</c> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
[Obsolete("Use GetChatMemberCountRequest instead")]
public class GetChatMembersCountRequest : RequestBase<int>, IChatTargetable
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public ChatId ChatId { get; }

    /// <summary>
    /// Initializes a new request with chatId
    /// </summary>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target supergroup or channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    public GetChatMembersCountRequest(ChatId chatId)
        : base("getChatMembersCount")
    {
        ChatId = chatId;
    }
}
