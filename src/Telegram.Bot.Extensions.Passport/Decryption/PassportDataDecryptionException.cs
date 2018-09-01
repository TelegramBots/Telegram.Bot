using System;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Exceptions
{
    /// <summary>
    /// Represents a fatal error in decryption of Telegram Passport Data
    /// </summary>
    public class PassportDataDecryptionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PassportDataDecryptionException"/>
        /// </summary>
        /// <param name="message">Error description</param>
        public PassportDataDecryptionException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="PassportDataDecryptionException"/>
        /// </summary>
        /// <param name="message">Error description</param>
        /// <param name="innerException">Root cause of the error</param>
        public PassportDataDecryptionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
