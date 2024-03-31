using Telegram.Bot.Serialization;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types;

/// <summary>
/// This object represents the scope to which bot commands are applied
/// </summary>
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType<BotCommandScopeDefault>("default")]
[CustomJsonDerivedType<BotCommandScopeAllPrivateChats>("all_private_chats")]
[CustomJsonDerivedType<BotCommandScopeAllGroupChats>("all_group_chats")]
[CustomJsonDerivedType<BotCommandScopeAllChatAdministrators>("all_chat_administrators")]
[CustomJsonDerivedType<BotCommandScopeChat>("chat")]
[CustomJsonDerivedType<BotCommandScopeChatAdministrators>("chat_administrators")]
[CustomJsonDerivedType<BotCommandScopeChatMember>("chat_member")]
public abstract class BotCommandScope
{
    /// <summary>
    /// Scope type
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract BotCommandScopeType Type { get; }

    /// <summary>
    /// Create a default <see cref="BotCommandScope"/> instance
    /// </summary>
    /// <returns></returns>
    public static BotCommandScopeDefault Default() => new();

    /// <summary>
    /// Create a <see cref="BotCommandScope"/> instance for all private chats
    /// </summary>
    /// <returns></returns>
    public static BotCommandScopeAllPrivateChats AllPrivateChats() => new();

    /// <summary>
    /// Create a <see cref="BotCommandScope"/> instance for all group chats
    /// </summary>
    public static BotCommandScopeAllGroupChats AllGroupChats() => new();

    /// <summary>
    /// Create a <see cref="BotCommandScope"/> instance for all chat administrators
    /// </summary>
    public static BotCommandScopeAllChatAdministrators AllChatAdministrators() =>
        new();

    /// <summary>
    /// Create a <see cref="BotCommandScope"/> instance for a specific <see cref="Chat"/>
    /// </summary>
    /// <param name="chatId">
    /// Unique identifier for the target <see cref="Chat"/> or username of the target supergroup
    /// </param>
    public static BotCommandScopeChat Chat(ChatId chatId) => new() { ChatId = chatId };

    /// <summary>
    /// Create a <see cref="BotCommandScope"/> instance for a specific member in a specific <see cref="Chat"/>
    /// </summary>
    /// <param name="chatId">
    /// Unique identifier for the target <see cref="Chat"/> or username of the target supergroup
    /// </param>
    public static BotCommandScopeChatAdministrators ChatAdministrators(ChatId chatId) =>
        new() { ChatId = chatId };

    /// <summary>
    /// Represents the <see cref="BotCommandScope">scope</see> of bot commands, covering a specific member of a group or supergroup chat.
    /// </summary>
    /// <param name="chatId">
    /// Unique identifier for the target <see cref="Chat"/> or username of the target supergroup
    /// </param>
    /// <param name="userId">Unique identifier of the target user</param>
    public static BotCommandScopeChatMember ChatMember(ChatId chatId, long userId) =>
        new() { ChatId = chatId, UserId = userId };
}

/// <inheritdoc cref="BotCommandScopeType.Default"/>
public class BotCommandScopeDefault : BotCommandScope
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override BotCommandScopeType Type => BotCommandScopeType.Default;
}

/// <inheritdoc cref="BotCommandScopeType.AllPrivateChats"/>
public class BotCommandScopeAllPrivateChats : BotCommandScope
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override BotCommandScopeType Type => BotCommandScopeType.AllPrivateChats;
}

/// <inheritdoc cref="BotCommandScopeType.AllGroupChats"/>
public class BotCommandScopeAllGroupChats : BotCommandScope
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override BotCommandScopeType Type => BotCommandScopeType.AllGroupChats;
}

/// <inheritdoc cref="BotCommandScopeType.AllChatAdministrators"/>
public class BotCommandScopeAllChatAdministrators : BotCommandScope
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override BotCommandScopeType Type => BotCommandScopeType.AllChatAdministrators;
}

/// <inheritdoc cref="BotCommandScopeType.Chat"/>
public class BotCommandScopeChat : BotCommandScope
{
    /// <inheritdoc />
    public override BotCommandScopeType Type => BotCommandScopeType.Chat;

    /// <summary>
    /// Unique identifier for the target <see cref="Chat"/> or username of the target supergroup
    /// (in the format @supergroupusername)
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public ChatId ChatId { get; set; } = default!;
}

/// <inheritdoc cref="BotCommandScopeType.ChatAdministrators"/>
public class BotCommandScopeChatAdministrators : BotCommandScope
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override BotCommandScopeType Type => BotCommandScopeType.ChatAdministrators;

    /// <summary>
    /// Unique identifier for the target <see cref="Chat"/> or username of the target supergroup
    /// (in the format @supergroupusername)
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public ChatId ChatId { get; set; } = default!;
}

/// <inheritdoc cref="BotCommandScopeType.ChatMember"/>
public class BotCommandScopeChatMember : BotCommandScope
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override BotCommandScopeType Type => BotCommandScopeType.ChatMember;

    /// <summary>
    /// Unique identifier for the target <see cref="Chat"/> or username of the target supergroup
    /// (in the format @supergroupusername)
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public ChatId ChatId { get; set; } = default!;

    /// <summary>
    /// Unique identifier of the target user
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public long UserId { get; set; }
}
