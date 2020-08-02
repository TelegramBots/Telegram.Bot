// ReSharper disable CheckNamespace
// ReSharper disable StringLiteralTypo

using System;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Passport;
using Telegram.Bot.Tests.Unit.Passport;
using Telegram.Bot.Types.Passport;
using Xunit;

namespace UnitTests
{
    /// <summary>
    /// Tests for decrypting file byte arrays using <see cref="IDecrypter.DecryptFile"/> method
    /// </summary>
    public class FileBytesDecryptionTests : IClassFixture<FileBytesDecryptionTests.Fixture>
    {
        public class Fixture
        {
            public Fixture() =>
                FixtureHelpers.CopyTestFiles(
                    ("driver_license-selfie.jpg.enc", "bytes_dec1.driver_license-selfie.jpg.enc"),
                    ("driver_license-selfie.jpg", "bytes_dec2.driver_license-selfie.jpg"),
                    ("driver_license-selfie.jpg.enc", "bytes_dec3.driver_license-selfie.jpg.enc")
                );
        }

        [Fact(DisplayName = "Should decrypt from file bytes")]
        public async Task Should_Decrypt_From_Bytes()
        {
            FileCredentials fileCredentials = new FileCredentials
            {
                FileHash = "v3q47iscI6TS94CMo7HGQUOxw28LIf82NJBkImzP57c=",
                Secret = "vF7nut7clg/H/pEaTJigo4mQJ0s8B+HGCWKTWtOTIdo=",
            };

            IDecrypter decrypter = new Decrypter();

            byte[] encContent = await File.ReadAllBytesAsync("Files/bytes_dec1.driver_license-selfie.jpg.enc");

            byte[] content = decrypter.DecryptFile(
                encContent,
                fileCredentials
            );

            Assert.NotEmpty(content);
            Assert.InRange(content.Length, encContent.Length - 256, encContent.Length - 33);
        }

        [Fact(DisplayName = "Should throw when trying to decrypt an unencrypted file with invalid data length")]
        public async Task Should_Throw_Decrypting_Unencrypted_File_Invalid_Length()
        {
            FileCredentials fileCredentials = new FileCredentials
            {
                FileHash = "v3q47iscI6TS94CMo7HGQUOxw28LIf82NJBkImzP57c=",
                Secret = "vF7nut7clg/H/pEaTJigo4mQJ0s8B+HGCWKTWtOTIdo=",
            };

            IDecrypter decrypter = new Decrypter();
            byte[] encContent = await File.ReadAllBytesAsync("Files/bytes_dec2.driver_license-selfie.jpg");

            Exception exception = Assert.ThrowsAny<Exception>(() =>
                decrypter.DecryptFile(
                    encContent,
                    fileCredentials
                )
            );

            Assert.Matches(@"^Data length is not divisible by 16: \d+\.$", exception.Message);
            Assert.IsType<PassportDataDecryptionException>(exception);
        }

        [Fact(DisplayName = "Should throw when trying to decrypt an unencrypted file but with valid data length")]
        public void Should_Throw_Decrypting_Unencrypted_File_Valid_Length()
        {
            FileCredentials fileCredentials = new FileCredentials
            {
                FileHash = "v3q47iscI6TS94CMo7HGQUOxw28LIf82NJBkImzP57c=",
                Secret = "vF7nut7clg/H/pEaTJigo4mQJ0s8B+HGCWKTWtOTIdo=",
            };

            IDecrypter decrypter = new Decrypter();
            byte[] encContent = new byte[2048]; // data length is divisible by 16

            Exception exception = Assert.ThrowsAny<Exception>(() =>
                decrypter.DecryptFile(
                    encContent,
                    fileCredentials
                )
            );

            Assert.Matches(@"^Data hash mismatch at position \d+\.$", exception.Message);
            Assert.IsType<PassportDataDecryptionException>(exception);
        }

        [Fact(DisplayName = "Should throw when decrypting a file(selfie) using wrong file credentials(front side)")]
        public async Task Should_Throw_Decrypting_Bytes_With_Wrong_FileCredentials()
        {
            FileCredentials wrongFileCredentials = new FileCredentials
            {
                FileHash = "THTjgv2FU7kff/29Vty/IcqKPmOGkL7F35fAzmkfZdI=",
                Secret = "a+jxJoKPEaz77VCjRvDVcYHfIO3+h+oI+ruZh+KkYa0=",
            };

            IDecrypter decrypter = new Decrypter();
            byte[] encContent = await File.ReadAllBytesAsync("Files/bytes_dec3.driver_license-selfie.jpg.enc");

            Exception exception = Assert.ThrowsAny<Exception>(() =>
                decrypter.DecryptFile(
                    encContent,
                    wrongFileCredentials
                )
            );

            Assert.Matches(@"^Data hash mismatch at position \d+\.$", exception.Message);
            Assert.IsType<PassportDataDecryptionException>(exception);
        }

        [Fact(DisplayName = "Should throw when null data bytes is passed")]
        public void Should_Throw_If_Null_EncryptedContent()
        {
            IDecrypter decrypter = new Decrypter();

            Exception exception = Assert.ThrowsAny<Exception>(() =>
                decrypter.DecryptFile(null!, null!)
            );

            Assert.Matches(@"^Value cannot be null\.\s+Parameter name: encryptedContent$", exception.Message);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact(DisplayName = "Should throw when null file credentials is passed")]
        public void Should_Throw_If_Null_FileCredentials()
        {
            IDecrypter decrypter = new Decrypter();

            Exception exception = Assert.ThrowsAny<Exception>(() =>
                decrypter.DecryptFile(new byte[0], null!)
            );

            Assert.Matches(@"^Value cannot be null\.\s+Parameter name: fileCredentials$", exception.Message);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact(DisplayName = "Should throw when null secret is passed")]
        public void Should_Throw_If_Null_Secret()
        {
            IDecrypter decrypter = new Decrypter();
            Exception exception = Assert.ThrowsAny<Exception>(() =>
                decrypter.DecryptFile(new byte[0], new FileCredentials())
            );

            Assert.Matches(@"^Value cannot be null\.\s+Parameter name: Secret$", exception.Message);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact(DisplayName = "Should throw when null file_hash is passed")]
        public void Should_Throw_If_Null_Hash()
        {
            IDecrypter decrypter = new Decrypter();

            FileCredentials fileCredentials = new FileCredentials {Secret = ""};
            Exception exception = Assert.ThrowsAny<Exception>(() =>
                decrypter.DecryptFile(new byte[0], fileCredentials)
            );

            Assert.Matches(@"^Value cannot be null\.\s+Parameter name: FileHash$", exception.Message);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact(DisplayName = "Should throw when data byte array is empty")]
        public void Should_Throw_If_Empty_Data_Bytes_Length()
        {
            IDecrypter decrypter = new Decrypter();
            FileCredentials fileCredentials = new FileCredentials {Secret = "", FileHash = ""};

            Exception exception = Assert.ThrowsAny<Exception>(() =>
                decrypter.DecryptFile(new byte[0], fileCredentials)
            );

            Assert.Matches(@"^Data array is empty\.\s+Parameter name: encryptedContent$", exception.Message);
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact(DisplayName = "Should throw when data byte array has invalid length")]
        public void Should_Throw_If_Invalid_Data_Bytes_Length()
        {
            IDecrypter decrypter = new Decrypter();
            FileCredentials fileCredentials = new FileCredentials {Secret = "", FileHash = ""};

            Exception exception = Assert.ThrowsAny<Exception>(() =>
                decrypter.DecryptFile(new byte[16 + 1], fileCredentials)
            );

            Assert.Equal("Data length is not divisible by 16: 17.", exception.Message);
            Assert.IsType<PassportDataDecryptionException>(exception);
        }

        [Theory(DisplayName = "Should throw when secret is not a valid base64-encoded string")]
        [InlineData("foo")]
        [InlineData("FooBarBazlg/H/pEaTJigo4mQJ0s8B+HGCWKTWtOTIdo=")]
        public void Should_Throw_If_Invalid_Secret(string secret)
        {
            FileCredentials fileCredentials = new FileCredentials {Secret = secret, FileHash = ""};
            IDecrypter decrypter = new Decrypter();

            Assert.Throws<FormatException>(() =>
                decrypter.DecryptFile(new byte[16], fileCredentials)
            );
        }

        [Theory(DisplayName = "Should throw when file_hash is not a valid base64-encoded string")]
        [InlineData("foo")]
        [InlineData("FooBarBazlg/H/pEaTJigo4mQJ0s8B+HGCWKTWtOTIdo=")]
        public void Should_Throw_If_Invalid_Hash(string fileHash)
        {
            FileCredentials fileCredentials = new FileCredentials {Secret = "", FileHash = fileHash};
            IDecrypter decrypter = new Decrypter();

            Assert.ThrowsAny<FormatException>(() =>
                decrypter.DecryptFile(new byte[16], fileCredentials)
            );
        }

        [Theory(DisplayName = "Should throw when length of file_hash is not 32")]
        [InlineData("")]
        [InlineData("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==")]
        [InlineData("Zm9v")]
        public void Should_Throw_If_Invalid_Hash_Length(string fileHash)
        {
            FileCredentials fileCredentials = new FileCredentials {Secret = "", FileHash = fileHash};
            IDecrypter decrypter = new Decrypter();

            Exception exception = Assert.ThrowsAny<Exception>(() =>
                decrypter.DecryptFile(new byte[16], fileCredentials)
            );

            Assert.Matches(@"^Hash length is not 32: \d+\.$", exception.Message);
            Assert.IsType<PassportDataDecryptionException>(exception);
        }
    }
}
