using Microsoft.Extensions.DependencyInjection;

namespace Messenger.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            return services;
        }
    }
}
