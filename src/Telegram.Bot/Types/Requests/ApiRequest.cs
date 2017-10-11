using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using Telegram.Bot.Converters;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to be sent to telegrams bot API
    /// </summary>
    public class ApiRequest : IApiRequest
    {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter>
            {
                new ChatIdConverter(),
                new FileToSendConverter(),
                new InlineQueryResultTypeConverter(),
                new ParseModeConverter(),
                new PhotoSizeConverter(),
                new UnixDateTimeConverter(),
            },
        };

        /// <summary>
        /// The encoding that should be used for this request
        /// </summary>
        public RequestEncoding Encoding => FileStream == null ? RequestEncoding.Json : RequestEncoding.Multipart;
        /// <summary>
        /// The method of this request
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// The parameters of this object
        /// </summary>
        public Dictionary<string, object> Parameters { get; set; }
        /// <summary>
        /// The file to be sent. Only present if the File is sent as a stream.
        /// </summary>
        public Stream FileStream { get; set; }
        /// <summary>
        /// The name of the file. Only present if <see cref="FileStream"/> is present.
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// The parameter name of the file. Only present if <see cref="FileStream"/> is present.
        /// </summary>
        public string FileParameterName { get; set; }
        /// <summary>
        /// Initializes a new object of the <see cref="ApiRequest"/> class
        /// </summary>
        /// <param name="method">The method name of this request.</param>
        /// <param name="parameters">The parameters for this request, if any</param>
        public ApiRequest(string method, Dictionary<string, object> parameters = null)
        {
            Method = method;
            Parameters = parameters ?? new Dictionary<string, object>();
        }
        /// <summary>
        /// Returns this request as a HttpResponseMessage for a webhook. Not recommended if you are expecting a response, because you won't get any by this.
        /// </summary>
        /// <returns><see cref="HttpResponseMessage"/></returns>
        public HttpResponseMessage AsWebhookResponse()
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            switch (Encoding)
            {
                case RequestEncoding.Multipart:
                    resp.Headers.Add("Content-Type", "multipart/form-data");
                    var form = new MultipartFormDataContent();
                    var fContent = new StreamContent(FileStream);
                    fContent.Headers.Add("Content-Type", "application/octet-stream");
                    string headerValue = $"form-data; name=\"{FileParameterName}\"; filename=\"{FileName}\"";
                    byte[] bytes = System.Text.Encoding.UTF8.GetBytes(headerValue);
                    headerValue = string.Join("", bytes.Select(b => (char)b));
                    fContent.Headers.Add("Content-Disposition", headerValue);
                    form.Add(fContent, FileParameterName, FileName);

                    foreach(var parameter in Parameters)
                    {
                        switch (parameter.Value)
                        {
                            case string str:
                                form.Add(new StringContent(str), parameter.Key);
                                break;
                            default:
                                form.Add(new StringContent(JsonConvert.SerializeObject(parameter.Value, SerializerSettings)), parameter.Key);
                                break;
                        }
                    }
                    resp.Content = form;
                    break;
                case RequestEncoding.Json:
                    var param = new Dictionary<string, object>();
                    foreach (var p in Parameters) param.Add(p.Key, p.Value);
                    param.Add("method", Method);
                    resp.Headers.Add("Content-Type", "application/json");
                    resp.Content = new StringContent(JsonConvert.SerializeObject(param, SerializerSettings));
                    break;
            }
            return resp;
        }
    }

    /// <summary>
    /// The encoding that should be used for this request
    /// </summary>
    public enum RequestEncoding
    {
        /// <summary>
        /// application/json encoding
        /// </summary>
        Json,
        /// <summary>
        /// multipart/form-data encoding
        /// </summary>
        Multipart
    }
}
