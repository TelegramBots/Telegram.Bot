using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Telegram.Bot.ApiParser.Models;

public sealed record BotApiDescription(
    [property: JsonPropertyName("descriptionText")]
    string DescriptionText,
    [property: JsonPropertyName("entities")]
    List<DescriptionEntity> Entities);
