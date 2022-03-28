using Bogus;
using Xunit;
using DomainEntity = luxclusif.order.domain.Entity;

namespace luxclusif.order.tests.UnitTest.domain.User;
public class UserTestFixture : UnitBaseFixture
{
    [CollectionDefinition(nameof(UserTestFixture))]
    public class UserTestFixtureCollection : ICollectionFixture<UserTestFixture>
    {

    }
}