using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using Telegram.Bot.Helpers.Passports;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Passport;
using Xunit;
using File = System.IO.File;

namespace Telegram.Bot.Tests.Unit.Passport
{
    public class PassportTests
    {
        [Fact]
        public void Should_DeserializeMessageWithPassport()
        {
            Message passportMessage = ReadPassportMessage();

            Assert.Equal(MessageType.PassportData, passportMessage.Type);
            Assert.NotEqual(default, passportMessage.PassportData);
        }

        [Fact]
        public void Should_DecryptPassportCredentials()
        {
            PassportData passport = ReadPassportMessage().PassportData;
            RSA privateKey = ReadPrivateKey();
            
            Assert.True(PassportCryptography.TryDecryptCredentials(passport.Credentials, privateKey, out Credentials credentials));
            Assert.NotEqual(default, credentials);
            Assert.Equal("payload", credentials.Payload);
        }

        private Message ReadPassportMessage()
        {
            string passportMessageJson = File.ReadAllText(Constants.FileNames.Passport.PassportMessage);
            return JsonConvert.DeserializeObject<Message>(passportMessageJson);
        }
        private RSA ReadPrivateKey()
        {
            string privateKeyJson = File.ReadAllText(Constants.FileNames.Passport.PrivateKey);
            JToken parametersObject = (JToken)JsonConvert.DeserializeObject(privateKeyJson);

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
