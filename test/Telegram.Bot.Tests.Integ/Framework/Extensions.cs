using System;
using System.Linq;
using Telegram.Bot.Types;

#nullable enable

namespace Telegram.Bot.Tests.Integ.Framework
{
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
                _ => throw new ArgumentException("Unsupported update type {0}.", update.Type.ToString())
            };

        public static string GetTesters(this UpdateReceiver updateReceiver) =>
            string.Join(", ",
                updateReceiver.AllowedUsernames.Select(username => username.Replace("_", "\\_"))
            );

        public static string? GetSafeUsername(this User user) => user.Username?.Replace("_", "\\_");
        public static string? GetSafeUsername(this Chat chat) => chat.Username?.Replace("_", "\\_");
    }
}
