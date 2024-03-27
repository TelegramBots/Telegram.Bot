using System.Reflection;

namespace Telegram.Bot.Serialization;

internal class MaybeInaccessibleMessageConverter : JsonConverter<MaybeInaccessibleMessage>
{
    static readonly TypeInfo BaseType = typeof(MaybeInaccessibleMessage).GetTypeInfo();

    public override bool CanConvert(Type objectType) =>
        BaseType.IsAssignableFrom(objectType.GetTypeInfo());

    public override MaybeInaccessibleMessage? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        var clonedReader = reader;
        var jsonElement = JsonElement.ParseValue(ref clonedReader);
        if (!jsonElement.TryGetProperty("date", out var dateElement))
            return null;

        if (!dateElement.TryGetInt64(out long date))
        {
            throw new JsonException($"Unknown chat member status value of '{dateElement}'");
        }

        var actualType = date is 0
            ? typeof(InaccessibleMessage)
            : typeof(Message);

        return (MaybeInaccessibleMessage?) JsonSerializer.Deserialize(ref reader, actualType, options);
    }

    public override void Write(
        Utf8JsonWriter writer,
        MaybeInaccessibleMessage value,
        JsonSerializerOptions options
    )
        => JsonSerializer.SerializeToElement(value, options).WriteTo(writer);
}
