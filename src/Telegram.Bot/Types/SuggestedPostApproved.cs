// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes a service message about the approval of a suggested post.</summary>
public partial class SuggestedPostApproved
{
    /// <summary><em>Optional</em>. Message containing the suggested post. Note that the <see cref="Message"/> object in this field will not contain the <em>ReplyToMessage</em> field even if it itself is a reply.</summary>
    [JsonPropertyName("suggested_post_message")]
    public Message? SuggestedPostMessage { get; set; }

    /// <summary><em>Optional</em>. Amount paid for the post</summary>
    public SuggestedPostPrice? Price { get; set; }

    /// <summary>Date when the post will be published</summary>
    [JsonPropertyName("send_date")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime SendDate { get; set; }
}
