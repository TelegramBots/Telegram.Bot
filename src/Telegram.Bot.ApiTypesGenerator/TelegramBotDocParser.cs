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
using Telegram.Bot.ApiTypesGenerator.Exceptions;
using Telegram.Bot.ApiTypesGenerator.Models;

namespace Telegram.Bot.ApiTypesGenerator;

internal sealed class TelegramBotDocParser
{
    private const string BotApiUrl = "https://core.telegram.org/bots/api";

    private string? _html;
    private EnumsModel? _enums;
    private readonly List<BotApiType> _types = new();
    private readonly List<BotApiMethod> _methods = new();

    public IReadOnlyCollection<BotApiType> Types => _types.ToImmutableArray();
    public IReadOnlyCollection<BotApiMethod> Methods => _methods.ToImmutableArray();

    public async Task LoadBotApiPageAsync()
    {
        using var httpClient = new HttpClient();
        _html = await httpClient.GetStringAsync(BotApiUrl);
    }

    public void ParseBotApiPage()
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(_html);

        HtmlNodeCollection types = doc.DocumentNode.SelectNodes("//h4");
        foreach (HtmlNode typeNameNode in types)
        {
            HtmlNode typeDescriptionNode = GetNextSibling(typeNameNode);
            var botApiType = new BotApiType(
                typeNameNode.GetDirectInnerText(),
                DecodeDescription(typeDescriptionNode.InnerText),
                new List<BotApiTypeParameter>());
            try
            {
                MapParametersForType(botApiType, typeDescriptionNode);

                _types.Add(botApiType);
            }
            catch (WrongTypeSignatureException)
            {
                var botApiMethod = new BotApiMethod(
                    botApiType.TypeName,
                    DecodeDescription(botApiType.TypeDescription),
                    new List<BotApiMethodParameter>());

                MapParametersForMethod(botApiMethod, typeDescriptionNode);

                _methods.Add(botApiMethod);
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

    private void MapParametersForType(BotApiType apiType, HtmlNode typeDescriptionNode)
    {
        if (_enums == null)
            throw new ArgumentException("Enums object is null. Consider calling the ParseEnumsAsync method.");

        HtmlNode parametersTableNode = GetNextSibling(typeDescriptionNode);

        foreach (HtmlNode tr in parametersTableNode.SelectSingleNode("./tbody").ChildNodes.Where(n => n.InnerText != "\n"))
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

            var botApiTypeParameter = new BotApiTypeParameter(
                parameterName.InnerText,
                DecodeDescription(parameterDescription.InnerText),
                GetDotNetTypeName(parameterType.InnerText),
                false,
                apiType);

            _enums.TryMapEnum(ref botApiTypeParameter);

            apiType.Parameters.Add(botApiTypeParameter);
        }
    }

    private void MapParametersForMethod(BotApiMethod apiMethod, HtmlNode typeDescriptionNode)
    {
        if (_enums == null)
            throw new ArgumentException("Enums object is null. Consider calling the ParseEnumsAsync method.");

        HtmlNode parametersTableNode = GetNextSibling(typeDescriptionNode);

        foreach (HtmlNode tr in parametersTableNode.SelectSingleNode("./tbody").ChildNodes.Where(n => n.InnerText != "\n"))
        {
            var tds = tr.ChildNodes.Where(n => n.InnerText != "\n").ToArray();

            if (tds.Length != 4)
                return;

            HtmlNode parameterName = tds[0];
            HtmlNode parameterType = tds[1];
            HtmlNode parameterRequired = tds[2];
            HtmlNode parameterDescription = tds[3];

            var botApiMethodParameter = new BotApiMethodParameter(
                parameterName.InnerText,
                DecodeDescription(parameterDescription.InnerText),
                GetDotNetTypeName(parameterType.InnerText),
                false,
                parameterRequired.InnerText == "Yes",
                apiMethod);

            _enums.TryMapEnum(ref botApiMethodParameter);

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

    private static HtmlNode GetNextSibling(HtmlNode node)
    {
        HtmlNode sibling = node.NextSibling;
        while (sibling.OuterHtml == "\n")
        {
            sibling = sibling.NextSibling;
        }

        return sibling;
    }

    private static string DecodeDescription(string description)
    {
        string decoded = HttpUtility.HtmlDecode(description);
        string withNewLines = Regex.Replace(decoded, "\\.([A-Z0-9])", ".\n$1");
        return withNewLines;
    }
}
