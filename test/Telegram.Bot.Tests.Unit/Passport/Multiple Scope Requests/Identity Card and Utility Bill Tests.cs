// ReSharper disable StringLiteralTypo

using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot.Passport;
using Telegram.Bot.Types.Passport;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Passport.Multiple_Scope_Requests
{
    /// <summary>
    /// Tests for decryption of passport_data received for authorization request with the scopes
    /// "identity_card" and "utility_bill"
    /// </summary>
    public class IdentityCardAndUtilityBillTests
    {
        [Fact(DisplayName = "Should decrypt 'passport_data.credentials'")]
        public void Should_Decrypt_Credentials()
        {
            RSA key = EncryptionKey.RsaPrivateKey;
            PassportData passData = GetPassportData();

            IDecrypter decrypter = new Decrypter();

            Credentials credentials = decrypter.DecryptCredentials(
                encryptedCredentials: passData.Credentials,
                key: key
            );

            Assert.NotNull(credentials);
            Assert.NotNull(credentials.SecureData);
            Assert.Equal("TEST", credentials.Nonce);

            // decryption of document data in 'identity_card' element requires accompanying DataCredentials
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
            Assert.NotNull(credentials.SecureData.UtilityBill?.Files);
            FileCredentials billFileCredentials = Assert.Single(credentials.SecureData.UtilityBill.Files);
            Assert.NotNull(billFileCredentials);
            Assert.NotEmpty(billFileCredentials.Secret);
            Assert.NotEmpty(billFileCredentials.FileHash);

            // decryption of translation file scan in 'utility_bill' element requires accompanying FileCredentials
            Assert.NotNull(credentials.SecureData.UtilityBill.Files);
            Assert.NotNull(credentials.SecureData.UtilityBill.Translation);
            FileCredentials billTranslationFileCredentials =
                Assert.Single(credentials.SecureData.UtilityBill.Translation);
            Assert.NotNull(billTranslationFileCredentials?.Secret);
            Assert.NotEmpty(billTranslationFileCredentials.Secret);
            Assert.NotEmpty(billTranslationFileCredentials.FileHash);
        }

        [Fact(DisplayName = "Should decrypt docuemnt data in 'identity_card' element")]
        public void Should_Decrypt_Element_Document()
        {
            PassportData passportData = GetPassportData();

            IDecrypter decrypter = new Decrypter();
            Credentials credentials = decrypter.DecryptCredentials(
                passportData.Credentials,
                EncryptionKey.RsaPrivateKey
            );

            EncryptedPassportElement idCardEl = Assert.Single(passportData.Data, el => el.Type == "identity_card");
            Assert.NotNull(idCardEl!.Data);
            Assert.NotNull(credentials.SecureData.IdentityCard?.Data);

            IdDocumentData documentData = decrypter.DecryptData<IdDocumentData>(
                idCardEl.Data,
                credentials.SecureData.IdentityCard.Data
            );

            Assert.Equal("9999R", documentData.DocumentNo);
            Assert.NotNull(documentData.ExpiryDate);
            Assert.Empty(documentData.ExpiryDate);
            Assert.Null(documentData.Expiry);
        }

        [Fact(DisplayName = "Should decrypt front side photo in 'identity_card' element")]
        public async Task Should_Decrypt_Identity_Card_Element_Front_Side()
        {
            PassportData passportData = GetPassportData();
            EncryptedPassportElement idCardEl = Assert.Single(passportData.Data, el => el.Type == "identity_card");

            Assert.NotNull(idCardEl?.FrontSide);
            Assert.Equal("DgADAQADGwADQnBBRBexahgtkoPgAg", idCardEl.FrontSide.FileId);
            Assert.InRange(idCardEl.FrontSide.FileDate, new DateTime(2018, 8, 30), new DateTime(2018, 8, 31));
            Assert.Null(idCardEl.FrontSide.FileSize);

            IDecrypter decrypter = new Decrypter();
            Credentials credentials = decrypter.DecryptCredentials(
                passportData.Credentials,
                EncryptionKey.RsaPrivateKey
            );

            Assert.NotNull(credentials.SecureData.IdentityCard?.FrontSide);

            byte[] encryptedContent = await File.ReadAllBytesAsync("Files/Passport/identity_card-front_side.jpg.enc");
            byte[] content = decrypter.DecryptFile(
                encryptedContent,
                credentials.SecureData.IdentityCard.FrontSide
            );

            Assert.NotEmpty(content);

            await File.WriteAllBytesAsync("Files/Passport/identity_card-front_side.jpg", content);

            await using MemoryStream encryptedFileStream = new MemoryStream(encryptedContent);
            await using MemoryStream decryptedFileStream = new MemoryStream();

            await decrypter.DecryptFileAsync(
                encryptedFileStream,
                credentials.SecureData.IdentityCard.FrontSide,
                decryptedFileStream
            );

            Assert.Equal(content, decryptedFileStream.ToArray());
        }

        [Fact(DisplayName = "Should decrypt reverse side photo in 'identity_card' element")]
        public async Task Should_Decrypt_Identity_Card_Element_Reverse_Side()
        {
            PassportData passportData = GetPassportData();
            EncryptedPassportElement idCardEl = Assert.Single(passportData.Data, el => el.Type == "identity_card");

            Assert.NotNull(idCardEl!.ReverseSide);
            Assert.Equal("DgADAQADKAADNfRARK9jbzh5AAFqvAI", idCardEl.ReverseSide.FileId);
            Assert.InRange(idCardEl.ReverseSide.FileDate, new DateTime(2018, 8, 30), new DateTime(2018, 8, 31));
            Assert.Null(idCardEl.ReverseSide.FileSize);

            IDecrypter decrypter = new Decrypter();
            Credentials credentials = decrypter.DecryptCredentials(
                passportData.Credentials,
                EncryptionKey.RsaPrivateKey
            );
            Assert.NotNull(credentials.SecureData.IdentityCard);
            Assert.NotNull(credentials.SecureData.IdentityCard.ReverseSide);

            byte[] encryptedContent =
                await File.ReadAllBytesAsync("Files/Passport/identity_card-reverse_side.jpg.enc");
            byte[] content = decrypter.DecryptFile(
                encryptedContent,
                credentials.SecureData.IdentityCard.ReverseSide
            );

            Assert.NotEmpty(content);

            await File.WriteAllBytesAsync("Files/Passport/identity_card-reverse_side.jpg", encryptedContent);

            await using MemoryStream encryptedFileStream = new MemoryStream(encryptedContent);
            await using MemoryStream decryptedFileStream = new MemoryStream();

            await decrypter.DecryptFileAsync(
                encryptedFileStream,
                credentials.SecureData.IdentityCard.ReverseSide,
                decryptedFileStream
            );

            Assert.Equal(content, decryptedFileStream.ToArray());
        }

        [Fact(DisplayName = "Should decrypt selfie photo in 'identity_card' element")]
        public async Task Should_decrypt_identity_card_element_selfie()
        {
            PassportData passportData = GetPassportData();
            EncryptedPassportElement idCardEl = Assert.Single(passportData.Data, el => el.Type == "identity_card");

            Assert.NotNull(idCardEl!.Selfie);
            Assert.Equal("DgADAQADNAADA1BJRCUHz9fqxiqJAg", idCardEl.Selfie.FileId);
            Assert.InRange(idCardEl.Selfie.FileDate, new DateTime(2018, 8, 30), new DateTime(2018, 8, 31));
            Assert.Null(idCardEl.Selfie.FileSize);

            IDecrypter decrypter = new Decrypter();
            Credentials credentials = decrypter.DecryptCredentials(
                passportData.Credentials,
                EncryptionKey.RsaPrivateKey
            );

            Assert.NotNull(credentials.SecureData.IdentityCard);
            Assert.NotNull(credentials.SecureData.IdentityCard.Selfie);

            byte[] encryptedContent = await File.ReadAllBytesAsync("Files/Passport/identity_card-selfie.jpg.enc");
            byte[] content = decrypter.DecryptFile(
                encryptedContent,
                credentials.SecureData.IdentityCard.Selfie
            );

            Assert.NotEmpty(content);

            await File.WriteAllBytesAsync("Files/Passport/identity_card-selfie.jpg", content);

            await using MemoryStream encryptedFileStream = new MemoryStream(encryptedContent);
            await using MemoryStream decryptedFileStream = new MemoryStream();

            await decrypter.DecryptFileAsync(
                encryptedFileStream,
                credentials.SecureData.IdentityCard.Selfie,
                decryptedFileStream
            );

            Assert.Equal(content, decryptedFileStream.ToArray());
        }

        [Fact(DisplayName = "Should decrypt the single file in 'utility_bill' element")]
        public async Task Should_Decrypt_Utility_Bill_Element_File()
        {
            PassportData passportData = GetPassportData();
            EncryptedPassportElement billElement = Assert.Single(passportData.Data, el => el.Type == "utility_bill");

            Assert.NotNull(billElement!.Files);
            PassportFile scanFile = Assert.Single(billElement.Files);

            Assert.Equal("DgADAQADQAADPupBRDIrCqSwkb4iAg", scanFile.FileId);
            Assert.InRange(scanFile.FileDate, new DateTime(2018, 8, 30), new DateTime(2018, 8, 31));
            Assert.Null(scanFile.FileSize);

            IDecrypter decrypter = new Decrypter();
            Credentials credentials = decrypter.DecryptCredentials(
                passportData.Credentials,
                EncryptionKey.RsaPrivateKey
            );

            Assert.NotNull(credentials.SecureData.UtilityBill);
            Assert.NotNull(credentials.SecureData.UtilityBill.Files);
            FileCredentials billFileCredentials = Assert.Single(credentials.SecureData.UtilityBill.Files);

            byte[] encryptedContent = await File.ReadAllBytesAsync("Files/Passport/utility_bill.jpg.enc");
            byte[] content = decrypter.DecryptFile(
                encryptedContent,
                billFileCredentials!
            );

            Assert.NotEmpty(content);

            await File.WriteAllBytesAsync("Files/Passport/utility_bill.jpg", content);

            await using MemoryStream encryptedFileStream = new MemoryStream(encryptedContent);
            await using MemoryStream decryptedFileStream = new MemoryStream();

            await decrypter.DecryptFileAsync(
                encryptedFileStream,
                billFileCredentials,
                decryptedFileStream
            );

            Assert.Equal(content, decryptedFileStream.ToArray());
        }

        [Fact(DisplayName = "Should decrypt the single translation file in 'utility_bill' element")]
        public async Task Should_Decrypt_Utility_Bill_Element_Translation()
        {
            PassportData passportData = GetPassportData();
            EncryptedPassportElement billElement = Assert.Single(passportData.Data, el => el.Type == "utility_bill");

            Assert.NotNull(billElement!.Translation);
            PassportFile translationFile = Assert.Single(billElement.Translation);

            Assert.Equal("DgADAQADOwADGV9BRP4b7RLGAtUKAg", translationFile!.FileId);
            Assert.InRange(translationFile.FileDate, new DateTime(2018, 8, 30), new DateTime(2018, 8, 31));
            Assert.Null(translationFile.FileSize);

            IDecrypter decrypter = new Decrypter();
            Credentials credentials = decrypter.DecryptCredentials(
                passportData.Credentials,
                EncryptionKey.RsaPrivateKey
            );

            Assert.NotNull(credentials.SecureData.UtilityBill);
            Assert.NotNull(credentials.SecureData.UtilityBill.Translation);
            FileCredentials translationFileCredentials = Assert.Single(credentials.SecureData.UtilityBill.Translation);

            byte[] encryptedContent = await File.ReadAllBytesAsync("Files/Passport/utility_bill-translation.jpg.enc");
            byte[] content = decrypter.DecryptFile(
                encryptedContent,
                translationFileCredentials!
            );

            Assert.NotEmpty(content);

            await File.WriteAllBytesAsync("Files/Passport/utility_bill-translation.jpg", content);

            await using MemoryStream encryptedFileStream = new MemoryStream(encryptedContent);
            await using MemoryStream decryptedFileStream = new MemoryStream();

            await decrypter.DecryptFileAsync(
                encryptedFileStream,
                translationFileCredentials,
                decryptedFileStream
            );

            Assert.Equal(content, decryptedFileStream.ToArray());
        }

        static PassportData GetPassportData() =>
            JsonConvert.DeserializeObject<PassportData>(@"
{
  ""data"": [
    {
      ""type"": ""identity_card"",
      ""data"": ""Xi+gxIkl9rgOvnK6NNT1kg8mf8DaXusx0gkENI/QrUTdQ7qfdT/FhOI8nq/xUiGVuX3QlBWT2kVk0CJ0NFhckQ+tbicHuErxq9+80hjBsaoRp2j6CDxU6gl1B3ZfJ9nVnk/HNMiXGfnz8GVk7XAp2A=="",
      ""front_side"": {
        ""file_id"": ""DgADAQADGwADQnBBRBexahgtkoPgAg"",
        ""file_unique_id"": ""AQADAv8sGwAEPjQGAAE"",
        ""file_date"": 1535639975
      },
      ""reverse_side"": {
        ""file_id"": ""DgADAQADKAADNfRARK9jbzh5AAFqvAI"",
        ""file_unique_id"": ""AQADAv8sGwAEPjQGAAE"",
        ""file_date"": 1535639975
      },
      ""selfie"": {
        ""file_id"": ""DgADAQADNAADA1BJRCUHz9fqxiqJAg"",
        ""file_unique_id"": ""AQADAv8sGwAEPjQGAAE"",
        ""file_date"": 1535639975
      },
      ""hash"": ""XAIrZ+liSIWhyaYerm4yj14ZJhw93S/IVeeoSUSavI8=""
    },
    {
      ""type"": ""utility_bill"",
      ""files"": [
        {
          ""file_id"": ""DgADAQADQAADPupBRDIrCqSwkb4iAg"",
          ""file_unique_id"": ""AQADAv8sGwAEPjQGAAE"",
          ""file_date"": 1535639861
        }
      ],
      ""translation"": [
        {
          ""file_id"": ""DgADAQADOwADGV9BRP4b7RLGAtUKAg"",
          ""file_unique_id"": ""AQADAv8sGwAEPjQGAAE"",
          ""file_date"": 1535639861
        }
      ],
      ""hash"": ""+teo2yrg2gOjMHCYof1FR0xgyDAdYk19TiYxecAaNKw=""
    }
  ],
  ""credentials"": {
    ""data"": ""8QKNwpmjaLNWOwK1Hqp0luB9/q/qSujDA5pGid1d7tyokaDn+6Y1RWFOGXBYoQMonm1kmIFmsw4iu8LjKMm4SZpH5bqSPMiuD7j7O2FPE5A99A/TrDsNCNzeu+TVKNBWInSrks3wsmITMLT3WPvVaXFeolCXugJChwgY+cnmpUecitbqQxR/9XpzTQapyIuKWdhWhcL8fS3fdCyojw5b7BWAxOXba7zuWA+nsKX1V7v1puMDiNHwDuIJGPl3mbAJ24afqdlT4UJXaVw9XWVb17c7g68qzaEqMaIb3OR1ydrE7vKpHHvFWSaEENoz2wv0PnDA0jyWMOnoL42HbVbu06XJoHuDSsFR2Rx2Hm7KZ1QM/OhDbpEiEa2+jJ1At9jftNL3JwEpktLAso5UQJbqCPgAPhowD9l/yvzUa0MRU+2P+lzZPDUNsZiy0wi0QhJafaHOg0mmjlaoNw37ebpPDCfF71NzoV4cetIv5YE2wauaN+20NQyPyUDlHKhpeEpsBVfpw0jGpannopX+aBwKEIANqlLto0HCY8/o81kfkgBqxqT7vKfr7Xm1glYwWNa9gNws4NEQZ2mCvZb3CyCsZL1b0GWc3O66F140rus+fO6Kz5jlc7ASVamsBytwvhTwnW/TllYTFRArNRRStlKDMxADKIdeTzVBWIRPxqEuI7TTT5r1gZBi7NW9to7nbMCoXjoLEdNOjtiRLgb5TyDjRXgdHbfKg428Q+Lvu1BlxGwTJuPQAwBdgpQKBoAmypix8BIHZBz0SBXozOq+KgHXWKX5tu5zDu86x1BMgl/l4KuqAz8NtPEB6VISPExuUxhDzWYqpI0yjFrKnE1aRX6I/gif4cQhkl+2eqUquYuxszTPrQJ4BhNsB7KR4x8WNcNm59xSjA1TxrNkJ6xcen7DNi0+bApRcPWMB+0dYBPXD1cFKo9he0fp9EpPN1tdvZQNpa4/LzjpwlbuX5bUSWWnlBKT60gMqtbzbRWC96deOHB11jTmvfFP+uTutO17Z3Gr9jIeC4cLjU5jcJh40KoNw3NHkIXx3ckixAvrGIsfkjDZrQa8ALqY8XXxgq7CaM8zYrbn9DU5rx5+MNa/IjpSDEELD4pFiPYUfm1Z4lF/N3pWDkiKZ10z+iPDigdIbdHcI7TPCDmpcHEHxYHvNG6GA2KLnliA0g3zLs6q+K8WnaqLZq4XqknT/98NvfjBm4OSjnnVmXEEz60G+1I7oRrZ0A=="",
    ""hash"": ""TMUO1tE81hIOCgaqN7buhqG8SIZHjFfJrD93LNKL4Yg="",
    ""secret"": ""QcOxuwd9OiB/9akjMzyY7wR4NcrpbhpjQuO9yOWhe0u34VVLraTr3gwkBNv0eKEZHoyulhhLr9tkSSO+BYZAp4engued3eL11jqQkosJQBCPg8m1arIvNM+/E5Kw8dnF7dEx9v8t9QA11kSfAqdgnqCtSAq6GGGu5ixuYM1VMbk270qcm3F7wrLN+9YQwUVkiai8WvdA7Q7BnywsbrekKOam95tiFeA7jE8Cf78D6gh47/uirO/KD3Hwl1PNo1f8ORgFf8EixSQuV5Gh8HxEY1uE+yfOxksG5MiWOC5A1lNQuVcZqzVfbReRvs2M2tvX5KeeN+/xsIps+Xp+szWSaw==""
  }
}
        ");
    }
}
