using Application.Services.Promotions.Commands;
using FluentValidation;

namespace Application.Services.Promotions.Validators
{
    public class CreatePromotionValidator : AbstractValidator<CreatePromotionCommand>
    {
        public CreatePromotionValidator()
        {
            RuleFor(x => x.Promotion.Name)
                .NotEmpty()
                .WithMessage("{PropertyName} is not assigned");
        }
    }
}
