using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Telegram.Bot.ApiParser.Models;

public sealed class EnumsModel
{
    private IReadOnlyDictionary<string, EnumModel>? _enumsMapped;

    [JsonExtensionData]
    public IDictionary<string, JsonElement> EnumsUnmapped { get; set; }

    internal IReadOnlyDictionary<string, EnumModel> Enums
    {
        get
        {
            return _enumsMapped ??=
                EnumsUnmapped.ToImmutableDictionary(e => e.Key, e => e.Value.Deserialize<EnumModel>()!);
        }
    }

    internal bool TryMapEnum(ref BotApiParameter apiTypeParameter, BotApiType type)
    {
        if (apiTypeParameter.ParameterTypeName is not "string")
            return false;

        var expectedMappingName = $"{type.TypeName}.{apiTypeParameter.ParameterName}";

        foreach ((string enumName, EnumModel enumModel) in Enums)
        {
            if (enumModel.MapTo.Contains(expectedMappingName))
            {
                apiTypeParameter = apiTypeParameter with
                {
                    ParameterTypeName = "Telegram.Bot.Types.Enums." + enumName
                };
                return true;
            }
        }

        return false;
    }

    internal bool TryMapEnum(ref BotApiParameter apiMethodParameter, BotApiMethod method)
    {
        if (apiMethodParameter.ParameterTypeName is not "string")
            return false;

        var expectedMappingName = $"{method.MethodName}.{apiMethodParameter.ParameterName}";

        foreach ((string enumName, EnumModel enumModel) in Enums)
        {
            if (enumModel.MapTo.Contains(expectedMappingName))
            {
                apiMethodParameter = apiMethodParameter with
                {
                    ParameterTypeName = "Telegram.Bot.Types.Enums." + enumName
                };
                return true;
            }
        }

        return false;
    }
}
