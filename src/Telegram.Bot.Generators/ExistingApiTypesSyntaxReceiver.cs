using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Telegram.Bot.Generators;

public sealed class ExistingApiTypesSyntaxReceiver : ISyntaxReceiver
{
    public List<TypeDeclarationSyntax> ExistingApiTypes { get; } = new();

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        if (syntaxNode is not TypeDeclarationSyntax typeDeclarationSyntax)
            return;

        //SpinWait.SpinUntil(() => Debugger.IsAttached);

        if (typeDeclarationSyntax.Keyword.ToString() is "record" or "class" &&
            typeDeclarationSyntax.Modifiers.Any(m => m.Text == "public") &&
            typeDeclarationSyntax.Modifiers.All(m => m.Text != "static") &&
            typeDeclarationSyntax.Parent is BaseNamespaceDeclarationSyntax namespaceDeclarationSyntax &&
            namespaceDeclarationSyntax.Name.ToString().Contains("Types"))
        {
            ExistingApiTypes.Add(typeDeclarationSyntax);
        }
    }
}
