using Telegram.Bot.CommandHandler.Attributes;
using Telegram.Bot.CommandHandler.Tools;
using Telegram.Bot.CommandHandler.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace Telegram.Bot.CommandHandler.Extensions
{
    public static class BotClientCommandHandler
    {

        private static readonly Dictionary<Type, List<MethodInfo>> ModulesAndMethods = new Dictionary<Type, List<MethodInfo>>();

        /// <summary>
        /// Initialize the TelegramCommandHandler with the client
        /// </summary>
        /// <param name="client">Client to initialize commands with</param>
        /// <param name="commandHandler">Command handler</param>
        public static void InitializeCommands(this TelegramBotClient client, TelegramCommandHandler commandHandler)
        {
            foreach (var commandModuleType in commandHandler.RegisteredCommandModules)
            {
                // Get all the modules and the methods that have on a single CommandContext parameter, is async, has a return type of Task,
                // and has the [Command] attribute.
                ModulesAndMethods.Add(commandModuleType,
                    commandModuleType
                        .GetMethods()
                        .Where(method =>
                            method.GetParameters()
                                .Length == 1
                            && CommandHandlerUtils.IsSameOrSubclass(method.GetParameters()[0].ParameterType, typeof(CommandContext))
                            && method.IsAsync()
                            && CommandHandlerUtils.IsSameOrSubclass(method.ReturnType, typeof(Task))
                            && method.GetCustomAttributes(typeof(CommandAttribute), false)?.Any() == true)
                        .ToList());
            }
            client.OnMessage += async (sender, e) =>
            {
                await OnMessageReceived(sender, e, client, commandHandler);
            };
        }

        private static async Task OnMessageReceived(object sender, MessageEventArgs e, TelegramBotClient client, TelegramCommandHandler commandHandler)
        {
            //Get the message
            Message message = e.Message;
            bool caseSensitive = commandHandler.CaseSensitive;
            //Is it a command?
            if (message?.Text?.StartsWith(commandHandler.Prefix) == true)
            {
                //Get the command without the prefix
                string command = e.Message.Text.Substring(commandHandler.Prefix.Length);
                //For every command module in the registered command modules
                //For every method that satisfies the condition above
                foreach (var moduleMethodsPair in ModulesAndMethods)
                {
                    //Create an instance of the command module
                    var commandModule = (CommandModule)Activator.CreateInstance(moduleMethodsPair.Key);
                    //Create a command context
                    foreach (var method in moduleMethodsPair.Value)
                    {
                        var commandAttribute = method
                                .GetCustomAttribute(typeof(CommandAttribute), false)
                                as CommandAttribute;
                        var aliasesAttribute = method
                            .GetCustomAttribute(typeof(AliasesAttribute), false)
                            as AliasesAttribute;
                        //If the method is linked to the sent command
                        if (commandAttribute
                            ?.CommandInvoker?
                            .ToLowerCaseConditioned(caseSensitive)
                                == command.ToLowerCaseConditioned(caseSensitive)
                            || aliasesAttribute
                            ?.Aliases
                            ?.Any(alias => alias.ToLowerCaseConditioned(caseSensitive) == command.ToLowerCaseConditioned(caseSensitive))
                                == true)
                        {
                            //Create a CommandContext to be used
                            CommandContext ctx = new CommandContext(e.Message, client);
                            // Call on the Before Execution Async method
                            await commandModule.BeforeExecutionAsync(ctx);
                            //Call on its method.
                            await (Task)method.Invoke(commandModule, new object[] { ctx });
                            //Call on the After Execution Async method
                            await commandModule.AfterExecutionAsync(ctx);
                            //Finish everything up
                            return;
                        }
                    }
                }
            }
            //else if (client.GetInteractivity().CurrentMessageInteractivityObjects.Count == 0)
            //{
            //    await client.SendTextMessageAsync(e.Message.Chat.Id, "Invalid or out of context.")
            //}
        }

    }
}
