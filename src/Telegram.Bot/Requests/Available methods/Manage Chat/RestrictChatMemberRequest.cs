using Newtonsoft.Json.Converters;
using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to restrict a user in a supergroup. The bot must be an administrator in the
/// supergroup for this to work and must have the appropriate admin rights. Pass <see langword="true"/>
/// for all permissions to lift restrictions from a user. Returns <see langword="true"/> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class RestrictChatMemberRequest : RequestBase<bool>, IChatTargetable, IUserTargetable
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public ChatId ChatId { get; }

    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public long UserId { get; }

    /// <summary>
    /// New user permissions
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public ChatPermissions Permissions { get; }

    /// <summary>
    /// Date when restrictions will be lifted for the user, unix time. If user is restricted for
    /// more than 366 days or less than 30 seconds from the current time, they are considered to
    /// be restricted forever.
    /// </summary>
    [JsonConverter(typeof(UnixDateTimeConverter))]
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public DateTime? UntilDate { get; set; }

    /// <summary>
    /// Initializes a new request with chatId, userId and new user permissions
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="userId">Unique identifier of the target user</param>
    /// <param name="permissions">New user permissions</param>
    public RestrictChatMemberRequest(ChatId chatId, long userId, ChatPermissions permissions)
        : base("restrictChatMember")
    {
        ChatId = chatId;
        UserId = userId;
        Permissions = permissions;
    }
}
