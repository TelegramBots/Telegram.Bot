#if ENABLE_CRYPTOGRAPHY
using Telegram.Bot.Types.Passport;

namespace Telegram.Bot.Helpers.Passports
{
    /// <summary>
    /// Holds information about a decryption attempt
    /// </summary>
    /// <typeparam name="T">Type of the decrypted data</typeparam>
    public class DecryptionResult<T> where T: IDecryptedData
    {
        /// <summary>
        /// Decrypted data if successful, invalid otherwise
        /// </summary>
        public T Result;

        /// <summary>
        /// Indicates whether the decryption was successful. If false <see cref="DecryptionResult{T}.ErrorDescription"/> is set
        /// </summary>
        public bool Successful;

        /// <summary>
        /// Description of the error if <see cref="DecryptionResult{T}.Successful"/> is false
        /// </summary>
        public string ErrorDescription;

        internal DecryptionResult(string errorDescription)
        {
            Successful = false;
            ErrorDescription = errorDescription;
        }
        internal DecryptionResult(T result)
        {
            Successful = true;
            Result = result;
        }
    }
}
#endif
