namespace Telegram.Bot.Serialization;

/// <summary>
/// Same as <see cref="JsonDerivedTypeAttribute"/> but used for the hack below. Necessary because using the built-in
/// attribute will lead to NotSupportedExceptions.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
internal class CustomJsonDerivedTypeAttribute(Type subtype, string? discriminator = default) : Attribute
{
    public Type Subtype { get; } = subtype;
    public string? Discriminator { get; set; } = discriminator;
}

/// <summary>
/// Same as <see cref="JsonDerivedTypeAttribute"/> but used for the hack below. Necessary because using the built-in
/// attribute will lead to NotSupportedExceptions.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
internal class CustomJsonDerivedTypeAttribute<T>(string? discriminator = default)
    : CustomJsonDerivedTypeAttribute(typeof(T), discriminator);
