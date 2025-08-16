// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Contains parameters of a post that is being suggested by the bot.</summary>
public partial class SuggestedPostParameters
{
    /// <summary><em>Optional</em>. Proposed price for the post. If the field is omitted, then the post is unpaid.</summary>
    public SuggestedPostPrice? Price { get; set; }

    /// <summary><em>Optional</em>. Proposed send date of the post. If specified, then the date must be between 300 second and 2678400 seconds (30 days) in the future. If the field is omitted, then the post can be published at any time within 30 days at the sole discretion of the user who approves it.</summary>
    [JsonPropertyName("send_date")]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime? SendDate { get; set; }
}
