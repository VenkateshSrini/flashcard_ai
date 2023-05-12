using Forge.OpenAI;
using Microsoft.Extensions.DependencyInjection;
using TechCards.Repository.ServiceExtension;

namespace TechCards.Services.ServiceCollection
{
    public static class ChatServiceExtension
    {
        public static IServiceCollection AddChatServices(this IServiceCollection services,
           string mongoConnectionstring, string openAPIKey)
        {
            services.AddRepositoryServices(mongoConnectionstring);
            services.AddForgeOpenAI(options =>
            {
                options.AuthenticationInfo = new(openAPIKey);
            });
            services.AddSingleton<IChatGPTServices, ChatGPTServices>();
            return services;
        }
    }
}
