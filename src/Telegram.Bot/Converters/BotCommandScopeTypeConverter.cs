using System;
using System.Collections.Generic;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Converters;

internal class BotCommandScopeTypeConverter : EnumConverter<BotCommandScopeType>
{
    static readonly IReadOnlyDictionary<string, BotCommandScopeType> StringToEnum =
        new Dictionary<string, BotCommandScopeType>
        {
            {"default", BotCommandScopeType.Default},
            {"all_private_chats", BotCommandScopeType.AllPrivateChats},
            {"all_group_chats", BotCommandScopeType.AllGroupChats},
            {"all_chat_administrators", BotCommandScopeType.AllChatAdministrators},
            {"chat", BotCommandScopeType.Chat},
            {"chat_administrators", BotCommandScopeType.ChatAdministrators},
            {"chat_member", BotCommandScopeType.ChatMember},
        };

    static readonly IReadOnlyDictionary<BotCommandScopeType, string> EnumToString =
        new Dictionary<BotCommandScopeType, string>
        {
            {0, "unknown"},
            {BotCommandScopeType.Default, "default"},
            {BotCommandScopeType.AllPrivateChats, "all_private_chats"},
            {BotCommandScopeType.AllGroupChats, "all_group_chats"},
            {BotCommandScopeType.AllChatAdministrators, "all_chat_administrators"},
            {BotCommandScopeType.Chat, "chat"},
            {BotCommandScopeType.ChatAdministrators, "chat_administrators"},
            {BotCommandScopeType.ChatMember, "chat_member"},
        };

    protected override BotCommandScopeType GetEnumValue(string value) =>
        StringToEnum.TryGetValue(value, out var messageEntityType)
            ? messageEntityType
            : 0;

    protected override string GetStringValue(BotCommandScopeType value) =>
        EnumToString.TryGetValue(value, out var messageEntityType)
            ? messageEntityType
            : throw new NotSupportedException();
}