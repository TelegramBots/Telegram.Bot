using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Requests;
using Telegram.Bot.Types.Passport;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot
{
    public static class TelegramBotClientExtensions
    {
        /// <summary>
        /// Informs a user that some of the Telegram Passport elements they provided contains errors. The user will
        /// not be able to re-submit their Passport to you until the errors are fixed (the contents of the field for
        /// which you returned the error must change). Returns True on success.
        /// Use this if the data submitted by the user doesn't satisfy the standards your service requires for
        /// any reason. For example, if a birthday date seems invalid, a submitted document is blurry, a scan shows
        /// evidence of tampering, etc. Supply some details in the error message to make sure the user knows how to
        /// correct the issues.
        /// </summary>
        /// <param name="botClient">Instance of bot client</param>
        /// <param name="userId">User identifier</param>
        /// <param name="errors">A JSON-serialized array describing the errors</param>
        /// <param name="cancellationToken">Task cancellation token</param>
        /// <see href="https://core.telegram.org/bots/api#setpassportdataerrors"/>
        public static Task SetPassportDataErrorsAsync(
            this ITelegramBotClient botClient,
            int userId,
            IEnumerable<PassportElementError> errors,
            CancellationToken cancellationToken = default
        ) =>
            botClient.MakeRequestAsync(new SetPassportDataErrorsRequest(userId, errors), cancellationToken);
    }
}
