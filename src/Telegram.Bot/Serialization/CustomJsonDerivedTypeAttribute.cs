namespace Telegram.Bot.Serialization;

/// <summary>Same as <see cref="JsonDerivedTypeAttribute"/> but used for the hack below.
/// Necessary because using the built-in attribute will lead to NotSupportedExceptions.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
internal sealed class CustomJsonDerivedTypeAttribute(Type subtype, string? discriminator = default) : Attribute
{
    public Type Subtype { get; } = subtype;
    public string? Discriminator { get; set; } = discriminator;
}
