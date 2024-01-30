using System;
using System.Text.Json.Serialization;
using Telegram.Bot.Gen.Shared;
using Telegram.Bot.Gen.Shared.Enums;

namespace Telegram.Bot.Gen.ApiParser.Models;

public sealed class DescriptionEntityInternal : DescriptionEntity
{
    [JsonIgnore]
    public Func<TelegramBotDocParser, DescriptionEntityKind> EntityKindFactory { get; set; }
}
