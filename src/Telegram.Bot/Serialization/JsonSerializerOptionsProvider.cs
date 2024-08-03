namespace Telegram.Bot.Serialization;

/// <summary>Static class offering JsonSerializerOptions configured for Telegram.Bot serialization</summary>
public static class JsonSerializerOptionsProvider
{
    /// <summary>JsonSerializerOptions configured for Telegram.Bot serialization</summary>
    [Obsolete("Use JsonBotAPI.Options")]
    public static JsonSerializerOptions Options => JsonBotAPI.Options;

    /// <summary>Configure JsonSerializerOptions for Telegram.Bot serialization</summary>
    /// <param name="options">JsonSerializerOptions to configure</param>
    [Obsolete("Use JsonBotAPI.Configure()")]
    public static void Configure(JsonSerializerOptions options) => JsonBotAPI.Configure(options);
}
