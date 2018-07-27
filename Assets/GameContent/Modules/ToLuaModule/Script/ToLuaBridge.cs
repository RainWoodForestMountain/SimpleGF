using System;
using GameFramework;

namespace GameContent {
	public class ToLuaBridge : INeedDestroy {
		//这里C#端的模块操作命令不影响lua端，lua端完全自力更生，但是lua对C#端的模块操作命令可以生效
		//这里只监听用户操作消息以及网络数据消息
		private MessageType[] types = new MessageType[] {
			MessageType.EventButtonClick,
			MessageType.EventButtonDoubleClick,
			MessageType.EventBeginDrag,
			MessageType.EventBeginSliding,
			MessageType.EventEndSliding,
			MessageType.EventOnSliding,
			MessageType.EventDragging,
			MessageType.EventEndDrag,
			MessageType.EventOnTouchDown,
			MessageType.EventOnTouchEnter,
			MessageType.EventOnTouchExit,
			MessageType.EventOnTouchUp,
			MessageType.ServerResponse,
			MessageType.ServerConnected,
			MessageType.ServerDisconnect,
			MessageType.OnHotupdateStep,
			MessageType.OnTotalHotupdateComplete,
			MessageType.PlatformResponse,
			MessageType.OnHotupdateFailed,
			MessageType.EventPressStart,
			MessageType.EventPressEnd,
            //MessageType.NetLostConnect,
            //MessageType.NetDelay,
        };

		private static Action<Message> action;

		public ToLuaBridge() {
			for (int i = 0; i < types.Length; i++) {
				MessageModule.instance.AddListener(types[i], OnMessageComeAndSendToLua);
			}
		}

		public void Destroy() {
			for (int i = 0; i < types.Length; i++) {
				MessageModule.instance.RemoveListener(types[i], OnMessageComeAndSendToLua);
			}
		}

		private void OnMessageComeAndSendToLua(Message _msg) {
			action(_msg);
		}

		//为了方便清理，Lua端的监听一律保存在ToLuaBridge
		public static void AddListener(Action<Message> _ac) {
			action += _ac;
		}

		public static void RemoveListener(Action<Message> _ac) {
			action -= _ac;
		}

		//lua端可以向C#端直接发送消息命令
		public static void Recevive(string _key, MessageType _type, string _content) {
			MessageModule.instance.Recevive(_key, _type, _content);
		}

		public static void Recevive(string _key, MessageType _type, UnityEngine.GameObject _content) {
			MessageModule.instance.Recevive(_key, _type, _content);
		}

		public static void Recevive(string _key, MessageType _type, ByteBuffer _content) {
			MessageModule.instance.Recevive(_key, _type, _content);
		}

		public static void Recevive(string _key, MessageType _type, UnityEngine.Object _content) {
			if (_content.GetType() == typeof(UnityEngine.GameObject)) {
				MessageModule.instance.Recevive(_key, _type, (UnityEngine.GameObject) _content);
			}
			else {
				MessageModule.instance.Recevive(_key, _type, _content);
			}
		}
	}
}