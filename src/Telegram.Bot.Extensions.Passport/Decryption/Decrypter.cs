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
            Credentials creds = JsonConvert.DeserializeObject<Credentials>(json);

            return creds;
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
            if (encryptedContent.Length % 16 != 0)
                throw new PassportDataDecryptionException($"Invalid data length: {encryptedContent.Length}");
            if (!destination.CanWrite)
                throw new ArgumentException("Stream does not support writing.", nameof(destination));

            byte[] dataSecret = Convert.FromBase64String(fileCredentials.Secret);
            byte[] dataHash = Convert.FromBase64String(fileCredentials.FileHash);

            if (dataHash.Length != 32)
                throw new PassportDataDecryptionException($"Invalid hash length: {dataHash.Length}");

            return DecryptDataStreamAsync(encryptedContent, dataSecret, dataHash, destination, cancellationToken);
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
            if (encryptedContent.Length % 16 != 0)
                throw new PassportDataDecryptionException($"Invalid data length: {encryptedContent.Length}");

            byte[] dataSecret = Convert.FromBase64String(fileCredentials.Secret);
            byte[] dataHash = Convert.FromBase64String(fileCredentials.FileHash);

            if (dataHash.Length != 32)
                throw new PassportDataDecryptionException($"Invalid hash length: {dataHash.Length}");

            return DecryptDataBytes(encryptedContent, dataSecret, dataHash);
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

                using (var decryptor = aes.CreateDecryptor())
                using (CryptoStream aesStream = new CryptoStream(data, decryptor, CryptoStreamMode.Read))
                using (var sha256 = SHA256.Create())
                using (CryptoStream shaStream = new CryptoStream(aesStream, sha256, CryptoStreamMode.Read))
                {
                    byte[] paddingBuffer = new byte[256];
                    int read = await shaStream.ReadAsync(paddingBuffer, 0, 256, cancellationToken)
                        .ConfigureAwait(false);

                    int paddingLength = paddingBuffer[0];
                    if (paddingLength < 32)
                        throw new PassportDataDecryptionException("Invalid padding size");

                    if (read < paddingLength)
                        throw new PassportDataDecryptionException("Invalid data");

                    await destination.WriteAsync(paddingBuffer, paddingLength, read - paddingLength, cancellationToken)
                        .ConfigureAwait(false);

                    await shaStream.CopyToAsync(destination, 81920, cancellationToken)
                        .ConfigureAwait(false);

                    byte[] paddedDataHash = sha256.Hash;
                    for (int i = 0; i < 32; i++)
                    {
                        if (hash[i] != paddedDataHash[i])
                            throw new PassportDataDecryptionException("Data hash mismatch");
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
                    using (var decryptor = aes.CreateDecryptor())
                    {
                        dataWithPadding = decryptor.TransformFinalBlock(data, 0, data.Length);
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
                        throw new PassportDataDecryptionException("Data hash mismatch");
                }
            }

            #endregion

            byte[] decryptedData;

            #region Step 3: remove padding to get the actual data

            {
                int paddingLength = dataWithPadding[0];
                if (!(32 <= paddingLength && paddingLength < 256))
                {
                    throw new PassportDataDecryptionException("Invalid data padding length");
                }

                int actualDataLength = dataWithPadding.Length - paddingLength;

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
