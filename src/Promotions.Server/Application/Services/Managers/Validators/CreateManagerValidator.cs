using Application.Services.Managers.Commands;
using FluentValidation;

namespace Application.Services.Managers.Validators
{
    public class CreateManagerValidator : AbstractValidator<CreateManagerCommand>
    {
        public CreateManagerValidator()
        {
            RuleFor(x => x.Manager.Name)
                .NotEmpty()
                .WithMessage("{PropertyName} is not assigned");
        }
    }
}
