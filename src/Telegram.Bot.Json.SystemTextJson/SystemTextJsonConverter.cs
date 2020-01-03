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
using Telegram.Bot.Json.Helpers;
using Telegram.Bot.Types;

namespace Telegram.Bot.Json
{
    public sealed class SystemTextJsonConverter : ITelegramBotJsonConverter
    {
        private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy(),
            Converters =
            {
                new JsonStringEnumConverter(new JsonSnakeCaseNamingPolicy(), false)
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

        public async ValueTask<IEnumerable<KeyValuePair<string, HttpContent>>> ToNodesAsync(object value, string[] propertyNamesToExcept, CancellationToken ct)
        {
            // TODO temporary workaround
            // https://github.com/dahomey-technologies/Dahomey.Json/issues/14

            JsonObject jsonObject;
            using (var jsonStream = new MemoryStream())
            {
                await JsonSerializer.SerializeAsync(jsonStream, value, value.GetType(), _serializerOptions, ct);
                jsonStream.Position = 0L;

                jsonObject = await JsonSerializer.DeserializeAsync<JsonObject>(jsonStream, _serializerOptions, ct);
            }

            var result = new Dictionary<string, HttpContent>();

            foreach (var (name, node) in jsonObject)
            {
                if (propertyNamesToExcept.Contains(name))
                    continue;

                result.Add(name, new StringContent(node.ToString()));
            }

            return result;
        }
    }
}
