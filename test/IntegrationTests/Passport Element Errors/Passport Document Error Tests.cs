// ReSharper disable PossibleNullReferenceException
// ReSharper disable CheckNamespace
// ReSharper disable StringLiteralTypo

using System.Security.Cryptography;
using System.Threading.Tasks;
using IntegrationTests.Framework;
using Telegram.Bot;
using Telegram.Bot.Passport;
using Telegram.Bot.Passport.Request;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Passport;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace IntegrationTests
{
    [Collection("Passport document errors")]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class IdentityCardErrorTests : IClassFixture<IdentityCardErrorTests.Fixture>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        private readonly Fixture _classFixture;

        public IdentityCardErrorTests(TestsFixture fixture, Fixture classFixture)
        {
            _fixture = fixture;
            _classFixture = classFixture;
        }

        [OrderedFact("Should generate passport authorization request link")]
        public async Task Should_Generate_Auth_Link()
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
                new PassportScopeElementOne(PassportEnums.Scope.Passport)
                {
                    Selfie = true,
                    Translation = true,
                },
            });
            AuthorizationRequestParameters authReq = new AuthorizationRequestParameters(
                botId: _fixture.BotUser.Id,
                publicKey: publicKey,
                nonce: "Test nonce for passport",
                scope: scope
            );

            await BotClient.SendTextMessageAsync(
                _fixture.SupergroupChat,
                "Share your *passport* with its *translation* and a *selfie* using Telegram Passport.\n\n" +
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

            RSA key = EncryptionKey.ReadAsRsa();
            IDecrypter decrypter = new Decrypter();
            Credentials credentials =
                decrypter.DecryptCredentials(passportUpdate.Message.PassportData.Credentials, key);

            Assert.Equal("Test nonce for passport", credentials.Nonce);

            _classFixture.AuthorizationRequestParameters = authReq;
            _classFixture.Credentials = credentials;
            _classFixture.Message = passportUpdate.Message;
        }

        [OrderedFact("Should set multiple errors for passport")]
        public async Task Should_Set_Errors_Passport()
        {
            AuthorizationRequestParameters authReq = _classFixture.AuthorizationRequestParameters;
            Credentials credentials = _classFixture.Credentials;
            Message passportMessage = _classFixture.Message;

            PassportElementError[] errors =
            {
                new PassportElementErrorDataField(
                    PassportEnums.Scope.Passport,
                    "document_no",
                    credentials.SecureData.Passport.Data.DataHash,
                    "Invalid passport number"
                ),
                new PassportElementErrorFrontSide(
                    PassportEnums.Scope.Passport,
                    credentials.SecureData.Passport.FrontSide.FileHash,
                    "Document scan is redacted"
                ),
                new PassportElementErrorSelfie(
                    PassportEnums.Scope.Passport,
                    credentials.SecureData.Passport.Selfie.FileHash,
                    "Take a selfie without glasses"
                ),
                new PassportElementErrorTranslationFile(
                    PassportEnums.Scope.Passport,
                    credentials.SecureData.Passport.Translation[0].FileHash,
                    "Document photo is blury"
                ),
            };

            await BotClient.SetPassportDataErrorsAsync(
                passportMessage.From.Id,
                errors
            );

            await BotClient.SendTextMessageAsync(
                _fixture.SupergroupChat,
                "Errors are set on all passport data and documents.\n" +
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

            public Credentials Credentials;

            public AuthorizationRequestParameters AuthorizationRequestParameters;
        }
    }
}
