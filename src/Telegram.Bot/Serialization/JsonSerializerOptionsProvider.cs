using System.Collections.Generic;

namespace Telegram.Bot.Serialization;

/// <summary>
///
/// </summary>
public static partial class JsonSerializerOptionsProvider
{
    static JsonSerializerOptionsProvider()
    {
        Options = new()
        {
            Converters =
            {
                new UnixDateTimeConverter(),
                new BanTimeConverter(),
                new ColorConverter(),
                new InputFileConverter(),
                new ChatIdConverter(),
                new PolymorphicJsonConverterFactory(),
            },
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip,
        };

        AddGeneratedConverters(Options.Converters);
    }

    /// <summary>
    ///
    /// </summary>
    public static JsonSerializerOptions Options { get; }

    static partial void AddGeneratedConverters(IList<JsonConverter> converters);
}
