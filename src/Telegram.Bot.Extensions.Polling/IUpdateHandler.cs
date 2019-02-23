using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Extensions.Polling
{
    public interface IUpdateHandler
    {
        Task HandleUpdate(Update update);

        Task HandleError(Exception exception);

        UpdateType[]? AllowedUpdates { get; }
    }
}
