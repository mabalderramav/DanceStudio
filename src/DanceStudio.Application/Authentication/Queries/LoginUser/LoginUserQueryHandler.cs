using DanceStudio.Application.Common.Authorization;
using DanceStudio.Application.Common.Interfaces;
using DanceStudio.Domain.Common.Interfaces;
using ErrorOr;
using MediatR;

namespace DanceStudio.Application.Authentication.Queries.LoginUser
{
    public class LoginUserQueryHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IPasswordHasher passwordHasher,
        IUsersRepository usersRepository)
        : IRequestHandler<LoginUserQuery, ErrorOr<AuthenticationResult>>
    {
        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginUserQuery query,
            CancellationToken cancellationToken)
        {
            var user = await usersRepository.GetByEmailAsync(query.Email);

            return user is null || !user.IsCorrectPasswordHash(query.Password, passwordHasher)
                ? AuthenticationErrors.InvalidCredentials
                : new AuthenticationResult(user, jwtTokenGenerator.GenerateToken(user));
        }
    }
}