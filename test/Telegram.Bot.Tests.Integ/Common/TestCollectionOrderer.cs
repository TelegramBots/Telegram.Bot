﻿using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Telegram.Bot.Tests.Integ.Common
{
    public class TestCollectionOrderer : ITestCollectionOrderer
    {
        private readonly string[] _orderedCollections = {
            CommonConstants.TestCollections.GettingUpdates,
            CommonConstants.TestCollections.TextMessage,
            CommonConstants.TestCollections.MultimediaMessage,
            CommonConstants.TestCollections.MessageReplyMarkup,
            CommonConstants.TestCollections.InlineQuery,
            CommonConstants.TestCollections.CallbackQuery,
            CommonConstants.TestCollections.AlbumMessage,
            CommonConstants.TestCollections.UpdateMessage,
            CommonConstants.TestCollections.LiveLocations,
            CommonConstants.TestCollections.Payment,
            CommonConstants.TestCollections.Stickers,
            CommonConstants.TestCollections.ChatInformation,
            CommonConstants.TestCollections.SuperGroupAdminBots,
            CommonConstants.TestCollections.ChannelAdminBots,
            CommonConstants.TestCollections.ChatMemberAdministration,
            CommonConstants.TestCollections.Exceptions,
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
