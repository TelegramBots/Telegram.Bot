using System;

namespace Telegram.Bot.Types
{
    public class ChosenInlineResultEventArgs : EventArgs
    {
        public ChosenInlineResult ChosenInlineResult { get; private set; }

        internal ChosenInlineResultEventArgs(Update update)
        {
            ChosenInlineResult = update.ChosenInlineResult;
        }

        internal ChosenInlineResultEventArgs(ChosenInlineResult chosenInlineResult)
        {
            ChosenInlineResult = chosenInlineResult;
        }

        public static implicit operator ChosenInlineResultEventArgs(UpdateEventArgs e) => new ChosenInlineResultEventArgs(e.Update);
    }
}
