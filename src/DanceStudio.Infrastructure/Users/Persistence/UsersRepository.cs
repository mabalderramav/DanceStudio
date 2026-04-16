using DanceStudio.Application.Common.Interfaces;
using DanceStudio.Domain.Users;
using DanceStudio.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DanceStudio.Infrastructure.Users.Persistence
{
    public class UsersRepository(ApplicationDbContext dbContext) : IUsersRepository
    {
        public async Task AddUserAsync(User user)
        {
            await dbContext.AddAsync(user);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await dbContext.Users.AnyAsync(user => user.Email == email);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User?> GetByIdAsync(Guid userId)
        {
            return await dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);
        }

        public Task UpdateAsync(User user)
        {
            dbContext.Update(user);

            return Task.CompletedTask;
        }
    }
}
