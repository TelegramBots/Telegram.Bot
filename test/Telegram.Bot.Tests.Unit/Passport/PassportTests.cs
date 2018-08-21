using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Passport;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Passport
{
    public class PassportTests
    {
        [Fact]
        public void Should_DeserializeMessageWithPassport()
        {
            Message passportMessage = ReadPassportMessage();

            Assert.NotNull(passportMessage.PassportData);
        }

        [Fact]
        public void Should_DecryptPassportCredentials()
        {
            PassportData passport = ReadPassportMessage().PassportData;
            RSA privateKey = ReadPrivateKey();

            Assert.True(PassportCryptography.TryDecryptCredentials(passport.Credentials, privateKey,
                out Credentials credentials));
            Assert.NotEqual(default, credentials);
            Assert.Equal("payload", credentials.Payload);
        }

        [Fact]
        public void Should_DecryptDataFields()
        {
            PassportData passportData = ReadPassportMessage().PassportData;
            RSA privateKey = ReadPrivateKey();

            Assert.True(PassportCryptography.TryDecryptCredentials(passportData.Credentials, privateKey,
                out Credentials masterCredentials));

            SecureData credentials = masterCredentials.SecureData;
            foreach (EncryptedPassportElement element in passportData.Data)
            {
                if (element.Data != null)
                {
                    switch (element.Type)
                    {
                        case PassportElementType.PersonalDetails:
                            Assert.True(element.DecryptData<PersonalDetails>(credentials.PersonalDetails.Data)
                                .Successful);
                            Assert.False(element.DecryptData<IdDocumentData>(credentials.PersonalDetails.Data)
                                .Successful);
                            Assert.False(element.DecryptData<ResidentialAddress>(credentials.PersonalDetails.Data)
                                .Successful);
                            break;

                        case PassportElementType.Passport:
                            Assert.True(element.DecryptData<IdDocumentData>(credentials.Passport.Data)
                                .Successful);
                            Assert.False(element.DecryptData<PersonalDetails>(credentials.Passport.Data)
                                .Successful);
                            Assert.False(element.DecryptData<ResidentialAddress>(credentials.Passport.Data)
                                .Successful);
                            break;

                        case PassportElementType.DriverLicense:
                            Assert.True(element.DecryptData<IdDocumentData>(credentials.DriverLicense.Data)
                                .Successful);
                            Assert.False(element.DecryptData<PersonalDetails>(credentials.DriverLicense.Data)
                                .Successful);
                            Assert.False(element.DecryptData<ResidentialAddress>(credentials.DriverLicense.Data)
                                .Successful);
                            break;

                        case PassportElementType.IdentityCard:
                            Assert.True(element.DecryptData<IdDocumentData>(credentials.IdentityCard.Data)
                                .Successful);
                            Assert.False(element.DecryptData<PersonalDetails>(credentials.IdentityCard.Data)
                                .Successful);
                            Assert.False(element.DecryptData<ResidentialAddress>(credentials.IdentityCard.Data)
                                .Successful);
                            break;

                        case PassportElementType.InternalPassport:
                            Assert.True(element.DecryptData<IdDocumentData>(credentials.InternalPassport.Data)
                                .Successful);
                            Assert.False(element.DecryptData<PersonalDetails>(credentials.InternalPassport.Data)
                                .Successful);
                            Assert.False(element.DecryptData<ResidentialAddress>(credentials.InternalPassport.Data)
                                .Successful);
                            break;

                        case PassportElementType.Address:
                            Assert.True(element.DecryptData<ResidentialAddress>(credentials.Address.Data)
                                .Successful);
                            Assert.False(element.DecryptData<PersonalDetails>(credentials.Address.Data)
                                .Successful);
                            Assert.False(element.DecryptData<IdDocumentData>(credentials.Address.Data)
                                .Successful);
                            break;

                        default:
                            throw new Exception("Should never get here - these are the only types with the Data field");
                    }
                }
                else
                {
                    Assert.False(element.DecryptData<PersonalDetails>(null)
                        .Successful);
                    Assert.False(element.DecryptData<IdDocumentData>(null)
                        .Successful);
                    Assert.False(element.DecryptData<ResidentialAddress>(null)
                        .Successful);
                }
            }
        }

        // ToDo - needs access to the bot token - secure value to be added to config - see Should_DecryptFile
        #pragma warning disable CS0162 // Unreachable code
        [Fact]
        public async Task Should_DecryptFileFields()
        {
            return;

            PassportData passportData = ReadPassportMessage().PassportData;
            RSA privateKey = ReadPrivateKey();

            Assert.True(PassportCryptography.TryDecryptCredentials(passportData.Credentials, privateKey, out Credentials masterCredentials));

            SecureData credentials = masterCredentials.SecureData;
            foreach (EncryptedPassportElement element in passportData.Data)
            {
                switch (element.Type)
                {
                    case PassportElementType.UtilityBill:
                        await Should_DecryptFiles(element.Files, credentials.UtilityBill.Files);
                        break;

                    case PassportElementType.BankStatement:
                        await Should_DecryptFiles(element.Files, credentials.BankStatement.Files);
                        break;

                    case PassportElementType.RentalAgreement:
                        await Should_DecryptFiles(element.Files, credentials.RentalAgreement.Files);
                        break;

                    case PassportElementType.Passport:
                        await Should_DecryptFiles(element.Files, credentials.Passport.Files);
                        await Should_DecryptFile(element.FrontSide, credentials.Passport.FrontSide);
                        if (element.Selfie != null)
                        {
                            await Should_DecryptFile(element.Selfie, credentials.Passport.Selfie);
                        }
                        break;

                    case PassportElementType.TemporaryRegistration:
                        await Should_DecryptFiles(element.Files, credentials.TemporaryRegistration.Files);
                        break;

                    case PassportElementType.DriverLicense:
                        await Should_DecryptFile(element.FrontSide, credentials.DriverLicense.FrontSide);
                        await Should_DecryptFile(element.ReverseSide, credentials.DriverLicense.ReverseSide);
                        if (element.Selfie != null)
                        {
                            await Should_DecryptFile(element.Selfie, credentials.DriverLicense.Selfie);
                        }
                        break;

                    case PassportElementType.IdentityCard:
                        await Should_DecryptFile(element.FrontSide, credentials.IdentityCard.FrontSide);
                        await Should_DecryptFile(element.ReverseSide, credentials.IdentityCard.ReverseSide);
                        if (element.Selfie != null)
                        {
                            await Should_DecryptFile(element.Selfie, credentials.IdentityCard.Selfie);
                        }
                        break;

                    case PassportElementType.InternalPassport:
                        await Should_DecryptFile(element.FrontSide, credentials.InternalPassport.FrontSide);
                        if (element.Selfie != null)
                        {
                            await Should_DecryptFile(element.Selfie, credentials.InternalPassport.Selfie);
                        }
                        break;
                }
            }
        }

        private async Task Should_DecryptFiles(PassportFile[] passportFiles, FileCredentials[] credentials)
        {
            Assert.NotNull(passportFiles);
            Assert.NotNull(credentials);
            Assert.NotEmpty(passportFiles);
            Assert.NotEmpty(credentials);
            Assert.Equal(passportFiles.Length, credentials.Length);
            for (int i = 0; i < passportFiles.Length; i++)
            {
                await Should_DecryptFile(passportFiles[i], credentials[i]);
            }
        }
        private async Task Should_DecryptFile(PassportFile passportFile, FileCredentials credentials)
        {
            Assert.NotNull(passportFile);
            Assert.NotNull(credentials);
            string token = "ToDo - get token from configuration";
            TelegramBotClient bot = new TelegramBotClient(token);
            byte[] encryptedFile;
            using (MemoryStream ms = new MemoryStream())
            {
                await bot.GetInfoAndDownloadFileAsync(passportFile.FileId, ms);
                encryptedFile = ms.ToArray();
            }
            Assert.NotEmpty(encryptedFile);
            Assert.True(PassportCryptography.TryDecryptFile(encryptedFile, credentials, out byte[] decrypted));
            Assert.NotEmpty(decrypted);
        }
        #pragma warning restore CS0162 // Unreachable code

        private Message ReadPassportMessage()
        {
            string passportMessageJson = System.IO.File.ReadAllText(Constants.FileNames.Passport.PassportMessage);
            return JsonConvert.DeserializeObject<Message>(passportMessageJson);
        }
        private RSA ReadPrivateKey()
        {
            string privateKeyJson = System.IO.File.ReadAllText(Constants.FileNames.Passport.PrivateKey);
            JToken parametersObject = (JToken) JsonConvert.DeserializeObject(privateKeyJson);

            // This insanity is here because private key material from RSAParameters is not serializable
            RSAParameters parameters;
            parameters.D = parametersObject["D"].ToObject<byte[]>();
            parameters.DP = parametersObject["DP"].ToObject<byte[]>();
            parameters.DQ = parametersObject["DQ"].ToObject<byte[]>();
            parameters.Exponent = parametersObject["Exponent"].ToObject<byte[]>();
            parameters.InverseQ = parametersObject["InverseQ"].ToObject<byte[]>();
            parameters.Modulus = parametersObject["Modulus"].ToObject<byte[]>();
            parameters.P = parametersObject["P"].ToObject<byte[]>();
            parameters.Q = parametersObject["Q"].ToObject<byte[]>();

            return RSA.Create(parameters);
        }
    }
}
