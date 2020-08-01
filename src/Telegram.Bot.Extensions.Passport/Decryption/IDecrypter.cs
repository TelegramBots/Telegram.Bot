using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.Passport;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Passport
{
    /// <summary>
    /// Provides decryption utilities for encrypted Telegram Passport data
    /// </summary>
    public interface IDecrypter
    {
        /// <summary>
        /// Decrypts encrypted credentials in <see cref="PassportData"/> using RSA key
        /// </summary>
        /// <param name="encryptedCredentials">Encrypted credentials in Passport data</param>
        /// <param name="key">RSA private key</param>
        /// <returns>Decrypted credentials</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="PassportDataDecryptionException"></exception>
        /// <exception cref="CryptographicException"></exception>
        Credentials DecryptCredentials(
            EncryptedCredentials encryptedCredentials,
            RSA key
        );

        /// <summary>
        /// Decrypts encrypted data using its accompanying data credentials and deserializes the result
        /// from JSON to an instance of <typeparamref name="TValue"/>
        /// </summary>
        /// <param name="encryptedData">Encrypted Passport data</param>
        /// <param name="dataCredentials">Accompanying data credentials required for decryption</param>
        /// <returns>Decrypted data</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="PassportDataDecryptionException"></exception>
        /// <exception cref="JsonSerializationException"></exception>
        TValue DecryptData<TValue>(
            string encryptedData,
            DataCredentials dataCredentials
        )
            where TValue : class, IDecryptedValue;

        /// <summary>
        /// Decrypts encrypted file bytes using its accompanying file credentials
        /// </summary>
        /// <param name="encryptedContent">Encrypted Passport file</param>
        /// <param name="fileCredentials">Accompanying file credentials required for decryption</param>
        /// <returns>Decrypted file bytes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="PassportDataDecryptionException"></exception>
        byte[] DecryptFile(
            byte[] encryptedContent,
            FileCredentials fileCredentials
        );

        /// <summary>
        /// Decrypts encrypted file from stream using its accompanying file credentials and writes it
        /// to <paramref name="destination"/> stream
        /// </summary>
        /// <param name="encryptedContent">Encrypted Passport file stream</param>
        /// <param name="fileCredentials">Accompanying file credentials required for decryption</param>
        /// <param name="destination">Stream to write decrypted file content to</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="PassportDataDecryptionException"></exception>
        /// <exception cref="CryptographicException"></exception>
        Task DecryptFileAsync(
            Stream encryptedContent,
            FileCredentials fileCredentials,
            Stream destination,
            CancellationToken cancellationToken = default
        );
    }
}
