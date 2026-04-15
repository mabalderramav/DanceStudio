using DanceStudio.Application.Common.Authorization;
using DanceStudio.Application.Common.Interfaces;
using DanceStudio.Domain.Common.Interfaces;
using DanceStudio.Domain.Users;
using ErrorOr;
using MediatR;

namespace DanceStudio.Application.Authentication.Commands.RegisterUser
{
    public class RegisterUserCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IPasswordHasher passwordHasher,
        IUsersRepository usersRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<RegisterUserCommand, ErrorOr<AuthenticationResult>>
    {
        public async Task<ErrorOr<AuthenticationResult>> Handle(
            RegisterUserCommand command,
            CancellationToken cancellationToken)
        {
            if (await usersRepository.ExistsByEmailAsync(command.Email))
            {
                return Error.Conflict(description: "User already exists");
            }

            var hashPasswordResult = passwordHasher.HashPassword(command.Password);

            if (hashPasswordResult.IsError)
            {
                return hashPasswordResult.Errors;
            }

            var user = new User(
                command.FirstName,
                command.LastName,
                command.Email,
                hashPasswordResult.Value);

            await usersRepository.AddUserAsync(user);
            await unitOfWork.CommitChangesAsync();

            var token = jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}