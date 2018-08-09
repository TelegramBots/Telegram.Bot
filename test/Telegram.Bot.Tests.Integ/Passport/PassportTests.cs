using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Passport
{
    [Collection(Constants.TestCollections.Passport)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class PassportTests
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        public PassportTests(TestsFixture fixture)
        {
            _fixture = fixture;
        }

        [OrderedFact("Should generate passport authorization request link")]
        [Trait(Constants.MethodTraitName, Constants.TelegramBotApiMethods.SendMessage)]
        public async Task Should_Generate_Authorize_Link()
        {
            string botId = _fixture.BotUser.Id.ToString();

            // Scope is a JSON serialized array of scope names e.g. [ "personal_details" ]
            string scope = JsonConvert.SerializeObject(new[] {Enums.Passport.Scope.PersonalDetails,});

            string publicKey = "-----BEGIN PUBLIC KEY-----\n" +
                               "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA0VElWoQA2SK1csG2/sY/\n" +
                               "wlssO1bjXRx+t+JlIgS6jLPCefyCAcZBv7ElcSPJQIPEXNwN2XdnTc2wEIjZ8bTg\n" +
                               "BlBqXppj471bJeX8Mi2uAxAqOUDuvGuqth+mq7DMqol3MNH5P9FO6li7nZxI1FX3\n" +
                               "9u2r/4H4PXRiWx13gsVQRL6Clq2jcXFHc9CvNaCQEJX95jgQFAybal216EwlnnVV\n" +
                               "giT/TNsfFjW41XJZsHUny9k+dAfyPzqAk54cgrvjgAHJayDWjapq90Fm/+e/DVQ6\n" +
                               "BHGkV0POQMkkBrvvhAIQu222j+03frm9b2yZrhX/qS01lyjW4VaQytGV0wlewV6B\n" +
                               "FwIDAQAB\n" +
                               "-----END PUBLIC KEY-----";

            string url = "tg://resolve?domain=telegrampassport" +
                         $"&bot_id={Uri.EscapeDataString(botId)}" +
                         $"&scope={Uri.EscapeDataString(scope)}" +
                         $"&public_key={Uri.EscapeDataString(publicKey)}" +
                         $"&payload={Uri.EscapeDataString("my payload")}";

            // On Android devices, URL should begin with "tg:" instead of "tg://"
            string urlAndroid = url.Replace("tg://", "tg:");

            Message message = await BotClient.SendTextMessageAsync(
                /* chatId: */ _fixture.SupergroupChat,
                /* text: */ "Share your personal_details with bot using Passport:\n\n" +
                            $@"<a href=""{url}"">Click to Share</a>" + '\n' +
                            $@"<a href=""{urlAndroid}"">Click to Share(Android)</a>",
                /* parseMode: */ ParseMode.Html,
                replyMarkup: new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Share", url),
                        InlineKeyboardButton.WithUrl("Share (Android)", urlAndroid),
                    }
                })
            );

            // Each "tg://" URL is considered a text_link
            Assert.Equal(2, message.Entities.Length);
            Assert.Contains(url, message.EntityValues);
            Assert.Contains(urlAndroid, message.EntityValues);
            Assert.All(message.Entities, e => Assert.Equal(MessageEntityType.TextLink, e.Type));
        }
    }
}
