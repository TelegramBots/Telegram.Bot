using System.Collections.Generic;

namespace Telegram.Bot.ApiParser.Models;

public sealed record EnumModel(
    string Description,
    IReadOnlyCollection<string> MapTo);
