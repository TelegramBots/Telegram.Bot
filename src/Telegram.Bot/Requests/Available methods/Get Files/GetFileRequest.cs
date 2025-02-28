// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to get basic information about a file and prepare it for downloading. For the moment, bots can download files of up to 20MB in size.<para>Returns: A <see cref="TGFile"/> object is returned. The file can then be downloaded via <see cref="TelegramBotClient.DownloadFile">DownloadFile</see>, where <c>&lt;FilePath&gt;</c> is taken from the response. It is guaranteed that the link will be valid for at least 1 hour. When the link expires, a new one can be requested by calling <see cref="TelegramBotClientExtensions.GetFile">GetFile</see> again.<br/><b>Note:</b> This function may not preserve the original file name and MIME type. You should save the file's MIME type and name (if available) when the File object is received.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class GetFileRequest() : RequestBase<TGFile>("getFile")
{
    /// <summary>File identifier to get information about</summary>
    [JsonPropertyName("file_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string FileId { get; set; }
}
