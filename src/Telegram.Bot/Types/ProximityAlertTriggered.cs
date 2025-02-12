// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents the content of a service message, sent whenever a user in the chat triggers a proximity alert set by another user.</summary>
public partial class ProximityAlertTriggered
{
    /// <summary>User that triggered the alert</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User Traveler { get; set; } = default!;

    /// <summary>User that set the alert</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User Watcher { get; set; } = default!;

    /// <summary>The distance between the users</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Distance { get; set; }
}
