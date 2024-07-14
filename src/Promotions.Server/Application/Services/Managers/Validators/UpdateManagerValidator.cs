using Application.Services.Managers.Commands;
using FluentValidation;

namespace Application.Services.Managers.Validators
{
    internal class UpdateManagerValidator : AbstractValidator<UpdateManagerCommand>
    {
        public UpdateManagerValidator()
        {
            RuleFor(x => x.Manager.Name)
              .NotEmpty()
              .WithMessage("{PropertyName} is not assigned");
        }
    }
}
