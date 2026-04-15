using ErrorOr;

namespace DanceStudio.Domain.Common.Interfaces
{
    public interface IPasswordHasher
    {
        ErrorOr<string> HashPassword(string password);
        bool IsCorrectPassword(string password, string hash);
    }
}
