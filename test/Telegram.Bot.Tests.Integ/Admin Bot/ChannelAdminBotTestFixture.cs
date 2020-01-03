using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Tests.Integ.Framework.Fixtures;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Integ.Admin_Bot
{
    public class ChannelAdminBotTestFixture : IAsyncLifetime
    {
        private readonly TestsFixture _fixture;
        private byte[] _oldChatPhoto;

        public Chat Chat { get; }
        public Message PinnedMessage { get; set; }


        public ChannelAdminBotTestFixture(TestsFixture fixture)
        {
            _fixture = fixture;
            Chat = new ChannelChatFixture(fixture, Constants.TestCollections.ChannelAdminBots).ChannelChat;
        }

        public async Task InitializeAsync()
        {
            // Save existing chat photo as byte[] to restore it later because Bot API 4.4+ invalidates old
            // file_ids after changing chat photo
            if (!string.IsNullOrEmpty(Chat.Photo?.BigFileId))
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    await _fixture.BotClient
                        .GetInfoAndDownloadFileAsync(Chat.Photo.BigFileId, stream);

                    _oldChatPhoto = stream.ToArray();
                }
            }
        }

        public async Task DisposeAsync()
        {
            // If chat had a photo before, reset the photo back.
            if (_oldChatPhoto != null)
            {
                using (MemoryStream photoStream = new MemoryStream(_oldChatPhoto))
                {
                    await _fixture.BotClient.SetChatPhotoAsync(
                        chatId: Chat.Id,
                        photo: photoStream
                    );
                }
            }
        }
    }
}
