// ReSharper disable PossibleNullReferenceException

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using IntegrationTests.Framework;
using IntegrationTests.Framework.Fixtures;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Passport;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Passport;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace IntegrationTests
{
    [Collection(Constants.TestCollections.PhoneEmail)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class PhoneEmailTests : IClassFixture<EntityFixture<Update>>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        private readonly EntityFixture<Update> _classFixture;

        public PhoneEmailTests(TestsFixture fixture, EntityFixture<Update> classFixture)
        {
            _fixture = fixture;
            _classFixture = classFixture;
        }

        [OrderedFact("Should generate passport authorization request link")]
        public async Task Should_Generate_Authorize_Link()
        {
            string botId = _fixture.BotUser.Id.ToString();

            // Scope is a JSON serialized array of scope names e.g. [ "phone_number", "email" ]
            string scope = JsonConvert.SerializeObject(new[]
            {
                PassportEnums.Scope.PhoneNumber,
                PassportEnums.Scope.Email,
            });

            string publicKey = "-----BEGIN PUBLIC KEY-----\n" +
                               "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA0VElWoQA2SK1csG2/sY/\n" +
                               "wlssO1bjXRx+t+JlIgS6jLPCefyCAcZBv7ElcSPJQIPEXNwN2XdnTc2wEIjZ8bTg\n" +
                               "BlBqXppj471bJeX8Mi2uAxAqOUDuvGuqth+mq7DMqol3MNH5P9FO6li7nZxI1FX3\n" +
                               "9u2r/4H4PXRiWx13gsVQRL6Clq2jcXFHc9CvNaCQEJX95jgQFAybal216EwlnnVV\n" +
                               "giT/TNsfFjW41XJZsHUny9k+dAfyPzqAk54cgrvjgAHJayDWjapq90Fm/+e/DVQ6\n" +
                               "BHGkV0POQMkkBrvvhAIQu222j+03frm9b2yZrhX/qS01lyjW4VaQytGV0wlewV6B\n" +
                               "FwIDAQAB\n" +
                               "-----END PUBLIC KEY-----";

            string queryString = "domain=telegrampassport" +
                                 $"&bot_id={Uri.EscapeDataString(botId)}" +
                                 $"&scope={Uri.EscapeDataString(scope)}" +
                                 $"&public_key={Uri.EscapeDataString(publicKey)}" +
                                 $"&payload={Uri.EscapeDataString("TEST")}";

            await BotClient.SendTextMessageAsync(
                _fixture.SupergroupChat,
                "Share your phone number and email with bot using Passport.\n\n" +
                "1. Click inline button\n" +
                "2. Open link in browser to redirect you back to Telegram passport\n" +
                "3. Authorize bot to access the info",
                replyMarkup: (InlineKeyboardMarkup) InlineKeyboardButton.WithUrl(
                    "Share via Passport",
                    $"https://telegrambots.github.io/Telegram.Bot.Extensions.Passport/redirect.html?{queryString}"
                )
            );

            Update[] updates = await _fixture.UpdateReceiver.GetUpdatesAsync(
                u => u.Message?.PassportData != null,
                updateTypes: UpdateType.Message
            );

            Update passportUpdate = Assert.Single(updates);

            _classFixture.Entity = passportUpdate;
        }

        [OrderedFact("Should validate encrypted data in a Passport massage")]
        public void Should_Validate_Passport_Update()
        {
            Update update = _classFixture.Entity;
            PassportData passportData = update.Message.PassportData;

            Assert.NotNull(passportData);
            Assert.NotNull(passportData.Data);
            Assert.Equal(2, passportData.Data.Length);

            // todo there is no need to check for anything in these types. tg has access to them. not encrypted. clear text
            // There is only
            EncryptedPassportElement phoneElement = Assert.Single(passportData.Data, el => el.Type == "phone_number");
            Assert.Equal(PassportEnums.Scope.PhoneNumber, phoneElement.Type);
            Assert.Null(phoneElement.Data);
            Assert.NotEmpty(phoneElement.Hash);

            EncryptedPassportElement emailElement = Assert.Single(passportData.Data, el => el.Type == "email");
            Assert.Equal(PassportEnums.Scope.Email, emailElement.Type);
            Assert.NotEmpty(emailElement.Data);
            Assert.NotEmpty(emailElement.Hash);

            Assert.NotNull(passportData.Credentials);
            Assert.NotEmpty(passportData.Credentials.Data);
            Assert.NotEmpty(passportData.Credentials.Hash);
            Assert.NotEmpty(passportData.Credentials.Secret);
        }

        [OrderedFact("Should decrypt values")]
        public void Should_Decrypt_Passport_Update()
        {
            Update update = _classFixture.Entity;
            PassportData passportData = update.Message.PassportData;
            EncryptedPassportElement phoneElement = passportData.Data.Single(el => el.Type == "phone_number");
            EncryptedPassportElement emailElement = passportData.Data.Single(el => el.Type == "email");

            RSA key = EncryptionKey.ReadAsRsa();

            IDecrypter decrypter = new Decrypter(key);

            Credentials credentials = decrypter.DecryptCredentials(passportData.Credentials);

            Assert.NotNull(credentials);
            Assert.NotEmpty(credentials.Payload);
            Assert.Equal("TEST", credentials.Payload);
            Assert.NotNull(credentials.SecureData);

//            string phone = decrypter.DecryptData(
//                phoneElement.Data,
//                credentials.SecureData
//            );
//
//            string email = decrypter.DecryptData(
//                emailElement,
//                credentials.SecureData
//            );

            // ToDo other tests
        }
    }
}
