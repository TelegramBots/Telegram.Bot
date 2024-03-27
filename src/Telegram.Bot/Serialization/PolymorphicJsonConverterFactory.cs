using System.Linq;
using System.Reflection;

namespace Telegram.Bot.Serialization;

internal sealed class PolymorphicJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
        => (typeToConvert.IsAbstract || typeToConvert.IsInterface) &&
           typeToConvert.GetCustomAttributes<CustomJsonDerivedTypeAttribute>().Any();

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        => (JsonConverter?) Activator.CreateInstance(
            typeof(PolymorphicJsonConverter<>).MakeGenericType(typeToConvert),
            options
        );
}
