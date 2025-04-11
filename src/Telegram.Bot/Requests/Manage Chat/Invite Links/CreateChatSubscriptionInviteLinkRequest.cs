// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to create a <a href="https://telegram.org/blog/superchannels-star-reactions-subscriptions#star-subscriptions">subscription invite link</a> for a channel chat. The bot must have the <em>CanInviteUsers</em> administrator rights. The link can be edited using the method <see cref="TelegramBotClientExtensions.EditChatSubscriptionInviteLink">EditChatSubscriptionInviteLink</see> or revoked using the method <see cref="TelegramBotClientExtensions.RevokeChatInviteLink">RevokeChatInviteLink</see>.<para>Returns: The new invite link as a <see cref="ChatInviteLink"/> object.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class CreateChatSubscriptionInviteLinkRequest() : RequestBase<ChatInviteLink>("createChatSubscriptionInviteLink"), IChatTargetable
{
    /// <summary>Unique identifier for the target channel chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>The number of seconds the subscription will be active for before the next payment. Currently, it must always be 2592000 (30 days).</summary>
    [JsonPropertyName("subscription_period")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int SubscriptionPeriod { get; set; }

    /// <summary>The amount of Telegram Stars a user must pay initially and after each subsequent subscription period to be a member of the chat; 1-10000</summary>
    [JsonPropertyName("subscription_price")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int SubscriptionPrice { get; set; }

    /// <summary>Invite link name; 0-32 characters</summary>
    public string? Name { get; set; }
}
