using System.Linq;
using Telegram.Bot.Gen.ApiParser;
using Telegram.Bot.Gen.ApiParser.Models;
using Telegram.Bot.Gen.Shared;

TelegramBotDocParser parser = new();
await parser.LoadBotApiPageAsync();
await parser.ParseEnumsAsync();
parser.ParseBotApiPage();

foreach (BotApiDescription a in parser.Types
             .SelectMany(t => t.Parameters.Select(p => p.ParameterDescription).Append(t.TypeDescription)).Concat(
                 parser.Methods.SelectMany(m =>
                     m.Parameters.Select(p => p.ParameterDescription).Append(m.MethodDescription))))
{
    foreach (DescriptionEntity t in a.Entities)
    {
        var entity = (DescriptionEntityInternal) t;
        entity.EntityKind = entity.EntityKindFactory(parser);
    }
}

var data = new BotApiData
{
    Info = new BotApiInfo
    {
        ApiVersion = parser.Version
    },
    Methods = parser.Methods,
    Types = parser.Types
};
await TelegramBotDocWriter.WriteBotApiDataAsync(data);
