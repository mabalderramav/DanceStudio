using DanceStudio.Application.Common.Authorization;
using ErrorOr;
using MediatR;

namespace DanceStudio.Application.Authentication.Commands.RegisterUser
{
    public record RegisterUserCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password
    ) : IRequest<ErrorOr<AuthenticationResult>>;
}