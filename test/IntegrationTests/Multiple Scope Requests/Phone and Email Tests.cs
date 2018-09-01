// ReSharper disable PossibleNullReferenceException
// ReSharper disable CheckNamespace

using System.Security.Cryptography;
using System.Threading.Tasks;
using IntegrationTests.Framework;
using IntegrationTests.Framework.Fixtures;
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
    [Collection(Constants.TestCollections.PhoneAndEmail)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class PhoneAndEmailTests : IClassFixture<EntityFixture<Update>>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        private readonly EntityFixture<Update> _classFixture;

        public PhoneAndEmailTests(TestsFixture fixture, EntityFixture<Update> classFixture)
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
                new PassportScopeElementOne(PassportEnums.Scope.PhoneNumber),
                new PassportScopeElementOne(PassportEnums.Scope.Email),
            });
            AuthorizationRequest authReq = new AuthorizationRequest(
                botId: _fixture.BotUser.Id,
                publicKey: publicKey,
                nonce: "Test nonce for phone and email",
                scope: scope
            );

            await BotClient.SendTextMessageAsync(
                _fixture.SupergroupChat,
                "Share your *phone number* and *email address* with bot using Passport.\n\n" +
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
            _classFixture.Entity = passportUpdate;
        }

        [OrderedFact("Should validate values in the Passport massage")]
        public void Should_validate_passport_message()
        {
            Update update = _classFixture.Entity;
            PassportData passportData = update.Message.PassportData;

            EncryptedPassportElement phoneElement = Assert.Single(passportData.Data, el => el.Type == "phone_number");
            Assert.NotNull(phoneElement);
            Assert.Equal(PassportEnums.Scope.PhoneNumber, phoneElement.Type);
            Assert.NotEmpty(phoneElement.PhoneNumber);
            Assert.True(long.TryParse(phoneElement.PhoneNumber, out _));
            Assert.NotEmpty(phoneElement.Hash);

            EncryptedPassportElement emailElement = Assert.Single(passportData.Data, el => el.Type == "email");
            Assert.NotNull(emailElement);
            Assert.Equal(PassportEnums.Scope.Email, emailElement.Type);
            Assert.NotEmpty(emailElement.Email);
            Assert.NotEmpty(emailElement.Hash);

            Assert.NotNull(passportData.Credentials);
            Assert.NotEmpty(passportData.Credentials.Data);
            Assert.NotEmpty(passportData.Credentials.Hash);
            Assert.NotEmpty(passportData.Credentials.Secret);
        }

        [OrderedFact("Should decrypt and validate credentials")]
        public void Should_decrypt_credentials()
        {
            Update update = _classFixture.Entity;
            PassportData passportData = update.Message.PassportData;
            RSA key = EncryptionKey.ReadAsRsa();

            IDecrypter decrypter = new Decrypter();

            Credentials credentials = decrypter.DecryptCredentials(
                key: key,
                encryptedCredentials: passportData.Credentials
            );

            Assert.NotNull(credentials);
            Assert.NotNull(credentials.SecureData);
            Assert.Equal("Test nonce for phone and email", credentials.Nonce);
        }
    }
}
