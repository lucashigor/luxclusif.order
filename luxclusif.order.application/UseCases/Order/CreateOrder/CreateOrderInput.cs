using MediatR;

namespace luxclusif.order.application.UseCases.Order.CreateOrder;
public class CreateOrderInput : IRequest<CreateOrderOutput>
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public decimal Value { get; set; }
    
}
