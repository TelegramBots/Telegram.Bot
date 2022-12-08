namespace EnumSerializer.Generator;

public sealed class EnumInfo
{
    public string Name { get; }

    public string Namespace { get; }

    public IReadOnlyList<KeyValuePair<string, string>> Members { get; }

    public EnumInfo(
        string name,
        string ns,
        List<KeyValuePair<string, string>> members)
    {
        Name = name;
        Namespace = ns;
        Members = members;
    }
}
