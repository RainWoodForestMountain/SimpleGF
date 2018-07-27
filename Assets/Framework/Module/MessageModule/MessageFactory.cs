namespace GameFramework
{
	public class MessageFactory
	{
		private static IDModel idmodel;

		static MessageFactory ()
		{
			idmodel = new IDModel ();
		}

        public static Message ProduceMessage (MessageType _type, IMessageBody _body)
		{
			return new Message (idmodel.GetLongId, _type, _body);
		}
        public static Message ProduceMessage(string _key, MessageType _type, string _content)
        {
            return ProduceMessage(_type, new MessageBody_String(_key, _content));
        }
        public static Message ProduceMessage(string _key, MessageType _type, UnityEngine.GameObject _content)
        {
            return ProduceMessage(_type, new MessageBody_GameObject(_key, _content));
        }
        public static Message ProduceMessage(string _key, MessageType _type, UnityEngine.Object _content)
        {
            return ProduceMessage(_type, new MessageBody_UnityObject(_key, _content));
        }
        public static Message ProduceMessage(string _key, MessageType _type, ByteBuffer _content)
        {
            return ProduceMessage(_type, new MessageBody_ByteBuffer(_key, _content));
        }
    }
}