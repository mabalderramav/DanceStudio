using DanceStudio.Application.Common.Interfaces;
using DanceStudio.Infrastructure.Admins.Persistence;
using DanceStudio.Infrastructure.Common.Persistence;
using DanceStudio.Infrastructure.Studios.Persistence;
using DanceStudio.Infrastructure.Subscriptions.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DanceStudio.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("defaultConnection");
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();
            services.AddScoped<IAdminsRepository, AdminsRepository>();
            services.AddScoped<IStudiosRepository, StudiosRepository>();
            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<ApplicationDbContext>());
            return services;
        }
    }
}
