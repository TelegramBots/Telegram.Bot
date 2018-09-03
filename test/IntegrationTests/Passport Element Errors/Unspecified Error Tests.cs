// ReSharper disable PossibleNullReferenceException
// ReSharper disable CheckNamespace
// ReSharper disable StringLiteralTypo

using System.Linq;
using System.Threading.Tasks;
using IntegrationTests.Framework;
using Telegram.Bot;
using Telegram.Bot.Passport.Request;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Passport;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace IntegrationTests
{
    [Collection("Unspecified error")]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class UnspecifiedErrorTests : IClassFixture<UnspecifiedErrorTests.Fixture>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        private readonly Fixture _classFixture;

        public UnspecifiedErrorTests(TestsFixture fixture, Fixture classFixture)
        {
            _fixture = fixture;
            _classFixture = classFixture;
        }

        [OrderedFact("Should generate passport authorization request link")]
        public async Task Should_generate_auth_link()
        {
            const string publicKey = "-----BEGIN PUBLIC KEY-----\n" +
                                     "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA0VElWoQA2SK1csG2/sY/\n" +
                                     "wlssO1bjXRx+t+JlIgS6jLPCefyCAcZBv7ElcSPJQIPEXNwN2XdnTc2wEIjZ8bTg\n" +
                                     "BlBqXppj471bJeX8Mi2uAxAqOUDuvGuqth+mq7DMqol3MNH5P9FO6li7nZxI1FX3\n" +
                                     "9u2r/4H4PXRiWx13gsVQRL6Clq2jcXFHc9CvNaCQEJX95jgQFAybal216EwlnnVV\n" +
                                     "giT/TNsfFjW41XJZsHUny9k+dAfyPzqAk54cgrvjgAHJayDWjapq90Fm/+e/DVQ6\n" +
                                     "BHGkV0POQMkkBrvvhAIQu222j+03frm9b2yZrhX/qS01lyjW4VaQytGV0wlewV6B\n" +
                                     "FwIDAQAB\n" +
                                     "-----END PUBLIC KEY-----";
            PassportScope scope = new PassportScope(new[]
            {
                new PassportScopeElementOne(PassportEnums.Scope.BankStatement),
            });
            AuthorizationRequest authReq = new AuthorizationRequest(
                botId: _fixture.BotUser.Id,
                publicKey: publicKey,
                nonce: "Test nonce for bank statement",
                scope: scope
            );

            await BotClient.SendTextMessageAsync(
                _fixture.SupergroupChat,
                "Share *only one bank statement* using Passport.\n\n" +
                "1. Click inline button\n" +
                "2. Open link in browser to redirect you back to Telegram passport\n" +
                "3. Authorize bot to access the info",
                ParseMode.Markdown,
                replyMarkup: (InlineKeyboardMarkup) InlineKeyboardButton.WithUrl(
                    "Share via Passport",
                    $"https://telegrambots.github.io/Telegram.Bot.Extensions.Passport/redirect.html?{authReq.Query}"
                )
            );

            Update passportUpdate = await _fixture.UpdateReceiver.GetPassportUpdate();

            _classFixture.AuthorizationRequest = authReq;
            _classFixture.Message = passportUpdate.Message;
        }

        [OrderedFact("Should set an unspecified error for the whole document")]
        public async Task Should_set_error_unspecified()
        {
            AuthorizationRequest authReq = _classFixture.AuthorizationRequest;
            Message passportMessage = _classFixture.Message;

            PassportElementError[] errors =
            {
                new PassportElementErrorUnspecified(
                    PassportEnums.Scope.BankStatement,
                    passportMessage.PassportData.Data.Single().Hash,
                    "Address is NOT mentioned in this document"
                ),
            };

            await BotClient.SetPassportDataErrorsAsync(
                passportMessage.From.Id,
                errors
            );

            await BotClient.SendTextMessageAsync(
                _fixture.SupergroupChat,
                "An error is set on the bank statement.\n" +
                "You can see error message with opening the request link again.",
                replyMarkup: (InlineKeyboardMarkup) InlineKeyboardButton.WithUrl(
                    "Passport Authorization Request",
                    $"https://telegrambots.github.io/Telegram.Bot.Extensions.Passport/redirect.html?{authReq.Query}"
                )
            );
        }

        public class Fixture
        {
            public Message Message;

            public AuthorizationRequest AuthorizationRequest;
        }
    }
}
