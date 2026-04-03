// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object contains information about the creation or token update of a bot that is managed by the current bot.</summary>
public partial class ManagedBotUpdated
{
    /// <summary>User that created the bot</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User User { get; set; } = default!;

    /// <summary>Information about the bot. Token of the bot can be fetched using the method <see cref="TelegramBotClientExtensions.GetManagedBotToken">GetManagedBotToken</see>.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User Bot { get; set; } = default!;
}
