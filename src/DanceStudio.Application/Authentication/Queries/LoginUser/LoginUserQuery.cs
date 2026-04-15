using DanceStudio.Application.Common.Authorization;
using ErrorOr;
using MediatR;

namespace DanceStudio.Application.Authentication.Queries.LoginUser
{
    public record LoginUserQuery(
        string Email,
        string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}