using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using Telegram.Bot.Gen.ApiParser.Exceptions;
using Telegram.Bot.Gen.ApiParser.Models;
using Telegram.Bot.Gen.Shared;
using Telegram.Bot.Gen.Shared.Enums;

namespace Telegram.Bot.Gen.ApiParser;

public sealed class TelegramBotDocParser
{
    private const string BotApiUrl = "https://core.telegram.org/bots/api";

    private string? _html;
    private EnumsModel? _enums;
    private readonly Queue<BotApiMethodInternal> _delayedMethodData = new();

    public List<BotApiTypeInternal> Types { get; } = new();
    public List<BotApiMethodInternal> Methods { get; } = new();
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

                    var botApiType = new BotApiTypeInternal
                    {
                        TypeName = typeNameNode.GetDirectInnerText(),
                        TypeDescription = ConstructDescription(typeDescriptionNode),
                        TypeGroup = sectionName,
                        Parameters = new List<BotApiParameter>(),
                        IsCompositeType = false,
                        SiteIdentifier = typeNameNode.FirstChild.GetAttributeValue("name", "")
                    };

                    try
                    {
                        MapParametersForType(botApiType, typeDescriptionNode);

                        Types.Add(botApiType);
                    }
                    catch (WrongTypeSignatureException)
                    {
                        var botApiMethod = new BotApiMethodInternal
                        {
                            MethodName = botApiType.TypeName,
                            MethodDescription = botApiType.TypeDescription,
                            MethodGroup = sectionName,
                            Parameters = new List<BotApiParameter>(),
                            MethodReturnType = string.Empty,
                            SiteIdentifier = botApiType.SiteIdentifier
                        };

                        MapParametersForMethod(botApiMethod, typeDescriptionNode);

                        _delayedMethodData.Enqueue(botApiMethod);
                    }
                    catch (NullReferenceException)
                    {
                    }
                }

                siblingNode = GetNextSibling(siblingNode);
            }
        }

        while (_delayedMethodData.TryDequeue(out BotApiMethodInternal? botApiMethod))
        {
            try
            {
                botApiMethod.MethodReturnType = GetMethodReturnType(botApiMethod.MethodDescription);
                Methods.Add(botApiMethod);
            }
            catch (NullReferenceException)
            {
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

    private void MapParametersForType(BotApiTypeInternal apiType, HtmlNode typeDescriptionNode)
    {
        if (_enums == null)
            throw new ArgumentException("Enums object is null. Consider calling the ParseEnumsAsync method.");

        HtmlNode? parametersTableNode = GetNextSibling(typeDescriptionNode);

        if (parametersTableNode is { Name: "ul" })
        {
            // This is a composite type such as 'ChatMember'
            apiType.IsCompositeType = true;
            return;
        }

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

            var botApiTypeParameter = new BotApiParameter
            {
                ParameterName = parameterName.InnerText,
                ParameterTypeName = GetDotNetTypeName(parameterType.InnerText),
                ParameterDescription = ConstructDescription(parameterDescription),
                IsRequired = !parameterDescription.InnerText.StartsWith("Optional")
            };

            _enums.TryMapEnum(botApiTypeParameter, apiType);

            apiType.Parameters.Add(botApiTypeParameter);
        }
    }

    private void MapParametersForMethod(BotApiMethodInternal apiMethod, HtmlNode typeDescriptionNode)
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

            var botApiMethodParameter = new BotApiParameter
            {
                ParameterName = parameterName.InnerText,
                ParameterTypeName = GetDotNetTypeName(parameterType.InnerText),
                ParameterDescription = ConstructDescription(parameterDescription),
                IsRequired = parameterRequired.InnerText == "Yes"
            };

            _enums.TryMapEnum(botApiMethodParameter, apiMethod);

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
        while (sibling is { OuterHtml: "\n" } or { Name: "blockquote" })
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

                yield return new DescriptionEntityInternal
                {
                    EntityText = childNode.InnerText,
                    EntityValue = targetHref,
                    EntityKindFactory = GetEntityKindFactory(targetHref)
                };
            }
        }

        var entities = MapEntities(descriptionNode.ChildNodes).ToList();
        string decoded = HttpUtility.HtmlDecode(descriptionNode.InnerText);
        string withNewLines = Regex.Replace(decoded, "\\.([A-Z0-9])", ".\n$1");

        return new BotApiDescription
        {
            DescriptionText = withNewLines,
            Entities = entities
        };
    }

    private static Func<TelegramBotDocParser, DescriptionEntityKind> GetEntityKindFactory(string entityText)
    {
        return parser =>
        {
            if (entityText.StartsWith("http"))
                return DescriptionEntityKind.Url;

            ReadOnlySpan<char> matchName = entityText.AsSpan(1);

            foreach (BotApiTypeInternal apiType in parser.Types)
                if (matchName.Equals(apiType.SiteIdentifier, StringComparison.OrdinalIgnoreCase))
                    return DescriptionEntityKind.Type;

            foreach (BotApiMethodInternal apiMethod in parser.Methods)
                if (matchName.Equals(apiMethod.SiteIdentifier, StringComparison.OrdinalIgnoreCase))
                    return DescriptionEntityKind.Method;

            return DescriptionEntityKind.Url;
        };
    }

    private string GetMethodReturnType(BotApiDescription description)
    {
        string[] sentences = description.DescriptionText.Split('.');

        foreach (string sentence in sentences)
        {
            if (!sentence.Contains("returns", StringComparison.OrdinalIgnoreCase) &&
                !sentence.Contains("is returned", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (sentence.Contains("True"))
                return "bool";

            bool TryGetTypeFromEntity([MaybeNullWhen(false)] out string dotnetType, string? entityText = null)
            {
                DescriptionEntity? entity;
                if (entityText == null)
                {
                    entity = description.Entities.FirstOrDefault(e =>
                        char.IsUpper(e.EntityText.AsSpan()[0]) && sentence.Contains(e.EntityText));
                }
                else
                {
                    entity = description.Entities.FirstOrDefault(e =>
                        e.EntityText == entityText);
                }

                if (entity == null)
                {
                    dotnetType = null;
                    return false;
                }

                dotnetType = Types.First(t => t.SiteIdentifier == entity.EntityValue[1..]).TypeName;
                return true;
            }

            int arrayOfIndex = sentence.IndexOf("array of", StringComparison.OrdinalIgnoreCase);
            if (arrayOfIndex != -1)
            {
                string substring = sentence[(arrayOfIndex + 9)..];
                substring = substring[..substring.IndexOf(' ')];

                return GetDotNetTypeName(TryGetTypeFromEntity(out string? dotnetType, entityText: substring)
                    ? $"Array of {dotnetType}"
                    : $"Array of {substring}");
            }

            {
                if (TryGetTypeFromEntity(out string? dotnetType))
                {
                    return GetDotNetTypeName(dotnetType);
                }
            }
        }

        return "bool";
    }
}
