using System;
using System.Collections.Generic;
using System.Text;

namespace Interactivity.Types
{
    /// <summary>
    /// Configuration for a TelegramBotClient's TelegramInteractivity
    /// </summary>
    public class InteractivityConfiguration
    {
        /// <summary>
        /// Indicates the time an interactivity process will take to time out and cancel automatically.
        /// Defaults to 2 minutes.
        /// </summary>
        public TimeSpan DefaultTimeOutTime { get; set; } = TimeSpan.FromMinutes(2);
        /// <summary>
        /// The TelegramInteractivity class will ignore messages starting with this property to prevent
        /// commands from being parsed as interactivity results. Defaults to /
        /// </summary>
        public string CommandPrefix { get; set; } = "/";
        /// <summary>
        /// Message to display when someone tries to start an interactivity operation when one has already
        /// started. Defaults to "You already have an ongoing operation."
        /// </summary>
        public string UserAlreadyHasOngoingOperationMessage { get; set; } 
            = "You already have an ongoing operation.";
    }
}
