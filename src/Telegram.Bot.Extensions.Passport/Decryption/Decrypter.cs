using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot.Extensions.Passport;
using Telegram.Bot.Types.Passport;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Passport
{
    public class Decrypter : IDecrypter
    {
        public Credentials DecryptCredentials(
            RSA key,
            EncryptedCredentials encryptedCredentials
        )
        {
            key = key ?? throw new ArgumentNullException(nameof(key));

            byte[] data, hash, secret;
            data = Convert.FromBase64String(encryptedCredentials.Data);
            hash = Convert.FromBase64String(encryptedCredentials.Hash);
            byte[] encryptedSecret = Convert.FromBase64String(encryptedCredentials.Secret);
            secret = key.Decrypt(encryptedSecret, RSAEncryptionPadding.OaepSHA1);

            byte[] decryptedData = DecryptDataBytes(data, secret, hash);
            string json = Encoding.UTF8.GetString(decryptedData);
            Credentials creds = JsonConvert.DeserializeObject<Credentials>(json);

            return creds;
        }

        public string DecryptData(
            string encryptedData,
            DataCredentials dataCredentials
        )
        {
            byte[] decryptedData;
            {
                byte[] data, dataHash, dataSecret;
                data = Convert.FromBase64String(encryptedData);
                dataHash = Convert.FromBase64String(dataCredentials.DataHash);
                dataSecret = Convert.FromBase64String(dataCredentials.Secret);
                decryptedData = DecryptDataBytes(data, dataSecret, dataHash);
            }

            string content = Encoding.UTF8.GetString(decryptedData);
            return content;
        }

        public TValue DecryptData<TValue>(
            string encryptedData,
            DataCredentials dataCredentials
        )
            where TValue : IDecryptedValue
        {
            string json = DecryptData(encryptedData, dataCredentials);
            return JsonConvert.DeserializeObject<TValue>(json);
        }

        public async Task DecryptFileAsync(
            Stream encryptedContent,
            FileCredentials fileCredentials,
            Stream destination,
            CancellationToken cancellationToken = default
        )
        {
            encryptedContent = encryptedContent ?? throw new ArgumentNullException(nameof(encryptedContent));
            fileCredentials = fileCredentials ?? throw new ArgumentNullException(nameof(fileCredentials));

            byte[] hash, secret;
            hash = Convert.FromBase64String(fileCredentials.FileHash);
            secret = Convert.FromBase64String(fileCredentials.Secret);

            byte[] aesKey, aesIv;

            #region Step 1: compute aes Key and IV

            {
                byte[] dataSecretHash;
                using (SHA512 sha512 = SHA512.Create())
                {
                    byte[] secretAndHashBytes = new byte[secret.Length + hash.Length];
                    Array.Copy(secret, 0, secretAndHashBytes, 0, secret.Length);
                    Array.Copy(hash, 0, secretAndHashBytes, secret.Length, hash.Length);
                    dataSecretHash = sha512.ComputeHash(secretAndHashBytes);
                }

                aesKey = new byte[32];
                Array.Copy(dataSecretHash, 0, aesKey, 0, 32);

                aesIv = new byte[16];
                Array.Copy(dataSecretHash, 32, aesIv, 0, 16);
            }

            #endregion

            #region Step 2.1, 2.2 and 3: decrypt, verify, remove paddding

            using (var aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.Mode = CipherMode.CBC;
                aes.Key = aesKey;
                aes.IV = aesIv;
                aes.Padding = PaddingMode.None;

                using (var decryptor = aes.CreateDecryptor())
                using (CryptoStream aesStream = new CryptoStream(encryptedContent, decryptor, CryptoStreamMode.Read))
                using (var sha256 = SHA256.Create())
                using (CryptoStream shaStream = new CryptoStream(aesStream, sha256, CryptoStreamMode.Read))
                {
                    byte[] buffer = new byte[81920]; // default Stream.CopyTo buffer size

                    int read = await shaStream.ReadAsync(buffer, 0, 81920, cancellationToken).ConfigureAwait(false);
                    if (read < 0)
                        throw new DecryptionException("No data in the input stream");

                    int paddingLength = buffer[0];
                    if (paddingLength < 32)
                        throw new DecryptionException("Invalid padding size");

                    if (read < paddingLength)
                        throw new DecryptionException("Invalid data");

                    destination.Write(buffer, paddingLength, read - paddingLength);

                    while ((read = await shaStream.ReadAsync(buffer, 0, 81920, cancellationToken).ConfigureAwait(false)) > 0)
                    {
                        await destination.WriteAsync(buffer, 0, read, cancellationToken).ConfigureAwait(false);
                    }

                    byte[] paddedDataHash = sha256.Hash;
                    for (int i = 0; i < 32; i++)
                    {
                        if (hash[i] != paddedDataHash[i])
                            throw new DecryptionException("Data hash mismatch");
                    }
                }
            }

            #endregion
        }

        public byte[] DecryptFile(
            byte[] encryptedContent,
            FileCredentials fileCredentials
        )
        {
            byte[] dataHash, dataSecret;
            dataHash = Convert.FromBase64String(fileCredentials.FileHash);
            dataSecret = Convert.FromBase64String(fileCredentials.Secret);

            return DecryptDataBytes(encryptedContent, dataSecret, dataHash);
        }

        private static byte[] DecryptDataBytes(byte[] data, byte[] secret, byte[] hash)
        {
            if (data.Length % 16 != 0)
            {
                throw new DecryptionException($"Invalid data length: {data.Length}");
            }

            if (hash.Length != 32)
            {
                throw new DecryptionException($"Invalid hash length: {hash.Length}");
            }

            byte[] dataKey, dataIv;

            #region Step 1: find data Key & IV

            {
                byte[] dataSecretHash;
                using (SHA512 sha512 = SHA512.Create())
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

            #endregion

            byte[] dataWithPadding;

            #region Step 2.1: decrypt data to get "data with random padding"

            {
                using (var aes = Aes.Create())
                {
                    aes.KeySize = 256;
                    aes.Mode = CipherMode.CBC;
                    aes.Key = dataKey;
                    aes.IV = dataIv;
                    aes.Padding = PaddingMode.None; // ToDo: Try to remove this line
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
                        throw new DecryptionException("Data hash mismatch");
                }
            }

            #endregion

            byte[] decryptedData;

            #region Step 3: remove padding to get the actual data

            {
                int paddingLength = dataWithPadding[0];
                if (!(32 <= paddingLength && paddingLength < 256))
                {
                    throw new DecryptionException("Invalid data padding length");
                }

                int actualDataLength = dataWithPadding.Length - paddingLength;

                decryptedData = new byte[actualDataLength];
                Array.Copy(dataWithPadding, paddingLength, decryptedData, 0, actualDataLength);
            }

            #endregion

            return decryptedData;
        }
    }
}
