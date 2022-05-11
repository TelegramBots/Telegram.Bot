using System.Collections.Generic;

namespace Telegram.Bot.ApiParser;

internal sealed record BotApiType(
    string TypeName,
    string TypeDescription,
    List<BotApiTypeParameter> Parameters);
