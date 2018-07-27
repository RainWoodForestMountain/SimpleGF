using System;

namespace GameFramework
{
	public abstract class ModuleBase : Object, IModule
	{
		#region Inherit
		public long id { get; protected set; }
		public virtual void Init (long _id)
		{
			id = _id;
			isRunning = true;
            name = GetType ().ToString ();
            alr = new AssetLoadRecord(name);
            logInfo = Utility.MergeString (name, "'s LogMsg = ");
            Log("模块启动！");
        }
		public virtual void Activate () 
		{
			isRunning = true;
		}
		public virtual void Sleep () 
		{
			isRunning = false;
		}
		public virtual void Destroy ()
        {
            alr.Destroy();
        }
		#endregion

		public string name;
		protected string logInfo;
		protected bool isRunning;

		/// <summary>
		/// Destroy接口为外部调用， kill接口为内部调用
		/// </summary>
		protected void Kill ()
		{
			ModuleController.instance.DestroyModule (id);
		}
		private string[] CreateLogMsg (params string[] _msg)
		{
			string[] _ss = new string[_msg.Length + 1];
			_msg.CopyTo (_ss, 1);
			_ss[0] = logInfo;
			return _ss;
		}
		protected void Log (params string[] _msg)
		{
			Utility.Log (CreateLogMsg (_msg));
		}
		protected void LogWarning (params string[] _msg)
		{
			Utility.LogWarning (CreateLogMsg (_msg));
		}
		protected void LogError (params string[] _msg)
		{
			Utility.LogError (CreateLogMsg (_msg));
		}
        
        private AssetLoadRecord alr;
        protected T LoadResource<T>(string _resName) where T : UnityEngine.Object
        {
            return alr.LoadResource<T>(_resName);
        }
    }
}