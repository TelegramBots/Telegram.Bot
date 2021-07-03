using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Requests;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Extensions.Polling.Tests
{
    public class MockClientOptions
    {
        public bool HandleNegativeOffset { get; set; }
        public string[] Messages { get; set; }
        public int RequestDelay { get; set; } = 10;
        public Exception ExceptionToThrow { get; set; }

    }

    public class MockTelegramBotClient : ITelegramBotClient
    {
        private readonly MockClientOptions _options;
        private readonly Queue<string[]> _messages;

        public int MessageGroupsLeft => _messages.Count;

        public MockTelegramBotClient(MockClientOptions options)
        {
            _options = options;
            _messages = new Queue<string[]>(
                options.Messages.Select(message => message.Split('-').ToArray())
            );
        }

        public async Task<TResponse> MakeRequestAsync<TResponse>(
            IRequest<TResponse> request,
            CancellationToken cancellationToken = default)
        {
            if (request is GetUpdatesRequest getUpdatesRequest)
            {
                await Task.Delay(RequestDelay, cancellationToken);

                if (ExceptionToThrow != null)
                    throw ExceptionToThrow;

                if (_options.HandleNegativeOffset && getUpdatesRequest.Offset == -1)
                {
                    var messageCount = _messages.Select(group => group.Length).Sum() + 1;
                    var lastMessage = _messages.Last().Last();

                    _messages.Clear();

                    return (TResponse)(object) new[]
                    {
                        new Update
                        {
                            Message = new Message
                            {
                                Text = lastMessage
                            },
                            Id = messageCount
                        }
                    };
                }

                if (!_messages.TryDequeue(out string[] messages))
                    return (TResponse)(object)new Update[0];

                return (TResponse)(object)messages.Select((message, i) => new Update
                {
                    Message = new Message()
                    {
                        Text = messages[i]
                    },
                    Id = getUpdatesRequest.Offset + i + 1
                }).ToArray();
            }

            throw new NotImplementedException();
        }

        public TimeSpan Timeout { get; set; } = TimeSpan.FromMilliseconds(50);


        // ---------------
        // NOT IMPLEMENTED
        // ---------------

        #pragma warning disable CS0067

        public long? BotId => throw new NotImplementedException();
        public bool IsReceiving => throw new NotImplementedException();
        public int MessageOffset { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public event AsyncEventHandler<ApiRequestEventArgs> OnMakingApiRequest;
        public event AsyncEventHandler<ApiResponseEventArgs> OnApiResponseReceived;
        public event EventHandler<UpdateEventArgs> OnUpdate;
        public event EventHandler<MessageEventArgs> OnMessage;
        public event EventHandler<MessageEventArgs> OnMessageEdited;
        public event EventHandler<InlineQueryEventArgs> OnInlineQuery;
        public event EventHandler<ChosenInlineResultEventArgs> OnInlineResultChosen;
        public event EventHandler<CallbackQueryEventArgs> OnCallbackQuery;
        public event EventHandler<ReceiveErrorEventArgs> OnReceiveError;
        public event EventHandler<ReceiveGeneralErrorEventArgs> OnReceiveGeneralError;
        public Task DownloadFileAsync(string filePath, Stream destination, CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public Task<Types.File> GetInfoAndDownloadFileAsync(string fileId, Stream destination, CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public void StartReceiving(UpdateType[] allowedUpdates = null, CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public void StopReceiving() => throw new NotImplementedException();
        public Task<bool> TestApiAsync(CancellationToken cancellationToken = default) => throw new NotImplementedException();
    }
}
