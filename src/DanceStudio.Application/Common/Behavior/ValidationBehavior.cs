using ErrorOr;
using FluentValidation;
using MediatR;

namespace DanceStudio.Application.Common.Behavior
{
    public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator = null) :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (validator is null)
                return await next();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid)
            {
                return await next();
            }
            var errors = validationResult.Errors
                .ConvertAll(x => Error.Validation(
                    code: x.PropertyName,
                    description: x.ErrorMessage
                ));
            return (dynamic)errors;
        }
    }
}
