using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTests.Framework
{
    public class TestCollectionOrderer : ITestCollectionOrderer
    {
        private readonly string[] _orderedCollections =
        {
            Constants.TestCollections.PersonalDetails,
            Constants.TestCollections.ResidentialAddress,
            Constants.TestCollections.DriverLicense,

            Constants.TestCollections.PhoneAndEmail,
            Constants.TestCollections.RentalAgreementAndBill,

            Constants.TestCollections.IdentityCardErrors,
            Constants.TestCollections.PassportRegistrationErrors,
            Constants.TestCollections.UnspecifiedError,
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

            return (int) order;
        }
    }
}
