using System;

namespace Telegram.Bot.Types
{
    public class ReceiveErrorEventArgs : EventArgs
    {
        public ApiRequestException ApiRequestException { get; private set; }

        internal ReceiveErrorEventArgs(ApiRequestException apiRequestException)
        {
            ApiRequestException = apiRequestException;
        }

        public static implicit operator ReceiveErrorEventArgs(ApiRequestException e) => new ReceiveErrorEventArgs(e);
    }
}
