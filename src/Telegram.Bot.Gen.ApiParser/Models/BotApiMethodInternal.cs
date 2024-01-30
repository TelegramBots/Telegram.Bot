using System.Text.Json.Serialization;
using Telegram.Bot.Gen.Shared;

namespace Telegram.Bot.Gen.ApiParser.Models;

public sealed class BotApiMethodInternal : BotApiMethod
{
    [JsonIgnore]
    public string SiteIdentifier { get; set; }
}
