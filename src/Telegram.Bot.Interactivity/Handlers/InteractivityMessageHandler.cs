using Interactivity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace Interactivity.Handlers
{
    /// <summary>
    /// Detects and handles interactivity messages.
    /// </summary>
    public class InteractivityMessageHandler
    {
        public static void OnMessageSent(object sender, MessageEventArgs e, TelegramInteractivity interactivity)
        {
            // If it's a command, ignore it.
            if (e.Message.Text?.StartsWith(interactivity.Configuration.CommandPrefix) == true)
            {
                return;
            }
            // Get the interactivity object of this message.
            var iObject = interactivity.CurrentMessageInteractivityObjects
                .FirstOrDefault(obj =>
                    e.Message.Chat.Id == obj.Chat.Id
                    && (obj.Predicate == null || obj.Predicate.Invoke(e.Message)));
            // Null check
            if (iObject != null)
            {
                // Set its result.
                iObject.InteractivityResult = 
                    new InteractivityResult<Message>
                    (e.Message, iObject.InteractivityResult?.TimedOut ?? false);
                // If it hasn't timed out
                if (iObject.InteractivityResult?.TimedOut == false)
                {
                    interactivity.CurrentMessageInteractivityObjects.Remove(iObject);
                    iObject.TimeoutThreadToken.Cancel();
                    iObject.WaitHandle.Set();
                }
            }
        }

    }
}
