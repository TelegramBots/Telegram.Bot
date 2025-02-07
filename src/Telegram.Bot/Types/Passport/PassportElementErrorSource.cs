// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Passport;

/// <summary>Error source</summary>
[JsonConverter(typeof(EnumConverter<PassportElementErrorSource>))]
public enum PassportElementErrorSource
{
    /// <summary>Represents an issue in one of the data fields that was provided by the user. The error is considered resolved when the field's value changes.<br/><br/><i>(<see cref="PassportElementError"/> can be cast into <see cref="PassportElementErrorDataField"/>)</i></summary>
    Data = 1,
    /// <summary>Represents an issue with the front side of a document. The error is considered resolved when the file with the front side of the document changes.<br/><br/><i>(<see cref="PassportElementError"/> can be cast into <see cref="PassportElementErrorFrontSide"/>)</i></summary>
    FrontSide,
    /// <summary>Represents an issue with the reverse side of a document. The error is considered resolved when the file with reverse side of the document changes.<br/><br/><i>(<see cref="PassportElementError"/> can be cast into <see cref="PassportElementErrorReverseSide"/>)</i></summary>
    ReverseSide,
    /// <summary>Represents an issue with the selfie with a document. The error is considered resolved when the file with the selfie changes.<br/><br/><i>(<see cref="PassportElementError"/> can be cast into <see cref="PassportElementErrorSelfie"/>)</i></summary>
    Selfie,
    /// <summary>Represents an issue with a document scan. The error is considered resolved when the file with the document scan changes.<br/><br/><i>(<see cref="PassportElementError"/> can be cast into <see cref="PassportElementErrorFile"/>)</i></summary>
    File,
    /// <summary>Represents an issue with a list of scans. The error is considered resolved when the list of files containing the scans changes.<br/><br/><i>(<see cref="PassportElementError"/> can be cast into <see cref="PassportElementErrorFiles"/>)</i></summary>
    Files,
    /// <summary>Represents an issue with one of the files that constitute the translation of a document. The error is considered resolved when the file changes.<br/><br/><i>(<see cref="PassportElementError"/> can be cast into <see cref="PassportElementErrorTranslationFile"/>)</i></summary>
    TranslationFile,
    /// <summary>Represents an issue with the translated version of a document. The error is considered resolved when a file with the document translation change.<br/><br/><i>(<see cref="PassportElementError"/> can be cast into <see cref="PassportElementErrorTranslationFiles"/>)</i></summary>
    TranslationFiles,
    /// <summary>Represents an issue in an unspecified place. The error is considered resolved when new data is added.<br/><br/><i>(<see cref="PassportElementError"/> can be cast into <see cref="PassportElementErrorUnspecified"/>)</i></summary>
    Unspecified,
}
