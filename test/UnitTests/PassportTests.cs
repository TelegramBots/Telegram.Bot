using System;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Helpers.Passports;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Passport;
using Xunit;

namespace UnitTests
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
                            Assert.True(element.DecryptData<IdDocumentData>(credentials.Passport.Data).Successful);
                            Assert.False(element.DecryptData<PersonalDetails>(credentials.Passport.Data).Successful);
                            Assert.False(element.DecryptData<ResidentialAddress>(credentials.Passport.Data).Successful);
                            break;

                        case PassportElementType.DriverLicense:
                            Assert.True(element.DecryptData<IdDocumentData>(credentials.DriverLicense.Data).Successful);
                            Assert.False(
                                element.DecryptData<PersonalDetails>(credentials.DriverLicense.Data).Successful);
                            Assert.False(element.DecryptData<ResidentialAddress>(credentials.DriverLicense.Data)
                                .Successful);
                            break;

                        case PassportElementType.IdentityCard:
                            Assert.True(element.DecryptData<IdDocumentData>(credentials.IdentityCard.Data).Successful);
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
                            Assert.True(element.DecryptData<ResidentialAddress>(credentials.Address.Data).Successful);
                            Assert.False(element.DecryptData<PersonalDetails>(credentials.Address.Data).Successful);
                            Assert.False(element.DecryptData<IdDocumentData>(credentials.Address.Data).Successful);
                            break;

                        default:
                            throw new Exception("Should never get here - these are the only types with the Data field");
                    }
                }
                else
                {
                    Assert.False(element.DecryptData<PersonalDetails>(null).Successful);
                    Assert.False(element.DecryptData<IdDocumentData>(null).Successful);
                    Assert.False(element.DecryptData<ResidentialAddress>(null).Successful);
                }
            }
        }

        [Fact]
        public void Should_DecryptFileFields()
        {
        }

        private Message ReadPassportMessage()
        {
            string passportMessageJson = System.IO.File.ReadAllText(Constants.Files.PassportMessage);
            return JsonConvert.DeserializeObject<Message>(passportMessageJson);
        }

        private RSA ReadPrivateKey()
        {
            string privateKeyJson = System.IO.File.ReadAllText(Constants.Files.PrivateKey);
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
