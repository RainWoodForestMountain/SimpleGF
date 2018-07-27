namespace GameFramework
{
    public class MessageBody_UnityObject : MessageBody
    {
        public UnityEngine.Object content { get; private set; }

        public MessageBody_UnityObject(string _key, UnityEngine.Object _con) : base(_key)
        {
            content = _con;
        }
    }
}