namespace Telegram.Bot.Serialization;

/// <summary>Static class offering JsonSerializerOptions configured for Telegram.Bot serialization</summary>
public static partial class JsonSerializerOptionsProvider
{
    /// <summary>JsonSerializerOptions configured for Telegram.Bot serialization</summary>
    public static JsonSerializerOptions Options { get; }

    static JsonSerializerOptionsProvider() => Configure(Options = new JsonSerializerOptions());

    /// <summary>Configure JsonSerializerOptions for Telegram.Bot serialization</summary>
    /// <param name="options">JsonSerializerOptions to configure</param>
    public static void Configure(JsonSerializerOptions options)
    {
        options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
        options.UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip;
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
