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
        //options.AllowOutOfOrderMetadataProperties = true;     // when System.Text.Json 9.0 is available, so we don't need custom PolymorphicJsonConverterFactory
        options.Converters.Add(new PolymorphicJsonConverterFactory());
        //options.Converters.Add(new BanTimeConverter());
        //options.Converters.Add(new ChatIdConverter());
        //options.Converters.Add(new InputFileConverter());
        //options.Converters.Add(new UnixDateTimeConverter());
        //AddGeneratedConverters(Options.Converters);
    }

    //static partial void AddGeneratedConverters(IList<JsonConverter> converters);
}
