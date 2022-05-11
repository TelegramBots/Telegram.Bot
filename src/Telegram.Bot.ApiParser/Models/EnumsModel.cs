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

    internal bool TryMapEnum(ref BotApiTypeParameter apiTypeParameter)
    {
        if (apiTypeParameter.ParameterTypeName is not "string")
            return false;

        var expectedMappingName = $"{apiTypeParameter.Parent.TypeName}.{apiTypeParameter.ParameterName}";

        foreach ((string enumName, EnumModel enumModel) in Enums)
        {
            if (enumModel.MapTo.Contains(expectedMappingName))
            {
                apiTypeParameter = apiTypeParameter with
                {
                    ParameterTypeName = enumName,
                    IsEnum = true
                };
                return true;
            }
        }

        return false;
    }

    internal bool TryMapEnum(ref BotApiMethodParameter apiMethodParameter)
    {
        if (apiMethodParameter.ParameterTypeName is not "string")
            return false;

        var expectedMappingName = $"{apiMethodParameter.Parent.MethodName}.{apiMethodParameter.ParameterName}";

        foreach ((string enumName, EnumModel enumModel) in Enums)
        {
            if (enumModel.MapTo.Contains(expectedMappingName))
            {
                apiMethodParameter = apiMethodParameter with
                {
                    ParameterTypeName = enumName,
                    IsEnum = true
                };
                return true;
            }
        }

        return false;
    }
}
