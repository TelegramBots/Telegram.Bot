namespace Telegram.Bot.Tests.Integ.Common
{
    public static class CommonConstants
    {
        public const string TestCollectionName = "All";

        public const string CategoryTraitName = "Category";

        public const string MethodTraitName = "Method";

        public static class TestCategories
        {
            public const string GettingUpdates = "Getting Updates";

            public const string SendingMessages = "Sending Messages";

            public const string CallbackQueries = "Callback Queries";

            public const string InlineQueries = "Inline Queries";

            public const string Games = "Games";

            public const string Payments = "Payments";
        }

        public static class TelegramBotApiMethods
        {
            public const string GetMe = "getMe";

            public const string SendMessage = "sendMessage";

            public const string AnswerCallbackQuery = "answerCallbackQuery";

            public const string AnswerInlineQuery = "answerInlineQuery";

            public const string SendInvoice = "sendInvoice";

            public const string AnswerShippingQuery = "answerShippingQuery";

            public const string AnswerPreCheckoutQuery = "answerPreCheckoutQuery";
        }
    }
}
