using Xunit.Sdk;

namespace Telegram.Bot.Tests.Integ.XunitExtensions
{
    public interface IAcceptRunSummary
    {
         RunSummary TestsSummary { get; set; }
    }
}
