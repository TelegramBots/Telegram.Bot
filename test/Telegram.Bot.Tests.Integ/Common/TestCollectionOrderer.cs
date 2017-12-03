using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Telegram.Bot.Tests.Integ.Common
{
    public class TestCollectionOrderer : ITestCollectionOrderer
    {
        private readonly string[] _orderedCollections = {
            Constants.TestCollections.GettingUpdates,
            Constants.TestCollections.TextMessage,
            Constants.TestCollections.MultimediaMessage,
            Constants.TestCollections.MessageReplyMarkup,
            Constants.TestCollections.InlineQuery,
            Constants.TestCollections.CallbackQuery,
            Constants.TestCollections.AlbumMessage,
            Constants.TestCollections.UpdateMessage,
            Constants.TestCollections.LiveLocations,
            Constants.TestCollections.Payment,
            Constants.TestCollections.Stickers,
            Constants.TestCollections.ChatInformation,
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
                throw new Exception($"Collection \"{collection.DisplayName}\" not found in execution list.");
            }

            return (int)order;
        }
    }
}
