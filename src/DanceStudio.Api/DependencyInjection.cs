using DanceStudio.Api.Services;
using DanceStudio.Application.Common.Interfaces;

namespace DanceStudio.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddProblemDetails();
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

            return services;
        }
    }
}
