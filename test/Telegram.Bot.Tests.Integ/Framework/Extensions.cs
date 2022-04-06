using System;
using System.Linq;
using Telegram.Bot.Types;

#nullable enable

namespace Telegram.Bot.Tests.Integ.Framework;

internal static class Extensions
{
    public static User GetUser(this Update update) =>
        update switch
        {
            { Message.From: {} user } => user,
            { InlineQuery.From: {} user } => user,
            { CallbackQuery.From: {} user } => user,
            { PreCheckoutQuery.From: {} user } => user,
            { ShippingQuery.From: {} user } => user,
            { ChosenInlineResult.From: {} user } => user,
            { PollAnswer.User: {} user } => user,
            { MyChatMember.NewChatMember.User: {} user } => user,
            { ChatMember.NewChatMember.User: {} user } => user,
            { EditedMessage.From: {} user } => user,
            { ChatJoinRequest.From: {} user } => user,
            _ => throw new ArgumentException("Unsupported update type {0}.", update.Type.ToString())
        };

    public static string GetTesters(this UpdateReceiver updateReceiver) =>
        string.Join(", ",
            updateReceiver.AllowedUsernames.Select(username => username.Replace("_", "\\_"))
        );

    public static string? GetSafeUsername(this User user) => user.Username?.Replace("_", "\\_");
    public static string? GetSafeUsername(this Chat chat) => chat.Username?.Replace("_", "\\_");

    public static DateTime With(this DateTime dateTime, DateTimeComponents components) =>
        new(year: components.Year ?? dateTime.Year,
            month: components.Month ?? dateTime.Month,
            day: components.Day ?? dateTime.Day,
            hour: components.Hour ?? dateTime.Hour,
            minute: components.Minute ?? dateTime.Minute,
            second: components.Second ?? dateTime.Second,
            millisecond: components.Millisecond ?? dateTime.Millisecond,
            kind: components.Kind ?? dateTime.Kind
        );
}

public class DateTimeComponents
{
    public int? Year { get; init; }
    public int? Month { get; init; }
    public int? Day { get; init; }
    public int? Hour { get; init; }
    public int? Minute { get; init; }
    public int? Second { get; init; }
    public int? Millisecond { get; init; }
    public DateTimeKind? Kind { get; init; }
}