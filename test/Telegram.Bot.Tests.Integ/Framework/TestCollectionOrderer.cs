using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Telegram.Bot.Tests.Integ.Framework
{
    public class TestCollectionOrderer : ITestCollectionOrderer
    {
        private readonly string[] _orderedCollections = {
            // Tests that require user interaction:
            Constants.TestCollections.CallbackQuery,
            Constants.TestCollections.PrivateChatReplyMarkup,
            Constants.TestCollections.InlineMessageLiveLocation,
            Constants.TestCollections.InlineQuery,

            // ToDo Tests that can be fully automated or divided into 2 collections
            Constants.TestCollections.GetUserProfilePhotos,
            Constants.TestCollections.EditMessage,
            Constants.TestCollections.DeleteMessage,
            Constants.TestCollections.Stickers,
            Constants.TestCollections.Games,
            Constants.TestCollections.Payment,
            Constants.TestCollections.SuperGroupAdminBots,
            Constants.TestCollections.ChannelAdminBots,
            Constants.TestCollections.ChatMemberAdministration,
            Constants.TestCollections.Exceptions,

            // Tests without user interaction:
            Constants.TestCollections.GettingUpdates,
            Constants.TestCollections.Webhook,
            Constants.TestCollections.SendTextMessage,
            Constants.TestCollections.SendAudioMessage,
            Constants.TestCollections.SendContactMessage,
            Constants.TestCollections.SendVenueMessage,
            Constants.TestCollections.SendDocumentMessage,
            Constants.TestCollections.SendPhotoMessage,
            Constants.TestCollections.SendVideoMessage,
            Constants.TestCollections.AlbumMessage,
            Constants.TestCollections.ReplyMarkup,
            Constants.TestCollections.LiveLocation,
            Constants.TestCollections.FileDownload,
            Constants.TestCollections.ChatInfo,
            Constants.TestCollections.LeaveChat,
        };

        public IEnumerable<ITestCollection> OrderTestCollections(IEnumerable<ITestCollection> testCollections)
        {
            testCollections = testCollections.OrderBy(FindExecutionOrder);

            foreach (var collection in testCollections)
            {
                yield return collection;
            }
        }

        private int FindExecutionOrder(ITestCollection collection)
        {
            int? order = null;
            for (int i = 0; i < _orderedCollections.Length; i++)
            {
                if (_orderedCollections[i] == collection.DisplayName)
                {
                    order = i;
                    break;
                }
            }

            if (order is null)
            {
                throw new ArgumentException(
                    $"Collection \"{collection.DisplayName}\" not found in execution list.", nameof(collection));
            }

            return (int)order;
        }
    }
}
