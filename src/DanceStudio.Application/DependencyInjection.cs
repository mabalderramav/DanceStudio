using DanceStudio.Application.Studios.Commands.CreateStudio;
using DanceStudio.Domain.Studios;
using ErrorOr;
using MediatR;
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
                options.AddBehavior<IPipelineBehavior<CreateStudioCommand, ErrorOr<Studio>>, CreateStudioCommandBehavior>();
            });

            return services;
        }
    }
}
