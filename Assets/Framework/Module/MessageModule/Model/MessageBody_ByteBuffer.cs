namespace GameFramework
{
    public class MessageBody_ByteBuffer : MessageBody
    {
        public ByteBuffer content { get; private set; }

        public MessageBody_ByteBuffer(string _key, ByteBuffer _con) : base(_key)
        {
            content = _con;
        }
    }
}