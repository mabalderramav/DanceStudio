using ErrorOr;

namespace DanceStudio.Application.Common.Authorization
{
    public static class AuthenticationErrors
    {
        public static readonly Error InvalidCredentials = Error.Validation(
            code: "Authentication.InvalidCredentials",
            description: "Invalid credentials");
    }
}