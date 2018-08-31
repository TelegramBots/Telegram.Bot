// ReSharper disable PossibleNullReferenceException
// ReSharper disable CheckNamespace

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
    [Collection(Constants.TestCollections.DriverLicense)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class DriverLicenseTests : IClassFixture<EntityFixture<Update>>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        private readonly EntityFixture<Update> _classFixture;

        public DriverLicenseTests(TestsFixture fixture, EntityFixture<Update> classFixture)
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

            PassportScope scope = new PassportScope
            {
                Data = new[]
                {
                    new PassportScopeElementOne(PassportEnums.Scope.DriverLicense)
                    {
                        Selfie = true,
                        Translation = true,
                    },
                }
            };
            AuthorizationRequest authReq = new AuthorizationRequest(
                botId: _fixture.BotUser.Id,
                publicKey: publicKey,
                nonce: "Test nonce for driver license",
                scope: scope
            );

            await BotClient.SendTextMessageAsync(
                _fixture.SupergroupChat,
                "Share your *driver license* with bot using Passport.\n\n" +
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

        [OrderedFact("Should validate driver license values in the Passport massage")]
        public void Should_validate_passport_update()
        {
            Update update = _classFixture.Entity;
            PassportData passportData = update.Message.PassportData;

            EncryptedPassportElement encryptedElement = Assert.Single(passportData.Data);
            Assert.NotNull(encryptedElement);
            Assert.Equal("driver_license", encryptedElement.Type);
            Assert.Equal(PassportEnums.Scope.DriverLicense, encryptedElement.Type);

            Assert.NotEmpty(encryptedElement.Data);
            Assert.NotEmpty(encryptedElement.Hash);

            Assert.NotNull(encryptedElement.FrontSide);
            Assert.NotEmpty(encryptedElement.FrontSide.FileId);
            Assert.NotNull(encryptedElement.FrontSide.FileSize);

            Assert.NotNull(encryptedElement.ReverseSide);
            Assert.NotEmpty(encryptedElement.ReverseSide.FileId);
            Assert.NotNull(encryptedElement.ReverseSide.FileSize);

            if (encryptedElement.Selfie != null)
            {
                Assert.NotEmpty(encryptedElement.Selfie.FileId);
                Assert.NotNull(encryptedElement.Selfie.FileSize);
            }

            if (encryptedElement.Translation != null)
            {
                Assert.NotEmpty(encryptedElement.Translation);
                Assert.All(encryptedElement.Translation, Assert.NotNull);
                Assert.All(
                    encryptedElement.Translation,
                    trnsltn => Assert.NotEmpty(trnsltn.FileId)
                );
                Assert.All(
                    encryptedElement.Translation,
                    trnsltn => Assert.NotNull(trnsltn.FileSize)
                );
            }

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
            EncryptedPassportElement element = passportData.Data.Single();

            RSA key = EncryptionKey.ReadAsRsa();

            IDecrypter decrypter = new Decrypter(key);

            Credentials credentials = decrypter.DecryptCredentials(passportData.Credentials);

            Assert.NotNull(credentials);
            Assert.Equal("Test nonce for driver license", credentials.Nonce);
            Assert.NotNull(credentials.SecureData);

            // decryption of document data in 'driver_license' element requires accompanying DataCredentials
            Assert.NotNull(credentials.SecureData.DriverLicense);
            Assert.NotNull(credentials.SecureData.DriverLicense.Data);
            Assert.NotEmpty(credentials.SecureData.DriverLicense.Data.Secret);
            Assert.NotEmpty(credentials.SecureData.DriverLicense.Data.DataHash);

            // decryption of front side file in 'driver_license' element requires accompanying FileCredentials
            Assert.NotNull(credentials.SecureData.DriverLicense.FrontSide);
            Assert.NotEmpty(credentials.SecureData.DriverLicense.FrontSide.Secret);
            Assert.NotEmpty(credentials.SecureData.DriverLicense.FrontSide.FileHash);

            // decryption of optional selfie file in 'driver_license' element requires accompanying FileCredentials
            Assert.NotNull(credentials.SecureData.DriverLicense.Selfie);
            Assert.NotEmpty(credentials.SecureData.DriverLicense.Selfie.Secret);
            Assert.NotEmpty(credentials.SecureData.DriverLicense.Selfie.FileHash);

            // decryption of optional translation file in 'driver_license' element requires accompanying FileCredentials
            Assert.Equal(
                element.Translation.Length,
                credentials.SecureData.DriverLicense.Translation.Length
            );
        }

        [OrderedFact("Should decrypt document data of 'driver_license' element")]
        public void Should_decreypt_document_data()
        {
            Update update = _classFixture.Entity;
            PassportData passportData = update.Message.PassportData;
            EncryptedPassportElement element = passportData.Data.Single();
            RSA key = EncryptionKey.ReadAsRsa();
            IDecrypter decrypter = new Decrypter(key);

            Credentials credentials = decrypter.DecryptCredentials(passportData.Credentials);

            string licenseDocJson = decrypter.DecryptData(
                encryptedData: element.Data,
                dataCredentials: credentials.SecureData.DriverLicense.Data
            );
            Assert.StartsWith("{", licenseDocJson);

            IdDocumentData licenseDoc = decrypter.DecryptData<IdDocumentData>(
                encryptedData: element.Data,
                dataCredentials: credentials.SecureData.DriverLicense.Data
            );

            Assert.NotEmpty(licenseDoc.DocumentNo);
            if (string.IsNullOrEmpty(licenseDoc.ExpiryDate))
            {
                Assert.Null(licenseDoc.Expiry);
            }
            else
            {
                Assert.NotNull(licenseDoc.Expiry);
            }
        }

        [OrderedFact("Should decrypt front side photo file of 'driver_license' element")]
        public async Task Should_decreypt_front_side_file()
        {
            Update update = _classFixture.Entity;
            PassportData passportData = update.Message.PassportData;
            RSA key = EncryptionKey.ReadAsRsa();
            EncryptedPassportElement element = passportData.Data.Single();
            IDecrypter decrypter = new Decrypter(key);

            Credentials credentials = decrypter.DecryptCredentials(passportData.Credentials);

            byte[] encrypted;
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(element.FrontSide.FileSize))
            {
                await BotClient.GetInfoAndDownloadFileAsync(
                    element.FrontSide.FileId,
                    stream
                );
                encrypted = stream.ToArray();
            }

            byte[] content = decrypter.DecryptFile(
                encrypted,
                credentials.SecureData.DriverLicense.FrontSide
            );
            Assert.NotEmpty(content);
        }

        [OrderedFact("Should decrypt reverse side photo file of 'driver_license' element")]
        public async Task Should_decreypt_reverse_side_file()
        {
            Update update = _classFixture.Entity;
            PassportData passportData = update.Message.PassportData;
            RSA key = EncryptionKey.ReadAsRsa();
            EncryptedPassportElement element = passportData.Data.Single();
            IDecrypter decrypter = new Decrypter(key);

            Credentials credentials = decrypter.DecryptCredentials(passportData.Credentials);

            byte[] encrypted;
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(element.ReverseSide.FileSize))
            {
                await BotClient.GetInfoAndDownloadFileAsync(
                    element.ReverseSide.FileId,
                    stream
                );
                encrypted = stream.ToArray();
            }

            byte[] content = decrypter.DecryptFile(
                encrypted,
                credentials.SecureData.DriverLicense.ReverseSide
            );
            Assert.NotEmpty(content);
        }

        [OrderedFact("Should decrypt selfie photo file of 'driver_license' element")]
        public async Task Should_decreypt_selfie_file()
        {
            Update update = _classFixture.Entity;
            PassportData passportData = update.Message.PassportData;
            RSA key = EncryptionKey.ReadAsRsa();
            EncryptedPassportElement element = passportData.Data.Single();

            IDecrypter decrypter = new Decrypter(key);
            Credentials credentials = decrypter.DecryptCredentials(passportData.Credentials);

            byte[] encrypted;
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(element.Selfie.FileSize))
            {
                await BotClient.GetInfoAndDownloadFileAsync(
                    element.Selfie.FileId,
                    stream
                );
                encrypted = stream.ToArray();
            }

            byte[] content = decrypter.DecryptFile(
                encrypted,
                credentials.SecureData.DriverLicense.Selfie
            );
            Assert.NotEmpty(content);
        }

        [OrderedFact("Should decrypt translation photo files of 'driver_license' element")]
        public async Task Should_decreypt_translation_file()
        {
            Update update = _classFixture.Entity;
            PassportData passportData = update.Message.PassportData;
            RSA key = EncryptionKey.ReadAsRsa();
            EncryptedPassportElement element = passportData.Data.Single();

            IDecrypter decrypter = new Decrypter(key);
            Credentials credentials = decrypter.DecryptCredentials(passportData.Credentials);

            for (int i = 0; i < element.Translation.Length; i++)
            {
                PassportFile encryptedFileInfo = element.Translation[i];
                FileCredentials fileCredentials = credentials.SecureData.DriverLicense.Translation[i];

                byte[] encrypted;
                using (System.IO.MemoryStream stream = new System.IO.MemoryStream(encryptedFileInfo.FileSize))
                {
                    await BotClient.GetInfoAndDownloadFileAsync(
                        encryptedFileInfo.FileId,
                        stream
                    );
                    encrypted = stream.ToArray();
                }

                byte[] content = decrypter.DecryptFile(
                    encrypted,
                    fileCredentials
                );
                Assert.NotEmpty(content);
            }
        }
    }
}
