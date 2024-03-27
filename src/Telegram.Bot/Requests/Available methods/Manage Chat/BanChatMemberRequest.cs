using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Serialization;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to ban a user in a group, a supergroup or a channel. In the case of supergroups
/// and channels, the user will not be able to return to the chat on their own using invite links,
/// etc., unless <see cref="UnbanChatMemberRequest">unbanned</see> first. The bot must be an
/// administrator in the chat for this to work and must have the appropriate admin rights.
/// Returns <see langword="true"/> on success.
/// </summary>
public class BanChatMemberRequest : RequestBase<bool>, IChatTargetable, IUserTargetable
{
    /// <inheritdoc />
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; init; }

    /// <inheritdoc />
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; init; }

    /// <summary>
    /// Date when the user will be unbanned. If user is banned for more than 366 days or less
    /// than 30 seconds from the current time they are considered to be banned forever.
    /// Applied for supergroups and channels only.
    /// </summary>
    [JsonConverter(typeof(UnixDateTimeConverter))]
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? UntilDate { get; set; }

    /// <summary>
    /// Pass <see langword="true"/> to delete all messages from the chat for the user that is being removed. If
    /// <see langword="false"/>, the user will be able to see messages in the group that were sent before
    /// the user was removed. Always <see langword="true"/> for supergroups and channels.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? RevokeMessages { get; set; }

    /// <summary>
    /// Initializes a new request with chatId and userId
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="userId">Unique identifier of the target user</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public BanChatMemberRequest(ChatId chatId, long userId)
        : this()
    {
        ChatId = chatId;
        UserId = userId;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    [JsonConstructor]
    public BanChatMemberRequest()
        : base("banChatMember")
    { }
}
