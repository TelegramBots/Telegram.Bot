using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;

namespace EnumSerializer.Generator;

public partial class EnumConverterGenerator
{
    private static EnumInfo? GetSemanticTargetForGeneration(GeneratorAttributeSyntaxContext context, CancellationToken cancellationToken)
    {
        var enumDef = (EnumDeclarationSyntax)context.TargetNode;
        BaseNamespaceDeclarationSyntax? ns = enumDef.Parent as BaseNamespaceDeclarationSyntax;
        if (ns is null)
        {
            if (enumDef.Parent is not CompilationUnitSyntax)
            {
                // since this generator doesn't know how to generate a nested type...
                return null;
            }
        }

        EnumInfo? enumDeclaration = null;
        string? nspace = null;

        foreach (AttributeData attribute in context.TargetSymbol.GetAttributes())
        {
            if (attribute.AttributeClass?.Name != "JsonConverterAttribute"
                || attribute.AttributeClass.ToDisplayString() != JsonConverterAttribute)
            {
                continue;
            }

            nspace ??= ConstructNamespace(ns);

            string enumName = enumDef.Identifier.ValueText;

            var enumMembers = enumDef.Members;
            var members = new List<KeyValuePair<string, string>>(enumMembers.Count);
            foreach (var member in enumMembers)
            {
                string? displayName = null;
                foreach (var enumAttribute in member.AttributeLists.SelectMany(a => a.Attributes))
                {
                    var identifierName = enumAttribute.Name as IdentifierNameSyntax;
                    if (identifierName?.Identifier.ValueText != "Display")
                    {
                        continue;
                    }

                    foreach (var argument in enumAttribute.ArgumentList?.Arguments)
                    {
                        if (argument.Expression is LiteralExpressionSyntax expression)
                        {
                            displayName = expression.Token.ValueText;
                            break;
                        }
                    }
                }

                members.Add(new(
                    member.Identifier.ValueText,
                    displayName ?? ToSnakeCase(member.Identifier.ValueText)));
            }

            enumDeclaration = new(enumName, nspace, members);
        }

        return enumDeclaration;
    }

    private static string ConstructNamespace(BaseNamespaceDeclarationSyntax? ns)
    {
        if (ns is null)
            return string.Empty;

        string nspace = ns.Name.ToString();
        while (true)
        {
            ns = ns.Parent as BaseNamespaceDeclarationSyntax;
            if (ns == null)
            {
                break;
            }

            nspace = $"{ns.Name}.{nspace}";
        }

        return nspace;
    }

    static string ToSnakeCase(string name)
    {
        StringBuilder sb = new();

        State previous = default;

        for (var index = 0; index < name.Length; index++)
        {
            char c = name[index];
            State current = new(c, char.IsUpper(c));

            if (current.IsUpper)
            {
                c = char.ToLowerInvariant(current.Character);

                if (index > 0
                    && previous.Character != '_'
                    && !previous.IsUpper)
                {
                    sb.Append('_');
                }
            }

            sb.Append(c);
            previous = current;
        }
        return sb.ToString();
    }

    private readonly struct State
    {
        public readonly char Character;
        public readonly bool IsUpper;
        public State(char character, bool isUpper)
        {
            Character = character;
            IsUpper = isUpper;
        }
    }
}
