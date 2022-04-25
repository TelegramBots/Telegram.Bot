using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Telegram.Bot.Converters;

internal abstract class EnumConverter<TEnum> : JsonConverter<TEnum>
    where TEnum : Enum
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected abstract TEnum GetEnumValue(string value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected abstract string GetStringValue(TEnum value);

    public override void WriteJson(JsonWriter writer, TEnum value, JsonSerializer serializer) =>
        writer.WriteValue(GetStringValue(value));

    public override TEnum ReadJson(
        JsonReader reader,
        Type objectType,
        TEnum existingValue,
        bool hasExistingValue,
        JsonSerializer serializer
    ) =>
        GetEnumValue(JToken.ReadFrom(reader).Value<string>());
}