// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes a service message about the failed approval of a suggested post. Currently, only caused by insufficient user funds at the time of approval.</summary>
public partial class SuggestedPostApprovalFailed
{
    /// <summary><em>Optional</em>. Message containing the suggested post whose approval has failed. Note that the <see cref="Message"/> object in this field will not contain the <em>ReplyToMessage</em> field even if it itself is a reply.</summary>
    [JsonPropertyName("suggested_post_message")]
    public Message? SuggestedPostMessage { get; set; }

    /// <summary>Expected price of the post</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public SuggestedPostPrice Price { get; set; } = default!;
}
