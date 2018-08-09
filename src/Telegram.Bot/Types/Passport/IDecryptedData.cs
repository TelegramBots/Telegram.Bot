#if ENABLE_CRYPTOGRAPHY
namespace Telegram.Bot.Types.Passport
{
    /// <summary>
    /// Type of data held in <see cref="EncryptedPassportElement.Data"/> field. Implemented by <see cref="IdDocumentData"/>, <see cref="PersonalDetails"/> and <see cref="ResidentialAddress"/>
    /// </summary>
    public interface IDecryptedData
    {
    }
}
#endif
