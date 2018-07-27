namespace GameFramework
{
    public class MessageBody_GameObject : MessageBody
    {
        public UnityEngine.GameObject content { get; private set; }

        public MessageBody_GameObject(string _key, UnityEngine.GameObject _con) : base(_key)
        {
            content = _con;
        }
    }
}