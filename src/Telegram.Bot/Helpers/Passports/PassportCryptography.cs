#if !DISABLE_CRYPTOGRAPHY
using System;
using System.Text;
using System.Security.Cryptography;
using Telegram.Bot.Types.Passport;
using Newtonsoft.Json;

namespace Telegram.Bot.Helpers.Passports
{

    /// <summary>
    /// Helper methods for Passports cryptography
    /// </summary>
    public static class PassportCryptography
    {
        /// <summary>
        /// Tries to decrypt <see cref="EncryptedCredentials"/> as specified in the documentation https://core.telegram.org/passport#decrypting-data
        /// </summary>
        /// <param name="encryptedCredentials">The <see cref="EncryptedCredentials"/> to decrypt</param>
        /// <param name="privateKey">The private key whose public component you registered via @BotFather</param>
        /// <param name="credentials">The decrypted credentials (null if not successful)</param>
        /// <returns></returns>
        public static bool TryDecryptCredentials(EncryptedCredentials encryptedCredentials, RSA privateKey, out Credentials credentials)
        {
            credentials = null;

            byte[] data, hash, decryptedSecret;
            try
            {
                data = Convert.FromBase64String(encryptedCredentials.Data);
                hash = Convert.FromBase64String(encryptedCredentials.Hash);
                byte[] secret = Convert.FromBase64String(encryptedCredentials.Secret);

                decryptedSecret = privateKey.Decrypt(secret, RSAEncryptionPadding.OaepSHA1);
            }
            catch
            {
                return false;
            }

            if (data.Length % 16 != 0 || hash.Length != 32)
                return false;
            
            byte[] credentialsSecretHash;
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] combined = new byte[decryptedSecret.Length + hash.Length];
                Array.Copy(decryptedSecret, 0, combined, 0, decryptedSecret.Length);
                Array.Copy(hash, 0, combined, decryptedSecret.Length, hash.Length);
                credentialsSecretHash = sha512.ComputeHash(combined);
            }

            byte[] credentialsKey = new byte[32];
            byte[] credentialsIv = new byte[16];
            Array.Copy(credentialsSecretHash, 0, credentialsKey, 0, 32);
            Array.Copy(credentialsSecretHash, 32, credentialsIv, 0, 16);

            byte[] credentialsData;
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.Mode = CipherMode.CBC;
                aes.Key = credentialsKey;
                aes.IV = credentialsIv;
                aes.Padding = PaddingMode.None;
                using (var decryptor = aes.CreateDecryptor())
                {
                    credentialsData = decryptor.TransformFinalBlock(data, 0, data.Length);
                }
            }

            int padding = credentialsData[0];
            int actualDataLength = credentialsData.Length - padding;
            if (actualDataLength < 0) return false;

            byte[] actualData = new byte[credentialsData.Length - padding];
            Array.Copy(credentialsData, padding, actualData, 0, actualData.Length);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] dataHash = sha256.ComputeHash(credentialsData);
                for (int i = 0; i < dataHash.Length; i++)
                {
                    if (dataHash[i] != hash[i]) return false;
                }
            }

            try
            {
                string dataString = Encoding.UTF8.GetString(actualData);
                credentials = JsonConvert.DeserializeObject<Credentials>(dataString);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
#endif
