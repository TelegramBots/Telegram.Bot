// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes a service message about a chat being added to a community.</summary>
public partial class CommunityChatAdded
{
    /// <summary>The new community to which the chat belongs</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Community Community { get; set; } = default!;

    /// <summary>Implicit conversion to Community (Community)</summary>
    public static implicit operator Community(CommunityChatAdded self) => self.Community;
    /// <summary>Implicit conversion from Community (Community)</summary>
    public static implicit operator CommunityChatAdded(Community community) => new() { Community = community };
}
