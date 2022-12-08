using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EnumSerializer.Generator;

[Generator(LanguageNames.CSharp)]
public partial class EnumConverterGenerator : IIncrementalGenerator
{
    const string JsonConverterAttribute = "Newtonsoft.Json.JsonConverterAttribute";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<EnumInfo> enumDeclarations = context.SyntaxProvider
            .ForAttributeWithMetadataName(JsonConverterAttribute,
               predicate: static (node, _) => node is EnumDeclarationSyntax { AttributeLists.Count: > 0 },
               GetSemanticTargetForGeneration)
           .Where(static m => m is not null)!;

        context.RegisterSourceOutput(enumDeclarations, EmitSourceFile);
    }
}
