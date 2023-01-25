using System;

namespace Telegram.Bot.AspNet.Exceptions;

public class InvalidSecretTokenException : Exception
{
    public InvalidSecretTokenException(string message) : base(message)
    { }
}