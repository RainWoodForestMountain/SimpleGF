using System;

namespace GameFramework
{
	public class MessageModule : ModuleBase
	{
		public static MessageModule instance
		{
			get 
			{
				return ModuleController.instance.GetModule<MessageModule> ();
			}
		}
        
		private MessageRepertory repertory;
		private MessageRecord record;

		public override void Init (long _id)
		{
			base.Init (_id);
			repertory = new MessageRepertory ();
			record = new MessageRecord ();
        }
		public override void Destroy ()
		{
			record.Destroy ();
		}

        //接受消息
        #region Recevive
        //--------------------------------
        public void Recevive(Type _key, int _type, string _content)
        {
            Recevive(_key, (MessageType)_type, _content);
        }
        public void Recevive(Type _key, MessageType _type, string _content)
        {
            Recevive(MessageFactory.ProduceMessage(_key.ToString(), _type, _content));
        }
        //--------------------------------
        public void Recevive(string _key, int _type, string _content)
        {
            Recevive(_key, (MessageType)_type, _content);
        }
        public void Recevive(string _key, MessageType _type, string _content)
        {
            Recevive(MessageFactory.ProduceMessage(_key, _type, _content));
        }
        //--------------------------------
        public void Recevive(string _key, int _type, UnityEngine.Object _content)
        {
            Recevive(_key, (MessageType)_type, _content);
        }
        public void Recevive(string _key, MessageType _type, UnityEngine.Object _content)
        {
            Recevive(MessageFactory.ProduceMessage(_key, _type, _content));
        }
        //--------------------------------
        public void Recevive(string _key, int _type, UnityEngine.GameObject _content)
        {
            Recevive(_key, (MessageType)_type, _content);
        }
        public void Recevive(string _key, MessageType _type, UnityEngine.GameObject _content)
        {
            Recevive(MessageFactory.ProduceMessage(_key, _type, _content));
        }
        //--------------------------------
        public void Recevive(string _key, int _type, ByteBuffer _content)
        {
            Recevive(_key, (MessageType)_type, _content);
        }
        public void Recevive(string _key, MessageType _type, ByteBuffer _content)
        {
            Recevive(MessageFactory.ProduceMessage(_key, _type, _content));
        }
        //--------------------------------
        public void Recevive(long _key, int _type, UnityEngine.GameObject _content)
        {
            Recevive(_key, (MessageType)_type, _content);
        }
        public void Recevive(long _key, MessageType _type, UnityEngine.GameObject _content)
        {
            Recevive(MessageFactory.ProduceMessage(_key.ToString(), _type, _content));
        }
        //--------------------------------
        public void Recevive(int _type, IMessageBody _body)
        {
            Recevive((MessageType)_type, _body);
        }
        public void Recevive (MessageType _type, IMessageBody _body)
		{
			Recevive (MessageFactory.ProduceMessage (_type, _body));
        }
        //--------------------------------
        public void Recevive (Message _msg)
		{
			if (!isRunning) return;

			//消息记录
			record.Record (_msg);
			Broadcast (_msg);
		}
        #endregion

        //广播消息
        private void Broadcast (Message _msg)
		{
			MessageType _type = _msg.msgType;

            MessageListener _listener = repertory.GetListener(_type);
            _listener.Broadcast(_msg);
        }
        /// <summary>
        /// 添加监听器，请不要用匿名函数和Lamda表达式！
        /// </summary>
        //--------------------------------
        public void AddListener(int _type, Action<Message> _ac)
        {
            AddListener((MessageType)_type, _ac);
        }
        public void AddListener (MessageType _type, Action<Message> _ac)
		{
			repertory.AddOneListener (_type, _ac);
        }
        //--------------------------------
        public void RemoveListener(int _type, Action<Message> _ac)
        {
            RemoveListener((MessageType)_type, _ac);
        }
        public void RemoveListener (MessageType _type, Action<Message> _ac)
		{
			repertory.RemoveOneListener (_type, _ac);
        }
        //--------------------------------
    }
}