# SignalR.Protocols.MemoryPack

通过实现SignalR 中 IHubProtocol接口,支持对于MemoryPack格式的支持(参照了官方的MessagePack的实现,但是并没有根据MemoryPack本身的优势进行优化,暂时只实现了消息的序列化、反序列化)
