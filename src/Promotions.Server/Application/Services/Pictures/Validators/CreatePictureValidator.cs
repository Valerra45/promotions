using Application.Services.Pictures.Commands;
using FluentValidation;

namespace Application.Services.Pictures.Validators
{
    public class CreatePictureValidator : AbstractValidator<CreatePictureCommand>
    {
        public CreatePictureValidator()
        {
            RuleFor(x => x.PictureCreate.Id)
                .NotEmpty()
                .WithMessage("{PropertyName} is not assigned");
        }
    }
}
