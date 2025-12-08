using DanceStudio.Application.Common.Interfaces;
using DanceStudio.Domain.Admins;
using DanceStudio.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DanceStudio.Infrastructure.Admins.Persistence
{
    public class AdminsRepository(ApplicationDbContext context) : IAdminsRepository
    {
        public async Task<Admin?> GetByIdAsync(Guid adminId)
        {
            return await context.Admins.FirstOrDefaultAsync(x => x.Id == adminId);
        }

        public Task UpdateAsync(Admin admin)
        {
            context.Admins.Update(admin);
            return Task.CompletedTask;
        }
    }
}
