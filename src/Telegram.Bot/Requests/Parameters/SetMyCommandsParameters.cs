using System.Collections.Generic;
using Telegram.Bot.Types;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.SetMyCommandsAsync" /> method.
    /// </summary>
    public class SetMyCommandsParameters : ParametersBase
    {
        /// <summary>
        ///     A list of bot commands to be set
        /// </summary>
        public IEnumerable<BotCommand> Commands { get; set; }

        /// <summary>
        ///     Sets <see cref="Commands" /> property.
        /// </summary>
        /// <param name="commands">A list of bot commands to be set</param>
        public SetMyCommandsParameters WithCommands(IEnumerable<BotCommand> commands)
        {
            Commands = commands;
            return this;
        }
    }
}