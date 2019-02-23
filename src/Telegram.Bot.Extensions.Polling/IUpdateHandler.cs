using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Extensions.Polling
{
    public interface IUpdateHandler
    {
        Task UpdateReceived(Update update);

        Task ErrorOccurred(Exception exception);

        UpdateType[]? AllowedUpdates { get; }
    }
}
