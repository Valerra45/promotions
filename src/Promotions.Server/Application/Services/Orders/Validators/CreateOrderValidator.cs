using Application.Services.Orders.Commands;
using FluentValidation;

namespace Application.Services.Orders.Validators
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.Order.PartnerId)
                .NotEmpty()
                .WithMessage("{PropertyName} is not assigned");
        }
    }
}
