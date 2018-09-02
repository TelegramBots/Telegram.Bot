using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.Passport;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Passport
{
    /// <summary>
    /// Default implementation of <see cref="IDecrypter"/>
    /// </summary>
    public class Decrypter : IDecrypter
    {
        /// <inheritdoc />
        public Credentials DecryptCredentials(
            RSA key,
            EncryptedCredentials encryptedCredentials
        )
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key));
            if (encryptedCredentials is null)
                throw new ArgumentNullException(nameof(encryptedCredentials));

            byte[] data = Convert.FromBase64String(encryptedCredentials.Data);
            if (data.Length % 16 != 0)
                throw new PassportDataDecryptionException($"Invalid data length: {data.Length}");

            byte[] hash = Convert.FromBase64String(encryptedCredentials.Hash);
            if (hash.Length != 32)
                throw new PassportDataDecryptionException($"Invalid hash length: {hash.Length}");

            byte[] encryptedSecret = Convert.FromBase64String(encryptedCredentials.Secret);
            byte[] secret = key.Decrypt(encryptedSecret, RSAEncryptionPadding.OaepSHA1);

            byte[] decryptedData = DecryptDataBytes(data, secret, hash);
            string json = Encoding.UTF8.GetString(decryptedData);
            Credentials credentials = JsonConvert.DeserializeObject<Credentials>(json);

            return credentials;
        }

        /// <inheritdoc />
        public string DecryptData(
            string encryptedData,
            DataCredentials dataCredentials
        )
        {
            if (encryptedData is null)
                throw new ArgumentNullException(nameof(encryptedData));
            if (dataCredentials is null)
                throw new ArgumentNullException(nameof(dataCredentials));

            byte[] data = Convert.FromBase64String(encryptedData);
            if (data.Length % 16 != 0)
                throw new PassportDataDecryptionException($"Invalid data length: {data.Length}");

            byte[] dataHash = Convert.FromBase64String(dataCredentials.DataHash);
            if (dataHash.Length != 32)
                throw new PassportDataDecryptionException($"Invalid hash length: {dataHash.Length}");

            byte[] dataSecret = Convert.FromBase64String(dataCredentials.Secret);

            byte[] decryptedData = DecryptDataBytes(data, dataSecret, dataHash);
            string content = Encoding.UTF8.GetString(decryptedData);

            return content;
        }

        /// <inheritdoc />
        public TValue DecryptData<TValue>(
            string encryptedData,
            DataCredentials dataCredentials
        )
            where TValue : IDecryptedValue
        {
            if (encryptedData is null)
                throw new ArgumentNullException(nameof(encryptedData));
            if (dataCredentials is null)
                throw new ArgumentNullException(nameof(dataCredentials));

            string json = DecryptData(encryptedData, dataCredentials);
            return JsonConvert.DeserializeObject<TValue>(json);
        }

        /// <inheritdoc />
        public byte[] DecryptFile(
            byte[] encryptedContent,
            FileCredentials fileCredentials
        )
        {
            if (encryptedContent is null)
                throw new ArgumentNullException(nameof(encryptedContent));
            if (fileCredentials is null)
                throw new ArgumentNullException(nameof(fileCredentials));
            if (fileCredentials.Secret is null)
                throw new ArgumentNullException(nameof(fileCredentials.Secret));
            if (fileCredentials.FileHash is null)
                throw new ArgumentNullException(nameof(fileCredentials.FileHash));
            if (encryptedContent.Length == 0)
                throw new ArgumentException("Data array is empty.", nameof(encryptedContent));
            if (encryptedContent.Length % 16 != 0)
                throw new PassportDataDecryptionException
                    ($"Data length is not divisible by 16: {encryptedContent.Length}.");

            byte[] dataSecret = Convert.FromBase64String(fileCredentials.Secret);
            byte[] dataHash = Convert.FromBase64String(fileCredentials.FileHash);

            if (dataHash.Length != 32)
                throw new PassportDataDecryptionException($"Hash length is not 32: {dataHash.Length}.");

            return DecryptDataBytes(encryptedContent, dataSecret, dataHash);
        }

        /// <inheritdoc />
        public Task DecryptFileAsync(
            Stream encryptedContent,
            FileCredentials fileCredentials,
            Stream destination,
            CancellationToken cancellationToken = default
        )
        {
            if (encryptedContent is null)
                throw new ArgumentNullException(nameof(encryptedContent));
            if (fileCredentials is null)
                throw new ArgumentNullException(nameof(fileCredentials));
            if (fileCredentials.Secret is null)
                throw new ArgumentNullException(nameof(fileCredentials.Secret));
            if (fileCredentials.FileHash is null)
                throw new ArgumentNullException(nameof(fileCredentials.FileHash));
            if (destination is null)
                throw new ArgumentNullException(nameof(destination));
            if (!encryptedContent.CanRead)
                throw new ArgumentException("Stream does not support reading.", nameof(encryptedContent));
            if (encryptedContent.CanSeek && encryptedContent.Length == 0)
                throw new ArgumentException("Stream is empty.", nameof(encryptedContent));
            if (encryptedContent.CanSeek && encryptedContent.Length % 16 != 0)
                throw new PassportDataDecryptionException("Data length is not divisible by 16: " +
                                                          $"{encryptedContent.Length}.");
            if (!destination.CanWrite)
                throw new ArgumentException("Stream does not support writing.", nameof(destination));

            byte[] dataSecret = Convert.FromBase64String(fileCredentials.Secret);
            byte[] dataHash = Convert.FromBase64String(fileCredentials.FileHash);

            if (dataHash.Length != 32)
                throw new PassportDataDecryptionException($"Hash length is not 32: {dataHash.Length}.");

            return DecryptDataStreamAsync(encryptedContent, dataSecret, dataHash, destination, cancellationToken);
        }

        private static async Task DecryptDataStreamAsync(
            Stream data,
            byte[] secret,
            byte[] hash,
            Stream destination,
            CancellationToken cancellationToken
        )
        {
            FindDataKeyAndIv(secret, hash, out byte[] dataKey, out byte[] dataIv);

            using (var aes = Aes.Create())
            {
                // ReSharper disable once PossibleNullReferenceException
                aes.KeySize = 256;
                aes.Mode = CipherMode.CBC;
                aes.Key = dataKey;
                aes.IV = dataIv;
                aes.Padding = PaddingMode.None;

                using (var decrypter = aes.CreateDecryptor())
                using (CryptoStream aesStream = new CryptoStream(data, decrypter, CryptoStreamMode.Read))
                using (var sha256 = SHA256.Create())
                using (CryptoStream shaStream = new CryptoStream(aesStream, sha256, CryptoStreamMode.Read))
                {
                    byte[] paddingBuffer = new byte[256];
                    int read = await shaStream.ReadAsync(paddingBuffer, 0, 256, cancellationToken)
                        .ConfigureAwait(false);

                    byte paddingLength = paddingBuffer[0];
                    if (paddingLength < 32)
                        throw new PassportDataDecryptionException($"Data padding length is invalid: {paddingLength}.");

                    int actualDataLength = read - paddingLength;
                    if (actualDataLength < 1)
                        throw new PassportDataDecryptionException($"Data length is invalid: {actualDataLength}.");

                    await destination.WriteAsync(paddingBuffer, paddingLength, actualDataLength, cancellationToken)
                        .ConfigureAwait(false);

                    // 81920 is the default Stream.CopyTo buffer size
                    // The overload without the buffer size does not accept a cancellation token
                    const int defaultBufferSize = 81920;
                    await shaStream.CopyToAsync(destination, defaultBufferSize, cancellationToken)
                        .ConfigureAwait(false);

                    byte[] paddedDataHash = sha256.Hash;
                    for (int i = 0; i < hash.Length; i++)
                    {
                        if (hash[i] != paddedDataHash[i])
                            throw new PassportDataDecryptionException($"Data hash mismatch at position {i}.");
                    }
                }
            }
        }

        private static byte[] DecryptDataBytes(byte[] data, byte[] secret, byte[] hash)
        {
            #region Step 1: find data Key & IV

            FindDataKeyAndIv(secret, hash, out byte[] dataKey, out byte[] dataIv);

            #endregion

            byte[] dataWithPadding;

            #region Step 2.1: decrypt data to get "data with random padding"

            {
                using (var aes = Aes.Create())
                {
                    // ReSharper disable once PossibleNullReferenceException
                    aes.KeySize = 256;
                    aes.Mode = CipherMode.CBC;
                    aes.Key = dataKey;
                    aes.IV = dataIv;
                    aes.Padding = PaddingMode.None;
                    using (var decrypter = aes.CreateDecryptor())
                    {
                        dataWithPadding = decrypter.TransformFinalBlock(data, 0, data.Length);
                    }
                }
            }

            #endregion

            #region Step 2.2: verify "data_hash" and hash of "data with padding" are the same

            {
                byte[] paddedDataHash;
                using (var sha256 = SHA256.Create())
                {
                    paddedDataHash = sha256.ComputeHash(dataWithPadding);
                }

                for (int i = 0; i < hash.Length; i++)
                {
                    if (hash[i] != paddedDataHash[i])
                        throw new PassportDataDecryptionException($"Data hash mismatch at position {i}.");
                }
            }

            #endregion

            byte[] decryptedData;

            #region Step 3: remove padding to get the actual data

            {
                byte paddingLength = dataWithPadding[0];
                if (paddingLength < 32)
                    throw new PassportDataDecryptionException($"Data padding length is invalid: {paddingLength}.");

                int actualDataLength = dataWithPadding.Length - paddingLength;
                if (actualDataLength < 1)
                    throw new PassportDataDecryptionException($"Data length is invalid: {actualDataLength}.");

                decryptedData = new byte[actualDataLength];
                Array.Copy(dataWithPadding, paddingLength, decryptedData, 0, actualDataLength);
            }

            #endregion

            return decryptedData;
        }

        private static void FindDataKeyAndIv(byte[] secret, byte[] hash, out byte[] dataKey, out byte[] dataIv)
        {
            byte[] dataSecretHash;
            using (var sha512 = SHA512.Create())
            {
                byte[] secretAndHashBytes = new byte[secret.Length + hash.Length];
                Array.Copy(secret, 0, secretAndHashBytes, 0, secret.Length);
                Array.Copy(hash, 0, secretAndHashBytes, secret.Length, hash.Length);
                dataSecretHash = sha512.ComputeHash(secretAndHashBytes);
            }

            dataKey = new byte[32];
            Array.Copy(dataSecretHash, 0, dataKey, 0, 32);

            dataIv = new byte[16];
            Array.Copy(dataSecretHash, 32, dataIv, 0, 16);
        }
    }
}
