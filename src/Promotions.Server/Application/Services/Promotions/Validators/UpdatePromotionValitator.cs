using Application.Services.Promotions.Commands;
using FluentValidation;

namespace Application.Services.Promotions.Validators
{
    public class UpdatePromotionValitator : AbstractValidator<UpdatePromotionCommand>
    {
        public UpdatePromotionValitator()
        {
            RuleFor(x => x.Promotion.Name)
                .NotEmpty()
                .WithMessage("{PropertyName} is not assigned");
        }
    }
}
