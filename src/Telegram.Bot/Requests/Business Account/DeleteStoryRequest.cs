// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Deletes a story previously posted by the bot on behalf of a managed business account. Requires the <em>CanManageStories</em> business bot right.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class DeleteStoryRequest() : RequestBase<bool>("deleteStory"), IBusinessConnectable
{
    /// <summary>Unique identifier of the business connection</summary>
    [JsonPropertyName("business_connection_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string BusinessConnectionId { get; set; }

    /// <summary>Unique identifier of the story to delete</summary>
    [JsonPropertyName("story_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int StoryId { get; set; }
}
