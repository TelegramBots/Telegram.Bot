using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Telegram.Bot.CommandHandler.Tools
{
    public static class CommandHandlerUtils
    {

        public static bool IsSameOrSubclass(Type potentialBase, Type potentialDescendant)
        {
            return potentialDescendant.IsSubclassOf(potentialBase)
                   || potentialDescendant == potentialBase;
        }

        public static bool IsAsync(this MethodInfo m)
        {
            return m?
                .GetCustomAttribute<AsyncStateMachineAttribute>()?
                .StateMachineType?
                .GetTypeInfo()
                .GetCustomAttribute<CompilerGeneratedAttribute>()
                != null;
        }

        /// <summary>
        /// Does .ToLower() if not case sensitive, doesn't otherwise.
        /// </summary>
        /// <param name="caseSensitive"></param>
        /// <returns></returns>
        public static string ToLowerCaseConditioned(this string text, bool caseSensitive)
        {
            return caseSensitive ? text : text.ToLower();
        }


    }
}
