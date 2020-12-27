using System;
using System.Collections.Generic;
using System.Text;

namespace Telegram.Bot.CommandHandler
{
    public class TelegramCommandHandler
    {

        /// <summary>
        /// All the registered command modules.
        /// </summary>
        public List<Type> RegisteredCommandModules { get; } = new List<Type>();

        /// <summary>
        /// Prefix for the commands. Defaults to a slach (/)
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Determines whether or not commands are case-sensitive.
        /// </summary>
        public bool CaseSensitive { get; set; }

        /// <summary>
        /// Create a new telegram command handler.
        /// </summary>
        /// <param name="prefix">Determines what's the prefix for commands. Defaults to a slash (/)</param>
        public TelegramCommandHandler(string prefix = "/", bool caseSensitive = false)
        {
            Prefix = prefix;
            CaseSensitive = caseSensitive;
        }

        /// <summary>
        /// Register a CommandModule subclass as a command class. You can register multiple classes.
        /// </summary>
        /// <typeparam name="T">CommandModule subclass</typeparam>
        public void RegisterCommands<T>() where T : CommandModule
        {
            RegisteredCommandModules.Add(typeof(T));
        }

    }
}
