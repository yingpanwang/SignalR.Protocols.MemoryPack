using MemoryPack;

namespace Test.Common
{
    [MemoryPackable(serializeLayout: SerializeLayout.Explicit)]
    public partial class InvocationObj
    {
        [MemoryPackOrder(0)]
        public DateTime InvocationDateTime { get; set; } = DateTime.Now;

        [MemoryPackOrder(1)]
        public int TestInt { get; set; } = int.MaxValue;

        [MemoryPackOrder(2)]
        public long TestLong { get; set; } = long.MaxValue;

        [MemoryPackOrder(3)]
        public byte TestByte { get; set; } = byte.MaxValue;

        [MemoryPackOrder(4)]
        public uint TestUInt { get; set; } = uint.MaxValue;

        [MemoryPackOrder(5)]
        public ulong TestULong { get; set; } = ulong.MaxValue;

        [MemoryPackOrder(6)]
        public char TestChar { get; set; } = Random.Shared.Next(0, 9).ToString()[0];

        [MemoryPackOrder(7)]
        public string TestString { get; set; } = Random.Shared.Next(1000, 9999).ToString();

        [MemoryPackOrder(8)]
        public InvocationInnerObj TestInnerObj { get; set; } = new();
    }

    [MemoryPackable(serializeLayout: SerializeLayout.Explicit)]
    public partial class InvocationInnerObj
    {
        [MemoryPackOrder(0)]
        public DateTime InvocationDateTime { get; set; } = DateTime.Now;

        [MemoryPackOrder(1)]
        public int TestInt { get; set; } = int.MaxValue;

        [MemoryPackOrder(2)]
        public long TestLong { get; set; } = long.MaxValue;

        [MemoryPackOrder(3)]
        public byte TestByte { get; set; } = byte.MaxValue;

        [MemoryPackOrder(4)]
        public uint TestUInt { get; set; } = uint.MaxValue;

        [MemoryPackOrder(5)]
        public ulong TestULong { get; set; } = ulong.MaxValue;

        [MemoryPackOrder(6)]
        public char TestChar { get; set; } = Random.Shared.Next(0, 9).ToString()[0];

        [MemoryPackOrder(7)]
        public string TestString { get; set; } = Random.Shared.Next(1000, 9999).ToString();
    }
}