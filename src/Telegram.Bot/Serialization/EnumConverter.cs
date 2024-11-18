namespace Telegram.Bot.Serialization;

#pragma warning disable CS1591

public class EnumConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct, Enum
{
    private static readonly Dictionary<string, TEnum> mapRead = [];
    private static readonly Dictionary<TEnum, string> mapWrite = [];

    static EnumConverter()
    {
        var enumType = typeof(TEnum);
        var names = Enum.GetNames(enumType);
#if NET6_0_OR_GREATER
        var values = Enum.GetValues<TEnum>();
#else
        var values = (TEnum[])Enum.GetValues(typeof(TEnum));
#endif
        for (int i = 0; i < names.Length; i++)
        {
            var snakeName = JsonNamingPolicy.SnakeCaseLower.ConvertName(names[i]);
            mapRead[snakeName] = values[i];
            mapWrite[values[i]] = snakeName;
        }
    }

    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString();
        if (str == null) return default;
        mapRead.TryGetValue(str, out TEnum value);
        return value;
    }

    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
    {
        if (!mapWrite.TryGetValue(value, out var str))
            throw new JsonException($"Can't serialize value {value} for enum {typeof(TEnum).Name}");
        writer.WriteStringValue(str);
    }
}
