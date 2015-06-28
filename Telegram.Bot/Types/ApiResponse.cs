using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ApiResponse<T>
    {
        [JsonProperty(PropertyName = "ok", Required = Required.Always)]
        public bool Ok;

        [JsonProperty(PropertyName = "result", Required = Required.Always)]
        public T ResultObject;

        [JsonProperty(PropertyName = "description", Required = Required.Default)]
        public string Message;

        [JsonProperty(PropertyName = "error_code", Required = Required.Default)]
        public int Code;
    }
}
