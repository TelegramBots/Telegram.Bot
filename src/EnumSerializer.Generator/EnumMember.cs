namespace EnumSerializer.Generator;

public readonly struct EnumMember
{
    public int MemberValue { get; }

    public string MemberName { get; }

    public EnumMember(int memberValue, string memberName)
    {
        MemberValue = memberValue;
        MemberName = memberName;
    }
}
