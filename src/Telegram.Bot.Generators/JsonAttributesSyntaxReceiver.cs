using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Telegram.Bot.Generators;

public sealed class JsonAttributesSyntaxReceiver : ISyntaxReceiver
{
    public List<TypeDeclarationSyntax> Candidates { get; } = new();

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        if (syntaxNode is not TypeDeclarationSyntax typeDeclarationSyntax)
            return;

        //SpinWait.SpinUntil(() => Debugger.IsAttached);

        if (typeDeclarationSyntax.Keyword.ToString() is "record" or "class" &&
            typeDeclarationSyntax.Modifiers.Any(m => m.Text == "public") &&
            typeDeclarationSyntax.Modifiers.All(m => m.Text != "static") &&
            typeDeclarationSyntax.Parent is BaseNamespaceDeclarationSyntax namespaceDeclarationSyntax &&
            (namespaceDeclarationSyntax.Name.ToString().Contains("Types") ||
            namespaceDeclarationSyntax.Name.ToString().Contains("Requests")))
        {
            Candidates.Add(typeDeclarationSyntax);
        }
    }
}
