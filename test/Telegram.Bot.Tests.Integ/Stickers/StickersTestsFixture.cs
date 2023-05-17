using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Tests.Integ.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Tests.Integ.Stickers;

public class StickersTestsFixture
{
    //Basic information
    public string TestStickerSetTitle { get; }

    public long OwnerUserId { get; }

    //Emojis
    public IEnumerable<string> FirstEmojis { get; }

    public IEnumerable<string> SecondEmojis { get; }

    public IEnumerable<string> ThirdEmojis { get; }

    //Regular stickers
    public string TestStaticRegularStickerSetName { get; }

    public File TestUploadedStaticStickerFile { get; set; }

    public StickerSet TestStaticRegularStickerSet { get; set; }

    public string TestAnimatedRegularStickerSetName { get; }

    public File TestUploadedAnimatedStickerFile { get; set; }

    public StickerSet TestAnimatedRegularStickerSet { get; set; }

    public string TestVideoRegularStickerSetName { get; }

    public File TestUploadedVideoStickerFile { get; set; }

    public StickerSet TestVideoRegularStickerSet { get; set; }

    //Mask stickers
    public string TestStaticMaskStickerSetName { get; }

    public StickerSet TestStaticMaskStickerSet { get; set; }

    //Custom emoji stickers
    public string TestStaticCustomEmojiStickerSetName { get; }

    public StickerSet TestStaticCustomEmojiStickerSet { get; set; }

    public StickersTestsFixture(TestsFixture testsFixture)
    {
        TestStickerSetTitle = "Test sticker set";

        OwnerUserId = GetStickerOwnerIdAsync(
            testsFixture,
            Constants.TestCollections.Stickers
        ).GetAwaiter().GetResult();

        FirstEmojis = new string[] { "😊" };
        SecondEmojis = new string[] { "🥰", "😘" };
        ThirdEmojis = new string[] { "😎" };

        TestStaticRegularStickerSetName = $"test_static_regular_set_by_{testsFixture.BotUser.Username}";
        TestAnimatedRegularStickerSetName = $"test_animated_regular_set_by_{testsFixture.BotUser.Username}";
        TestVideoRegularStickerSetName = $"test_video_regular_set_by_{testsFixture.BotUser.Username}";

        TestStaticMaskStickerSetName = $"test_static_mask_set_by_{testsFixture.BotUser.Username}";

        TestStaticCustomEmojiStickerSetName = $"test_static_c_emoji_set_by_{testsFixture.BotUser.Username}";
    }

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
            Message cqMessage = await testsFixture.BotClient.SendTextMessageAsync(
                testsFixture.SupergroupChat,
                testsFixture.UpdateReceiver.GetTesters() +
                "\nUse the following button to become Sticker Set Owner",
                replyToMessageId: notificationMessage.MessageId,
                replyMarkup: new InlineKeyboardMarkup(
                    InlineKeyboardButton.WithCallbackData("I am the Owner!", cqData)
                )
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
