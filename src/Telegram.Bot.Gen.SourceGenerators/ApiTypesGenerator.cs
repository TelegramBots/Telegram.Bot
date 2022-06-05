using System.Diagnostics;
using System.Text.Json;
using Microsoft.CodeAnalysis;
using Telegram.Bot.Gen.Shared;

namespace Telegram.Bot.Gen.SourceGenerators;

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

        AdditionalText? apiDataFileInfo = context.AdditionalFiles.FirstOrDefault(f => f.Path.EndsWith("data.json"));
        if (apiDataFileInfo == null)
        {
            throw new FileNotFoundException(
                "File containing the API types data was not found in the Telegram.Bot project.",
                "data.json");
        }

        using FileStream apiDataFile = File.OpenRead(apiDataFileInfo.Path);
        var apiData = JsonSerializer.Deserialize<BotApiData>(apiDataFile)!;

        foreach (BotApiType botApiType in apiData.Types.Where(t => !t.IsCompositeType))
        {
            if (Templates.Types.IgnoreTypes.Contains(botApiType.TypeName))
                continue;

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

            context.AddSource(Path.Combine(botApiType.TypeGroup, $"{botApiType.TypeName}.generated.cs"), data);
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
