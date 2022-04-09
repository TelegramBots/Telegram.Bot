using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Telegram.Bot.Converters.SystemTextJson;

internal abstract class EnumConverter<TEnum> : JsonConverter<TEnum>
    where TEnum : struct, Enum
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected abstract TEnum GetEnumValue(string value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected abstract string GetStringValue(TEnum value);

    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string enumText = JsonElement.ParseValue(ref reader).GetString()!;
        return GetEnumValue(enumText);
    }

    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(GetStringValue(value));
    }
}
