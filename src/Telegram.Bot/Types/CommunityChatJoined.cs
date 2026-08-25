// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes a service message about a chat being joined by a user from a community.</summary>
public partial class CommunityChatJoined
{
    /// <summary>The community from which the chat was joined</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Community Community { get; set; } = default!;

    /// <summary>Implicit conversion to Community (Community)</summary>
    public static implicit operator Community(CommunityChatJoined self) => self.Community;
    /// <summary>Implicit conversion from Community (Community)</summary>
    public static implicit operator CommunityChatJoined(Community community) => new() { Community = community };
}
