namespace GameFramework
{
    public class MessageBody_ActionNumber : MessageBody
    {
        public System.Action<float> content { get; private set; }

        public MessageBody_ActionNumber(string _key, System.Action<float> _con) : base(_key)
        {
            content = _con;
        }
    }
}
