using FluentAssertions;
using luxclusif.order.tests.Integrations.Persintence;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using infra = luxclusif.order.infrastructure;

namespace luxclusif.order.tests.Integrations.Persintence.UnitOfWork;

[Collection(nameof(IntegrationDataBaseTestFixture))]
public class UnitOfWorkTests
{
    public IntegrationDataBaseTestFixture fixture;

    public UnitOfWorkTests(IntegrationDataBaseTestFixture fixture)
    {
        this.fixture = fixture;
        fixture.ResetDatabase();
    }

    [Fact(DisplayName = nameof(CommitTest))]
    [Trait("Integration/infrastructure", "UnitOfWork - Persistence")]
    public async Task CommitTest()
    {
        var dbContext = fixture.GetDatabase();
        var dbContext2 = fixture.GetDatabase();

        var item = fixture.GetValidUser();

        await dbContext.AddAsync(item);

        var firstCall = await dbContext2.Order.FindAsync(item.Id);

        var unitOfWork = new infra.UnitOfWork(dbContext);

        await unitOfWork.CommitAsync(CancellationToken.None);

        var secondCall = await dbContext2.Order.FindAsync(item.Id);

        firstCall.Should().BeNull();
        secondCall.Should().NotBeNull();

        secondCall?.Name.Should().Be(item.Name);
        secondCall?.CreatedAt.Should().Be(item.CreatedAt);
        secondCall?.LastUpdateAt.Should().Be(item.LastUpdateAt);
        secondCall?.DeletedAt.Should().Be(item.DeletedAt);
    }

    [Fact(DisplayName = nameof(RollbackTest))]
    [Trait("Integration/infrastructure", "UnitOfWork - Persistence")]
    public async Task RollbackTest()
    {
        var dbContext = fixture.GetDatabase();
        var dbContext2 = fixture.GetDatabase();

        var item = fixture.GetValidUser();

        await dbContext.AddAsync(item);

        var db2firstCall = await dbContext2.Order.FindAsync(item.Id);

        var dbfirstCall = await dbContext.Order.FindAsync(item.Id);

        var unitOfWork = new infra.UnitOfWork(dbContext);

        await unitOfWork.RollbackAsync(CancellationToken.None);


        var dbsecondCall = await dbContext.Order.FindAsync(item.Id);

        var db2secondCall = await dbContext2.Order.FindAsync(item.Id);

        db2firstCall.Should().BeNull();
        db2secondCall.Should().BeNull();

        dbfirstCall.Should().NotBeNull();
        dbsecondCall.Should().BeNull();
    }
}
