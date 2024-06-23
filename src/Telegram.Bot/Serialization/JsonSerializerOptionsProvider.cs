using System.Collections.Generic;

namespace Telegram.Bot.Serialization;

/// <summary>
///
/// </summary>
public static partial class JsonSerializerOptionsProvider
{
    static JsonSerializerOptionsProvider()
    {
        Options = new JsonSerializerOptions();
        Configure(Options);
        AddGeneratedConverters(Options.Converters);
    }

    /// <summary>
    ///
    /// </summary>
    public static JsonSerializerOptions Options { get; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="options"></param>
    public static void Configure(JsonSerializerOptions options)
    {
        options.Converters.Add(new UnixDateTimeConverter());
        options.Converters.Add(new BanTimeConverter());
        options.Converters.Add(new InputFileConverter());
        options.Converters.Add(new ChatIdConverter());
        options.Converters.Add(new PolymorphicJsonConverterFactory());
        options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
        options.UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip;
    }

    static partial void AddGeneratedConverters(IList<JsonConverter> converters);
}
