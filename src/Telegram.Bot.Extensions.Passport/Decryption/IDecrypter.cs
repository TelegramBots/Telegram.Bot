using Telegram.Bot.Types.Passport;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Passport
{
    public interface IDecrypter
    {
        Credentials DecryptCredentials(
            EncryptedCredentials encryptedCredentials
        );

        string DecryptData(
            string encryptedData,
            DataCredentials dataCredentials
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
    }
}
