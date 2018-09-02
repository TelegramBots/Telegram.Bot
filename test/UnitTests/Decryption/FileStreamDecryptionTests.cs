// ReSharper disable CheckNamespace

using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Passport;
using Telegram.Bot.Types.Passport;
using Xunit;

namespace UnitTests
{
    /// <summary>
    /// Tests for decrypting file streams using <see cref="IDecrypter.DecryptFileAsync"/> method
    /// </summary>
    public class FileStreamDecryptionTests
    {
        [Fact(DisplayName = "Should decrypt from a seekable stream")]
        public async Task Should_Decrypt_From_Seekable_Stream()
        {
            FileCredentials fileCredentials = new FileCredentials
            {
                FileHash = "v3q47iscI6TS94CMo7HGQUOxw28LIf82NJBkImzP57c=",
                Secret = "vF7nut7clg/H/pEaTJigo4mQJ0s8B+HGCWKTWtOTIdo=",
            };

            IDecrypter decrypter = new Decrypter();

            Stream encContentStream = new MemoryStream();
            using (Stream encFileStream = new NonSeekableFileReadStream("Files/driver_license-selfie.jpg.enc"))
            {
                await encFileStream.CopyToAsync(encContentStream);
            }

            using (encContentStream)
            using (Stream contentStream = new MemoryStream())
            {
                encContentStream.Position = 0; // Ensure method starts reading the content fro the beginning

                await decrypter.DecryptFileAsync(
                    encContentStream,
                    fileCredentials,
                    contentStream
                );
            }
        }

        [Fact(DisplayName = "Should decrypt from non-seekable stream")]
        public async Task Should_Decrypt_From_NonSeekable_Stream()
        {
            FileCredentials fileCredentials = new FileCredentials
            {
                FileHash = "v3q47iscI6TS94CMo7HGQUOxw28LIf82NJBkImzP57c=",
                Secret = "vF7nut7clg/H/pEaTJigo4mQJ0s8B+HGCWKTWtOTIdo=",
            };

            IDecrypter decrypter = new Decrypter();

            using (Stream
                encFileStream = new NonSeekableFileReadStream("Files/driver_license-selfie.jpg.enc"),
                fileStream = new MemoryStream()
            )
            {
                await decrypter.DecryptFileAsync(
                    encFileStream,
                    fileCredentials,
                    fileStream
                );
            }
        }

        [Fact(DisplayName = "Should throw when trying to decrypt an unencrypted file")]
        public async Task Should_Throw_Decrypting_Unencrypted_File()
        {
            FileCredentials fileCredentials = new FileCredentials
            {
                FileHash = "v3q47iscI6TS94CMo7HGQUOxw28LIf82NJBkImzP57c=",
                Secret = "vF7nut7clg/H/pEaTJigo4mQJ0s8B+HGCWKTWtOTIdo=",
            };

            IDecrypter decrypter = new Decrypter();

            Exception exception;
            using (Stream
                encFileStream = new NonSeekableFileReadStream("Files/driver_license-selfie.jpg"),
                fileStream = new MemoryStream()
            )
            {
                exception = await Assert.ThrowsAnyAsync<Exception>(() =>
                    decrypter.DecryptFileAsync(
                        encFileStream,
                        fileCredentials,
                        fileStream
                    )
                );
            }

            Assert.Equal("The input data is not a complete block.", exception.Message);
            Assert.IsType<CryptographicException>(exception);
        }

        [Fact(DisplayName = "Should throw when decrypting from a stream that is not positioned at the beginning")]
        public async Task Should_Throw_Decrypting_Mispositioned_Stream()
        {
            FileCredentials fileCredentials = new FileCredentials
            {
                FileHash = "v3q47iscI6TS94CMo7HGQUOxw28LIf82NJBkImzP57c=",
                Secret = "vF7nut7clg/H/pEaTJigo4mQJ0s8B+HGCWKTWtOTIdo=",
            };

            IDecrypter decrypter = new Decrypter();

            Stream encContentStream = new MemoryStream();
            using (Stream encFileStream = new NonSeekableFileReadStream("Files/driver_license-selfie.jpg.enc"))
            {
                await encFileStream.CopyToAsync(encContentStream);
            }

            Exception exception;
            using (encContentStream) // Stream position is at the end and not at 0 position
            using (Stream contentStream = new MemoryStream())
            {
                exception = await Assert.ThrowsAnyAsync<Exception>(() =>
                    decrypter.DecryptFileAsync(
                        encContentStream,
                        fileCredentials,
                        contentStream
                    )
                );
            }

            Assert.Matches(@"^Data has invalid padding length: \d+\.", exception.Message);
            Assert.IsType<PassportDataDecryptionException>(exception);
        }

        [Fact(DisplayName = "Should throw when decrypting a file(selfie) using wrong file credentials(front side)")]
        public async Task Should_Throw_Decrypting_With_Wrong_FileCredentials()
        {
            FileCredentials wrongFileCredentials = new FileCredentials
            {
                FileHash = "THTjgv2FU7kff/29Vty/IcqKPmOGkL7F35fAzmkfZdI=",
                Secret = "a+jxJoKPEaz77VCjRvDVcYHfIO3+h+oI+ruZh+KkYa0=",
            };

            IDecrypter decrypter = new Decrypter();

            Exception exception;
            using (Stream
                encFileStream = new NonSeekableFileReadStream("Files/driver_license-selfie.jpg.enc"),
                fileStream = new MemoryStream()
            )
            {
                exception = await Assert.ThrowsAnyAsync<Exception>(() =>
                    decrypter.DecryptFileAsync(
                        encFileStream,
                        wrongFileCredentials,
                        fileStream
                    )
                );
            }

            Assert.Matches(@"^Data hash mismatch at position \d+.$", exception.Message);
            Assert.IsType<PassportDataDecryptionException>(exception);
        }

        [Fact(DisplayName = "Should throw when null data stream is passed")]
        public async Task Should_Throw_If_Null_EncryptedContent()
        {
            IDecrypter decrypter = new Decrypter();

            Exception exception = await Assert.ThrowsAnyAsync<Exception>(() =>
                decrypter.DecryptFileAsync(null, null, null)
            );

            Assert.Matches(@"^Value cannot be null.\s+Parameter name: encryptedContent$", exception.Message);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact(DisplayName = "Should throw when null file credentials is passed")]
        public async Task Should_Throw_If_Null_FileCredentials()
        {
            IDecrypter decrypter = new Decrypter();

            Exception exception = await Assert.ThrowsAnyAsync<Exception>(() =>
                decrypter.DecryptFileAsync(new MemoryStream(), null, null)
            );

            Assert.Matches(@"^Value cannot be null.\s+Parameter name: fileCredentials$", exception.Message);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact(DisplayName = "Should throw when null secret is passed")]
        public async Task Should_Throw_If_Null_Secret()
        {
            IDecrypter decrypter = new Decrypter();
            Exception exception = await Assert.ThrowsAnyAsync<Exception>(() =>
                decrypter.DecryptFileAsync(new MemoryStream(), new FileCredentials(), new MemoryStream())
            );

            Assert.Matches(@"^Value cannot be null.\s+Parameter name: Secret$", exception.Message);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact(DisplayName = "Should throw when null file_hash is passed")]
        public async Task Should_Throw_If_Null_Hash()
        {
            IDecrypter decrypter = new Decrypter();

            FileCredentials fileCredentials = new FileCredentials {Secret = ""};
            Exception exception = await Assert.ThrowsAnyAsync<Exception>(() =>
                decrypter.DecryptFileAsync(new MemoryStream(), fileCredentials, new MemoryStream())
            );

            Assert.Matches(@"^Value cannot be null.\s+Parameter name: FileHash$", exception.Message);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact(DisplayName = "Should throw when non-readable data stream is passed")]
        public async Task Should_Throw_If_NonReadable_Data_Stream()
        {
            IDecrypter decrypter = new Decrypter();
            FileCredentials fileCredentials = new FileCredentials {Secret = "", FileHash = ""};

            Exception exception;
            using (Stream encStream = File.OpenWrite("Files/driver_license-selfie.jpg"))
            {
                exception = await Assert.ThrowsAnyAsync<Exception>(() =>
                    decrypter.DecryptFileAsync(encStream, fileCredentials, new MemoryStream())
                );
            }

            Assert.Matches(@"^Stream does not support reading.\s+Parameter name: encryptedContent$", exception.Message);
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact(DisplayName = "Should throw when readable data stream has invalid length")]
        public async Task Should_Throw_If_Invalid_Readable_Data_Stream_Length()
        {
            IDecrypter decrypter = new Decrypter();
            FileCredentials fileCredentials = new FileCredentials {Secret = "", FileHash = ""};

            Exception exception;
            using (Stream encStream = new MemoryStream(new byte[16 - 1]))
            {
                exception = await Assert.ThrowsAnyAsync<Exception>(() =>
                    decrypter.DecryptFileAsync(encStream, fileCredentials, new MemoryStream())
                );
            }

            Assert.Equal("Length of padded data is not divisible by 16: 15.", exception.Message);
            Assert.IsType<PassportDataDecryptionException>(exception);
        }

        [Fact(DisplayName = "Should throw when null destination stream is passed")]
        public async Task Should_Throw_If_Null_Destination()
        {
            IDecrypter decrypter = new Decrypter();
            FileCredentials fileCredentials = new FileCredentials {Secret = "", FileHash = ""};

            Exception exception = await Assert.ThrowsAnyAsync<Exception>(() =>
                decrypter.DecryptFileAsync(new MemoryStream(), fileCredentials, null)
            );

            Assert.Matches(@"^Value cannot be null.\s+Parameter name: destination$", exception.Message);
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact(DisplayName = "Should throw when destination data stream is not writable")]
        public async Task Should_Throw_If_NonWritable_Destination()
        {
            IDecrypter decrypter = new Decrypter();
            FileCredentials fileCredentials = new FileCredentials {Secret = "", FileHash = ""};

            Exception exception;
            using (Stream destStream = File.OpenRead("Files/driver_license-selfie.jpg"))
            {
                exception = await Assert.ThrowsAnyAsync<Exception>(() =>
                    decrypter.DecryptFileAsync(new MemoryStream(), fileCredentials, destStream)
                );
            }

            Assert.Matches(@"^Stream does not support writing.\s+Parameter name: destination$", exception.Message);
            Assert.IsType<ArgumentException>(exception);
        }


        [Theory(DisplayName = "Should throw when secret is not a valid base64-encoded string")]
        [InlineData("foo")]
        [InlineData("FooBarBazlg/H/pEaTJigo4mQJ0s8B+HGCWKTWtOTIdo=")]
        public async Task Should_Throw_If_Invalid_Secret(string secret)
        {
            FileCredentials fileCredentials = new FileCredentials {Secret = secret, FileHash = ""};
            IDecrypter decrypter = new Decrypter();

            await Assert.ThrowsAsync<FormatException>(() =>
                decrypter.DecryptFileAsync(new MemoryStream(), fileCredentials, new MemoryStream())
            );
        }

        [Theory(DisplayName = "Should throw when file_hash is not a valid base64-encoded string")]
        [InlineData("foo")]
        [InlineData("FooBarBazlg/H/pEaTJigo4mQJ0s8B+HGCWKTWtOTIdo=")]
        public async Task Should_Throw_If_Invalid_Hash(string fileHash)
        {
            FileCredentials fileCredentials = new FileCredentials {Secret = "", FileHash = fileHash};
            IDecrypter decrypter = new Decrypter();

            await Assert.ThrowsAnyAsync<FormatException>(() =>
                decrypter.DecryptFileAsync(new MemoryStream(), fileCredentials, new MemoryStream())
            );
        }

        [Theory(DisplayName = "Should throw when file_hash is not a valid base64-encoded string")]
        [InlineData("")]
        [InlineData("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==")]
        [InlineData("Zm9v")]
        public async Task Should_Throw_If_Invalid_Hash2(string fileHash)
        {
            FileCredentials fileCredentials = new FileCredentials {Secret = "", FileHash = fileHash};
            IDecrypter decrypter = new Decrypter();

            Exception exception = await Assert.ThrowsAnyAsync<Exception>(() =>
                decrypter.DecryptFileAsync(new MemoryStream(), fileCredentials, new MemoryStream())
            );

            Assert.Matches(@"^file hash has invalid length: \d+\.$", exception.Message);
            Assert.IsType<PassportDataDecryptionException>(exception);
        }

        class NonSeekableFileReadStream : Stream
        {
            private readonly Stream _fileStream;

            public NonSeekableFileReadStream(string path)
            {
                _fileStream = File.OpenRead(path);
            }

            public override int Read(byte[] buffer, int offset, int count) => _fileStream.Read(buffer, offset, count);

            public override Task<int> ReadAsync
                (byte[] buffer, int offset, int count, CancellationToken cancellationToken) =>
                _fileStream.ReadAsync(buffer, offset, count, cancellationToken);

            public override bool CanRead => _fileStream.CanRead;

            protected override void Dispose(bool disposing)
            {
                _fileStream.Dispose();
                base.Dispose(disposing);
            }

            public override void Flush() => throw new NotSupportedException();
            public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
            public override void SetLength(long value) => throw new NotSupportedException();
            public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();
            public override bool CanSeek => false;
            public override bool CanWrite => false;
            public override long Length => throw new NotSupportedException();

            public override long Position
            {
                get => throw new NotSupportedException();
                set => throw new NotSupportedException();
            }
        }
    }
}
