using System;
using System.Collections.Generic;
using System.Text;

namespace Interactivity.Exceptions
{
    /// <summary>
    /// Called when an attempt to get interactivity was made when TelegramBotClient.UseInteractivty() hasn't yet been called.
    /// </summary>
    public class InteractivityNotUsedException : Exception
    {
        public InteractivityNotUsedException() 
            : base("Interactivity isn't used with this client. " +
                  "Consider using TelegramBotClient.UseInteractivity.")
        {

        }
    }
}
