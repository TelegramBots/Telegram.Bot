using Telegram.Bot.CommandHandler.Types;
using System.Threading.Tasks;

namespace Telegram.Bot.CommandHandler
{
    /// <summary>
    /// Base class for command modules.
    /// </summary>
    public abstract class CommandModule
    {

        /// <summary>
        /// Called before a command is executed.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public virtual async Task BeforeExecutionAsync(CommandContext ctx) => await Task.Delay(0);

        /// <summary>
        /// Called after a command is executed.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public virtual async Task AfterExecutionAsync(CommandContext ctx) => await Task.Delay(0);

    }
}
