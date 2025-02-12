// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Enums;

/// <summary>Scope type</summary>
[JsonConverter(typeof(EnumConverter<BotCommandScopeType>))]
public enum BotCommandScopeType
{
    /// <summary>Represents the default <see cref="BotCommandScope">scope</see> of bot commands. Default commands are used if no commands with a <a href="https://core.telegram.org/bots/api#determining-list-of-commands">narrower scope</a> are specified for the user.<br/><br/><i>(<see cref="BotCommandScope"/> can be cast into <see cref="BotCommandScopeDefault"/>)</i></summary>
    Default = 1,
    /// <summary>Represents the <see cref="BotCommandScope">scope</see> of bot commands, covering all private chats.<br/><br/><i>(<see cref="BotCommandScope"/> can be cast into <see cref="BotCommandScopeAllPrivateChats"/>)</i></summary>
    AllPrivateChats,
    /// <summary>Represents the <see cref="BotCommandScope">scope</see> of bot commands, covering all group and supergroup chats.<br/><br/><i>(<see cref="BotCommandScope"/> can be cast into <see cref="BotCommandScopeAllGroupChats"/>)</i></summary>
    AllGroupChats,
    /// <summary>Represents the <see cref="BotCommandScope">scope</see> of bot commands, covering all group and supergroup chat administrators.<br/><br/><i>(<see cref="BotCommandScope"/> can be cast into <see cref="BotCommandScopeAllChatAdministrators"/>)</i></summary>
    AllChatAdministrators,
    /// <summary>Represents the <see cref="BotCommandScope">scope</see> of bot commands, covering a specific chat.<br/><br/><i>(<see cref="BotCommandScope"/> can be cast into <see cref="BotCommandScopeChat"/>)</i></summary>
    Chat,
    /// <summary>Represents the <see cref="BotCommandScope">scope</see> of bot commands, covering all administrators of a specific group or supergroup chat.<br/><br/><i>(<see cref="BotCommandScope"/> can be cast into <see cref="BotCommandScopeChatAdministrators"/>)</i></summary>
    ChatAdministrators,
    /// <summary>Represents the <see cref="BotCommandScope">scope</see> of bot commands, covering a specific member of a group or supergroup chat.<br/><br/><i>(<see cref="BotCommandScope"/> can be cast into <see cref="BotCommandScopeChatMember"/>)</i></summary>
    ChatMember,
}
