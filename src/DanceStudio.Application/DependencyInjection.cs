using DanceStudio.Application.Common.Interfaces;
using DanceStudio.Application.Subscriptions;
using DanceStudio.Application.Subscriptions.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DanceStudio.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // services.AddScoped<ISubscriptionWriteService, SubscriptionWriteService>();
            services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
            });

            return services;
        }
    }
}
