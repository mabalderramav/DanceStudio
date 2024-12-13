using DanceStudio.Domain.Studios;

namespace DanceStudio.Application.Common.Interfaces
{
    public interface IStudiosRepository
    {
        Task AddAsync(Studio studio);
        Task<Studio?> GetByIdAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<List<Studio>> ListBySubscriptionIdAsync(Guid subscriptionId);
        Task UpdateAsync(Studio studio);
        Task RemoveAsync(Studio studio);
        Task RemoveRangeAsync(List<Studio> studios);
    }
}
