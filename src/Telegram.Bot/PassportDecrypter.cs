using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable CA2208, MA0015
#pragma warning disable CA1850, CA1835

namespace Telegram.Bot.Types.Passport
{
    /// <summary>Marker interface for type of data in <see cref="EncryptedPassportElement.Data"/> property</summary>
    public interface IDecryptedValue { }
}

namespace Telegram.Bot.Passport
{
    /// <summary>Represents a fatal error in decryption of Telegram Passport Data</summary>
    public class PassportDataDecryptionException(string message) : Exception(message) { }

    /// <summary>Provides decryption utilities for encrypted Telegram Passport data</summary>
    public class Decrypter
    {
        /// <summary>Decrypts encrypted credentials in <see cref="PassportData"/> using RSA key</summary>
        /// <param name="encryptedCredentials">Encrypted credentials in Passport data</param>
        /// <param name="key">RSA private key</param>
        /// <returns>Decrypted credentials</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="PassportDataDecryptionException"></exception>
        /// <exception cref="CryptographicException"></exception>
        public Credentials? DecryptCredentials(EncryptedCredentials encryptedCredentials, RSA key)
        {
            if (encryptedCredentials is null) throw new ArgumentNullException(nameof(encryptedCredentials));
            if (key is null) throw new ArgumentNullException(nameof(key));
            if (encryptedCredentials.Data is null) throw new ArgumentNullException(nameof(encryptedCredentials.Data));
            if (encryptedCredentials.Secret is null) throw new ArgumentNullException(nameof(encryptedCredentials.Secret));
            if (encryptedCredentials.Hash is null) throw new ArgumentNullException(nameof(encryptedCredentials.Hash));

            byte[] data = Convert.FromBase64String(encryptedCredentials.Data);
            if (data.Length == 0) throw new ArgumentException("Data is empty.", nameof(encryptedCredentials.Data));
            if (data.Length % 16 != 0) throw new PassportDataDecryptionException($"Data length is not divisible by 16: {data.Length}.");

            byte[] encryptedSecret = Convert.FromBase64String(encryptedCredentials.Secret);

            byte[] hash = Convert.FromBase64String(encryptedCredentials.Hash);
            if (hash.Length != 32) throw new PassportDataDecryptionException($"Hash length is not 32: {hash.Length}.");

            byte[] secret = key.Decrypt(encryptedSecret, RSAEncryptionPadding.OaepSHA1);

            byte[] decryptedData = DecryptDataBytes(data, secret, hash);
            string json = Encoding.UTF8.GetString(decryptedData);
            return JsonSerializer.Deserialize<Credentials>(json, JsonBotAPI.Options);
        }

        /// <summary>Decrypts encrypted data using its accompanying data credentials and deserializes the result from JSON to an instance of <typeparamref name="TValue"/></summary>
        /// <param name="encryptedData">Encrypted Passport data</param>
        /// <param name="dataCredentials">Accompanying data credentials required for decryption</param>
        /// <returns>Decrypted data</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="PassportDataDecryptionException"></exception>
        public TValue? DecryptData<TValue>(string encryptedData, DataCredentials dataCredentials) where TValue : class, IDecryptedValue
        {
            if (encryptedData is null) throw new ArgumentNullException(nameof(encryptedData));
            if (dataCredentials is null) throw new ArgumentNullException(nameof(dataCredentials));
            if (dataCredentials.Secret is null) throw new ArgumentNullException(nameof(dataCredentials.Secret));
            if (dataCredentials.DataHash is null) throw new ArgumentNullException(nameof(dataCredentials.DataHash));

            byte[] data = Convert.FromBase64String(encryptedData);
            if (data.Length == 0) throw new ArgumentException("Data is empty.", nameof(encryptedData));
            if (data.Length % 16 != 0) throw new PassportDataDecryptionException($"Data length is not divisible by 16: {data.Length}.");

            byte[] dataSecret = Convert.FromBase64String(dataCredentials.Secret);

            byte[] dataHash = Convert.FromBase64String(dataCredentials.DataHash);
            if (dataHash.Length != 32) throw new PassportDataDecryptionException($"Hash length is not 32: {dataHash.Length}.");

            byte[] decryptedData = DecryptDataBytes(data, dataSecret, dataHash);
            string json = Encoding.UTF8.GetString(decryptedData);
            return JsonSerializer.Deserialize<TValue>(json, JsonBotAPI.Options);
        }

        /// <summary>Decrypts encrypted file bytes using its accompanying file credentials</summary>
        /// <param name="encryptedContent">Encrypted Passport file</param>
        /// <param name="fileCredentials">Accompanying file credentials required for decryption</param>
        /// <returns>Decrypted file bytes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="PassportDataDecryptionException"></exception>
        public byte[] DecryptFile(
            byte[] encryptedContent,
            FileCredentials fileCredentials
        )
        {
            if (encryptedContent is null) throw new ArgumentNullException(nameof(encryptedContent));
            if (fileCredentials is null) throw new ArgumentNullException(nameof(fileCredentials));
            if (fileCredentials.Secret is null) throw new ArgumentNullException(nameof(fileCredentials.Secret));
            if (fileCredentials.FileHash is null) throw new ArgumentNullException(nameof(fileCredentials.FileHash));
            if (encryptedContent.Length == 0) throw new ArgumentException("Data array is empty.", nameof(encryptedContent));
            if (encryptedContent.Length % 16 != 0) throw new PassportDataDecryptionException
                    ($"Data length is not divisible by 16: {encryptedContent.Length}.");

            byte[] dataSecret = Convert.FromBase64String(fileCredentials.Secret);
            byte[] dataHash = Convert.FromBase64String(fileCredentials.FileHash);
            if (dataHash.Length != 32) throw new PassportDataDecryptionException($"Hash length is not 32: {dataHash.Length}.");

            return DecryptDataBytes(encryptedContent, dataSecret, dataHash);
        }

        /// <summary>Decrypts encrypted file from stream using its accompanying file credentials and writes it to <paramref name="destination"/> stream</summary>
        /// <param name="encryptedContent">Encrypted Passport file stream</param>
        /// <param name="fileCredentials">Accompanying file credentials required for decryption</param>
        /// <param name="destination">Stream to write decrypted file content to</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="PassportDataDecryptionException"></exception>
        /// <exception cref="CryptographicException"></exception>
        public Task DecryptFileAsync(Stream encryptedContent, FileCredentials fileCredentials, Stream destination, CancellationToken cancellationToken = default)
        {
            if (encryptedContent is null) throw new ArgumentNullException(nameof(encryptedContent));
            if (fileCredentials is null) throw new ArgumentNullException(nameof(fileCredentials));
            if (fileCredentials.Secret is null) throw new ArgumentNullException(nameof(fileCredentials.Secret));
            if (fileCredentials.FileHash is null) throw new ArgumentNullException(nameof(fileCredentials.FileHash));
            if (destination is null) throw new ArgumentNullException(nameof(destination));
            if (!encryptedContent.CanRead) throw new ArgumentException("Stream does not support reading.", nameof(encryptedContent));
            if (encryptedContent.CanSeek && encryptedContent.Length == 0) throw new ArgumentException("Stream is empty.", nameof(encryptedContent));
            if (encryptedContent.CanSeek && encryptedContent.Length % 16 != 0) throw new PassportDataDecryptionException($"Data length is not divisible by 16: {encryptedContent.Length}.");
            if (!destination.CanWrite) throw new ArgumentException("Stream does not support writing.", nameof(destination));

            byte[] dataSecret = Convert.FromBase64String(fileCredentials.Secret);
            byte[] dataHash = Convert.FromBase64String(fileCredentials.FileHash);
            if (dataHash.Length != 32) throw new PassportDataDecryptionException($"Hash length is not 32: {dataHash.Length}.");

            return DecryptDataStreamAsync(encryptedContent, dataSecret, dataHash, destination, cancellationToken);
        }

        private static async Task DecryptDataStreamAsync(Stream data, byte[] secret, byte[] hash, Stream destination, CancellationToken cancellationToken)
        {
            FindDataKeyAndIv(secret, hash, out byte[] dataKey, out byte[] dataIv);

            using var aes = Aes.Create();
            aes.KeySize = 256;
            aes.Mode = CipherMode.CBC;
            aes.Key = dataKey;
            aes.IV = dataIv;
            aes.Padding = PaddingMode.None;

            using var decrypter = aes.CreateDecryptor();
            using CryptoStream aesStream = new CryptoStream(data, decrypter, CryptoStreamMode.Read);
            using var sha256 = SHA256.Create();
            using CryptoStream shaStream = new CryptoStream(aesStream, sha256, CryptoStreamMode.Read);
            byte[] paddingBuffer = new byte[256];
            int read = await shaStream.ReadAsync(paddingBuffer, 0, 256, cancellationToken).ConfigureAwait(false);

            byte paddingLength = paddingBuffer[0];
            if (paddingLength < 32) throw new PassportDataDecryptionException($"Data padding length is invalid: {paddingLength}.");

            int actualDataLength = read - paddingLength;
            if (actualDataLength < 1) throw new PassportDataDecryptionException($"Data length is invalid: {actualDataLength}.");

            await destination.WriteAsync(paddingBuffer, paddingLength, actualDataLength, cancellationToken).ConfigureAwait(false);

            // 81920 is the default Stream.CopyTo buffer size
            // The overload without the buffer size does not accept a cancellation token
            const int defaultBufferSize = 81920;
            await shaStream.CopyToAsync(destination, defaultBufferSize, cancellationToken).ConfigureAwait(false);

            byte[] paddedDataHash = sha256.Hash!;
            for (int i = 0; i < hash.Length; i++)
                if (hash[i] != paddedDataHash[i])
                    throw new PassportDataDecryptionException($"Data hash mismatch at position {i}.");
        }

        private static byte[] DecryptDataBytes(byte[] data, byte[] secret, byte[] hash)
        {
            #region Step 1: find data Key & IV
            FindDataKeyAndIv(secret, hash, out byte[] dataKey, out byte[] dataIv);
            #endregion

            #region Step 2.1: decrypt data to get "data with random padding"
            byte[] dataWithPadding;
            using (var aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.Mode = CipherMode.CBC;
                aes.Key = dataKey;
                aes.IV = dataIv;
                aes.Padding = PaddingMode.None;
                using var decrypter = aes.CreateDecryptor();
                dataWithPadding = decrypter.TransformFinalBlock(data, 0, data.Length);
            }
            #endregion

            #region Step 2.2: verify "data_hash" and hash of "data with padding" are the same
            byte[] paddedDataHash;
            using (var sha256 = SHA256.Create())
                paddedDataHash = sha256.ComputeHash(dataWithPadding);

            for (int i = 0; i < hash.Length; i++)
                if (hash[i] != paddedDataHash[i])
                    throw new PassportDataDecryptionException($"Data hash mismatch at position {i}.");
            #endregion

            #region Step 3: remove padding to get the actual data
            byte paddingLength = dataWithPadding[0];
            if (paddingLength < 32) throw new PassportDataDecryptionException($"Data padding length is invalid: {paddingLength}.");

            int actualDataLength = dataWithPadding.Length - paddingLength;
            if (actualDataLength < 1) throw new PassportDataDecryptionException($"Data length is invalid: {actualDataLength}.");

            var decryptedData = new byte[actualDataLength];
            Array.Copy(dataWithPadding, paddingLength, decryptedData, 0, actualDataLength);
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
