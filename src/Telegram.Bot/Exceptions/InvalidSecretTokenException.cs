namespace Telegram.Bot.Exceptions;

public class InvalidSecretTokenException : Exception
{
    public InvalidSecretTokenException(string message) : base(message)
    { }
}