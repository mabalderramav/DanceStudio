using DanceStudio.Application.Common.Interfaces;
using DanceStudio.Domain.Admins;
using DanceStudio.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace DanceStudio.Infrastructure.Admins.Persistence
{
    public class AdminsRepository : IAdminsRepository
    {
        private readonly ApplicationDbContext context;

        public AdminsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
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
