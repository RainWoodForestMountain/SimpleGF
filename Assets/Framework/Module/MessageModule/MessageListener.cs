using System;
using System.Collections.Generic;

namespace GameFramework
{
	internal class MessageListener
	{
        private Action<Message> acs;
        internal MessageType mtype { get; private set; }

		internal MessageListener (MessageType _type)
		{
			mtype = _type;
		}
		internal void Broadcast (Message _msg)
		{
            if (acs == null) return;

            acs(_msg);
        }
		internal void Clear ()
		{
            acs = null;
        }
		internal void Add (Action<Message> _ac)
		{
			if (_ac == null) return;
            
            if (acs == null) acs = _ac;
            else acs += _ac;
        }
		internal void Remove (Action<Message> _ac)
		{
			if (_ac == null || acs == null) return;
            
            acs -= _ac;
        }
	}
}