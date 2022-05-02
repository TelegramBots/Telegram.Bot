using System.Collections.Generic;

namespace Telegram.Bot.ApiTypesGenerator;

internal sealed record BotApiType(
    string TypeName,
    string TypeDescription,
    List<BotApiTypeParameter> Parameters);
