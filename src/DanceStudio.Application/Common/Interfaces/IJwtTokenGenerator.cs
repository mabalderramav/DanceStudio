using DanceStudio.Domain.Users;

namespace DanceStudio.Application.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}