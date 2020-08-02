// ReSharper disable StringLiteralTypo

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot.Passport;
using Telegram.Bot.Types.Passport;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Passport.Single_Scope_Requests
{
    /// <summary>
    /// Tests for decryption of "message.passport_data" received for authorization request with the scope
    /// of "driver_license"
    /// </summary>
    public class DriverLicenseDecryptionTests
    {
        [Fact(DisplayName = "Should decrypt 'passport_data.credentials'")]
        public void Should_Decrypt_Credentials()
        {
            PassportData passportData = GetPassportData();

            IDecrypter decrypter = new Decrypter();

            Credentials credentials = decrypter.DecryptCredentials(
                passportData.Credentials,
                EncryptionKey.RsaPrivateKey
            );

            Assert.NotNull(credentials);
            Assert.NotNull(credentials.SecureData);
            Assert.NotNull(credentials.Nonce);
            Assert.NotEmpty(credentials.Nonce);
            Assert.Equal("TEST", credentials.Nonce);

            // decryption of document data in 'driver_license' element requires accompanying DataCredentials
            Assert.NotNull(credentials.SecureData.DriverLicense);
            Assert.NotNull(credentials.SecureData.DriverLicense.Data);
            Assert.NotEmpty(credentials.SecureData.DriverLicense.Data.Secret);
            Assert.NotEmpty(credentials.SecureData.DriverLicense.Data.DataHash);

            // decryption of front side file in 'driver_license' element requires accompanying FileCredentials
            Assert.NotNull(credentials.SecureData.DriverLicense.FrontSide);
            Assert.NotEmpty(credentials.SecureData.DriverLicense.FrontSide.Secret);
            Assert.NotEmpty(credentials.SecureData.DriverLicense.FrontSide.FileHash);

            // decryption of selfie file in 'driver_license' element requires accompanying FileCredentials
            Assert.NotNull(credentials.SecureData.DriverLicense.Selfie);
            Assert.NotEmpty(credentials.SecureData.DriverLicense.Selfie.Secret);
            Assert.NotEmpty(credentials.SecureData.DriverLicense.Selfie.FileHash);

            // decryption of translation file in 'driver_license' element requires accompanying FileCredentials
            Assert.NotNull(credentials.SecureData.DriverLicense.Translation);
            Assert.NotEmpty(credentials.SecureData.DriverLicense.Translation);
            FileCredentials translationFileCredentials = Assert.Single(
                credentials.SecureData.DriverLicense.Translation
            );
            Assert.NotNull(translationFileCredentials);
            Assert.NotEmpty(translationFileCredentials.Secret);
            Assert.NotEmpty(translationFileCredentials.FileHash);
        }

        [Fact(DisplayName = "Should decrypt document data of 'driver_license' element")]
        public void Should_Decrypt_Document_Data()
        {
            PassportData passportData = GetPassportData();

            IDecrypter decrypter = new Decrypter();
            Credentials credentials = decrypter.DecryptCredentials(
                passportData.Credentials,
                EncryptionKey.RsaPrivateKey
            );
            Assert.NotNull(credentials.SecureData.DriverLicense?.Data);

            EncryptedPassportElement licenseEl = Assert.Single(passportData.Data, el => el.Type == "driver_license");
            Assert.NotNull(licenseEl!.Data);

            IdDocumentData licenseDoc = decrypter.DecryptData<IdDocumentData>(
                encryptedData: licenseEl.Data,
                dataCredentials: credentials.SecureData.DriverLicense.Data
            );

            Assert.Equal("G544-061", licenseDoc.DocumentNo);
            Assert.Equal("26.11.2022", licenseDoc.ExpiryDate);
            Assert.NotNull(licenseDoc.Expiry);
            Assert.InRange(licenseDoc.Expiry.Value, new DateTime(2022, 11, 26), new DateTime(2022, 11, 26, 0, 0, 1));
        }

        [Fact(DisplayName = "Should decrypt front side photo file of 'driver_license' element")]
        public async Task Should_Decrypt_Front_Side_File()
        {
            PassportData passportData = GetPassportData();

            IDecrypter decrypter = new Decrypter();
            Credentials credentials = decrypter.DecryptCredentials(
                passportData.Credentials,
                EncryptionKey.RsaPrivateKey
            );

            Assert.NotNull(credentials.SecureData.DriverLicense);
            Assert.NotNull(credentials.SecureData.DriverLicense.FrontSide);

            byte[] encryptedContent = await File.ReadAllBytesAsync(
                "Files/Passport/driver_license-front_side.jpg.enc"
            );
            byte[] content = decrypter.DecryptFile(
                encryptedContent,
                credentials.SecureData.DriverLicense.FrontSide
            );
            Assert.NotEmpty(content);
            await File.WriteAllBytesAsync("Files/Passport/driver_license-front_side.jpg", content);

            await using MemoryStream encryptedFileStream = new MemoryStream(encryptedContent);
            await using MemoryStream decryptedFileStream = new MemoryStream();

            await decrypter.DecryptFileAsync(
                encryptedFileStream,
                credentials.SecureData.DriverLicense.FrontSide,
                decryptedFileStream
            );

            Assert.Equal(content, decryptedFileStream.ToArray());
        }

        [Fact(DisplayName = "Should decrypt reverse side photo file of 'driver_license' element")]
        public async Task Should_Decrypt_Reverse_Side_File()
        {
            PassportData passportData = GetPassportData();

            IDecrypter decrypter = new Decrypter();
            Credentials credentials = decrypter.DecryptCredentials(
                passportData.Credentials,
                EncryptionKey.RsaPrivateKey
            );

            Assert.NotNull(credentials.SecureData.DriverLicense);
            Assert.NotNull(credentials.SecureData.DriverLicense.ReverseSide);

            byte[] encryptedContent = await File.ReadAllBytesAsync(
                "Files/Passport/driver_license-reverse_side.jpg.enc"
            );
            byte[] content = decrypter.DecryptFile(
                encryptedContent,
                credentials.SecureData.DriverLicense.ReverseSide
            );
            Assert.NotEmpty(content);
            await File.WriteAllBytesAsync(
                "Files/Passport/driver_license-reverse_side.jpg",
                content
            );

            await using MemoryStream encryptedFileStream = new MemoryStream(encryptedContent);
            await using MemoryStream decryptedFileStream = new MemoryStream();

            await decrypter.DecryptFileAsync(
                encryptedFileStream,
                credentials.SecureData.DriverLicense.ReverseSide,
                decryptedFileStream
            );

            Assert.Equal(content, decryptedFileStream.ToArray());
        }

        [Fact(DisplayName = "Should decrypt selfie photo file of 'driver_license' element")]
        public async Task Should_Decrypt_Selfie_File()
        {
            PassportData passportData = GetPassportData();

            IDecrypter decrypter = new Decrypter();
            Credentials credentials = decrypter.DecryptCredentials(
                passportData.Credentials,
                EncryptionKey.RsaPrivateKey
            );

            Assert.NotNull(credentials.SecureData.DriverLicense);
            Assert.NotNull(credentials.SecureData.DriverLicense.Selfie);

            await using Stream encryptedFileStream = File.OpenRead("Files/Passport/driver_license-selfie.jpg.enc");
            await using Stream decryptedFileStream = File.OpenWrite("Files/Passport/driver_license-selfie.jpg");

            await decrypter.DecryptFileAsync(
                encryptedFileStream,
                credentials.SecureData.DriverLicense.Selfie,
                decryptedFileStream
            );
        }

        [Fact(DisplayName = "Should decrypt translation photo file of 'driver_license' element")]
        public async Task Should_Decrypt_Translation_File()
        {
            PassportData passportData = GetPassportData();

            IDecrypter decrypter = new Decrypter();
            Credentials credentials = decrypter.DecryptCredentials(
                passportData.Credentials,
                EncryptionKey.RsaPrivateKey
            );

            Assert.NotNull(credentials.SecureData.DriverLicense);
            Assert.NotNull(credentials.SecureData.DriverLicense.Translation);

            byte[] encryptedContent = await File.ReadAllBytesAsync(
                "Files/Passport/driver_license-translation0.jpg.enc"
            );
            byte[] content = decrypter.DecryptFile(
                encryptedContent,
                credentials.SecureData.DriverLicense.Translation.Single()
            );
            Assert.NotEmpty(content);
            await File.WriteAllBytesAsync("Files/Passport/driver_license-translation0.jpg", content);

            await using MemoryStream encryptedFileStream = new MemoryStream(encryptedContent);
            await using MemoryStream decryptedFileStream = new MemoryStream();

            await decrypter.DecryptFileAsync(
                encryptedFileStream,
                credentials.SecureData.DriverLicense.Translation.Single(),
                decryptedFileStream
            );

            Assert.Equal(content, decryptedFileStream.ToArray());
        }

        private static PassportData GetPassportData() =>
            JsonConvert.DeserializeObject<PassportData>(@"
{
  ""data"": [
    {
      ""type"": ""driver_license"",
      ""data"": ""+ThtmwC1Cq5JPUg8h3E3aSia0qFhuDCGBQap3FzC7MpPp32DtElU1WucEwEHi2zLmCQbSABZ4JbHREYGJhjfJJthRhrdmyvFQDWeU6kQjD2FG2QzPCWB1hNorAXX/X9pLRZiHUrmbqMcU2Mch/X/jVEQcTAXYwkTUdit3ZU+aGzz79tXos4ZCHfnBf5bKww/1n54TPLsVwOa4TAywcaahsJdCDZn2gxCNig2/dY7nfU="",
      ""front_side"": {
        ""file_id"": ""DgADAQADQwAD8dA5RLvzieFGLU4nAg"",
        ""file_unique_id"": ""AQADAv8sGwAEPjQGAAE"",
        ""file_date"": 1535597542
      },
      ""reverse_side"": {
        ""file_id"": ""DgADAQADHAADkeFARJRE1buibKe-Ag"",
        ""file_unique_id"": ""AQADAv8sGwAEPjQGAAE"",
        ""file_date"": 1535597542
      },
      ""selfie"": {
        ""file_id"": ""DgADAQADLgADdYs5RME9OcP0l7GQAg"",
        ""file_unique_id"": ""AQADAv8sGwAEPjQGAAE"",
        ""file_date"": 1535597542
      },
      ""translation"": [
        {
          ""file_id"": ""DgADAQADMQADnHM4RKOZghHOVNRHAg"",
          ""file_unique_id"": ""AQADAv8sGwAEPjQGAAE"",
          ""file_date"": 1535597788
        }
      ],
      ""hash"": ""49P+iSr6aZsnB1bMsPyUva1zGVTUJtOX+XwUGrAqWng=""
    }
  ],
  ""credentials"": {
    ""data"": ""nh/lJN9e0kATdoWVU01ellh1Hdl4IkqkJIxtIUMwi37wx5cTsZC0d2NFOs6W2WYaGkOzvxyvAEuIK5UlVS05Wyjr7cMImY7hcO5v8FZyIXCs1PGnOEBr+U3kB7adWoAIKyfcBlQlTnqlRJaUXoqTZuErqpcI/XBAZWMSj09oQ4vxB7ReQxQxLLTw3PHsfl6Oij52tvQrjtfMQSChyEB2BS29xmykn/cx3Bx8866x2HSlD47MU8NxQR0rxwesXzqS6sz7IJh/iRbJVpCeSRxgGtczrks7qT33GUlGd7uLOqv/vss5Yhn08Vo3nwemew3/JPoJ0ZORYa5+CR4L/SZqtGNIdhqXLWhvGyPa6VW3VrS27juzHarGQDiEBBO1+TzQ/uPyEmLcurcJ1d8uOnD61lIkyVzd6E0IGfRw/VPv0H0BJpKbh81hD012ESl4CxKLnnWBbwkqIiImCLmMZMp8bxlQn4oV1MXhSqVEODUVyThKq6+kAYBMX6Nse/w0xt0UJxwfKojYcWnBR3SSSBAr3TZOjguZ8DPIixVGDgKSArmgEdYx71XYrfCjM97iDJ/3BJENTxHX7zfbCXBrt41HhCsZ09MaWFlWUUivMDelCsxY7a/yiODkh1rFdTjzCm11s8kvp9wEXU8EXSqNJ0BoC+AbIgdp4n3r0hjbPkPvN+0dAuBXHPpCfMfJRSadscAnWJG0NTnfIbE9GLcOCIUt90d0Fx1yoCNEmMFUiifofOzpr5nvirAU6cHNESRh/cCJTnMF21YuE6wPR0TK3f9326sG4xmYE65gya9xVNZaA7TSME9w7M9gADDERcAUPqUlQVFejWdT/VfTDcFCgpcGNnF4MSP8dWMmOWBYQOJU9WgK1xzftrL30et8w/OAFFsYx3eOVPnnby0KVeEZFj4AI2BQu46JBaKjE0+gTU+7KC49icXzleVniMSLF+5jUO964ZGEOuiubmEmc+Ncxvs5C2+c5zcptGq75JBd6jHxMNdLCxUa5eubShysT+1VSxOepemjvHKIkxkdFMtr3r0rs0ilRBw4tbspoVcPZ1/abb/KD99qYqIse8H37OmWm2LcQnknZvi8TsFNAPaW4+2+Ko2E1sfMrvB3+EJMGyUhqkvvC5XRYvK7kXr3xrxX48w9"",
    ""hash"": ""y8fTLZW7Gvf1c25tQY/bMjzbnGpnfGHPKnEC9Vk39Us="",
    ""secret"": ""J4j2cRBWuNLRc6yXsCL8RgzKSDZAlS27uFqFw3pMs+w3ScHDLcQgk/6+QidKSAzX0EccS6rbZ0UTDoSEptvdUT61A4hqMG61kbczf0UAopVQAeqlTkbZfgiUUXj5hpAKJI2Z/o78UWzRH6hoFhqPN1T+zs4FAhBEbv6nF1K2Rav8SOmE5OXa7B4a31FhH/1b47uAT1AxskzJZ6LjY6UrgkHU4/em413L0Boyl/nh1PNmgoTFCd3S+CnujpyZW67rBuNodFzJAEzXTe8M4bm/diGXNjht+mq0vB8dnwkGcNKFNVv6wWqvNWY8AZdJDdZChW+N4weATQGUAAgNQax1Tw==""
  }
}
            ");
    }
}
