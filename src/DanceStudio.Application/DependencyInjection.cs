using Microsoft.Extensions.DependencyInjection;

namespace DanceStudio.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // services.AddScoped<ISubscriptionWriteService, SubscriptionWriteService>();

            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
            });

            return services;
        }
    }
}
