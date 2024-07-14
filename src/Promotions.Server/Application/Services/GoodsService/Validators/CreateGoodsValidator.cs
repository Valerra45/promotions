using Application.Services.GoodsService.Commands;
using FluentValidation;

namespace Application.Services.GoodsService.Validators
{
    public class CreateGoodsValidator : AbstractValidator<CreateGoodsCommand>
    {
        public CreateGoodsValidator()
        {
            RuleFor(x => x.Goods.Name)
                   .NotEmpty()
                   .WithMessage("{PropertyName} is not assigned");
        }
    }
}
