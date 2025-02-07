// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents a story.</summary>
public partial class Story
{
    /// <summary>Chat that posted the story</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Chat Chat { get; set; } = default!;

    /// <summary>Unique identifier for the story in the chat</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Id { get; set; }
}
