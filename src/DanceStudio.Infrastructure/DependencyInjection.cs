using DanceStudio.Application.Common.Interfaces;
using DanceStudio.Infrastructure.Common.Persistence;
using DanceStudio.Infrastructure.Subcriptions.Persistence;
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
            services.AddScoped<ISubscriptionsRepository, SubcriptionsRepository>();
            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<ApplicationDbContext>());
            return services;
        }
    }
}
