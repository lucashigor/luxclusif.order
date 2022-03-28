using FluentAssertions;
using luxclusif.order.infrastructure.Repositories;
using luxclusif.order.tests.Integrations.Persintence;
using System.Threading;
using Xunit;

namespace luxclusif.order.tests.Integrations.Persintence.Repositories;

[Collection(nameof(IntegrationDataBaseTestFixture))]
public class UserRepositoryTests
{
    public IntegrationDataBaseTestFixture fixture;

    public UserRepositoryTests(IntegrationDataBaseTestFixture fixture)
    {
        this.fixture = fixture;
        fixture.ResetDatabase();
    }

    [Fact(DisplayName = nameof(CreateUser))]
    [Trait("Integration/infrastructure", "UserRepository - Repositories")]
    public async void CreateUser()
    {
        var dbContext = fixture.GetDatabase();
        var app = new OrderRepository(dbContext);
        var item = fixture.GetValidUser();

        await app.Insert(item, CancellationToken.None);
        await dbContext.SaveChangesAsync(CancellationToken.None);

        var user = dbContext.Order.Find(item.Id);

        user.Should().NotBeNull();
        user?.Name.Should().Be(item.Name);
        user?.CreatedAt.Should().Be(item.CreatedAt);
        user?.LastUpdateAt.Should().Be(item.LastUpdateAt);
        user?.DeletedAt.Should().Be(item.DeletedAt);
    }
}
