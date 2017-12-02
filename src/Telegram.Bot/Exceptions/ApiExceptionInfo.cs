using System;

namespace Telegram.Bot.Exceptions
{
    // ReSharper disable once UnusedTypeParameter
    internal interface IApiExceptionInfo<out T>
        where T : ApiRequestException
    {
        int ErrorCode { get; }

        string ErrorMessage { get; }

        Type Type { get; }
    }

    internal class BadRequestExceptionInfo<T> : IApiExceptionInfo<T>
        where T : ApiRequestException
    {
        public int ErrorCode => BadRequestException.BadRequestErrorCode;

        public string ErrorMessage { get; }

        public Type Type => typeof(T);

        public BadRequestExceptionInfo(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }

    internal class ForbiddenExceptionInfo<T> : IApiExceptionInfo<T>
        where T : ApiRequestException
    {
        public int ErrorCode => ForbiddenException.ForbiddenErrorCode;

        public string ErrorMessage { get; }

        public Type Type => typeof(T);

        public ForbiddenExceptionInfo(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
