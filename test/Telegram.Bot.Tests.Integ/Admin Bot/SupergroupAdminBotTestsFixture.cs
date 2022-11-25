using System.Collections.Generic;
using System.IO;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Tests.Integ.Framework.Fixtures;
using Telegram.Bot.Types;

namespace Telegram.Bot.Tests.Integ.Admin_Bot;

public class SupergroupAdminBotTestsFixture : AsyncLifetimeFixture
{
    byte[] _oldChatPhoto;
    ChatPermissions _existingDefaultPermissions;

    public TestsFixture TestsFixture { get; }
    public Chat Chat => TestsFixture.SupergroupChat;
    public List<Message> PinnedMessages { get; }
    public ChatInviteLink ChatInviteLink { get; set; }

    public SupergroupAdminBotTestsFixture(TestsFixture testsFixture)
    {
        TestsFixture = testsFixture;
        PinnedMessages = new(3);

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
                _existingDefaultPermissions = chat.Permissions!;
            },
            dispose: async () =>
            {
                // If chat had a photo before, reset the photo back.
                if (_oldChatPhoto is not null)
                {
                    await using MemoryStream photoStream = new(_oldChatPhoto);
                    await TestsFixture.BotClient.SetChatPhotoAsync(
                        chatId: Chat.Id,
                        photo: new InputFile(photoStream)
                    );
                }

                // Reset original default permissions
                await TestsFixture.BotClient.SetChatPermissionsAsync(
                    TestsFixture.SupergroupChat,
                    _existingDefaultPermissions!

                );

                // Revoke invite link created during the test run
                if (ChatInviteLink is not null)
                {
                    await TestsFixture.BotClient.RevokeChatInviteLinkAsync(
                        chatId: TestsFixture.SupergroupChat,
                        inviteLink: ChatInviteLink.InviteLink
                    );
                }
            }
        );
    }
}
