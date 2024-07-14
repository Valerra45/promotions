using Application.Services.Orders.Commands;
using FluentValidation;

namespace Application.Services.Orders.Validators
{
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderValidator()
        {
            RuleFor(x => x.Order.PartnerId)
                 .NotEmpty()
                 .WithMessage("{PropertyName} is not assigned");
        }
    }
}
