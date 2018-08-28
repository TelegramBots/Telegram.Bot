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
    [Collection(Constants.TestCollections.PersonalDetails)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class PersonalDetailsTests : IClassFixture<EntityFixture<Update>>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        private readonly EntityFixture<Update> _classFixture;

        public PersonalDetailsTests(TestsFixture fixture, EntityFixture<Update> classFixture)
        {
            _fixture = fixture;
            _classFixture = classFixture;
        }

        [OrderedFact("Should generate passport authorization request link")]
        public async Task Should_Generate_Authorize_Link()
        {
            string botId = _fixture.BotUser.Id.ToString();

            // Scope is a JSON serialized array of scope names e.g. [ "personal_details" ]
            string scope = JsonConvert.SerializeObject(new[] {PassportEnums.Scope.PersonalDetails,});

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
                "Share your personal details with bot using Passport.\n\n" +
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

        [OrderedFact("Should validate personal details in a Passport massage")]
        public void Should_Validate_Passport_Update()
        {
            Update update = _classFixture.Entity;
            PassportData passportData = update.Message.PassportData;

            Assert.NotNull(passportData);

            EncryptedPassportElement encryptedElement = Assert.Single(passportData.Data);
            Assert.NotNull(encryptedElement);
            Assert.Equal("personal_details", encryptedElement.Type);
            Assert.Equal(PassportEnums.Scope.PersonalDetails, encryptedElement.Type);
            Assert.NotEmpty(encryptedElement.Data);
            Assert.NotEmpty(encryptedElement.Hash);

            Assert.NotNull(passportData.Credentials);
            Assert.NotEmpty(passportData.Credentials.Data);
            Assert.NotEmpty(passportData.Credentials.Hash);
            Assert.NotEmpty(passportData.Credentials.Secret);
        }

        [OrderedFact("Should decrypt personal details values")]
        public void Should_Decrypt_Passport_Update()
        {
            Update update = _classFixture.Entity;
            PassportData passportData = update.Message.PassportData;
            EncryptedPassportElement element = passportData.Data.Single();

            RSA key = EncryptionKeys.ReadAsRsa();

            IDecrypter decrypter = new Decrypter(key);

            Credentials credentials = decrypter.DecryptCredentials(passportData.Credentials);

            Assert.NotNull(credentials);
            Assert.NotEmpty(credentials.Payload);
            Assert.Equal("TEST", credentials.Payload);
            Assert.NotNull(credentials.SecureData);

            PersonalDetails personalDetails = decrypter.DecryptData<PersonalDetails>(
                element.Data,
                credentials.SecureData.PersonalDetails.Data
            );

            Assert.NotNull(personalDetails);
        }
    }
}
