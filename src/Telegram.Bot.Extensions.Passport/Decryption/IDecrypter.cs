using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types.Passport;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Passport
{
    public interface IDecrypter
    {
        Credentials DecryptCredentials(
            RSA key,
            EncryptedCredentials encryptedCredentials
        );

        TValue DecryptData<TValue>(
            string encryptedData,
            DataCredentials dataCredentials
        )
            where TValue : IDecryptedValue;

        byte[] DecryptFile(
            byte[] encryptedContent,
            FileCredentials fileCredentials
        );

        Task DecryptFileAsync(
            Stream encryptedContent,
            FileCredentials fileCredentials,
            Stream destination,
            CancellationToken cancellationToken = default
        );
    }
}
