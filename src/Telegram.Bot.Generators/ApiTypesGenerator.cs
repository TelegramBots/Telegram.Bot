using System.Diagnostics;
using System.Text.Json;
using Microsoft.CodeAnalysis;
using Telegram.Bot.Generators.Models;

namespace Telegram.Bot.Generators;

[Generator]
public sealed class ApiTypesGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new ExistingApiTypesSyntaxReceiver());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        if (context.SyntaxReceiver is not ExistingApiTypesSyntaxReceiver existingApiTypesSyntaxReceiver)
            return;

        var existingApiTypes = existingApiTypesSyntaxReceiver.ExistingApiTypes;

        // For debugging in Visual Studio
        //Debugger.Launch();

        // For debugging in Rider (after build is started, go Run -> Attach to Process... from toolbar)
        //SpinWait.SpinUntil(() => Debugger.IsAttached);

        AdditionalText? apiTypesFileData = context.AdditionalFiles.FirstOrDefault(f => f.Path.EndsWith("types.json"));
        if (apiTypesFileData == null)
        {
            throw new FileNotFoundException(
                "File containing the API types data was not found in the Telegram.Bot project.",
                "types.json");
        }

        using FileStream apiTypesFile = File.OpenRead(apiTypesFileData.Path);
        var types = JsonSerializer.Deserialize<List<BotApiType>>(apiTypesFile)!;

        foreach (BotApiType botApiType in types)
        {
            botApiType.TypeName = Templates.Types.Mapping.TryGetValue(botApiType.TypeName, out string? newTypeName)
                ? newTypeName
                : botApiType.TypeName;

            if (existingApiTypes.FirstOrDefault(t =>
                {
                    SemanticModel model = context.Compilation.GetSemanticModel(t.SyntaxTree);
                    var type = (ITypeSymbol)model.GetDeclaredSymbol(t)!;
                    return type.Name == botApiType.TypeName;
                }) is { } existingApiType
                && existingApiType.Modifiers.All(m => m.Text != "partial"))
            {
                // Skip this botApiType if non-partial class with corresponding name already exists
                // We want to declare some types manually without generating them
                continue;
            }

            // TODO allow sub-namespaces for some types

            var namespaceName = "Telegram.Bot.Types";
            string data = GetPlaceholder(botApiType, namespaceName);

            context.AddSource($"{botApiType.TypeName}.generated.cs", data);
        }
    }

    private static string GetPlaceholder(BotApiType type, string namespaceName)
    {
        string result = Templates.Types.Template.Render(new
        {
            type.TypeName,
            type.TypeDescription,
            type.Parameters,
            TypeNamespace = namespaceName
        });
        return result;
    }
}
