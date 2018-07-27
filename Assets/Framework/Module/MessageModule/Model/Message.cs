namespace GameFramework
{
    public struct Message
    {
        public long id { get; private set; }
        public IMessageBody body { get; private set; }
        public MessageType msgType { get; private set; }

        public Message (long _id, MessageType _msgType, IMessageBody _body)
		{
			id = _id;
            msgType = _msgType;
            body = _body;
		}
		public override string ToString ()
		{
			return Utility.MergeString ("name:", body.key, " type:", msgType.ToString());
		}
	}
}