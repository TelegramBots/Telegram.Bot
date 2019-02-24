using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Extensions.Polling
{
    /// <summary>
    /// Processes <see cref="Update"/>s and errors.
    /// <para>See <see cref="DefaultUpdateHandler"/> for a simple implementation</para>
    /// </summary>
    public interface IUpdateHandler
    {
        /// <summary>
        /// Handles an <see cref="Update"/>
        /// </summary>
        /// <param name="update">The <see cref="Update"/> to handle</param>
        /// <returns></returns>
        Task HandleUpdate(Update update);

        /// <summary>
        /// Handles an <see cref="Exception"/>
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> to handle</param>
        /// <returns></returns>
        Task HandleError(Exception exception);

        /// <summary>
        /// Indicates which <see cref="UpdateType"/>s are allowed to be received. null means all updates
        /// </summary>
        UpdateType[]? AllowedUpdates { get; }
    }
}
