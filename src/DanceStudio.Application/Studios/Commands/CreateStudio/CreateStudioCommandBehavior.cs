using DanceStudio.Domain.Studios;
using ErrorOr;
using MediatR;

namespace DanceStudio.Application.Studios.Commands.CreateStudio
{
    public class CreateStudioCommandBehavior : IPipelineBehavior<CreateStudioCommand, ErrorOr<Studio>>
    {
        public async Task<ErrorOr<Studio>> Handle(
            CreateStudioCommand request,
            RequestHandlerDelegate<ErrorOr<Studio>> next,
            CancellationToken cancellationToken)
        {
            // Validating...
            var validator = new CreateStudioCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors
                    .Select(x => Error.Validation(code: x.PropertyName, description: x.ErrorMessage))
                    .ToList();
            }

            return await next();
        }
    }
}
