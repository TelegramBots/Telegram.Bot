using System;
using System.Collections.Generic;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Converters;

internal class ChatMemberStatusConverter : EnumConverter<ChatMemberStatus>
{
    static readonly IReadOnlyDictionary<string, ChatMemberStatus> StringToEnum =
        new Dictionary<string, ChatMemberStatus>
        {
            {"creator", ChatMemberStatus.Creator},
            {"administrator", ChatMemberStatus.Administrator},
            {"member", ChatMemberStatus.Member},
            {"left", ChatMemberStatus.Left},
            {"kicked", ChatMemberStatus.Kicked},
            {"restricted", ChatMemberStatus.Restricted},
        };

    static readonly IReadOnlyDictionary<ChatMemberStatus, string> EnumToString =
        new Dictionary<ChatMemberStatus, string>
        {
            { 0, "unknown" },
            {ChatMemberStatus.Creator, "creator"},
            {ChatMemberStatus.Administrator, "administrator"},
            {ChatMemberStatus.Member, "member"},
            {ChatMemberStatus.Left, "left"},
            {ChatMemberStatus.Kicked, "kicked"},
            {ChatMemberStatus.Restricted, "restricted"},
        };

    protected override ChatMemberStatus GetEnumValue(string value) =>
        StringToEnum.TryGetValue(value, out var enumValue)
            ? enumValue
            : 0;

    protected override string GetStringValue(ChatMemberStatus value) =>
        EnumToString.TryGetValue(value, out var stringValue)
            ? stringValue
            : throw new NotSupportedException();
}