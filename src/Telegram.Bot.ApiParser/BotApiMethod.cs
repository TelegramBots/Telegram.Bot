using System.Collections.Generic;

namespace Telegram.Bot.ApiParser;

public sealed record BotApiMethod(
    string MethodName,
    string MethodDescription,
    List<BotApiMethodParameter> Parameters);
