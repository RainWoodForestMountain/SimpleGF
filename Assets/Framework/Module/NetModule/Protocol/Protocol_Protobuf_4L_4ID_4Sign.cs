using System;
using System.IO;
using System.Net;

namespace GameFramework
{
    public class Protocol_Protobuf_4L_4ID_4Sign : IProtocol
    {
        public Protocol_Protobuf_4L_4ID_4Sign()
        {
            totalHead = 12;
        }

        public int totalHead { get; private set; }

        /// <summary>
        /// 消息体：4位msglen(内容长度，非总长度)，4位msgid
        /// </summary>
        public ByteBuffer Get(MemoryStream _ms, BinaryReader _br)
        {
            int _length = IPAddress.NetworkToHostOrder(_br.ReadInt32());
            int _msgid = IPAddress.NetworkToHostOrder(_br.ReadInt32());
            int _sign = IPAddress.NetworkToHostOrder(_br.ReadInt32());
            if (_length > (_ms.Length - _ms.Position))
            {
                _ms.Position -= totalHead;
                return null;
            }

            ByteBuffer _bf = new ByteBuffer();
            _bf.WriteInt(_msgid);
            _bf.WriteInt(_sign);
            _bf.WriteBytes(_br.ReadBytes(_length));
            
            return new ByteBuffer(_bf.ToBytes());
        }
        public byte[] Set(ByteBuffer _bb)
        {
            _bb = new ByteBuffer(_bb.ToBytes());

            byte[] _msgid = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(_bb.ReadInt()));
            byte[] _sign = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(_bb.ReadInt()));
            byte[] _body = _bb.ReadBytes();
            byte[] _length = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(_body.Length));
            byte[] _array = new byte[_body.Length + totalHead];

            Array.Copy(_length, 0, _array, 0, _length.Length);
            Array.Copy(_msgid, 0, _array, 4, _msgid.Length);
            Array.Copy(_msgid, 0, _array, 8, _sign.Length);
            Array.Copy(_body, 0, _array, 12, _body.Length);

            return _array;
        }
    }
}