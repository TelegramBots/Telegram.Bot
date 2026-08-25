// The original implementation is taken from there
// https://github.com/dotnet/runtime/issues/72604#issuecomment-1932302266

using System.Reflection;

namespace Telegram.Bot.Serialization;

/// <summary>
/// Supports deserializing JSON payloads that use polymorphism but don't specify $type as the first field.
/// Modified from https://github.com/dotnet/runtime/issues/72604#issuecomment-1440708052.
/// </summary>
internal class PolymorphicJsonConverter<T> : JsonConverter<T>
{
    private readonly string _discriminatorPropName;
    private readonly Func<string, JsonDocument, object>? _unsupportedHandling;
    private readonly Dictionary<string, Type> _discriminatorToSubtype = [];

    public PolymorphicJsonConverter()
    {
        var attr = typeof(T).GetCustomAttribute<CustomJsonPolymorphicAttribute>()!;
        _discriminatorPropName = JsonNamingPolicy.SnakeCaseLower.ConvertName(attr.TypeDiscriminatorPropertyName ?? "$type");
        if (attr.UnsupportedType is not null)
            _unsupportedHandling = (typeName, doc) => Activator.CreateInstance(attr.UnsupportedType, typeName, doc)!;

        foreach (var subtype in typeof(T).GetCustomAttributes<CustomJsonDerivedTypeAttribute>())
            if (subtype.Discriminator is not null)
                _discriminatorToSubtype.Add(subtype.Discriminator, subtype.Subtype);
    }

    public override bool CanConvert(Type typeToConvert) => typeof(T) == typeToConvert;

    public override T Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions options)
    {
        var reader2 = reader;
        var doc = JsonDocument.ParseValue(ref reader2);
        try
        {
            var root = doc.RootElement;
            var typeField = root.GetProperty(_discriminatorPropName);

            if (typeField.GetString() is not { } typeName)
                throw new JsonException($"Could not find string property {_discriminatorPropName} when trying to deserialize {typeof(T).Name}");

            if (!_discriminatorToSubtype.TryGetValue(typeName, out var type))
                if (_unsupportedHandling is not null) { var result = (T)_unsupportedHandling(typeName, doc); reader = reader2; doc = null; return result; }
                else throw new JsonException($"Unknown type: {typeName}");

            return (T)JsonSerializer.Deserialize(ref reader, type, options)!;
        }
        finally
        {
            doc?.Dispose();
        }
    }

    public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options)
    {
        var type = value!.GetType();
        JsonSerializer.Serialize(writer, value, type, options);
    }
}
