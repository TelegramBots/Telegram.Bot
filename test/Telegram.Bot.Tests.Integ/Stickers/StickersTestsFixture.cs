using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Tests.Integ.Framework.Fixtures;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Tests.Integ.Stickers;

public class StickersTestsFixture(TestsFixture testsFixture) : AsyncLifetimeFixture
{
    protected override IEnumerable<Func<Task>> Initializers() =>
    [
        async () =>
        {
            OwnerUserId = await GetStickerOwnerIdAsync(
                testsFixture,
                Constants.TestCollections.Stickers
            );
        }
    ];

    //Basic information
    public string TestStickerSetTitle => "Test sticker set";

    public long OwnerUserId { get; private set; }

    //Emojis
    public IEnumerable<string> FirstEmojis { get; } = ["😊"];

    public IEnumerable<string> SecondEmojis { get; } = ["🥰", "😘"];

    public IEnumerable<string> ThirdEmojis { get; } = ["😎"];

    //Regular stickers
    public string TestStaticRegularStickerSetName { get; } = $"test_static_regular_set_by_{testsFixture.BotUser.Username}";

    public File TestUploadedStaticStickerFile { get; set; }

    public StickerSet TestStaticRegularStickerSet { get; set; }

    public string TestAnimatedRegularStickerSetName { get; } = $"test_animated_regular_set_by_{testsFixture.BotUser.Username}";

    public File TestUploadedAnimatedStickerFile { get; set; }

    public StickerSet TestAnimatedRegularStickerSet { get; set; }

    public string TestVideoRegularStickerSetName { get; } = $"test_video_regular_set_by_{testsFixture.BotUser.Username}";

    public File TestUploadedVideoStickerFile { get; set; }

    public StickerSet TestVideoRegularStickerSet { get; set; }

    //Mask stickers
    public string TestStaticMaskStickerSetName { get; } = $"test_static_mask_set_by_{testsFixture.BotUser.Username}";

    public StickerSet TestStaticMaskStickerSet { get; set; }

    //Custom emoji stickers
    public string TestStaticCustomEmojiStickerSetName { get; } = $"test_static_c_emoji_set_by_{testsFixture.BotUser.Username}";

    public StickerSet TestStaticCustomEmojiStickerSet { get; set; }

    static async Task<long> GetStickerOwnerIdAsync(TestsFixture testsFixture, string collectionName)
    {
        long ownerId;

        if (testsFixture.Configuration.StickerOwnerUserId == default)
        {
            await testsFixture.UpdateReceiver.DiscardNewUpdatesAsync();

            Message notificationMessage = await testsFixture.SendTestCollectionNotificationAsync(collectionName,
                $"\nNo value is set for `{nameof(TestConfiguration.StickerOwnerUserId)}` " +
                "in test settings.\n\n"
            );

            const string cqData = "sticker_tests:owner";
            Message cqMessage = await testsFixture.BotClient.SendMessageAsync(
                new()
                {
                    ChatId = testsFixture.SupergroupChat,
                    Text = $"{testsFixture.UpdateReceiver.GetTesters()}\nUse the following button to become Sticker Set Owner",
                    ReplyParameters = new() { MessageId = notificationMessage.MessageId },
                    ReplyMarkup = new InlineKeyboardMarkup(
                        InlineKeyboardButton.WithCallbackData("I am the Owner!", cqData)
                    ),
                }
            );

            Update cqUpdate = await testsFixture.UpdateReceiver
                .GetCallbackQueryUpdateAsync(cqMessage.MessageId, cqData);

            ownerId = cqUpdate.CallbackQuery!.From.Id;
        }
        else
        {
            ownerId = testsFixture.Configuration.StickerOwnerUserId.Value;
        }

        return ownerId;
    }
}
