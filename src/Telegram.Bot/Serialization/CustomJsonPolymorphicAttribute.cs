namespace Telegram.Bot.Serialization;

/// <summary>When placed on a type, indicates that the type should be serialized polymorphically.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
internal sealed class CustomJsonPolymorphicAttribute(string? typeDiscriminatorPropertyName = default) : JsonAttribute
{
    /// <summary>Gets or sets a custom type discriminator property name for the polymorhic type. Uses '$type' property name if unset.</summary>
    public string? TypeDiscriminatorPropertyName { get; } = typeDiscriminatorPropertyName;
}
