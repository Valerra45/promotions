using Application.Services.GoodsService.Commands;
using FluentValidation;

namespace Application.Services.GoodsService.Validators
{
    public class UpdateGoodsValidator : AbstractValidator<UpdateGoodsCommand>
    {
        public UpdateGoodsValidator()
        {
            RuleFor(x => x.Goods.Name)
              .NotEmpty()
              .WithMessage("{PropertyName} is not assigned");
        }
    }
}
