using System;
using System.Collections.Generic;
using System.Text;

namespace Telegram.Bot.CommandHandler.Attributes
{
    /// <summary>
    /// Alternative commands to invoke the same method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AliasesAttribute : Attribute
    {
        /// <summary>
        /// Alternative commands to invoke the same method.
        /// </summary>
        public string[] Aliases { get; set; }

        /// <summary>
        /// Alternative commands to invoke the same method.
        /// </summary>
        /// <param name="aliases">Aliases of the command</param>
        public AliasesAttribute(params string[] aliases)
        {
            Aliases = aliases;
        }
    }
}
