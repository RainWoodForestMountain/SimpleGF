namespace GameFramework
{
    public class MessageBody_Number : MessageBody
    {
        public double content { get; private set; }

        public MessageBody_Number(string _key, double _con) : base(_key)
        {
            content = _con;
        }
        
        public int nInt { get { return (int)content; } }
        public float nFloat { get { return (float)content; } }
        public long nLong { get { return (long)content; } }
    }
}