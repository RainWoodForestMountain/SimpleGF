namespace GameFramework
{
	public class MessageBody_String : MessageBody
    {
		public string content { get; private set; }

		public MessageBody_String(string _key, string _con) : base(_key)
		{
			content = _con;
		}
	}
}