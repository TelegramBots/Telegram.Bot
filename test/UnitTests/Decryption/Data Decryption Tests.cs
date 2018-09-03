// ReSharper disable CheckNamespace
// ReSharper disable StringLiteralTypo

using System;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Passport;
using Telegram.Bot.Types.Passport;
using Xunit;

namespace UnitTests
{
    /// <summary>
    /// Tests for decrypting data using <see cref="IDecrypter.DecryptData{TValue}"/> method
    /// </summary>
    public class DataDecryptionTests
    {
        [Fact(DisplayName = "Should decrypt address data")]
        public void Should_Decrypt_Address_Data()
        {
            const string data =
                "r9y49J5oJiFTmPzvFtqf80ngL2Ymr90QzmTBptvhFsovZ4yBc06CU2wPhq0hSSLOkmbJq4NTy54sCIpmpIw7rM/QQQYvS5NRWH" +
                "S7wHpSrgCU0FIP6G1Jp1Gx36ksy3/Z6KAyHY85LX99Odjl0SD3iIArtIQXNFHxIypNZWzdVgyWXKiOtBkKztAEYL+6vRJ8Uj1j" +
                "5njMCThNJg3T0Ju+3QpjNUYpKd/dgOZRcm/z1ae0pcMiIUO0mpoeV0okSDnOEUGTTj3J2yrSxjeF39okufr0bwCQZQ7xQ8px2n" +
                "BMiKuxwxGID9d9EjiQBvFofzNtDw56H/KQNwe57M4FfrV4Gv2JM+q3RyI0/81gOc+hXnIOq9Hi6PXl5DBfuPqQq7a6d0+WeyL2" +
                "Q7/ruRwknggmUFxmgPadmZMTZS1XwcYBQtvLnXUtBiK+/asCCbNsZsM9qcUcpUn2hYIlpqu16Un7cA==";

            DataCredentials dataCredentials = new DataCredentials
            {
                DataHash = "s8B6UA9rwy3Z+rNvqSyJf/qGyKD01XnWDkF+esIzm14=",
                Secret = "s5+CjA48fIOabQuvTHJGu5JLvPrCbhN/AFtJg5hxJg4=",
            };

            IDecrypter decrypter = new Decrypter();
            ResidentialAddress address = decrypter.DecryptData<ResidentialAddress>(data, dataCredentials);

            Assert.Equal("123 Maple Street", address.StreetLine1);
            Assert.Equal("Unit 4", address.StreetLine2);
            Assert.Equal("A1A 1A1", address.PostCode);
            Assert.Equal("Toronto", address.City);
            Assert.Equal("Ontario", address.State);
            Assert.Equal("CA", address.CountryCode);
        }

        [Fact(DisplayName = "Should throw when null data is passed")]
        public void Should_Throw_If_Null_EncryptedContent()
        {
            IDecrypter decrypter = new Decrypter();

            Exception exception = Assert.ThrowsAny<Exception>(() =>
                decrypter.DecryptData<IDecryptedValue>(null, null)
            );

            Assert.Matches(@"^Value cannot be null\.\s+Parameter name: encryptedData$", exception.Message);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact(DisplayName = "Should throw when null data credentials is passed")]
        public void Should_Throw_If_Null_DataCredentials()
        {
            IDecrypter decrypter = new Decrypter();

            Exception exception = Assert.ThrowsAny<Exception>(() =>
                decrypter.DecryptData<IDecryptedValue>("", null)
            );

            Assert.Matches(@"^Value cannot be null\.\s+Parameter name: dataCredentials$", exception.Message);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact(DisplayName = "Should throw when null secret is passed")]
        public void Should_Throw_If_Null_Secret()
        {
            IDecrypter decrypter = new Decrypter();
            Exception exception = Assert.ThrowsAny<Exception>(() =>
                decrypter.DecryptData<IDecryptedValue>("", new DataCredentials())
            );

            Assert.Matches(@"^Value cannot be null\.\s+Parameter name: Secret$", exception.Message);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact(DisplayName = "Should throw when null data_hash is passed")]
        public void Should_Throw_If_Null_Hash()
        {
            IDecrypter decrypter = new Decrypter();

            DataCredentials dataCredentials = new DataCredentials {Secret = ""};
            Exception exception = Assert.ThrowsAny<Exception>(() =>
                decrypter.DecryptData<IDecryptedValue>("", dataCredentials)
            );

            Assert.Matches(@"^Value cannot be null\.\s+Parameter name: DataHash$", exception.Message);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact(DisplayName = "Should throw when data string is empty")]
        public void Should_Throw_If_Empty_Data_String_Length()
        {
            IDecrypter decrypter = new Decrypter();
            DataCredentials dataCredentials = new DataCredentials {Secret = "", DataHash = ""};

            Exception exception = Assert.ThrowsAny<Exception>(() =>
                decrypter.DecryptData<IDecryptedValue>("", dataCredentials)
            );

            Assert.Matches(@"^Data is empty\.\s+Parameter name: encryptedData", exception.Message);
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact(DisplayName = "Should throw when data string has invalid length")]
        public void Should_Throw_If_Invalid_Data_String_Length()
        {
            IDecrypter decrypter = new Decrypter();
            DataCredentials dataCredentials = new DataCredentials {Secret = "", DataHash = ""};

            Exception exception = Assert.ThrowsAny<Exception>(() =>
                decrypter.DecryptData<IDecryptedValue>("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==", dataCredentials)
            );

            Assert.Equal("Data length is not divisible by 16: 31.", exception.Message);
            Assert.IsType<PassportDataDecryptionException>(exception);
        }

        [Theory(DisplayName = "Should throw when secret is not a valid base64-encoded string")]
        [InlineData("foo")]
        [InlineData("FooBarBazlg/H/pEaTJigo4mQJ0s8B+HGCWKTWtOTIdo=")]
        public void Should_Throw_If_Invalid_Secret(string secret)
        {
            DataCredentials dataCredentials = new DataCredentials {Secret = secret, DataHash = ""};
            IDecrypter decrypter = new Decrypter();

            Assert.Throws<FormatException>(() =>
                decrypter.DecryptData<IDecryptedValue>("dGV4dCBvZiAxNiBjaGFycw==", dataCredentials)
            );
        }

        [Theory(DisplayName = "Should throw when data_hash is not a valid base64-encoded string")]
        [InlineData("foo")]
        [InlineData("FooBarBazlg/H/pEaTJigo4mQJ0s8B+HGCWKTWtOTIdo=")]
        public void Should_Throw_If_Invalid_Data_Hash(string dataHash)
        {
            DataCredentials dataCredentials = new DataCredentials {Secret = "", DataHash = dataHash};
            IDecrypter decrypter = new Decrypter();

            Assert.ThrowsAny<FormatException>(() =>
                decrypter.DecryptData<IDecryptedValue>("dGV4dCBvZiAxNiBjaGFycw==", dataCredentials)
            );
        }

        [Theory(DisplayName = "Should throw when length of data_hash is not 32")]
        [InlineData("")]
        [InlineData("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==")]
        [InlineData("Zm9v")]
        public void Should_Throw_If_Invalid_Data_Hash_Length(string dataHash)
        {
            DataCredentials dataCredentials = new DataCredentials {Secret = "", DataHash = dataHash};
            IDecrypter decrypter = new Decrypter();

            Exception exception = Assert.ThrowsAny<Exception>(() =>
                decrypter.DecryptData<IDecryptedValue>("dGV4dCBvZiAxNiBjaGFycw==", dataCredentials)
            );

            Assert.Matches(@"^Hash length is not 32: \d+\.$", exception.Message);
            Assert.IsType<PassportDataDecryptionException>(exception);
        }

        [Fact(DisplayName = "Should throw when trying to decrypt unencrypted data with invalid data length")]
        public void Should_Throw_Decrypting_Unencrypted_Data_Invalid_Length()
        {
            DataCredentials dataCredentials = new DataCredentials
            {
                DataHash = "v3q47iscI6TS94CMo7HGQUOxw28LIf82NJBkImzP57c=",
                Secret = "vF7nut7clg/H/pEaTJigo4mQJ0s8B+HGCWKTWtOTIdo=",
            };

            IDecrypter decrypter = new Decrypter();
            Exception exception = Assert.ThrowsAny<Exception>(() =>
                decrypter.DecryptData<IDecryptedValue>("dGV4dCBvZiAxNiBjaGFycw==", dataCredentials)
            );

            Assert.Matches(@"^Data hash mismatch at position \d+\.$", exception.Message);
            Assert.IsType<PassportDataDecryptionException>(exception);
        }

        [Fact(DisplayName = "Should throw when decrypting data(address) with wrong data credentials(personal details)")]
        public void Should_Throw_Decrypting_Data_With_Wrong_DataCredentials()
        {
            const string data =
                "r9y49J5oJiFTmPzvFtqf80ngL2Ymr90QzmTBptvhFsovZ4yBc06CU2wPhq0hSSLOkmbJq4NTy54sCIpmpIw7rM/QQQYvS5NRWH" +
                "S7wHpSrgCU0FIP6G1Jp1Gx36ksy3/Z6KAyHY85LX99Odjl0SD3iIArtIQXNFHxIypNZWzdVgyWXKiOtBkKztAEYL+6vRJ8Uj1j" +
                "5njMCThNJg3T0Ju+3QpjNUYpKd/dgOZRcm/z1ae0pcMiIUO0mpoeV0okSDnOEUGTTj3J2yrSxjeF39okufr0bwCQZQ7xQ8px2n" +
                "BMiKuxwxGID9d9EjiQBvFofzNtDw56H/KQNwe57M4FfrV4Gv2JM+q3RyI0/81gOc+hXnIOq9Hi6PXl5DBfuPqQq7a6d0+WeyL2" +
                "Q7/ruRwknggmUFxmgPadmZMTZS1XwcYBQtvLnXUtBiK+/asCCbNsZsM9qcUcpUn2hYIlpqu16Un7cA==";

            DataCredentials wrongDataCredentials = new DataCredentials
            {
                DataHash = "IeyXbEWBTXXQYG+O0vv7munuGs0H0S4Jr7jzYV1ltCk=",
                Secret = "v9Hx0oaQHLxyGuZdiYmszC3gTGyfYnc57zWLg+WaMus=",
            };

            IDecrypter decrypter = new Decrypter();
            Exception exception = Assert.ThrowsAny<Exception>(() =>
                decrypter.DecryptData<IDecryptedValue>(
                    data,
                    wrongDataCredentials
                )
            );

            Assert.Matches(@"^Data hash mismatch at position \d+\.$", exception.Message);
            Assert.IsType<PassportDataDecryptionException>(exception);
        }
    }
}
