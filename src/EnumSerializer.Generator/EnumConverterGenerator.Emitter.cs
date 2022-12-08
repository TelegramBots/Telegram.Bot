using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Scriban;
using System.Text;

namespace EnumSerializer.Generator;

public partial class EnumConverterGenerator
{
    private static void EmitSourceFile(SourceProductionContext context, EnumInfo ei)
    {
        Template template = Template.Parse(SourceGenerationHelper.ConverterTemplate);

        var result = SourceGenerationHelper.GenerateConverterClass(template, ei);
        context.AddSource(
            hintName: $"{ei.Name}Converter.g.cs",
            sourceText: SourceText.From(result, Encoding.UTF8));
    }
}
