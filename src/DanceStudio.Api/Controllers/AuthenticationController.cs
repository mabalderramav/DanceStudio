using DanceStudio.Application.Authentication.Commands.RegisterUser;
using DanceStudio.Application.Authentication.Queries.LoginUser;
using DanceStudio.Application.Common.Authorization;
using DanceStudio.Contracts.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DanceStudio.Api.Controllers
{
    [Route("[controller]")]
    [AllowAnonymous]
    public class AuthenticationController(ISender sender) : ApiController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = new RegisterUserCommand(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password);

            var authResult = await sender.Send(command);

            return authResult.Match(
                authenticationResult => base.Ok(MapToAuthResponse(authenticationResult)),
                Problem);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = new LoginUserQuery(request.Email, request.Password);

            var authResult = await sender.Send(query);

            if (authResult.IsError && authResult.FirstError == AuthenticationErrors.InvalidCredentials)
            {
                return Problem(
                    detail: authResult.FirstError.Description,
                    statusCode: StatusCodes.Status401Unauthorized);
            }

            return authResult.Match(
                authenticationResult => Ok(MapToAuthResponse(authenticationResult)),
                Problem);
        }

        private static AuthenticationResponse MapToAuthResponse(AuthenticationResult authResult)
        {
            return new AuthenticationResponse(
                authResult.User.Id,
                authResult.User.FirstName,
                authResult.User.LastName,
                authResult.User.Email,
                authResult.Token);
        }
    }
}
