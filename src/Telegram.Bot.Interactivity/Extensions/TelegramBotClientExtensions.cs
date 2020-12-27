using Interactivity.Exceptions;
using Interactivity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Interactivity.Extensions
{
    public static class TelegramBotClientExtensions
    {
        /// <summary>
        /// Current interactivity objects
        /// </summary>
        private static readonly Dictionary<TelegramBotClient, TelegramInteractivity> currentInteractivities
            = new Dictionary<TelegramBotClient, TelegramInteractivity>();

        /// <summary>
        /// Use Interactivity with this Bot Client.
        /// </summary>
        /// <param name="client">Your TelegramBotClient</param>
        /// <param name="configuration">Interactivity Configuration</param>
        public static void UseInteractivity(this TelegramBotClient client, InteractivityConfiguration configuration)
        {
            var interactivity = new TelegramInteractivity(client, configuration);
            currentInteractivities.Add(client, interactivity);
        }

        /// <summary>
        /// Get this client's TelegramInteractivity.
        /// </summary>
        /// <param name="client">The Telegram Client</param>
        /// <returns>This client's TelegramInteractivity. Throws an exception if you haven't done UseInteractivty().</returns>
        public static TelegramInteractivity GetInteractivity(this TelegramBotClient client)
        {
            if (!currentInteractivities.ContainsKey(client))
            {
                throw new InteractivityNotUsedException();
            }
            else
            {
                return currentInteractivities[client];
            }
        }

    }
}
