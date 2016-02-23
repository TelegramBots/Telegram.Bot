using System;

namespace Telegram.Bot.Types
{
    public class UpdateEventArgs : EventArgs
    {
        public Update Update { get; private set; }

        internal UpdateEventArgs(Update update)
        {
            Update = update;
        }
    }
}