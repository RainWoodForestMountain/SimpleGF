using System.IO;

namespace GameFramework
{
    public interface IProtocol
    {
        int totalHead { get; }
        
        ByteBuffer Get(MemoryStream _ms, BinaryReader _br);
        byte[] Set(ByteBuffer _bb);
    }
}