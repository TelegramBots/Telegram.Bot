using System.Collections.Generic;
using System.IO;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Tests.Integ.Framework.Fixtures;
using Telegram.Bot.Types;

namespace Telegram.Bot.Tests.Integ.Admin_Bot
{
    public class SupergroupAdminBotTestsFixture : AsyncLifetimeFixture
    {
        private byte[] _oldChatPhoto;

        public TestsFixture TestsFixture { get; }
        public Chat Chat => TestsFixture.SupergroupChat;
        public List<Message> PinnedMessages { get; }
        public ChatPermissions ExistingDefaultPermissions { get; private set; }

        public SupergroupAdminBotTestsFixture(TestsFixture testsFixture)
        {
            TestsFixture = testsFixture;
            PinnedMessages = new List<Message>(3);

            AddLifetime(
                initialize: async () =>
                {
                    Chat chat = await TestsFixture.BotClient.GetChatAsync(TestsFixture.SupergroupChat);

                    // Save existing chat photo as byte[] to restore it later because Bot API 4.4+ invalidates old
                    // file_ids after changing chat photo
                    if (!string.IsNullOrEmpty(chat.Photo?.BigFileId))
                    {
                        await using MemoryStream stream = new();
                        await TestsFixture.BotClient.GetInfoAndDownloadFileAsync(chat.Photo.BigFileId, stream);

                        _oldChatPhoto = stream.ToArray();
                    }

                    // Save default permissions so they can be restored
                    ExistingDefaultPermissions = chat.Permissions;
                },
                dispose: async () =>
                {
                    // If chat had a photo before, reset the photo back.
                    if (_oldChatPhoto is not null)
                    {
                        await using MemoryStream photoStream = new(_oldChatPhoto);
                        await TestsFixture.BotClient.SetChatPhotoAsync(
                            chatId: Chat.Id,
                            photo: photoStream
                        );
                    }

                    // Reset original default permissions
                    await TestsFixture.BotClient.SetChatPermissionsAsync(
                        TestsFixture.SupergroupChat,
                        ExistingDefaultPermissions
                    );
                }
            );
        }
    }
}
