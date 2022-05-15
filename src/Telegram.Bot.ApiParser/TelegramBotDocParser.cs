using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using Telegram.Bot.ApiParser.Exceptions;
using Telegram.Bot.ApiParser.Models;
using Telegram.Bot.ApiParser.Models.Enums;

namespace Telegram.Bot.ApiParser;

public sealed class TelegramBotDocParser
{
    private const string BotApiUrl = "https://core.telegram.org/bots/api";

    private string? _html;
    private EnumsModel? _enums;

    public List<BotApiType> Types { get; } = new();
    public List<BotApiMethod> Methods { get; } = new();
    public string Version { get; private set; } = string.Empty;

    public async Task LoadBotApiPageAsync()
    {
        using var httpClient = new HttpClient();
        _html = await httpClient.GetStringAsync(BotApiUrl);
    }

    public void ParseBotApiPage()
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(_html);

        // Parsing version
        HtmlNode versionNode = doc.DocumentNode.SelectSingleNode("//div[@id='dev_page_content']/p");
        Version = versionNode.InnerText.Split(' ')[2];

        // Parsing types and methods
        HtmlNodeCollection sections = doc.DocumentNode.SelectNodes("//h3");
        foreach (HtmlNode section in sections)
        {
            string sectionName = section.InnerText;

            HtmlNode? siblingNode = GetNextSibling(section);
            while (siblingNode is { Name: not "h3" })
            {
                if (siblingNode is { Name: "h4" } typeNameNode)
                {
                    HtmlNode? typeDescriptionNode = GetNextSibling(typeNameNode);
                    if (typeDescriptionNode == null)
                        break;

                    var botApiType = new BotApiType(
                        typeNameNode.GetDirectInnerText(),
                        ConstructDescription(typeDescriptionNode),
                        sectionName,
                        new List<BotApiParameter>(),
                        typeNameNode.FirstChild.GetAttributeValue("name", ""));
                    try
                    {
                        MapParametersForType(botApiType, typeDescriptionNode);

                        Types.Add(botApiType);
                    }
                    catch (WrongTypeSignatureException)
                    {
                        var botApiMethod = new BotApiMethod(
                            botApiType.TypeName,
                            botApiType.TypeDescription,
                            sectionName,
                            new List<BotApiParameter>(),
                            typeNameNode.FirstChild.GetAttributeValue("name", ""));

                        MapParametersForMethod(botApiMethod, typeDescriptionNode);

                        Methods.Add(botApiMethod);
                    }
                    catch (NullReferenceException)
                    {
                    }
                }

                siblingNode = GetNextSibling(siblingNode);
            }
        }
    }

    public async Task ParseEnumsAsync()
    {
        await using FileStream file = File.OpenRead("enums.json");
        _enums = await JsonSerializer.DeserializeAsync<EnumsModel>(file);

        // Trigger enums lazy mapping
        _ = _enums!.Enums;
    }

    private void MapParametersForType(BotApiType apiType, HtmlNode typeDescriptionNode)
    {
        if (_enums == null)
            throw new ArgumentException("Enums object is null. Consider calling the ParseEnumsAsync method.");

        HtmlNode? parametersTableNode = GetNextSibling(typeDescriptionNode);

        // Throwing NullReferenceException is okay here, we suppress the warning
        foreach (HtmlNode tr in parametersTableNode!.SelectSingleNode("./tbody").ChildNodes.Where(n => n.InnerText != "\n"))
        {
            var tds = tr.ChildNodes.Where(n => n.InnerText != "\n").ToArray();

            if (tds.Length != 3)
            {
                // This entry is not a type, it's most likely a method description
                throw new WrongTypeSignatureException();
            }

            HtmlNode parameterName = tds[0];
            HtmlNode parameterType = tds[1];
            HtmlNode parameterDescription = tds[2];

            var botApiTypeParameter = new BotApiParameter(
                parameterName.InnerText,
                GetDotNetTypeName(parameterType.InnerText),
                ConstructDescription(parameterDescription),
                !parameterDescription.InnerText.StartsWith("Optional"));

            _enums.TryMapEnum(ref botApiTypeParameter, apiType);

            apiType.Parameters.Add(botApiTypeParameter);
        }
    }

    private void MapParametersForMethod(BotApiMethod apiMethod, HtmlNode typeDescriptionNode)
    {
        if (_enums == null)
            throw new ArgumentException("Enums object is null. Consider calling the ParseEnumsAsync method.");

        HtmlNode? parametersTableNode = GetNextSibling(typeDescriptionNode);

        // Throwing NullReferenceException is okay here, we suppress the warning
        foreach (HtmlNode tr in parametersTableNode!.SelectSingleNode("./tbody").ChildNodes.Where(n => n.InnerText != "\n"))
        {
            var tds = tr.ChildNodes.Where(n => n.InnerText != "\n").ToArray();

            if (tds.Length != 4)
                return;

            HtmlNode parameterName = tds[0];
            HtmlNode parameterType = tds[1];
            HtmlNode parameterRequired = tds[2];
            HtmlNode parameterDescription = tds[3];

            var botApiMethodParameter = new BotApiParameter(
                parameterName.InnerText,
                GetDotNetTypeName(parameterType.InnerText),
                ConstructDescription(parameterDescription),
                parameterRequired.InnerText == "Yes");

            _enums.TryMapEnum(ref botApiMethodParameter, apiMethod);

            apiMethod.Parameters.Add(botApiMethodParameter);
        }
    }

    private static string GetDotNetTypeName(string parameterTypeName)
    {
        return parameterTypeName switch
        {
            "String" => "string",
            "Integer" => "int",
            "Boolean" or "True" => "bool",
            "Float" or "Float number" => "float",
            "Integer or String" => "Telegram.Bot.Types.ChatId",
            "InputFile or String" => GetDotNetTypeName("InputMedia"),
            "InlineKeyboardMarkup or ReplyKeyboardMarkup or ReplyKeyboardRemove or ForceReply" => GetDotNetTypeName("IReplyMarkup"),
            { } value when value.StartsWith("Array of") => $"System.Collections.Generic.List<{GetDotNetTypeName(value.Split(' ', 3)[2])}>",
            { } value => $"Telegram.Bot.Types.{value}",
            _ => throw new NotSupportedException()
        };
    }

    private static HtmlNode? GetNextSibling(HtmlNode node)
    {
        HtmlNode? sibling = node.NextSibling;
        while (sibling is { OuterHtml: "\n" })
        {
            sibling = sibling.NextSibling;
        }

        return sibling;
    }

    private static BotApiDescription ConstructDescription(HtmlNode descriptionNode)
    {
        static IEnumerable<DescriptionEntity> MapEntities(HtmlNodeCollection childNodes)
        {
            foreach (HtmlNode childNode in childNodes.Where(n => n.Name == "a"))
            {
                string targetHref = childNode.GetAttributeValue("href", string.Empty);
                if (string.IsNullOrEmpty(targetHref))
                    continue;

                yield return new DescriptionEntity(
                    EntityText: childNode.InnerText,
                    EntityValue: targetHref,
                    EntityKind: null,
                    EntityKindFactory: GetEntityKindFactory(targetHref));
            }
        }

        var entities = MapEntities(descriptionNode.ChildNodes).ToList();
        string decoded = HttpUtility.HtmlDecode(descriptionNode.InnerText);
        string withNewLines = Regex.Replace(decoded, "\\.([A-Z0-9])", ".\n$1");

        return new BotApiDescription(
            DescriptionText: withNewLines,
            Entities: entities);
    }

    private static Func<TelegramBotDocParser, DescriptionEntityKind> GetEntityKindFactory(string entityText)
    {
        return parser =>
        {
            if (entityText.StartsWith("http"))
                return DescriptionEntityKind.Url;

            if (entityText.Contains("forwardmessage"))
            {
                bool a = true;
            }

            ReadOnlySpan<char> matchName = entityText.AsSpan(1);

            foreach (BotApiType apiType in parser.Types)
                if (matchName.Equals(apiType.SiteIdentifier, StringComparison.OrdinalIgnoreCase))
                    return DescriptionEntityKind.Type;

            foreach (BotApiMethod apiMethod in parser.Methods)
                if (matchName.Equals(apiMethod.SiteIdentifier, StringComparison.OrdinalIgnoreCase))
                    return DescriptionEntityKind.Method;

            return DescriptionEntityKind.Url;
        };
    }
}
