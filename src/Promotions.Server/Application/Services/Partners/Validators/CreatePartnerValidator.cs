using Application.Services.Partners.Commands;
using FluentValidation;

namespace Application.Services.Partners.Validators
{
    public class CreatePartnerValidator : AbstractValidator<CreatePartnerCommand>
    {
        public CreatePartnerValidator()
        {
            RuleFor(x => x.Partner.Name)
                .NotEmpty()
                .WithMessage("{PropertyName} is not assigned");
        }
    }
}
