// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes a service message about the rejection of a suggested post.</summary>
public partial class SuggestedPostDeclined
{
    /// <summary><em>Optional</em>. Message containing the suggested post. Note that the <see cref="Message"/> object in this field will not contain the <em>ReplyToMessage</em> field even if it itself is a reply.</summary>
    [JsonPropertyName("suggested_post_message")]
    public Message? SuggestedPostMessage { get; set; }

    /// <summary><em>Optional</em>. Comment with which the post was declined</summary>
    public string? Comment { get; set; }
}
