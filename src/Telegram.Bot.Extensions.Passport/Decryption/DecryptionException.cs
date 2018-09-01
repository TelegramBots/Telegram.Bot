using System;

namespace Telegram.Bot.Extensions.Passport
{
    public class DecryptionException : Exception
    {
        public readonly string Message;

        public DecryptionException(string message)
        {
            Message = message;
        }
    }
}
