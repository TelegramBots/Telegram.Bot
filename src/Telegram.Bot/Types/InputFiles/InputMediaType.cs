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
        // file_id or http(s) url
        public InputMediaType(string value)
        {
            bool isUrl = Uri.TryCreate(value, UriKind.Absolute, out Uri _);
            if (isUrl)
            {
                Url = value;
            }
            else
            {
                FileId = value;
            }
        }

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
