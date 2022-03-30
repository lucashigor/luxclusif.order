using luxclusif.order.application.Constants;
using luxclusif.order.application.Interfaces;
using luxclusif.order.application.Models;
using luxclusif.order.domain.Exceptions;
using luxclusif.order.domain.Repository;
using MediatR;
using DomainEntity = luxclusif.order.domain.Entity;

namespace luxclusif.order.application.UseCases.Order.CreateOrder;
public class CreateOrder :
    IRequestHandler<CreateOrderInput, CreateOrderOutput>
{
    public readonly IOrderRepository userRepository;
    public readonly IUnitOfWork unityOfWork;
    public readonly Notifier notifier;
    public readonly IMediator mediator;

    private readonly string EventName = "topic.createdorder";

    public CreateOrder(IOrderRepository userRepository,
        IUnitOfWork unityOfWork,
        Notifier notifier,
        IMediator mediator)
    {
        this.unityOfWork = unityOfWork;
        this.userRepository = userRepository;
        this.notifier = notifier;
        this.mediator = mediator;
    }

    public async Task<CreateOrderOutput> Handle(CreateOrderInput userInput, CancellationToken cancellationToken)
    {
        DomainEntity.Order entity;

        try
        {
            entity = new DomainEntity.Order(userInput.Name, userInput.UserId, userInput.Value);
        }
        catch (EntityGenericException)
        {
            notifier.Erros.Add(ErrorCodeConstant.Validation);

            return null!;
        }

        try
        {
            await userRepository.Insert(entity, cancellationToken);

            var message = new DefaultMessageNotification(EventName, entity);

            await mediator.Publish(message);

            await unityOfWork.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {
            notifier.Erros.Add(ErrorCodeConstant.ErrorOnSavingNewUser);

            return null!;
        }

        return CreateOrderOutput.FromUser(entity);
    }
}
