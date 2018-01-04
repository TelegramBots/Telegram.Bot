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
            Constants.TestCollections.GettingUpdates,
            Constants.TestCollections.Webhook,
            Constants.TestCollections.SendTextMessage,
            Constants.TestCollections.PrivateChatReplyMarkup,
            Constants.TestCollections.ReplyMarkup,
            Constants.TestCollections.SendPhotoMessage,
            Constants.TestCollections.SendVideoMessage,
            Constants.TestCollections.InlineQuery,
            Constants.TestCollections.FileDownload,
            Constants.TestCollections.SendDocumentMessage,
            Constants.TestCollections.ChatInfo,
            Constants.TestCollections.AlbumMessage,
            Constants.TestCollections.CallbackQuery,
            Constants.TestCollections.EditReplyMarkup,
            Constants.TestCollections.InlineMessageLiveLocation,
            Constants.TestCollections.LiveLocation,
            Constants.TestCollections.Stickers,
            Constants.TestCollections.Payment,
            Constants.TestCollections.SuperGroupAdminBots,
            Constants.TestCollections.ChannelAdminBots,
            Constants.TestCollections.ChatMemberAdministration,
            Constants.TestCollections.Exceptions,
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
