// ReSharper disable InconsistentNaming
// ReSharper disable PossibleNullReferenceException
// ReSharper disable CheckNamespace

using System;
using System.Linq;
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
    /// <summary>
    /// Tests for request personal details using Telegram Passport v1.1
    /// </summary>
    [Collection(Constants.TestCollections.PersonalDetails2)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class PersonalDetailsTests1_1 : IClassFixture<EntityFixture<Update>>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        private readonly EntityFixture<Update> _classFixture;

        public PersonalDetailsTests1_1(TestsFixture fixture, EntityFixture<Update> classFixture)
        {
            _fixture = fixture;
            _classFixture = classFixture;
        }

        [OrderedFact("Should generate passport authorization request link")]
        public async Task Should_Generate_Authorize_Link()
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
            PassportScope scope = new PassportScope
            {
                Data = new[]
                {
                    new PassportScopeElementOne(PassportEnums.Scope.PersonalDetails)
                    {
                        NativeNames = true,
                    },
                }
            };
            AuthorizationRequest authReq = new AuthorizationRequest(
                botId: _fixture.BotUser.Id,
                publicKey: publicKey,
                nonce: "TEST",
                scope: scope
            );

            await BotClient.SendTextMessageAsync(
                _fixture.SupergroupChat,
                "Share your personal details with bot using *Passport v1.1*.\n\n" +
                "1. Click inline button\n" +
                "2. Open link in browser to redirect you back to Telegram passport\n" +
                "3. Authorize bot to access the info",
                ParseMode.Markdown,
                replyMarkup: (InlineKeyboardMarkup) InlineKeyboardButton.WithUrl(
                    "Share via Passport",
                    $"https://telegrambots.github.io/Telegram.Bot.Extensions.Passport/redirect.html?{authReq.Query}"
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

            EncryptedPassportElement encryptedElement = Assert.Single(passportData.Data);
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
            RSA key = EncryptionKey.ReadAsRsa();

            IDecrypter decrypter = new Decrypter(key);

            Credentials credentials = decrypter.DecryptCredentials(passportData.Credentials);

            Assert.NotNull(credentials);
            Assert.NotEmpty(credentials.Nonce);
            Assert.Equal("TEST", credentials.Nonce);
            Assert.NotNull(credentials.SecureData);

            string personalDetailsJson = decrypter.DecryptData(
                element.Data,
                credentials.SecureData.PersonalDetails.Data
            );
            Assert.NotEmpty(personalDetailsJson);
            Assert.StartsWith("{\"", personalDetailsJson);

            PersonalDetails personalDetails = decrypter.DecryptData<PersonalDetails>(
                element.Data,
                credentials.SecureData.PersonalDetails.Data
            );

            Assert.NotNull(personalDetails);
            Assert.NotEmpty(personalDetails.FirstName);
            Assert.NotEmpty(personalDetails.Gender);
            Assert.NotEmpty(personalDetails.CountryCode);
            Assert.Equal(2, personalDetails.CountryCode.Length);
            Assert.NotEmpty(personalDetails.ResidenceCountryCode);
            Assert.Equal(2, personalDetails.ResidenceCountryCode.Length);
            Assert.NotEmpty(personalDetails.BirthDate);
            Assert.InRange(personalDetails.Birthdate, new DateTime(1900, 1, 1), DateTime.Today);
        }
    }
}
