using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Scriban;
using System.Collections.Immutable;
using System.Text;

namespace EnumSerializer.Generator;

[Generator]
public class EnumConverterGenerator : IIncrementalGenerator
{
    const string JsonConverterAttribute = "Newtonsoft.Json.JsonConverterAttribute";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<EnumDeclarationSyntax> enumDeclarations = context.SyntaxProvider
           .CreateSyntaxProvider(
               predicate: static (s, _) => IsSyntaxTargetForGeneration(s),
               transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx))
           .Where(static m => m is not null)!;

        IncrementalValueProvider<(Compilation, ImmutableArray<EnumDeclarationSyntax>)> compilationAndEnums
            = context.CompilationProvider.Combine(enumDeclarations.Collect());

        context.RegisterSourceOutput(compilationAndEnums,
            static (spc, source) => Execute(source.Item1, source.Item2, spc));
    }

    static bool IsSyntaxTargetForGeneration(SyntaxNode node)
        => node is EnumDeclarationSyntax { AttributeLists.Count: > 0 };

    static EnumDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        // we know the node is a EnumDeclarationSyntax thanks to IsSyntaxTargetForGeneration
        var enumDeclarationSyntax = (EnumDeclarationSyntax)context.Node;

        // loop through all the attributes on the method
        foreach (AttributeListSyntax attributeListSyntax in enumDeclarationSyntax.AttributeLists)
        {
            foreach (AttributeSyntax attributeSyntax in attributeListSyntax.Attributes)
            {
                if (context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol
                    is not IMethodSymbol attributeSymbol)
                {
                    // weird, we couldn't get the symbol, ignore it
                    continue;
                }

                INamedTypeSymbol attributeContainingTypeSymbol = attributeSymbol.ContainingType;
                string fullName = attributeContainingTypeSymbol.ToDisplayString();

                // Is the attribute the [JsonConverterAttribute] attribute?
                if (fullName == JsonConverterAttribute)
                {
                    // return the enum
                    return enumDeclarationSyntax;
                }
            }
        }

        // we didn't find the attribute we were looking for
        return null;
    }

    static void Execute(
        Compilation compilation,
        ImmutableArray<EnumDeclarationSyntax> enums,
        SourceProductionContext context)
    {
        if (enums.IsDefaultOrEmpty)
        {
            // nothing to do yet
            return;
        }

        IEnumerable<EnumDeclarationSyntax> distinctEnums = enums.Distinct();

        List<EnumInfo> enumsToProcess = GetTypesToGenerate(compilation, distinctEnums, context.CancellationToken);
        if (enumsToProcess.Count == 0)
        {
            return;
        }

        Template template = Template.Parse(SourceGenerationHelper.ConverterTemplate);
        foreach (var enumToProcess in enumsToProcess)
        {
            var result = SourceGenerationHelper.GenerateConverterClass(template, enumToProcess);
            context.AddSource(
                hintName: $"{enumToProcess.Name}Converter.g.cs",
                sourceText: SourceText.From(result, Encoding.UTF8)
            );
        }
    }

    static List<EnumInfo> GetTypesToGenerate(
        Compilation compilation,
        IEnumerable<EnumDeclarationSyntax> enums, CancellationToken ct)
    {
        var enumsToProcess = new List<EnumInfo>();
        INamedTypeSymbol? enumAttribute = compilation.GetTypeByMetadataName(JsonConverterAttribute);
        if (enumAttribute is null)
        {
            // nothing to do if this type isn't available
            return enumsToProcess;
        }

        foreach (var enumDeclarationSyntax in enums)
        {
            // stop if we're asked to
            ct.ThrowIfCancellationRequested();

            SemanticModel semanticModel = compilation.GetSemanticModel(enumDeclarationSyntax.SyntaxTree);
            if (semanticModel.GetDeclaredSymbol(enumDeclarationSyntax, cancellationToken: ct)
                is not INamedTypeSymbol enumSymbol)
            {
                // report diagnostic, something went wrong
                continue;
            }

            string name = enumSymbol.Name;
            string nameSpace = enumSymbol.ContainingNamespace.IsGlobalNamespace
                ? string.Empty
                : enumSymbol.ContainingNamespace.ToString();

            string fullyQualifiedName = enumSymbol.ToString();

            var enumMembers = enumSymbol.GetMembers();
            var members = new List<KeyValuePair<string, string>>(enumMembers.Length);

            foreach (var member in enumMembers)
            {
                if (member is not IFieldSymbol field
                    || field.ConstantValue is null)
                {
                    continue;
                }

                string? displayName = null;
                foreach (var attribute in member.GetAttributes())
                {
                    if (attribute.AttributeClass is null
                        || attribute.AttributeClass.Name != "DisplayAttribute")
                    {
                        continue;
                    }

                    foreach (var namedArgument in attribute.NamedArguments)
                    {
                        if (namedArgument.Key == "Name" && namedArgument.Value.Value?.ToString() is { } dn)
                        {
                            displayName = dn;
                            break;
                        }
                    }
                }

                members.Add(new(
                    member.Name,
                    displayName ?? ToSnakeCase(member.Name)
                ));
            }

            enumsToProcess.Add(new(
                name: name,
                ns: nameSpace,
                fullyQualifiedName: fullyQualifiedName,
                members: members
            ));
        }
        return enumsToProcess;
    }

    static string ToSnakeCase(string name) =>
        string.Concat(name.Select((x, i) => i > 0 && char.IsUpper(x)
            ? $"_{x}"
            : x.ToString())
        ).ToLower();
}
