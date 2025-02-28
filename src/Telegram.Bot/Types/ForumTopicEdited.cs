// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents a service message about an edited forum topic.</summary>
public partial class ForumTopicEdited
{
    /// <summary><em>Optional</em>. New name of the topic, if it was edited</summary>
    public string? Name { get; set; }

    /// <summary><em>Optional</em>. New identifier of the custom emoji shown as the topic icon, if it was edited; an empty string if the icon was removed</summary>
    [JsonPropertyName("icon_custom_emoji_id")]
    public string? IconCustomEmojiId { get; set; }
}
