namespace GameFramework
{
    public class MessageBody : IMessageBody
    {
        public string key { get; private set; }

        public MessageBody(string _key)
        {
            key = _key;
        }
    }
}