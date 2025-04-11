// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Edits a story previously posted by the bot on behalf of a managed business account. Requires the <em>CanManageStories</em> business bot right.<para>Returns: <see cref="Story"/> on success.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class EditStoryRequest() : FileRequestBase<Story>("editStory"), IBusinessConnectable
{
    /// <summary>Unique identifier of the business connection</summary>
    [JsonPropertyName("business_connection_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string BusinessConnectionId { get; set; }

    /// <summary>Unique identifier of the story to edit</summary>
    [JsonPropertyName("story_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int StoryId { get; set; }

    /// <summary>Content of the story</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputStoryContent Content { get; set; }

    /// <summary>Caption of the story, 0-2048 characters after entities parsing</summary>
    public string? Caption { get; set; }

    /// <summary>Mode for parsing entities in the story caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    [JsonPropertyName("parse_mode")]
    public ParseMode ParseMode { get; set; }

    /// <summary>A list of special entities that appear in the caption, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    [JsonPropertyName("caption_entities")]
    public IEnumerable<MessageEntity>? CaptionEntities { get; set; }

    /// <summary>A list of clickable areas to be shown on the story</summary>
    public IEnumerable<StoryArea>? Areas { get; set; }
}
