using System.Text;

namespace Telegram.Bot.Requests;

/// <summary>Represents an API request with a file</summary>
/// <typeparam name="TResponse">Type of result expected in result</typeparam>
/// <param name="methodName">Bot API method</param>
public abstract class FileRequestBase<TResponse>(string methodName) : RequestBase<TResponse>(methodName)
{
    private static readonly Encoding Latin1 = Encoding.GetEncoding(28591);

    /// <inheritdoc/>
    public override HttpContent? ToHttpContent()
    {
        InputFileConverter.Attachments.Value = null;
        var utf8Json = JsonSerializer.SerializeToUtf8Bytes(this, GetType(), JsonBotAPI.Options);
        var attachments = InputFileConverter.Attachments.Value;
        if (attachments == null)
            return new ByteArrayContent(utf8Json) { Headers = { ContentType = new("application/json") { CharSet = "utf-8" } } };

        var multipartContent = new MultipartFormDataContent();
        var firstLevel = JsonDocument.Parse(utf8Json).RootElement.EnumerateObject();
        foreach (var prop in firstLevel)
            multipartContent.Add(new StringContent(prop.Value.ToString()), prop.Name);
        for (int i = 0; i < attachments.Count; i++)
        {
            var inputFile = attachments[i];
            string fileName = inputFile.FileName ?? "file";
            string contentDisposition = FormattableString.Invariant($"form-data; name=\"{i}\"; filename=\"{fileName}\"");
            contentDisposition = Latin1.GetString(Encoding.UTF8.GetBytes(contentDisposition));
#pragma warning disable CA2000
            var mediaPartContent = new StreamContent(inputFile.Content)
            {
                Headers =
                {
                    {"Content-Type", "application/octet-stream"},
                    {"Content-Disposition", contentDisposition},
                },
            };
#pragma warning restore CA2000
            multipartContent.Add(mediaPartContent);
        }
        return multipartContent;
    }
}
