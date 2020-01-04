using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Dahomey.Json;
using Dahomey.Json.NamingPolicies;
using Telegram.Bot.Types;

#if NETSTANDARD2_0
using Telegram.Bot.Json.Helpers;
#endif

namespace Telegram.Bot.Json
{
    public sealed class SystemTextJsonConverter : ITelegramBotJsonConverter
    {
        private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
            Converters =
            {
                new JsonStringEnumConverter(new SnakeCaseNamingPolicy(), false)
            }
        }.SetupExtensions();

        public ValueTask<Update> DeserializeAsync(Stream jsonStream, CancellationToken ct)
        {
            return JsonSerializer.DeserializeAsync<Update>(jsonStream, _serializerOptions, ct);
        }

        public ValueTask SerializeAsync(Stream outputStream, object inputModel, Type inputType, CancellationToken ct)
        {
            return new ValueTask(JsonSerializer.SerializeAsync(outputStream, inputModel, inputType, _serializerOptions, ct));
        }

        public ValueTask<IEnumerable<KeyValuePair<string, HttpContent>>> ToNodesAsync(object value, string[] propertyNamesToExcept, CancellationToken ct)
        {
            // waiting for non-generic version of FromObject method
            // https://github.com/dahomey-technologies/Dahomey.Json/pull/18

            var jsonObject = JsonObject.FromObject(value, _serializerOptions);
            var result = new Dictionary<string, HttpContent>();

            foreach (var (name, node) in jsonObject)
            {
                if (propertyNamesToExcept.Contains(name))
                    continue;

                result.Add(name, new StringContent(node.ToString()));
            }

            return new ValueTask<IEnumerable<KeyValuePair<string, HttpContent>>>(result);
        }
    }
}
