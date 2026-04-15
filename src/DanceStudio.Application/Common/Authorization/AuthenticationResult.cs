using DanceStudio.Domain.Users;

namespace DanceStudio.Application.Common.Authorization
{
    public record AuthenticationResult(
        User User,
        string Token);
}