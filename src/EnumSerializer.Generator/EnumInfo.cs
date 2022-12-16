namespace EnumSerializer.Generator;

internal sealed class EnumInfo
{
    public string Name { get; }
    public string Namespace { get; }

    /// <summary>
    /// Key is the enum name.
    /// </summary>
    public  IReadOnlyList<KeyValuePair<string, string>> Members { get; }

    public EnumInfo(
        string name,
        string ns,
        IReadOnlyList<KeyValuePair<string, string>> members)
    {
        Name = name;
        Namespace = ns;
        Members = members;
    }
}
