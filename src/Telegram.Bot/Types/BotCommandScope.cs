// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents the scope to which bot commands are applied. Currently, the following 7 scopes are supported:<br/><see cref="BotCommandScopeDefault"/>, <see cref="BotCommandScopeAllPrivateChats"/>, <see cref="BotCommandScopeAllGroupChats"/>, <see cref="BotCommandScopeAllChatAdministrators"/>, <see cref="BotCommandScopeChat"/>, <see cref="BotCommandScopeChatAdministrators"/>, <see cref="BotCommandScopeChatMember"/></summary>
[JsonConverter(typeof(PolymorphicJsonConverter<BotCommandScope>))]
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(BotCommandScopeDefault), "default")]
[CustomJsonDerivedType(typeof(BotCommandScopeAllPrivateChats), "all_private_chats")]
[CustomJsonDerivedType(typeof(BotCommandScopeAllGroupChats), "all_group_chats")]
[CustomJsonDerivedType(typeof(BotCommandScopeAllChatAdministrators), "all_chat_administrators")]
[CustomJsonDerivedType(typeof(BotCommandScopeChat), "chat")]
[CustomJsonDerivedType(typeof(BotCommandScopeChatAdministrators), "chat_administrators")]
[CustomJsonDerivedType(typeof(BotCommandScopeChatMember), "chat_member")]
public abstract partial class BotCommandScope
{
    /// <summary>Scope type</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract BotCommandScopeType Type { get; }
}

/// <summary>Represents the default <see cref="BotCommandScope">scope</see> of bot commands. Default commands are used if no commands with a <a href="https://core.telegram.org/bots/api#determining-list-of-commands">narrower scope</a> are specified for the user.</summary>
public partial class BotCommandScopeDefault : BotCommandScope
{
    /// <summary>Scope type, always <see cref="BotCommandScopeType.Default"/></summary>
    public override BotCommandScopeType Type => BotCommandScopeType.Default;
}

/// <summary>Represents the <see cref="BotCommandScope">scope</see> of bot commands, covering all private chats.</summary>
public partial class BotCommandScopeAllPrivateChats : BotCommandScope
{
    /// <summary>Scope type, always <see cref="BotCommandScopeType.AllPrivateChats"/></summary>
    public override BotCommandScopeType Type => BotCommandScopeType.AllPrivateChats;
}

/// <summary>Represents the <see cref="BotCommandScope">scope</see> of bot commands, covering all group and supergroup chats.</summary>
public partial class BotCommandScopeAllGroupChats : BotCommandScope
{
    /// <summary>Scope type, always <see cref="BotCommandScopeType.AllGroupChats"/></summary>
    public override BotCommandScopeType Type => BotCommandScopeType.AllGroupChats;
}

/// <summary>Represents the <see cref="BotCommandScope">scope</see> of bot commands, covering all group and supergroup chat administrators.</summary>
public partial class BotCommandScopeAllChatAdministrators : BotCommandScope
{
    /// <summary>Scope type, always <see cref="BotCommandScopeType.AllChatAdministrators"/></summary>
    public override BotCommandScopeType Type => BotCommandScopeType.AllChatAdministrators;
}

/// <summary>Represents the <see cref="BotCommandScope">scope</see> of bot commands, covering a specific chat.</summary>
public partial class BotCommandScopeChat : BotCommandScope
{
    /// <summary>Scope type, always <see cref="BotCommandScopeType.Chat"/></summary>
    public override BotCommandScopeType Type => BotCommandScopeType.Chat;

    /// <summary>Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>). Channel direct messages chats and channel chats aren't supported.</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public ChatId ChatId { get; set; } = default!;
}

/// <summary>Represents the <see cref="BotCommandScope">scope</see> of bot commands, covering all administrators of a specific group or supergroup chat.</summary>
public partial class BotCommandScopeChatAdministrators : BotCommandScope
{
    /// <summary>Scope type, always <see cref="BotCommandScopeType.ChatAdministrators"/></summary>
    public override BotCommandScopeType Type => BotCommandScopeType.ChatAdministrators;

    /// <summary>Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>). Channel direct messages chats and channel chats aren't supported.</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public ChatId ChatId { get; set; } = default!;
}

/// <summary>Represents the <see cref="BotCommandScope">scope</see> of bot commands, covering a specific member of a group or supergroup chat.</summary>
public partial class BotCommandScopeChatMember : BotCommandScope
{
    /// <summary>Scope type, always <see cref="BotCommandScopeType.ChatMember"/></summary>
    public override BotCommandScopeType Type => BotCommandScopeType.ChatMember;

    /// <summary>Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>). Channel direct messages chats and channel chats aren't supported.</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public ChatId ChatId { get; set; } = default!;

    /// <summary>Unique identifier of the target user</summary>
    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public long UserId { get; set; }
}
