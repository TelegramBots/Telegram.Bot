using System;
using System.Collections.Generic;
using System.IO;
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

        public async ValueTask SerializeAsync(Stream outputStream, object inputModel, Type inputType, CancellationToken ct)
        {
            await JsonSerializer.SerializeAsync(outputStream, inputModel, inputType, _serializerOptions, ct);
        }

        public ValueTask<IEnumerable<KeyValuePair<string, HttpContent>>> ToNodesAsync(object value, string[] propertyNamesToExcept, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
