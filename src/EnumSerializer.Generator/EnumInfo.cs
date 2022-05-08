namespace EnumSerializer.Generator;

public readonly struct EnumInfo
{
    public readonly string Name;
    public readonly string FullyQualifiedName;
    public readonly string Namespace;

    /// <summary>
    /// Key is the enum name.
    /// </summary>
    public readonly List<KeyValuePair<string, string>> Members;

    public EnumInfo(
        string name,
        string ns,
        string fullyQualifiedName,
        List<KeyValuePair<string, string>> members)
    {
        Name = name;
        Namespace = ns;
        Members = members;
        FullyQualifiedName = fullyQualifiedName;
    }
}
