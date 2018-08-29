// ReSharper disable once CheckNamespace

namespace Telegram.Bot.Passport.Request
{
    /// <summary>
    /// A marker interface for object represents a requested element
    /// </summary>
    public interface IPassportScopeElement
    {
        /// <summary>
        /// Optional. Use this parameter if you want to request a selfie with the document.
        /// </summary>
        bool? Selfie { get; }

        /// <summary>
        /// Optional. Use this parameter if you want to request a translation of the document.
        /// </summary>
        bool? Translation { get; }
    }
}
