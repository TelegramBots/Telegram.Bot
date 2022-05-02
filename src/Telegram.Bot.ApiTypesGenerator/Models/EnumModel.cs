using System.Collections.Generic;

namespace Telegram.Bot.ApiTypesGenerator.Models;

public sealed record EnumModel(
    string Description,
    IReadOnlyCollection<string> MapTo);
