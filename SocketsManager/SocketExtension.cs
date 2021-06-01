using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Chat.SocketsManager
{
    public static class SocketExtension
    {
        public static IServiceCollection AddWebSocketManager(this IServiceCollection services)
        {
            services.AddTransient<ConnectionManager>();

            foreach (var item in Assembly.GetEntryAssembly().ExportedTypes)
            {
                if (item.GetTypeInfo().BaseType == typeof(SocketHandler))
                    services.AddSingleton(item);
            }
            return services;
        }
    }
}

