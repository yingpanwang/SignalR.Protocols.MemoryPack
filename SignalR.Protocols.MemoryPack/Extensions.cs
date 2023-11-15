using MemoryPack;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SignalR.Protocols.MemoryPack
{
    public static class Extensions
    {
        public static TBuilder AddMemoryPackProtocol<TBuilder>(this TBuilder builder, Action<MemoryPackSerializerOptions>? configure = null) where TBuilder : ISignalRBuilder
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IHubProtocol, MemoryPackHubProtocol>());

            if (configure != null)
            {
                builder.Services.Configure(configure);
            }
            return builder;
        }
    }
}