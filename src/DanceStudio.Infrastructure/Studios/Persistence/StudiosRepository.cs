using DanceStudio.Application.Common.Interfaces;
using DanceStudio.Domain.Studios;
using DanceStudio.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DanceStudio.Infrastructure.Studios.Persistence
{
    public class StudiosRepository(ApplicationDbContext context) : IStudiosRepository
    {
        public async Task AddAsync(Studio studio)
        {
            await context.Studios.AddAsync(studio);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await context.Studios.AsNoTracking().AnyAsync(x => x.Id == id);
        }

        public async Task<Studio?> GetByIdAsync(Guid id)
        {
            return await context.Studios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Studio>> ListBySubscriptionIdAsync(Guid subscriptionId)
        {
            return await context.Studios.Where(x => x.SubscriptionId == subscriptionId).ToListAsync();
        }

        public Task RemoveAsync(Studio studio)
        {
            context.Remove(studio);
            return Task.CompletedTask;
        }

        public Task RemoveRangeAsync(List<Studio> studios)
        {
            context.RemoveRange(studios);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Studio studio)
        {
            context.Update(studio);
            return Task.CompletedTask;
        }
    }
}
