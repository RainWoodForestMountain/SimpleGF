using System;
using System.Collections;
using System.Collections.Generic;

namespace GameFramework
{
	internal class MessageRepertory
	{
		private Dictionary<MessageType, MessageListener> listeners;

		internal MessageRepertory ()
		{
			listeners = new Dictionary<MessageType, MessageListener> ();
		}
        
        private void AddNewListeners(MessageType _type)
        {
			if (!listeners.ContainsKey (_type))
			{
				listeners.Add (_type, new MessageListener (_type));
			}
		}
		internal void AddOneListener (MessageType _type, Action<Message> _ac)
		{
			AddNewListeners (_type);
			listeners[_type].Add (_ac);
		}
		internal void RemoveOneListener (MessageType _type, Action<Message> _ac)
		{
			AddNewListeners (_type);
			listeners[_type].Remove (_ac);
		}
		internal MessageListener GetListener (MessageType _type)
		{
			AddNewListeners (_type);
			return listeners[_type];
		}
		internal List<MessageListener> GetListeners ()
		{
			List<MessageListener> _lis = new List<MessageListener> ();
			foreach (var _kvp in listeners)
			{
				_lis.Add (_kvp.Value);
			}
			return _lis;
		}
	}
}