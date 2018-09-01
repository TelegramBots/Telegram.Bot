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
using Xunit.Abstractions;

namespace IntegrationTests
{
    [Collection(Constants.TestCollections.DriverLicense)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class DriverLicenseTests : IClassFixture<EntityFixture<Update>>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        private readonly EntityFixture<Update> _classFixture;

        private readonly ITestOutputHelper _output;

        public DriverLicenseTests(TestsFixture fixture, EntityFixture<Update> classFixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _classFixture = classFixture;
            _output = output;
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
                new PassportScopeElementOne(PassportEnums.Scope.DriverLicense)
                {
                    Selfie = true,
                    Translation = true,
                },
            });
            AuthorizationRequest authReq = new AuthorizationRequest(
                botId: _fixture.BotUser.Id,
                publicKey: publicKey,
                nonce: "Test nonce for driver license",
                scope: scope
            );

            await BotClient.SendTextMessageAsync(
                _fixture.SupergroupChat,
                "Share your *driver license* in addition to *a selfie with it* and *a tranlation scan* " +
                "with bot using Telegram Passport.\n\n" +
                "1. Click inline button\n" +
                "2. Open link in browser to so it redirects you to Telegram Passport\n" +
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
            Assert.InRange(encryptedElement.FrontSide.FileSize, 1_000, 50_000_000);

            Assert.NotNull(encryptedElement.ReverseSide);
            Assert.NotEmpty(encryptedElement.ReverseSide.FileId);
            Assert.InRange(encryptedElement.ReverseSide.FileSize, 1_000, 50_000_000);

            Assert.NotNull(encryptedElement.Selfie);
            Assert.NotEmpty(encryptedElement.Selfie.FileId);
            Assert.InRange(encryptedElement.Selfie.FileSize, 1_000, 50_000_000);

            Assert.NotNull(encryptedElement.Translation);
            Assert.NotEmpty(encryptedElement.Translation);
            Assert.All(encryptedElement.Translation, Assert.NotNull);
            Assert.All(
                encryptedElement.Translation,
                trnsltn => Assert.NotEmpty(trnsltn.FileId)
            );
            Assert.All(
                encryptedElement.Translation,
                trnsltn => Assert.InRange(trnsltn.FileSize, 1_000, 50_000_000)
            );

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

            IDecrypter decrypter = new Decrypter();

            Credentials credentials = decrypter.DecryptCredentials(key, passportData.Credentials);

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
            IDecrypter decrypter = new Decrypter();
            Credentials credentials = decrypter.DecryptCredentials(key, passportData.Credentials);

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
            IDecrypter decrypter = new Decrypter();
            Credentials credentials = decrypter.DecryptCredentials(key, passportData.Credentials);

            File encryptedFileInfo;
            string decyptedFilePath = System.IO.Path.GetTempFileName();
            using (System.IO.Stream decryptedFile = System.IO.File.OpenWrite(decyptedFilePath))
            {
                encryptedFileInfo = await BotClient.DownloadAndDecryptPassportFileAsync(
                    element.FrontSide,
                    credentials.SecureData.DriverLicense.FrontSide,
                    decryptedFile
                );
            }

            _output.WriteLine("Front side JPEG file is written to \"{0}\".", decyptedFilePath);

            Assert.NotEmpty(encryptedFileInfo.FilePath);
            Assert.NotEmpty(encryptedFileInfo.FileId);
            Assert.InRange(encryptedFileInfo.FileSize, 1_000, 50_000_000);
        }

        [OrderedFact("Should decrypt reverse side photo file of 'driver_license' element")]
        public async Task Should_decreypt_reverse_side_file()
        {
            Update update = _classFixture.Entity;
            PassportData passportData = update.Message.PassportData;
            RSA key = EncryptionKey.ReadAsRsa();
            EncryptedPassportElement element = passportData.Data.Single();
            IDecrypter decrypter = new Decrypter();
            Credentials credentials = decrypter.DecryptCredentials(key, passportData.Credentials);

            File encryptedFileInfo;
            string decyptedFilePath = System.IO.Path.GetTempFileName();
            using (System.IO.Stream
                encryptedContent = new System.IO.MemoryStream(element.ReverseSide.FileSize),
                decryptedFile = System.IO.File.OpenWrite(decyptedFilePath)
            )
            {
                encryptedFileInfo = await BotClient.GetInfoAndDownloadFileAsync(
                    element.ReverseSide.FileId,
                    encryptedContent
                );

                await decrypter.DecryptFileAsync(
                    encryptedContent,
                    credentials.SecureData.DriverLicense.ReverseSide,
                    decryptedFile
                );
            }

            _output.WriteLine("Reverse side JPEG file is written to \"{0}\".", decyptedFilePath);

            Assert.NotEmpty(encryptedFileInfo.FilePath);
            Assert.NotEmpty(encryptedFileInfo.FileId);
            Assert.InRange(encryptedFileInfo.FileSize, 1_000, 50_000_000);
        }

        [OrderedFact("Should decrypt selfie photo file of 'driver_license' element and send it to chat")]
        public async Task Should_decreypt_selfie_file()
        {
            Update update = _classFixture.Entity;
            PassportData passportData = update.Message.PassportData;
            RSA key = EncryptionKey.ReadAsRsa();
            EncryptedPassportElement element = passportData.Data.Single();
            IDecrypter decrypter = new Decrypter();
            Credentials credentials = decrypter.DecryptCredentials(key, passportData.Credentials);

            byte[] encryptedContent;
            {
                File encryptedFileInfo = await BotClient.GetFileAsync(element.Selfie.FileId);

                Assert.NotEmpty(encryptedFileInfo.FilePath);
                Assert.NotEmpty(encryptedFileInfo.FileId);
                Assert.InRange(encryptedFileInfo.FileSize, 1_000, 50_000_000);

                using (System.IO.MemoryStream stream = new System.IO.MemoryStream(encryptedFileInfo.FileSize))
                {
                    await BotClient.DownloadFileAsync(encryptedFileInfo.FilePath, stream);
                    encryptedContent = stream.ToArray();
                }
            }

            byte[] selfieContent = decrypter.DecryptFile(
                encryptedContent,
                credentials.SecureData.DriverLicense.Selfie
            );

            Assert.NotEmpty(selfieContent);

            using (System.IO.Stream stream = new System.IO.MemoryStream(selfieContent))
            {
                await BotClient.SendPhotoAsync(
                    _fixture.SupergroupChat,
                    stream,
                    "selfie with driver license",
                    replyToMessageId: update.Message.MessageId
                );
            }
        }

        [OrderedFact("Should decrypt translation photo files of 'driver_license' element")]
        public async Task Should_decreypt_translation_file()
        {
            Update update = _classFixture.Entity;
            PassportData passportData = update.Message.PassportData;
            RSA key = EncryptionKey.ReadAsRsa();
            EncryptedPassportElement element = passportData.Data.Single();

            IDecrypter decrypter = new Decrypter();
            Credentials credentials = decrypter.DecryptCredentials(key, passportData.Credentials);

            for (int i = 0; i < element.Translation.Length; i++)
            {
                PassportFile passportFile = element.Translation[i];
                FileCredentials fileCredentials = credentials.SecureData.DriverLicense.Translation[i];

                byte[] encryptedContent;
                {
                    File encryptedFileInfo = await BotClient.GetFileAsync(passportFile.FileId);

                    Assert.NotEmpty(encryptedFileInfo.FilePath);
                    Assert.NotEmpty(encryptedFileInfo.FileId);
                    Assert.InRange(encryptedFileInfo.FileSize, 1_000, 50_000_000);

                    using (System.IO.MemoryStream stream = new System.IO.MemoryStream(encryptedFileInfo.FileSize))
                    {
                        await BotClient.DownloadFileAsync(encryptedFileInfo.FilePath, stream);
                        encryptedContent = stream.ToArray();
                    }
                }

                byte[] translationContent = decrypter.DecryptFile(
                    encryptedContent,
                    fileCredentials
                );

                Assert.NotEmpty(translationContent);

                string decyptedFilePath = System.IO.Path.GetTempFileName();
                await System.IO.File.WriteAllBytesAsync(decyptedFilePath, translationContent);
                _output.WriteLine("Translation JPEG file is written to \"{0}\".", decyptedFilePath);
            }
        }
    }
}
