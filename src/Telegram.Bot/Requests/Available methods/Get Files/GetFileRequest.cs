namespace Telegram.Bot.Requests;

/// <summary>Use this method to get basic information about a file and prepare it for downloading. For the moment, bots can download files of up to 20MB in size.<para>Returns: A <see cref="File"/> object is returned. The file can then be downloaded via <see cref="TelegramBotClient.DownloadFileAsync"/>, where <c>&lt;FilePath&gt;</c> is taken from the response. It is guaranteed that the link will be valid for at least 1 hour. When the link expires, a new one can be requested by calling <see cref="TelegramBotClientExtensions.GetFileAsync">GetFile</see> again.<br/><b>Note:</b> This function may not preserve the original file name and MIME type. You should save the file's MIME type and name (if available) when the File object is received.</para></summary>
public partial class GetFileRequest : RequestBase<File>
{
    /// <summary>File identifier to get information about</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string FileId { get; set; }

    /// <summary>Initializes an instance of <see cref="GetFileRequest"/></summary>
    /// <param name="fileId">File identifier to get information about</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public GetFileRequest(string fileId) : this() => FileId = fileId;

    /// <summary>Instantiates a new <see cref="GetFileRequest"/></summary>
    public GetFileRequest() : base("getFile") { }
}
