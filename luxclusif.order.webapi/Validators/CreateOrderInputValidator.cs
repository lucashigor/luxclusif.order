using FluentValidation;
using luxclusif.order.application.UseCases.Order.CreateOrder;
using luxclusif.order.domain.Validation;

namespace luxclusif.order.webapi.Validators
{
    public class CreateOrderInputValidator : AbstractValidator<CreateOrderInput>
    {
        public CreateOrderInputValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(ValidationConstant.RequiredParameter);

            RuleFor(x => x.Name)
                .Length(3,100)
                .WithMessage(ValidationConstant.RequiredParameter);
        }
    }
}
