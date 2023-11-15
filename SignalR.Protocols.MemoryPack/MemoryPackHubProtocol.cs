using MemoryPack;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Extensions.Options;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;

namespace SignalR.Protocols.MemoryPack
{
    public class MemoryPackHubProtocol : IHubProtocol
    {
        private readonly MemoryPackHubProtocolWorker _worker;

        public string Name => "memorypack";

        public TransferFormat TransferFormat => TransferFormat.Binary;
        public int Version => 1;

        public MemoryPackHubProtocol(IOptions<MemoryPackSerializerOptions> options)
        {
            _worker = new MemoryPackHubProtocolWorker(options.Value);
        }

        public ReadOnlyMemory<byte> GetMessageBytes(HubMessage message)
        {
            return _worker.GetMessageBytes(message);
        }

        public bool IsVersionSupported(int version)
        {
            return version <= Version;
        }

        public bool TryParseMessage(ref ReadOnlySequence<byte> input, IInvocationBinder binder, [NotNullWhen(true)] out HubMessage? message)
        {
            return _worker.TryParseMessage(ref input, binder, out message);
        }

        public void WriteMessage(HubMessage message, IBufferWriter<byte> output)
        {
            _worker.WriteMessage(message, output);
        }
    }
}