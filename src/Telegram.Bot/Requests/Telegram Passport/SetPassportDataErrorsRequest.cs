namespace Telegram.Bot.Requests;

/// <summary>Informs a user that some of the Telegram Passport elements they provided contains errors. The user will not be able to re-submit their Passport to you until the errors are fixed (the contents of the field for which you returned the error must change).<br/>Use this if the data submitted by the user doesn't satisfy the standards your service requires for any reason. For example, if a birthday date seems invalid, a submitted document is blurry, a scan shows evidence of tampering, etc. Supply some details in the error message to make sure the user knows how to correct the issues.<para>Returns: </para></summary>
public partial class SetPassportDataErrorsRequest : RequestBase<bool>, IUserTargetable
{
    /// <summary>User identifier</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>A array describing the errors</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required IEnumerable<PassportElementError> Errors { get; set; }

    /// <summary>Initializes an instance of <see cref="SetPassportDataErrorsRequest"/></summary>
    /// <param name="userId">User identifier</param>
    /// <param name="errors">A array describing the errors</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public SetPassportDataErrorsRequest(long userId, IEnumerable<PassportElementError> errors) : this()
    {
        UserId = userId;
        Errors = errors;
    }

    /// <summary>Instantiates a new <see cref="SetPassportDataErrorsRequest"/></summary>
    public SetPassportDataErrorsRequest() : base("setPassportDataErrors") { }
}
