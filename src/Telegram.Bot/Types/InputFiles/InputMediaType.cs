using System;
using System.IO;
using Newtonsoft.Json;
using Telegram.Bot.Converters;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types
{
    [JsonConverter(typeof(InputMediaTypeConverter))]
    public class InputMediaType : InputFileBase
    {
        public InputMediaType(string fileName, Stream content)
        {
            FileName = fileName;
            Content = content;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
