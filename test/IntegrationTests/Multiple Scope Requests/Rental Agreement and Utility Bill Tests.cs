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
    [Collection(Constants.TestCollections.RentalAgreementAndBill)]
    [TestCaseOrderer(Constants.TestCaseOrderer, Constants.AssemblyName)]
    public class RentalAgreementAndBillTests : IClassFixture<EntityFixture<Update>>
    {
        private ITelegramBotClient BotClient => _fixture.BotClient;

        private readonly TestsFixture _fixture;

        private readonly EntityFixture<Update> _classFixture;

        public RentalAgreementAndBillTests(TestsFixture fixture, EntityFixture<Update> classFixture)
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
                    new PassportScopeElementOneOfSeveral
                    {
                        OneOf = new[]
                        {
                            new PassportScopeElementOne(PassportEnums.Scope.IdentityCard),
                            new PassportScopeElementOne(PassportEnums.Scope.Passport),
                        },
                        Selfie = true, // selfie can only be requested for documents used as proof of identity
                    },
                    new PassportScopeElementOneOfSeveral
                    {
                        OneOf = new[]
                        {
                            new PassportScopeElementOne(PassportEnums.Scope.UtilityBill),
                            new PassportScopeElementOne(PassportEnums.Scope.RentalAgreement),
                        },
                        Translation = true,
                        // Selfie = null // selfie cannot be requested for documents used as proof of address
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
                "Share your *identity card with a selfie* and " +
                "a *utiltiy bill with its translation* with bot using Passport.\n\n" +
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

            #region identity card element validation

            EncryptedPassportElement idCardEl = Assert.Single(passportData.Data, el => el.Type == "identity_card");
            Assert.NotNull(idCardEl);
            Assert.Equal(PassportEnums.Scope.IdentityCard, idCardEl.Type);

            Assert.NotEmpty(idCardEl.Data);
            Assert.NotEmpty(idCardEl.Hash);

            Assert.NotNull(idCardEl.FrontSide);
            Assert.NotEmpty(idCardEl.FrontSide.FileId);
            Assert.NotNull(idCardEl.FrontSide.FileDate);

            Assert.NotNull(idCardEl.ReverseSide);
            Assert.NotEmpty(idCardEl.ReverseSide.FileId);
            Assert.NotNull(idCardEl.ReverseSide.FileDate);

            Assert.NotNull(idCardEl.Selfie);
            Assert.NotEmpty(idCardEl.Selfie.FileId);
            Assert.NotNull(idCardEl.Selfie.FileDate);

            #endregion

            #region utility bill element validation

            EncryptedPassportElement billElement = Assert.Single(passportData.Data, el => el.Type == "utility_bill");
            Assert.NotNull(billElement);
            Assert.Equal(PassportEnums.Scope.UtilityBill, billElement.Type);

            Assert.NotEmpty(billElement.Hash);
            Assert.Null(billElement.Data);

            PassportFile billScanFile = Assert.Single(billElement.Files);
            Assert.NotNull(billScanFile);
            Assert.NotEmpty(billScanFile.FileId);
            Assert.NotNull(billScanFile.FileDate);

            PassportFile billTranslationFile = Assert.Single(billElement.Files);
            Assert.NotNull(billTranslationFile);
            Assert.NotEmpty(billTranslationFile.FileId);
            Assert.NotNull(billTranslationFile.FileDate);

            #endregion

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

            IDecrypter decrypter = new Decrypter(key);

            Credentials credentials = decrypter.DecryptCredentials(
                encryptedCredentials: passportData.Credentials
            );

            Assert.NotNull(credentials);
            Assert.NotNull(credentials.SecureData);
            Assert.Equal("TEST", credentials.Nonce);

            // decryption of docuemnt data in 'identity_card' element requires accompanying DataCredentials
            Assert.NotNull(credentials.SecureData.IdentityCard);
            Assert.NotNull(credentials.SecureData.IdentityCard.Data);
            Assert.NotEmpty(credentials.SecureData.IdentityCard.Data.Secret);
            Assert.NotEmpty(credentials.SecureData.IdentityCard.Data.DataHash);

            // decryption of front side of 'identity_card' element requires accompanying FileCredentials
            Assert.NotNull(credentials.SecureData.IdentityCard.FrontSide);
            Assert.NotEmpty(credentials.SecureData.IdentityCard.FrontSide.Secret);
            Assert.NotEmpty(credentials.SecureData.IdentityCard.FrontSide.FileHash);

            // decryption of reverse side of 'identity_card' element requires accompanying FileCredentials
            Assert.NotNull(credentials.SecureData.IdentityCard.ReverseSide);
            Assert.NotEmpty(credentials.SecureData.IdentityCard.ReverseSide.Secret);
            Assert.NotEmpty(credentials.SecureData.IdentityCard.ReverseSide.FileHash);

            // decryption of selfie of 'identity_card' element requires accompanying FileCredentials
            Assert.NotNull(credentials.SecureData.IdentityCard.Selfie);
            Assert.NotEmpty(credentials.SecureData.IdentityCard.Selfie.Secret);
            Assert.NotEmpty(credentials.SecureData.IdentityCard.Selfie.FileHash);

            Assert.Null(credentials.SecureData.IdentityCard.Translation);
            Assert.Null(credentials.SecureData.IdentityCard.Files);

            // decryption of file scan in 'utility_bill' element requires accompanying FileCredentials
            Assert.NotNull(credentials.SecureData.UtilityBill.Files);
            FileCredentials billFileCreds = Assert.Single(credentials.SecureData.UtilityBill.Files);
            Assert.NotEmpty(billFileCreds.Secret);
            Assert.NotEmpty(billFileCreds.FileHash);

            // decryption of translation file scan in 'utility_bill' element requires accompanying FileCredentials
            Assert.NotNull(credentials.SecureData.UtilityBill.Files);
            FileCredentials billTranslationFileCreds = Assert.Single(credentials.SecureData.UtilityBill.Translation);
            Assert.NotEmpty(billTranslationFileCreds.Secret);
            Assert.NotEmpty(billTranslationFileCreds.FileHash);
        }

        [OrderedFact("Should decrypt docuemnt data in 'identity_card' element")]
        public void Should_decreypt_identity_card_element_document()
        {
            Update update = _classFixture.Entity;
            PassportData passportData = update.Message.PassportData;

            RSA key = EncryptionKey.ReadAsRsa();

            IDecrypter decrypter = new Decrypter(key);
            Credentials credentials = decrypter.DecryptCredentials(passportData.Credentials);
            EncryptedPassportElement idCardEl = Assert.Single(passportData.Data, el => el.Type == "identity_card");

            string documentDataJson = decrypter.DecryptData(
                idCardEl.Data,
                credentials.SecureData.IdentityCard.Data
            );
            Assert.StartsWith("{", documentDataJson);

            IdDocumentData documentData = decrypter.DecryptData<IdDocumentData>(
                idCardEl.Data,
                credentials.SecureData.IdentityCard.Data
            );

            Assert.NotEmpty(documentData.DocumentNo);
            if (string.IsNullOrEmpty(documentData.ExpiryDate))
            {
                Assert.Null(documentData.Expiry);
            }
            else
            {
                Assert.NotNull(documentData.Expiry);
            }
        }

        [OrderedFact("Should decrypt front side photo in 'identity_card' element")]
        public async Task Should_decreypt_identity_card_element_frontside()
        {
            Update update = _classFixture.Entity;
            PassportData passportData = update.Message.PassportData;
            RSA key = EncryptionKey.ReadAsRsa();
            EncryptedPassportElement idCardEl = Assert.Single(passportData.Data, el => el.Type == "identity_card");

            Assert.NotNull(idCardEl.FrontSide);
            Assert.NotEmpty(idCardEl.FrontSide.FileId);
            Assert.NotNull(idCardEl.FrontSide.FileDate);

            IDecrypter decrypter = new Decrypter(key);
            Credentials credentials = decrypter.DecryptCredentials(passportData.Credentials);

            byte[] encryptedContent;
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(idCardEl.FrontSide.FileSize))
            {
                await BotClient.GetInfoAndDownloadFileAsync(
                    idCardEl.FrontSide.FileId,
                    stream
                );
                encryptedContent = stream.ToArray();
            }

            byte[] content = decrypter.DecryptFile(
                encryptedContent,
                credentials.SecureData.IdentityCard.FrontSide
            );

            Assert.NotEmpty(content);
        }

        [OrderedFact("Should decrypt reverse side photo in 'identity_card' element")]
        public async Task Should_decreypt_identity_card_element_reverseside()
        {
            Update update = _classFixture.Entity;
            PassportData passportData = update.Message.PassportData;
            RSA key = EncryptionKey.ReadAsRsa();
            EncryptedPassportElement idCardEl = Assert.Single(passportData.Data, el => el.Type == "identity_card");

            Assert.NotNull(idCardEl.ReverseSide);
            Assert.NotEmpty(idCardEl.ReverseSide.FileId);
            Assert.NotNull(idCardEl.ReverseSide.FileDate);

            IDecrypter decrypter = new Decrypter(key);
            Credentials credentials = decrypter.DecryptCredentials(passportData.Credentials);

            byte[] encryptedContent;
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(idCardEl.ReverseSide.FileSize))
            {
                await BotClient.GetInfoAndDownloadFileAsync(
                    idCardEl.ReverseSide.FileId,
                    stream
                );
                encryptedContent = stream.ToArray();
            }

            byte[] content = decrypter.DecryptFile(
                encryptedContent,
                credentials.SecureData.IdentityCard.ReverseSide
            );

            Assert.NotEmpty(content);
        }

        [OrderedFact("Should decrypt selfie photo in 'identity_card' element")]
        public async Task Should_decreypt_identity_card_element_selfie()
        {
            Update update = _classFixture.Entity;
            PassportData passportData = update.Message.PassportData;
            RSA key = EncryptionKey.ReadAsRsa();
            EncryptedPassportElement idCardEl = Assert.Single(passportData.Data, el => el.Type == "identity_card");

            Assert.NotNull(idCardEl.Selfie);
            Assert.NotEmpty(idCardEl.Selfie.FileId);
            Assert.NotNull(idCardEl.Selfie.FileDate);

            IDecrypter decrypter = new Decrypter(key);
            Credentials credentials = decrypter.DecryptCredentials(passportData.Credentials);

            byte[] encryptedContent;
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(idCardEl.Selfie.FileSize))
            {
                await BotClient.GetInfoAndDownloadFileAsync(
                    idCardEl.Selfie.FileId,
                    stream
                );
                encryptedContent = stream.ToArray();
            }

            byte[] content = decrypter.DecryptFile(
                encryptedContent,
                credentials.SecureData.IdentityCard.Selfie
            );

            Assert.NotEmpty(content);
        }

        [OrderedFact("Should decrypt the single file in 'utility_bill' element")]
        public async Task Should_decreypt_utility_bill_element_file()
        {
            Update update = _classFixture.Entity;
            PassportData passportData = update.Message.PassportData;
            RSA key = EncryptionKey.ReadAsRsa();
            EncryptedPassportElement billElement = Assert.Single(passportData.Data, el => el.Type == "utility_bill");

            Assert.NotNull(billElement.Files);
            PassportFile billScanFile = Assert.Single(billElement.Files);

            Assert.NotEmpty(billScanFile.FileId);
            Assert.NotNull(billScanFile.FileDate);

            IDecrypter decrypter = new Decrypter(key);
            Credentials credentials = decrypter.DecryptCredentials(passportData.Credentials);

            FileCredentials billFileCreds = Assert.Single(credentials.SecureData.UtilityBill.Files);

            byte[] encryptedContent;
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(billScanFile.FileSize))
            {
                await BotClient.GetInfoAndDownloadFileAsync(
                    billScanFile.FileId,
                    stream
                );
                encryptedContent = stream.ToArray();
            }

            byte[] content = decrypter.DecryptFile(
                encryptedContent,
                billFileCreds
            );

            Assert.NotEmpty(content);
        }

        [OrderedFact("Should decrypt the single translation file in 'utility_bill' element")]
        public async Task Should_decreypt_utility_bill_element_translation()
        {
            Update update = _classFixture.Entity;
            PassportData passportData = update.Message.PassportData;
            RSA key = EncryptionKey.ReadAsRsa();
            EncryptedPassportElement billElement = Assert.Single(passportData.Data, el => el.Type == "utility_bill");

            Assert.NotNull(billElement.Translation);
            PassportFile translationFile = Assert.Single(billElement.Translation);

            Assert.NotEmpty(translationFile.FileId);
            Assert.NotNull(translationFile.FileDate);

            IDecrypter decrypter = new Decrypter(key);
            Credentials credentials = decrypter.DecryptCredentials(passportData.Credentials);

            FileCredentials billTranslationFileCreds = Assert.Single(credentials.SecureData.UtilityBill.Translation);

            byte[] encryptedContent;
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(translationFile.FileSize))
            {
                await BotClient.GetInfoAndDownloadFileAsync(
                    translationFile.FileId,
                    stream
                );
                encryptedContent = stream.ToArray();
            }

            byte[] content = decrypter.DecryptFile(
                encryptedContent,
                billTranslationFileCreds
            );

            Assert.NotEmpty(content);
        }
    }
}
