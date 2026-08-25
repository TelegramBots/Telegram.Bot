namespace Telegram.Bot.Serialization;

/// <summary>When placed on a type, indicates that the type should be serialized polymorphically.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
internal sealed class CustomJsonPolymorphicAttribute(string? typeDiscriminatorPropertyName = default, Type? unsupportedType = null) : JsonAttribute
{
    /// <summary>Gets or sets a custom type discriminator property name for the polymorhic type. Uses '$type' property name if unset.</summary>
    public string? TypeDiscriminatorPropertyName { get; } = typeDiscriminatorPropertyName;
    /// <summary>Fallback type with ctor(string, JsonDocument) for unsupported discriminator values. If unset, an exception will be thrown when an unsupported type is encountered.</summary>
    public Type? UnsupportedType { get; } = unsupportedType;
}
