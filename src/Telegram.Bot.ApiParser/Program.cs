using System.Linq;
using Telegram.Bot.ApiParser;
using Telegram.Bot.ApiParser.Models;

TelegramBotDocParser parser = new();
await parser.LoadBotApiPageAsync();
await parser.ParseEnumsAsync();
parser.ParseBotApiPage();

foreach (BotApiDescription a in parser.Types
             .SelectMany(t => t.Parameters.Select(p => p.ParameterDescription).Append(t.TypeDescription)).Concat(
                 parser.Methods.SelectMany(m =>
                     m.Parameters.Select(p => p.ParameterDescription).Append(m.MethodDescription))))
{
    for (var i = 0; i < a.Entities.Count; i++)
    {
        DescriptionEntity entity = a.Entities[i];
        a.Entities[i] = entity with
        {
            EntityKind = entity.EntityKindFactory!.Invoke(parser)
        };
    }
}

var data = new BotApiData(
    new BotApiInfo(ApiVersion: parser.Version),
    Methods: parser.Methods,
    Types: parser.Types);
await TelegramBotDocWriter.WriteBotApiDataAsync(data);
