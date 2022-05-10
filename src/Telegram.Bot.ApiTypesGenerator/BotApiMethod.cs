using System.Collections.Generic;

namespace Telegram.Bot.ApiTypesGenerator;

public sealed record BotApiMethod(
    string MethodName,
    string MethodDescription,
    List<BotApiMethodParameter> Parameters);
