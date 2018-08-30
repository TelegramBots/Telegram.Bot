// ReSharper disable PossibleNullReferenceException

using System;
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
    [Collection(Constants.TestCollections.Passport)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class PassportTests : IClassFixture<EntityFixture<Update>>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        private readonly EntityFixture<Update> _classFixture;

        public PassportTests(TestsFixture fixture, EntityFixture<Update> classFixture)
        {
            _fixture = fixture;
            _classFixture = classFixture;
        }

        [OrderedFact("Should generate passport authorization request link")]
        public async Task Should_Generate_Authorize_Link()
        {
            string botId = _fixture.BotUser.Id.ToString();

            // Scope is a JSON serialized array of scope names e.g. [ "passport" ]
            string scope = JsonConvert.SerializeObject(new[] {PassportEnums.Scope.Passport,});

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
                "Share your `passport` with bot using Passport.\n\n" +
                "1. Click inline button\n" +
                "2. Open link in browser to redirect you back to Telegram passport\n" +
                "3. Authorize bot to access the info",
                ParseMode.Markdown,
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

        [OrderedFact("Should validate fields in a Passport massage")]
        public void Should_Validate_Passport_Update()
        {
            Update update = _classFixture.Entity;
            PassportData passportData = update.Message.PassportData;

            Assert.NotNull(passportData);

            /* In the case of "passport" scope, there should be 2 elements in the passport message:
             * Personal Details and Passport.
             */
            Assert.Equal(2, passportData.Data.Length);
            EncryptedPassportElement personalDetailsEl = Assert.Single(
                passportData.Data,
                el => el.Type == "personal_details"
            );
            Assert.NotNull(personalDetailsEl);
            Assert.Equal(PassportEnums.Scope.PersonalDetails, personalDetailsEl.Type);
            Assert.NotEmpty(personalDetailsEl.Data);
            Assert.NotEmpty(personalDetailsEl.Hash);

            EncryptedPassportElement passportEl = Assert.Single(
                passportData.Data,
                el => el.Type == "passport"
            );
            Assert.NotNull(passportEl);
            Assert.Equal(PassportEnums.Scope.Passport, passportEl.Type);
            Assert.NotEmpty(passportEl.Data);
            Assert.NotEmpty(passportEl.Hash);
            Assert.NotNull(passportEl.FrontSide);
            Assert.NotEmpty(passportEl.FrontSide.FileId);
            Assert.NotEqual(default, passportEl.FrontSide.FileDate);

            Assert.NotNull(passportData.Credentials);
            Assert.NotEmpty(passportData.Credentials.Data);
            Assert.NotEmpty(passportData.Credentials.Hash);
            Assert.NotEmpty(passportData.Credentials.Secret);
        }

        [OrderedFact("Should decrypt passport values")]
        public void Should_Decrypt_Passport_Update()
        {
            Update update = _classFixture.Entity;
            RSA key = EncryptionKey.ReadAsRsa();
            PassportData passportData = update.Message.PassportData;

            IDecrypter decrypter = new Decrypter(key);

            Credentials credentials = decrypter.DecryptCredentials(passportData.Credentials);

            Assert.NotNull(credentials);
            Assert.NotEmpty(credentials.Payload);
            Assert.Equal("TEST", credentials.Payload);
            Assert.NotNull(credentials.SecureData);

            /* In the case of "passport" scope, there should be 2 elements in the passport message:
             * Personal Details and Passport.
             */

            #region Personal Details Element

            EncryptedPassportElement persDetEl = Assert.Single(passportData.Data, el => el.Type == "personal_details");
            PersonalDetails personalDetails = decrypter.DecryptData<PersonalDetails>(
                persDetEl.Data,
                credentials.SecureData.PersonalDetails.Data
            );

            Assert.NotNull(personalDetails);

            #endregion

            #region Passport Element

//            EncryptedPassportElement passportEl = Assert.Single(passportData.Data, el => el.Type == "passport");
//            PassportFile passportFile = decrypter.DecryptElementData<PassportFile>(
//                passportEl,
//                credentials.SecureData
//            );

//            Assert.NotNull(passportFile);

            #endregion

            // ToDo other tests
        }
    }
}
