// ReSharper disable PossibleNullReferenceException
// ReSharper disable CheckNamespace
// ReSharper disable StringLiteralTypo

using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Passport;
using Telegram.Bot.Passport.Request;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Tests.Integ.Framework.Fixtures;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Passport;
using Telegram.Bot.Types.ReplyMarkups;
using Xunit;

namespace IntegrationTests
{
    [Collection(Constants.TestCollections.Passport.ResidentialAddress)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    [Trait(Constants.CategoryTraitName, Constants.InteractiveCategoryValue)]
    [Trait(Constants.PassportTraitName, Constants.PassportTraitValue)]
    public class ResidentialAddressTests : IClassFixture<EntityFixture<Update>>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        private readonly EntityFixture<Update> _classFixture;

        public ResidentialAddressTests(TestsFixture fixture, EntityFixture<Update> classFixture)
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
                new PassportScopeElementOne(PassportEnums.Scope.Address)
            });
            AuthorizationRequestParameters authReq = new AuthorizationRequestParameters(
                botId: _fixture.BotUser.Id,
                publicKey: publicKey,
                nonce: "Test nonce for address",
                scope: scope
            );

            await BotClient.SendTextMessageAsync(
                _fixture.SupergroupChat,
                "Share your *residential address* with bot using Passport.\n\n" +
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

        [OrderedFact("Should validate address in a Passport massage")]
        public void Should_Validate_Passport_Update()
        {
            Update update = _classFixture.Entity;
            PassportData passportData = update.Message.PassportData;

            Assert.NotNull(passportData);

            EncryptedPassportElement encryptedElement = Assert.Single(passportData.Data);
            Assert.NotNull(encryptedElement);
            Assert.Equal("address", encryptedElement.Type);
            Assert.Equal(PassportEnums.Scope.Address, encryptedElement.Type);
            Assert.NotNull(encryptedElement.Data);
            Assert.NotEmpty(encryptedElement.Data);
            Assert.NotEmpty(encryptedElement.Hash);

            Assert.NotNull(passportData.Credentials);
            Assert.NotEmpty(passportData.Credentials.Data);
            Assert.NotEmpty(passportData.Credentials.Hash);
            Assert.NotEmpty(passportData.Credentials.Secret);
        }

        [OrderedFact("Should decrypt and validate credentials")]
        public void Should_Decrypt_Credentials()
        {
            Update update = _classFixture.Entity;
            PassportData passportData = update.Message.PassportData;

            IDecrypter decrypter = new Decrypter();
            Credentials credentials = decrypter.DecryptCredentials(passportData.Credentials, EncryptionKey.ReadAsRsa());

            Assert.NotNull(credentials);
            Assert.Equal("Test nonce for address", credentials.Nonce);
            Assert.NotNull(credentials.SecureData);
        }

        [OrderedFact("Should decrypt data of 'address' element")]
        public void Should_Decrypt_Data()
        {
            Update update = _classFixture.Entity;
            PassportData passportData = update.Message.PassportData;
            EncryptedPassportElement element = passportData.Data.Single();

            Assert.NotNull(element.Data);

            IDecrypter decrypter = new Decrypter();
            Credentials credentials = decrypter.DecryptCredentials(passportData.Credentials, EncryptionKey.ReadAsRsa());

            ResidentialAddress residentialAddress = decrypter.DecryptData<ResidentialAddress>(
                element.Data,
                credentials.SecureData.Address.Data
            );

            Assert.NotNull(residentialAddress);
            Assert.NotEmpty(residentialAddress.StreetLine1);
            Assert.NotEmpty(residentialAddress.City);
            Assert.NotEmpty(residentialAddress.PostCode);
            Assert.Equal(2, residentialAddress.CountryCode.Length);
        }
    }
}
