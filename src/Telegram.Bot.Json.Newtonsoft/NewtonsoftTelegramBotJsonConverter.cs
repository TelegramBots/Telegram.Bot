using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Json.Converters;

namespace Telegram.Bot.Json
{
    public sealed class NewtonsoftTelegramBotJsonConverter : ITelegramBotJsonConverter
    {
        private readonly JsonSerializer _jsonSerializer = new JsonSerializer
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            },
            Converters =
            {
                new ChatIdConverter(),
                new InputFileConverter(),
                new InputMediaConverter(),
                /*new MessageEntityTypeConverter(),*/
                new StringEnumConverter(new SnakeCaseNamingStrategy())
            }
        };

        public NewtonsoftTelegramBotJsonConverter(JsonSerializer baseSerializer = default)
        {
            if (baseSerializer == default)
                return;

            _jsonSerializer.Context = baseSerializer.Context;
            _jsonSerializer.Culture = baseSerializer.Culture;
            _jsonSerializer.Formatting = baseSerializer.Formatting;
            _jsonSerializer.ConstructorHandling = baseSerializer.ConstructorHandling;
            _jsonSerializer.EqualityComparer = baseSerializer.EqualityComparer;
            _jsonSerializer.MaxDepth = baseSerializer.MaxDepth;
            _jsonSerializer.ReferenceResolver = baseSerializer.ReferenceResolver;
            _jsonSerializer.SerializationBinder = baseSerializer.SerializationBinder;
            _jsonSerializer.TraceWriter = baseSerializer.TraceWriter;
            _jsonSerializer.CheckAdditionalContent = baseSerializer.CheckAdditionalContent;
            _jsonSerializer.DateFormatHandling = baseSerializer.DateFormatHandling;
            _jsonSerializer.DateFormatString = baseSerializer.DateFormatString;
            _jsonSerializer.DateParseHandling = baseSerializer.DateParseHandling;
            _jsonSerializer.DefaultValueHandling = baseSerializer.DefaultValueHandling;
            _jsonSerializer.FloatFormatHandling = baseSerializer.FloatFormatHandling;
            _jsonSerializer.FloatParseHandling = baseSerializer.FloatParseHandling;
            _jsonSerializer.MetadataPropertyHandling = baseSerializer.MetadataPropertyHandling;
            _jsonSerializer.MissingMemberHandling = baseSerializer.MissingMemberHandling;
            _jsonSerializer.NullValueHandling = baseSerializer.NullValueHandling;
            _jsonSerializer.ObjectCreationHandling = baseSerializer.ObjectCreationHandling;
            _jsonSerializer.PreserveReferencesHandling = baseSerializer.PreserveReferencesHandling;
            _jsonSerializer.ReferenceLoopHandling = baseSerializer.ReferenceLoopHandling;
            _jsonSerializer.StringEscapeHandling = baseSerializer.StringEscapeHandling;
            _jsonSerializer.TypeNameHandling = baseSerializer.TypeNameHandling;
            _jsonSerializer.DateTimeZoneHandling = baseSerializer.DateTimeZoneHandling;
            _jsonSerializer.TypeNameAssemblyFormatHandling = baseSerializer.TypeNameAssemblyFormatHandling;

            foreach (var jsonSerializerConverter in baseSerializer.Converters)
            {
                _jsonSerializer.Converters.Add(jsonSerializerConverter);
            }
        }

        public ValueTask SerializeAsync(Stream outputStream, object inputModel, Type inputType, CancellationToken ct)
        {
            using (var sw = new StreamWriter(outputStream))
            using (var jsonTextWriter = new JsonTextWriter(sw))
            {
                _jsonSerializer.Serialize(jsonTextWriter, inputModel, inputType);
            }

            return default;
        }

        public ValueTask<IEnumerable<KeyValuePair<string, HttpContent>>> ToNodesAsync(
            object value, Type valueType, string[] propertyNamesToExcept, CancellationToken ct)
        {
            var stringContents = JObject.FromObject(value)
                .Properties()
                .Where(prop => propertyNamesToExcept?.Contains(prop.Name) == false)
                .Select(prop => new KeyValuePair<string, HttpContent>(prop.Name, new StringContent(prop.Value.ToString())));

            return new ValueTask<IEnumerable<KeyValuePair<string, HttpContent>>>(stringContents);
        }

        public ValueTask<TOutput> DeserializeAsync<TOutput>(Stream jsonStream, CancellationToken ct)
        {
            using (var sr = new StreamReader(jsonStream))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                return new ValueTask<TOutput>(_jsonSerializer.Deserialize<TOutput>(jsonTextReader));
            }
        }
    }
}
