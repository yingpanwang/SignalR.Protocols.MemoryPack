using MemoryPack;

namespace Test.Common;

[MemoryPackable]
public partial class UploadBlock
{
    [MemoryPackOrder(0)]
    public string Name { get; set; }

    [MemoryPackOrder(1)]
    public byte[] Data { get; set; }
}