// ReSharper disable InconsistentNaming
// ReSharper disable PossibleNullReferenceException
// ReSharper disable CheckNamespace
// ReSharper disable StringLiteralTypo

using Newtonsoft.Json;
using Telegram.Bot.Passport;
using Telegram.Bot.Tests.Unit.Passport;
using Telegram.Bot.Types.Passport;
using Xunit;

namespace UnitTests
{
    /// <summary>
    /// Tests for decryption of "message.passport_data" received for authorization request with the scope
    /// of "address"
    /// </summary>
    public class AddressDecryptionTests
    {
        [Fact(DisplayName = "Should decrypt 'passport_data.credentials'")]
        public void Should_decrypt_credentials()
        {
            PassportData passportData = GetPassportData();

            IDecrypter decrypter = new Decrypter();

            Credentials credentials =
                decrypter.DecryptCredentials(passportData.Credentials, EncryptionKey.RsaPrivateKey);

            Assert.NotNull(credentials);
            Assert.NotNull(credentials.SecureData);
            Assert.NotEmpty(credentials.Nonce);
            Assert.Equal("TEST", credentials.Nonce);

            // 'address' element decryption needs accompanying DataCredentials
            Assert.NotNull(credentials.SecureData.Address);
            Assert.NotNull(credentials.SecureData.Address.Data);
            Assert.NotEmpty(credentials.SecureData.Address.Data.Secret);
            Assert.NotEmpty(credentials.SecureData.Address.Data.DataHash);
        }

        [Fact(DisplayName = "Should decrypt 'address' element")]
        public void Should_decrypt_address_element()
        {
            PassportData passportData = GetPassportData();

            IDecrypter decrypter = new Decrypter();
            Credentials credentials =
                decrypter.DecryptCredentials(passportData.Credentials, EncryptionKey.RsaPrivateKey);

            EncryptedPassportElement addressEl = Assert.Single(passportData.Data, el => el.Type == "address");
            Assert.NotNull(addressEl.Data);

            ResidentialAddress residentialAddress = decrypter.DecryptData<ResidentialAddress>(
                encryptedData: addressEl.Data,
                dataCredentials: credentials.SecureData.Address.Data
            );

            Assert.Equal("123 Maple Street", residentialAddress.StreetLine1);
            Assert.Equal("Unit 4", residentialAddress.StreetLine2);
            Assert.Equal("A1A 1A1", residentialAddress.PostCode);
            Assert.Equal("Toronto", residentialAddress.City);
            Assert.Equal("Ontario", residentialAddress.State);
            Assert.Equal("CA", residentialAddress.CountryCode);
        }

        static PassportData GetPassportData() =>
            JsonConvert.DeserializeObject<PassportData>(@"
{
  ""data"": [
    {
      ""type"": ""address"",
      ""data"": ""r9y49J5oJiFTmPzvFtqf80ngL2Ymr90QzmTBptvhFsovZ4yBc06CU2wPhq0hSSLOkmbJq4NTy54sCIpmpIw7rM/QQQYvS5NRWHS7wHpSrgCU0FIP6G1Jp1Gx36ksy3/Z6KAyHY85LX99Odjl0SD3iIArtIQXNFHxIypNZWzdVgyWXKiOtBkKztAEYL+6vRJ8Uj1j5njMCThNJg3T0Ju+3QpjNUYpKd/dgOZRcm/z1ae0pcMiIUO0mpoeV0okSDnOEUGTTj3J2yrSxjeF39okufr0bwCQZQ7xQ8px2nBMiKuxwxGID9d9EjiQBvFofzNtDw56H/KQNwe57M4FfrV4Gv2JM+q3RyI0/81gOc+hXnIOq9Hi6PXl5DBfuPqQq7a6d0+WeyL2Q7/ruRwknggmUFxmgPadmZMTZS1XwcYBQtvLnXUtBiK+/asCCbNsZsM9qcUcpUn2hYIlpqu16Un7cA=="",
      ""hash"": ""AUwqQH5aIPdALyMZyAMWGu1sTw26RVmgPdyA2RqX1f8=""
    }
  ],
  ""credentials"": {
    ""data"": ""4XxSgNDwYAG8N9UvpWCMGvo4NhJUo+zlnkTTyrakcStPSAozeTDpCs90/cWNolA8/5UtCv6+F+N5jil7z/sTMsSkbEVzROUUkS9P9cBFyRbhZGbwVMTOQQfjsqcxiOwu4y2/AMf+pIyjYB7orXO3DIOBs+AF1BRs99WxQ07sgCCGINlTLlNOf73iP/Y1P6E7W5+u4NBUUA+OWbzTFr65jLybWEEWySgWsex8rPzQP5fG9tidwEAanYYQsU5FSuGpYolf+0qpPveSF3Gew2Pwf+Hh1v12CM1N9iQ0f98+fWVcD65wupR0VYqbfTWt24jzIFhQiBNklDC30CPeNKOTNXtGneQKgwKg1yUCLUYiH4C7ejgWkVZN342k2FjX3FukbYIsQNdpzlR+07qDJZIYbA=="",
    ""hash"": ""fLmR8WmXFST1R/0cM6bTnIUrifburBCcmIBVHhuhLpU="",
    ""secret"": ""gBm0me6sRCLjtLcDvaSI812OoVvXMVKTa0Zo5sq4N6ZczfRoBxF0Gh/05OiAebXuL5RSJsqcuIQxYkEAOO/HDi+lLScE1aF1t1YvJw9Hb0Ct0wf/ucaNVDigGTtOZXbza83wy59kUD9EqxLD4j7nNqzxgPugzGRqpVxgxLJAExN5fPa5jwe8qHIe09rfm4e7cr9K3+oN5UHorMez+aI0xpu3z9Vbmq9mGC41DuIxJ11ERVqDGwdyjdVwdo7X5kSjkopJFRR8kH2CYuufC8YDOSoMyFyFdsM9Y35e7PVA+u0AE3O1wWkE0x1XKdNn87z7aSmQe3zna4EcfexfsUee3g==""
  }
}
            ");
    }
}
