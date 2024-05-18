using Messenger.Application.Helpers.PasswordHasher;
using Messenger.Application.Services.Authentication;
using Messenger.Application.Services.JWTTokenHandler;
using Microsoft.Extensions.DependencyInjection;

namespace Messenger.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IPasswordHasherService, PasswordHasherService>();
            services.AddScoped<IJWTTokenHandlerService, JWTTokenHandlerService>();

            services.AddHttpContextAccessor();

            return services;
        }
    }
}
