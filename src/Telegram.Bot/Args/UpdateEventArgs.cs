using System;
using Telegram.Bot.Types;

namespace Telegram.Bot.Args
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