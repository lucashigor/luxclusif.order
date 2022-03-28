using luxclusif.order.application.Interfaces;
using luxclusif.order.application.UseCases.Order.CreateOrder;
using luxclusif.order.domain.Repository;
using MediatR;
using Moq;
using Xunit;

namespace luxclusif.order.tests.UnitTest.Application.CreateUser;
public class CreateUserTestsFixture : UnitBaseFixture
{
    public Mock<IOrderRepository> GetRepositoryMock() => new();
    public Mock<IUnitOfWork> GetUnitOfWorkMock() => new();
    public Mock<IMediator> GetMediator() => new();

    public CreateOrderInput GetUserCreateInput()
    {
        return new CreateOrderInput(
            GetValidUserName()
            );
    }

    [CollectionDefinition(nameof(CreateUserTestsFixture))]
    public class UserTestFixtureCollection : ICollectionFixture<CreateUserTestsFixture>
    {

    }
}
