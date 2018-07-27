namespace GameFramework
{
    public class MessageBody_Action : MessageBody
    {
        public System.Action content { get; private set; }

        public MessageBody_Action(string _key, System.Action _con) : base(_key)
        {
            content = _con;
        }
    }
}
