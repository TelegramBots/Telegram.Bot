namespace Telegram.Bot;

/// <summary>Static class offering JsonSerializerOptions configured for Bot API serialization</summary>
public static class JsonBotAPI
{
    /// <summary>JsonSerializerOptions configured for Bot API serialization</summary>
    public static JsonSerializerOptions Options { get; }

    static JsonBotAPI() => Configure(Options = new());

    /// <summary>Configure JsonSerializerOptions for Bot API serialization</summary>
    /// <param name="options">JsonSerializerOptions to configure</param>
    public static void Configure(JsonSerializerOptions options)
    {
        options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
#if NET6_0_OR_GREATER
        if (!JsonSerializer.IsReflectionEnabledByDefault) options.TypeInfoResolverChain.Add(JsonBotSerializerContext.Default);
#endif
        //when System.Text.Json 9.0 is available, we can use
        //options.AllowOutOfOrderMetadataProperties = true; // so we don't need custom PolymorphicJsonConverterFactory
    }
}
