using FluentValidation;

namespace DanceStudio.Application.Studios.Commands.CreateStudio
{
    public class CreateStudioCommandValidator : AbstractValidator<CreateStudioCommand>
    {
        public CreateStudioCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(3)
                .WithMessage("The name of studio should have more than 3 character")
                .MaximumLength(15)
                .WithMessage("The studio name must not have more than 15 characters.");
        }
    }
}
