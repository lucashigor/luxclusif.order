using FluentAssertions;
using luxclusif.order.application.Models;
using luxclusif.order.application.UseCases.Order.CreateOrder;
using luxclusif.order.tests.Components.Utils;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace luxclusif.order.tests.Components
{
    public class OrderControllerTests : IClassFixture<IntegrationTestFixture>
    {
        private readonly IntegrationTestFixture _fixture;

        private const string orderUrl = "/order";

        public OrderControllerTests(IntegrationTestFixture integrationTestFixture)
        {
            _fixture = integrationTestFixture;
            _fixture.ResetDatabase();
        }

        [Fact(DisplayName = nameof(Post_NewOrder_WithSuccess))]
        [Trait("Componente/WebApi", "OrderController - Post")]
        public async Task Post_NewOrder_WithSuccess()
        {
            //Arrange
            string url = $"{orderUrl}";
            var client = _fixture.CreateAuthorizedClient();

            var data = new CreateOrderInput(
            _fixture.Faker.Commerce.ProductName(),
            System.Guid.NewGuid(),
            120
            );

            string json = JsonConvert.SerializeObject(data);

            var httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync(url, httpContent);
            var content = await response.Content.ReadAsStringAsync();
            var records = JsonConvert.DeserializeObject<DefaultResponseDto<CreateOrderOutput>>(content);

            //Asset
            records.Should().NotBeNull();

            _fixture.rabbit.list.Should().NotBeNull();

            var obj = _fixture.rabbit.list[_fixture.EventName];

            PactsGenericValidator.EnsureEventApiHonoursPactWithConsumer(
                _fixture.MicroServiceName,
                _fixture.EventName,
                "Aggregator Event Consumer-luxclusif.order.webapi.json",
                obj
                );
        }
    }
}