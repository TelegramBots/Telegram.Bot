using System;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Telegram.Bot.Helpers.Passports;
using Telegram.Bot.Types.Passport;

namespace Telegram.Bot.Extensions.Passport
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

            byte[] data, hash, secret;
            try
            {
                data = Convert.FromBase64String(encryptedCredentials.Data);
                hash = Convert.FromBase64String(encryptedCredentials.Hash);
                byte[] encryptedSecret = Convert.FromBase64String(encryptedCredentials.Secret);

                secret = privateKey.Decrypt(encryptedSecret, RSAEncryptionPadding.OaepSHA1);
            }
            catch
            {
                return false;
            }

            if (TryDecryptInternal(data, hash, secret, out byte[] decryptedData) != null)
                return false;

            try
            {
                string dataString = Encoding.UTF8.GetString(decryptedData);
                credentials = JsonConvert.DeserializeObject<Credentials>(dataString);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Tries to decrypt encrypted data (<see cref="EncryptedPassportElement.Data"/>) as specified in the documentation https://core.telegram.org/passport#datacredentials
        /// </summary>
        /// <param name="encryptedElement"><see cref="EncryptedPassportElement"/> that contains encrypted data in the Data property</param>
        /// <param name="credentials"><see cref="DataCredentials"/> that correspond to the <see cref="EncryptedPassportElement"/></param>
        /// <typeparam name="T">Type of the decrypted data</typeparam>
        /// <returns></returns>
        public static DecryptionResult<T> DecryptData<T>(this EncryptedPassportElement encryptedElement, DataCredentials credentials) where T: IDecryptedData
        {
            switch (encryptedElement.Type)
            {
                case PassportEnums.ElementType.Passport:
                case PassportEnums.ElementType.DriverLicense:
                case PassportEnums.ElementType.IdentityCard:
                case PassportEnums.ElementType.InternalPassport:
                    if (typeof(T) != typeof(IdDocumentData))
                        return new DecryptionResult<T>("T must be IdDocumentData for this element");
                    break;

                case PassportEnums.ElementType.PersonalDetails:
                    if (typeof(T) != typeof(PersonalDetails))
                        return new DecryptionResult<T>("T must be PersonalDetails for this element");
                    break;

                case PassportEnums.ElementType.Address:
                    if (typeof(T) != typeof(ResidentialAddress))
                        return new DecryptionResult<T>("T must be ResidentialAddress for this element");
                    break;

                default:
                    return new DecryptionResult<T>("This encryptedElement does not have a valid data field. Consult https://core.telegram.org/passport#fields");
            }

            byte[] data, hash, secret;
            try
            {
                data = Convert.FromBase64String(encryptedElement.Data);
                hash = Convert.FromBase64String(credentials.DataHash);
                secret = Convert.FromBase64String(credentials.Secret);
            }
            catch
            {
                return new DecryptionResult<T>("Invalid input data");
            }

            string errorDescription = TryDecryptInternal(data, hash, secret, out byte[] decryptedDataBytes);
            if (errorDescription != null)
                return new DecryptionResult<T>(errorDescription);

            try
            {
                string json = Encoding.UTF8.GetString(decryptedDataBytes);
                T decryptedData = JsonConvert.DeserializeObject<T>(json);
                return new DecryptionResult<T>(decryptedData);
            }
            catch
            {
                return new DecryptionResult<T>("Invalid decrypted data");
            }
        }

        /// <summary>
        /// Tries to decrypt the encrypted file as specified in the documentation https://core.telegram.org/passport#filecredentials
        /// </summary>
        /// <param name="encryptedFileData">Encrypted file data</param>
        /// <param name="credentials"><see cref="FileCredentials"/> that correspond to the file</param>
        /// <param name="decryptedFileData">Decrypted file data (null if not successful)</param>
        /// <returns></returns>
        public static bool TryDecryptFile(byte[] encryptedFileData, FileCredentials credentials, out byte[] decryptedFileData)
        {
            byte[] hash, secret;
            try
            {
                hash = Convert.FromBase64String(credentials.FileHash);
                secret = Convert.FromBase64String(credentials.Secret);
            }
            catch
            {
                decryptedFileData = null;
                return false;
            }

            return TryDecryptInternal(encryptedFileData, hash, secret, out decryptedFileData) == null;
        }

        /// <summary>
        /// Null if successful - error description otherwise
        /// </summary>
        private static string TryDecryptInternal(byte[] data, byte[] hash, byte[] secret, out byte[] decryptedData)
        {
            decryptedData = null;

            if (data.Length % 16 != 0)
                return "Invalid data length";
            if (hash.Length != 32)
                return "Invalid hash length";

            byte[] credentialsSecretHash;
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] combined = new byte[secret.Length + hash.Length];
                Array.Copy(secret, 0, combined, 0, secret.Length);
                Array.Copy(hash, 0, combined, secret.Length, hash.Length);
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

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] dataHash = sha256.ComputeHash(credentialsData);
                for (int i = 0; i < dataHash.Length; i++)
                {
                    if (dataHash[i] != hash[i]) return "Hash mismatch";
                }
            }

            int padding = credentialsData[0];
            int actualDataLength = credentialsData.Length - padding;

            if (actualDataLength < 0)
                return "Invalid data and padding length";
            if (padding < 32)
                return "Invalid padding length";

            decryptedData = new byte[actualDataLength];
            Array.Copy(credentialsData, padding, decryptedData, 0, actualDataLength);
            return null;
        }
    }
}
