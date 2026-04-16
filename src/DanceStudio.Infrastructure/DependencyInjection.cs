using System.Text;
using DanceStudio.Application.Common.Interfaces;
using DanceStudio.Domain.Common.Interfaces;
using DanceStudio.Infrastructure.Admins.Persistence;
using DanceStudio.Infrastructure.Authentication.PasswordHasher;
using DanceStudio.Infrastructure.Authentication.TokenGenerator;
using DanceStudio.Infrastructure.Common.Persistence;
using DanceStudio.Infrastructure.Studios.Persistence;
using DanceStudio.Infrastructure.Subscriptions.Persistence;
using DanceStudio.Infrastructure.Users.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DanceStudio.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .AddAuthentication(configuration)
                .AddPersistence();
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services)
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
            services.AddScoped<IUsersRepository, UsersRepository>();

            services
                .AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<ApplicationDbContext>());

            // Execute migrations at startup
            services.AddHostedService<MigrationHostedService>();

            return services;
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.Section, jwtSettings);

            services.AddSingleton(Options.Create(jwtSettings));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                });
            return services;
        }
    }

    public class MigrationHostedService(IServiceProvider serviceProvider) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await dbContext.Database.MigrateAsync(cancellationToken); // Execute migrations
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}