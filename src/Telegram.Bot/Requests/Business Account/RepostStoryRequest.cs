// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Reposts a story on behalf of a business account from another business account. Both business accounts must be managed by the same bot, and the story on the source account must have been posted (or reposted) by the bot. Requires the <em>CanManageStories</em> business bot right for both business accounts.<para>Returns: <see cref="Story"/> on success.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class RepostStoryRequest() : RequestBase<Story>("repostStory"), IBusinessConnectable
{
    /// <summary>Unique identifier of the business connection</summary>
    [JsonPropertyName("business_connection_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string BusinessConnectionId { get; set; }

    /// <summary>Unique identifier of the chat which posted the story that should be reposted</summary>
    [JsonPropertyName("from_chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long FromChatId { get; set; }

    /// <summary>Unique identifier of the story that should be reposted</summary>
    [JsonPropertyName("from_story_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int FromStoryId { get; set; }

    /// <summary>Period after which the story is moved to the archive, in seconds; must be one of <c>6 * 3600</c>, <c>12 * 3600</c>, <c>86400</c>, or <c>2 * 86400</c></summary>
    [JsonPropertyName("active_period")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int ActivePeriod { get; set; }

    /// <summary>Pass <see langword="true"/> to keep the story accessible after it expires</summary>
    [JsonPropertyName("post_to_chat_page")]
    public bool PostToChatPage { get; set; }

    /// <summary>Pass <see langword="true"/> if the content of the story must be protected from forwarding and screenshotting</summary>
    [JsonPropertyName("protect_content")]
    public bool ProtectContent { get; set; }
}
