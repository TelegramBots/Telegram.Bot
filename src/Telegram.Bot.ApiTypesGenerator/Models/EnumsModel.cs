using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Telegram.Bot.ApiTypesGenerator.Models;

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
}
