using System;
using System.Linq;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Tests.Integ.Framework
{
    internal static class Extensions
    {
        public static User GetUser(this Update update)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    return update.Message.From;
                case UpdateType.InlineQuery:
                    return update.InlineQuery.From;
                case UpdateType.CallbackQuery:
                    return update.CallbackQuery.From;
                case UpdateType.PreCheckoutQuery:
                    return update.PreCheckoutQuery.From;
                case UpdateType.ShippingQuery:
                    return update.ShippingQuery.From;
                case UpdateType.ChosenInlineResult:
                    return update.ChosenInlineResult.From;
                default:
                    throw new ArgumentException("Unsupported update type {0}.", update.Type.ToString());
            }
        }

        public static string GetTesters(this UpdateReceiver updateReceiver)
        {
            return string.Join(", ",
                updateReceiver.AllowedUsernames.Select(username => username.Replace("_", "\\_"))
            );
        }
    }
}
