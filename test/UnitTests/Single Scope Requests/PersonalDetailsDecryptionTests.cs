// ReSharper disable InconsistentNaming
// ReSharper disable PossibleNullReferenceException
// ReSharper disable CheckNamespace

using System;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Passport;
using Telegram.Bot.Types.Passport;
using Xunit;

namespace UnitTests
{
    /// <summary>
    /// Tests for decryption of "message.passport_data" received for authorization request with the scope
    /// of "personal_details". Request was in Telegram Passport v1.1 format.
    /// </summary>
    public class PersonalDetailsDecryptionTests
    {
        [Fact(DisplayName = "Should decrypt 'passport_data.credentials'")]
        public void Should_decrypt_credentials()
        {
            RSA key = EncryptionKey.GetRsaPrivateKey();
            PassportData passportData = GetPassportData();

            IDecrypter decrypter = new Decrypter(key);

            Credentials credentials = decrypter.DecryptCredentials(passportData.Credentials);

            Assert.NotNull(credentials);
            Assert.NotNull(credentials.SecureData);
            Assert.NotEmpty(credentials.Nonce);
            Assert.Equal("TEST", credentials.Nonce);

            // 'personal_details' element decryption needs accompanying DataCredentials
            Assert.NotNull(credentials.SecureData.PersonalDetails);
            Assert.NotNull(credentials.SecureData.PersonalDetails.Data);
            Assert.NotEmpty(credentials.SecureData.PersonalDetails.Data.Secret);
            Assert.NotEmpty(credentials.SecureData.PersonalDetails.Data.DataHash);
        }

        [Fact(DisplayName = "Should decrypt 'personal_details' element")]
        public void Should_decreypt_personal_details_element()
        {
            RSA key = EncryptionKey.GetRsaPrivateKey();
            PassportData passportData = GetPassportData();

            IDecrypter decrypter = new Decrypter(key);
            Credentials credentials = decrypter.DecryptCredentials(passportData.Credentials);

            EncryptedPassportElement persDetEl = Assert.Single(passportData.Data, el => el.Type == "personal_details");

            string personalDetailsJson = decrypter.DecryptData(
                encryptedData: persDetEl.Data,
                dataCredentials: credentials.SecureData.PersonalDetails.Data
            );
            Assert.StartsWith("{", personalDetailsJson);

            PersonalDetails personalDetails = decrypter.DecryptData<PersonalDetails>(
                encryptedData: persDetEl.Data,
                dataCredentials: credentials.SecureData.PersonalDetails.Data
            );

            Assert.Equal("Poulad", personalDetails.FirstName);
            Assert.Equal("Ashrafpour", personalDetails.LastName);
            Assert.Equal("پولاد", personalDetails.FirstNameNative);
            Assert.Equal("اشرف پور", personalDetails.LastNameNative);
            Assert.Empty(personalDetails.MiddleName);
            Assert.Empty(personalDetails.MiddleNameNative);
            Assert.Equal("male", personalDetails.Gender);
            Assert.Equal(PassportEnums.Gender.Male, personalDetails.Gender);
            Assert.Equal("US", personalDetails.CountryCode); // U.S.A
            Assert.Equal("IR", personalDetails.ResidenceCountryCode); // Iran
            Assert.Equal("30.07.1990", personalDetails.BirthDate);
            Assert.InRange(personalDetails.Birthdate, new DateTime(1990, 7, 30), new DateTime(1990, 7, 30, 1, 0, 0));
        }

        static PassportData GetPassportData() =>
            JsonConvert.DeserializeObject<PassportData>(@"
{
  ""data"": [
    {
      ""type"": ""personal_details"",
      ""data"": ""z7wHqOsGuXWZPhdoOjxB6/ZxWtGw0xJsxQCFVX6mk+z+w3MD95pprEqo9CiJ/MBySERQSNwgmDKal+ZIoNsNpBPVVX8EzFiI8LuF8rFqgLAWVVhT5u+HtOJeDkccakzNWZijBgxezEi1x2kAeyMo5NeQkC0PB84TfWzBbQePwddqdlputsdftO82jilBeoMBF931EBUa6XE65xjmnXegNz927h6GqkVWNoADe6bzowzH+a+2oFFGVw3u1YD6n0iRd/C4vzy2l4BxRXqJd8dvTHd0cPxyd9tkY3hbHxzGceZb7/6El6W8a2VFXpVQY8rwkh1tnQY2M9jPXnRVWr/3GmHP+rtoCPpgp/RQeBYO412zefFeTLLgQcIlxby2sSagCVsxqSzfSli3GpYVBHezIq4t3mF6KkfEmlLVL7Bpn12mcJpq7gmMhgyu3FOiR0HfJQDLJagekaKDYwdjQyChWE9oonQDv5pGApHT8TZnmURBa2+mgKgNiFA6WXpIjf9PkN26MvjWsbHKQCwWGDfDsDFrwoMeQx6/8DKx61c7Lj0tlihKscBJuFItCmrSTTUBo9Ly8cKtTESFMchvRQtktTDuHpDXYWqNdHo/uUmlstja/jRYsEAck0ps6GXn6Ssx"",
      ""hash"": ""Y1xwNmvfOr+VVhy7kctRp/OHzOwsFi4mq70iJSD80Yw=""
    }
  ],
  ""credentials"": {
    ""data"": ""tHcn5IEhx7REdkb/C9BTEJW7Ftob4UFzl/vWQXADBqfTCG05OvvgMn6GYZQpi8qW92tREsju35adGvzX6+lJrcSZYPr3sRbok+2lBIBs/tIeGWl39HpTTHhQsMTCILnOsuqpJzYAq0TvbTcaq2rkD8qTG30fxbVNWpQbRJCvFkLH3ueuJfMHs/igP85QsO1sjz4915ZOPbsh9VR3x3dS+pKM+LCB4sQs2/o8Qy6jES1ZIHckTRNHNBfKeMnzlOPbTZHjJvAJ4B0P8sCpbzKQM/buRZhpLRsv5Pe9U61UNALSg/Vq98st41WKH35CaLME+dwHvO4a+xCO78GnySjNjCPsCjCqEWHEXtUtbodZsMw4sdjerwfC3LBpPJygjn8pAwyt2LjqRSjtwxqW86AdkkFpAW4qJJ2Uy70onxtY2M97yYXRkizIt6y/sLvkh2mRWW917lUhdTf/M3YaTK6kiQXhWPTX/78U8AtXvhiw07iMRxVwRmHKyAVyI334C3ZKiY0rscRAVwYlrCHFtVcxMQ=="",
    ""hash"": ""QN3IRzvFR9k/yvDfi9ChnQtIHo8No2SZm3iGwwj4NVk="",
    ""secret"": ""Sr/17/6JrtKCP7X/e9c7XIMAdigeI1QO6u43prhnS9wuNralsZhvPnKIc3qL7A2jcgML273TM2blHywbzt6cAqLxjCntyjSay0FyMnctarY3soCkCZsUynMsPC9g39CTVBCUXbZZ6tWZ8mgQ9WDXeMVTRaLXLBr9EZdICauFGsln/LaopfU9CvdYXQ0PcdhCFNbisuPwXOqd5jUu0x49+sPAc4V68TsnWRUC3CYEhEfqkRmtomM8UV+/JyHk0zYdiRxarGzAXfgdXJwjfjXARhERA/hZRYKH+w9vsPpZWdqQg7zSi5EU8Fr2Cs3IzAes+txLUekFprWsKff7j21KXg==""
  }
}
            ");
    }
}
