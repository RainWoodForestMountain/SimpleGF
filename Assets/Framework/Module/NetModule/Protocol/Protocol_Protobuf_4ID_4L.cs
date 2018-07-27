using System;
using System.IO;
using System.Net;

namespace GameFramework
{
    public class Protocol_Protobuf_4ID_4L : IProtocol
    {
        public Protocol_Protobuf_4ID_4L()
        {
            totalHead = 8;
        }

        public int totalHead { get; private set; }

        /// <summary>
        /// 消息体：4位msgid，4位msglen(内容长度，非总长度)
        /// </summary>
        public ByteBuffer Get(MemoryStream _ms, BinaryReader _br)
        {
            int _msgid = IPAddress.NetworkToHostOrder(_br.ReadInt32());
            int _length = IPAddress.NetworkToHostOrder(_br.ReadInt32());
            if (_length > (_ms.Length - _ms.Position))
            {
                _ms.Position -= totalHead;
                return null;
            }

            ByteBuffer _bf = new ByteBuffer();
            _bf.WriteInt(_msgid);
            _bf.WriteBytes(_br.ReadBytes(_length));

            ByteBuffer _test = new ByteBuffer(_bf.ToBytes());

            return new ByteBuffer(_bf.ToBytes());
        }
        public byte[] Set(ByteBuffer _bb)
        {
            _bb = new ByteBuffer(_bb.ToBytes());
            
            byte[] _msgid =  BitConverter.GetBytes(IPAddress.HostToNetworkOrder(_bb.ReadInt()));
            byte[] _body = _bb.ReadBytes();
            byte[] _length = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(_body.Length));
            byte[] _array = new byte[_body.Length + totalHead];

            Array.Copy(_msgid, 0, _array, 0, _msgid.Length);
            Array.Copy(_length, 0, _array, 4, _length.Length);
            Array.Copy(_body, 0, _array, 8, _body.Length);

            return _array;
        }
    }
}