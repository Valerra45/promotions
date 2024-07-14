using Application.Services.Partners.Commands;
using FluentValidation;

namespace Application.Services.Partners.Validators
{
    public class UpdatePartnerValidator : AbstractValidator<UpdatePartnerCommand>
    {
        public UpdatePartnerValidator()
        {
            RuleFor(x => x.Partner.Name)
                .NotEmpty()
                .WithMessage("{PropertyName} is not assigned");

        }
    }
}
