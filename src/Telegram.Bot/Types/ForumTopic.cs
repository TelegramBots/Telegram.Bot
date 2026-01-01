// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents a forum topic.</summary>
public partial class ForumTopic
{
    /// <summary>Unique identifier of the forum topic</summary>
    [JsonPropertyName("message_thread_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int MessageThreadId { get; set; }

    /// <summary>Name of the topic</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Name { get; set; } = default!;

    /// <summary>Color of the topic icon in RGB format</summary>
    [JsonPropertyName("icon_color")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int IconColor { get; set; }

    /// <summary><em>Optional</em>. Unique identifier of the custom emoji shown as the topic icon</summary>
    [JsonPropertyName("icon_custom_emoji_id")]
    public string? IconCustomEmojiId { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the name of the topic wasn't specified explicitly by its creator and likely needs to be changed by the bot</summary>
    [JsonPropertyName("is_name_implicit")]
    public bool IsNameImplicit { get; set; }
}
