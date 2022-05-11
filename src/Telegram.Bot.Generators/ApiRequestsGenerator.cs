using System.Diagnostics;
using System.Text.Json;
using Microsoft.CodeAnalysis;
using Telegram.Bot.Generators.Models;

namespace Telegram.Bot.Generators;

[Generator]
public sealed class ApiRequestsGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new ExistingApiRequestsSyntaxReceiver());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        if (context.SyntaxReceiver is not ExistingApiRequestsSyntaxReceiver existingApiTypesSyntaxReceiver)
            return;

        var existingApiTypes = existingApiTypesSyntaxReceiver.ExistingApiRequests;

        // For debugging in Visual Studio
        //Debugger.Launch();

        // For debugging in Rider (after build is started, go Run -> Attach to Process... from toolbar)
        //SpinWait.SpinUntil(() => Debugger.IsAttached);

        AdditionalText? apiMethodsFileData = context.AdditionalFiles.FirstOrDefault(f => f.Path.EndsWith("methods.json"));
        if (apiMethodsFileData == null)
        {
            throw new FileNotFoundException(
                "File containing the API methods data was not found in the Telegram.Bot project.",
                "methods.json");
        }

        using FileStream apiMethodsFile = File.OpenRead(apiMethodsFileData.Path);
        var types = JsonSerializer.Deserialize<List<BotApiMethod>>(apiMethodsFile)!;

        foreach (BotApiMethod botApiMethod in types)
        {
            string requestName = char.ToUpperInvariant(botApiMethod.MethodName[0]) + botApiMethod.MethodName[1..]
                + "Request";

            if (existingApiTypes.FirstOrDefault(t =>
                {
                    SemanticModel model = context.Compilation.GetSemanticModel(t.SyntaxTree);
                    var type = (ITypeSymbol)model.GetDeclaredSymbol(t)!;
                    return type.Name == requestName;
                }) is { } existingApiType
                && existingApiType.Modifiers.All(m => m.Text != "partial"))
            {
                // Skip this botApiType if non-partial class with corresponding name already exists
                // We want to declare some types manually without generating them
                continue;
            }

            // TODO allow sub-namespaces for some types

            var namespaceName = "Telegram.Bot.Requests";
            string data = GetPlaceholder(botApiMethod, namespaceName);

            context.AddSource($"{requestName}.generated.cs", data);
        }
    }

    private static string GetPlaceholder(BotApiMethod method, string namespaceName)
    {
        string result = Templates.Requests.Template.Render(new
        {
            method.MethodName,
            method.MethodDescription,
            method.Parameters,
            RequestNamespace = namespaceName
        });
        return result;
    }
}
