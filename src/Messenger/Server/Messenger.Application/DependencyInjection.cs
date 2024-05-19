using Messenger.Application.Helpers.PasswordHasher;
using Messenger.Application.Services.Authentication;
using Messenger.Application.Services.Chats;
using Messenger.Application.Services.ChatUser;
using Messenger.Application.Services.JWTTokenHandler;
using Messenger.Application.Services.Messages;
using Messenger.Application.Services.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Messenger.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddScoped<IChatService, ChatService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IMessageService, MessageService>()
                .AddScoped<IChatUserService, ChatUserService>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            //Handlers
            services.AddScoped<IPasswordHasherService, PasswordHasherService>();
            services.AddScoped<IJWTTokenHandlerService, JWTTokenHandlerService>();

            services.AddHttpContextAccessor();

            return services;
        }
    }
}
