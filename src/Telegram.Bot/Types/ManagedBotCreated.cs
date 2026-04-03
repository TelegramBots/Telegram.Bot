// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object contains information about the bot that was created to be managed by the current bot.</summary>
public partial class ManagedBotCreated
{
    /// <summary>Information about the bot. The bot's token can be fetched using the method <see cref="TelegramBotClientExtensions.GetManagedBotToken">GetManagedBotToken</see>.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User Bot { get; set; } = default!;

    /// <summary>Implicit conversion to User (Bot)</summary>
    public static implicit operator User(ManagedBotCreated self) => self.Bot;
    /// <summary>Implicit conversion from User (Bot)</summary>
    public static implicit operator ManagedBotCreated(User bot) => new() { Bot = bot };
}
