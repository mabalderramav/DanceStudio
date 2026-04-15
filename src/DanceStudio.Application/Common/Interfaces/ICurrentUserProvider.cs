using DanceStudio.Application.Common.Models;

namespace DanceStudio.Application.Common.Interfaces
{
    public interface ICurrentUserProvider
    {
        CurrentUser GetCurrentUser();
    }
}