// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to get basic info about a file and prepare it for downloading. For the moment,
/// bots can download files of up to 20MB in size. On success, a <see cref="File"/> object is
/// returned. The file can then be downloaded via the link
/// <c>https://api.telegram.org/file/bot&lt;token&gt;/&lt;file_path&gt;</c>, where
/// <c>&lt;file_path&gt;</c> is taken from the response. It is guaranteed that the link will be valid
/// for at least 1 hour. When the link expires, a new one can be requested by calling
/// <see cref="GetFileRequest"/> again.
/// </summary>
/// <remarks>
/// You can use <see cref="ITelegramBotClient.DownloadFileAsync"/> or
/// <see cref="TelegramBotClientExtensions.GetInfoAndDownloadFileAsync"/> methods to download the file
/// </remarks>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class GetFileRequest : RequestBase<File>
{
    /// <summary>
    /// File identifier to get info about
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string FileId { get; }

    /// <summary>
    /// Initializes a new request with <see cref="FileId"/>
    /// </summary>
    /// <param name="fileId">File identifier to get info about</param>
    public GetFileRequest(string fileId)
        : base("getFile")
    {
        FileId = fileId;
    }
}
