//////////////////////////////////////////////////////////////////////////////////////////
/// Module 模块
/// 每个模块独立负责自己的生存、死亡、更新、消息处理等功能
/// 外部控制器仅仅对模块的几个全局接口进行操控
/// 当模块的生存状态发生改变时，应该向全局发送消息
//////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Reflection;
using System.Collections.Generic;

//额外的程序集添加到这里
namespace GameContent
{
    public static class GameContentAssembly
    {
        private static Assembly ass;
        public static Assembly assembly
        {
            get
            {
                if (ass == null)
                {
                    ass = Assembly.GetExecutingAssembly();
                }
                return ass;
            }
        }
    }
}

namespace GameFramework
{
	public class ModuleController
	{
		public static ModuleController instance;

		private IDModel idModel;
		private Dictionary<long, IModule> modules;
		private List<Assembly> assembly;

		#region Common
		public void Init ()
		{
			idModel = new IDModel ();
			modules = new Dictionary<long, IModule> ();
            assembly = new List<Assembly>();
            //添加需要托管的程序集，兼容ios无法加载dll
            assembly.Add(Assembly.GetExecutingAssembly());
            assembly.Add(GameContent.GameContentAssembly.assembly);

            MessageModule.instance.AddListener(MessageType.ModuleOpen, OpenModule);
            MessageModule.instance.AddListener(MessageType.ModuleClose, CloseModule);

            //默认启动的模块，这里启动的模块因为其完全封装，无法外部主动调用
            Open("GameFramework.UIModule");
            Open("GameFramework.MusicModule");
            Open("GameFramework.NetworkModule");
            Open("GameFramework.ReporterModule");

            Open("GameContent.ToLuaModule");
        }
        public void Activate ()
        {
            foreach (KeyValuePair<long, IModule> _v in modules)
            {
                _v.Value.Activate();
            }
        }
		public void Sleep ()
        {
            foreach (KeyValuePair<long, IModule> _v in modules)
            {
                _v.Value.Sleep();
            }
        }
		public void Destroy ()
		{
            MessageModule.instance.RemoveListener(MessageType.ModuleOpen, OpenModule);
            MessageModule.instance.RemoveListener(MessageType.ModuleClose, CloseModule);

            foreach (KeyValuePair<long, IModule> _v in modules)
			{
				_v.Value.Destroy ();
			}
			modules.Clear ();
			modules = null;
			idModel = null;
		}
        #endregion

        #region Event
        /// <summary>
        /// 注意加上命名空间名称
        /// </summary>
        private void OpenModule (Message _msg)
        {
            if (_msg.msgType != MessageType.ModuleOpen) return;
            Open(_msg.body.key);
        }
        /// <summary>
        /// 注意加上命名空间名称
        /// </summary>
        private void CloseModule (Message _msg)
        {
            if (_msg.msgType != MessageType.ModuleClose) return;
            Close(_msg.body.key);
        }
        private void Open(string _name)
        {
            Type _t = Type.GetType(_name, true);
            //判断继承接口
            if (_t.GetInterface("IModule") == null) return;

            foreach (var _v in modules)
            {
                if (_v.Value.GetType() == _t)
                {
                    _v.Value.Activate();
                    return;
                }
            }

            //遍历程序集
            object _o = null;
            for (int i = 0; i < assembly.Count; i++)
            {
                _o = assembly[i].CreateInstance(_t.ToString(), true);
                if (_o != null) break;
            }
            if (_o == null)
            {
                Utility.LogError("没有找到名称为", _name, "的模块！");
                return;
            }
            IModule _im = _o as IModule;
            modules.Add(_im.id, _im);
            _im.Init(idModel.GetLongId);
            _im.Activate();
        }
        private void Close (string _name)
        {
            Utility.Log("将要关闭模块", _name);
            Type _t = Type.GetType(_name, true);
            //判断继承接口
            if (_t.GetInterface("IModule") == null) return;

            List<long> _ids = new List<long>();
            foreach (var _v in modules)
            {
                if (_v.Value.GetType() == _t)
                {
                    _ids.Add(_v.Key);
                }
            }
            for (int i = 0; i < _ids.Count; i++)
            {
                DestroyModule(_ids[i]);
            }
        }
        #endregion

        #region Child Operate
        internal IModule GetModule(long _id)
		{
			if (modules.ContainsKey (_id))
			{
				return modules[_id];
			}
			else
			{
				return null;
			}
		}
        internal T GetModule<T>() where T : IModule
		{
			Type _type = typeof (T);
			foreach (var _v in modules)
			{
				if (_v.Value.GetType () == _type)
				{
					return (T)_v.Value;
				}
			}
            
            //遍历程序集
            object _o = null;
            for (int i = 0; i < assembly.Count; i++)
            {
                _o = assembly[i].CreateInstance(_type.ToString(), true);
                if (_o != null) break;
            }
            if (_o == null)
            {
                Utility.LogError("没有找到类型为", _type.ToString(), "的模块！");
                return default(T);
            }
            T _t = (T)_o;
			_t.Init (idModel.GetLongId);
			modules.Add (_t.id, _t);
			return _t;
		}
        internal List<T> GetAll<T>() where T : IModule
		{
			Type _type = typeof (T);
			List<T> _list = new List<T> ();
			foreach (var _v in modules)
			{
				if (_v.Value.GetType () == _type)
				{
					_list.Add ((T)_v.Value);
				}
			}
			if (_list.Count > 0)
			{
				return _list;
			}

            object _o = null;
            for (int i = 0; i < assembly.Count; i++)
            {
                _o = assembly[i].CreateInstance(_type.ToString(), true);
                if (_o != null) break;
            }
            if (_o == null)
            {
                Utility.LogError("没有找到类型为", _type.ToString(), "的模块！");
                return null;
            }
			T _t = (T)_o;
			_t.Init (idModel.GetLongId);
			modules.Add (_t.id, _t);
			_list.Add (_t);
			return _list;
		}
        internal void DestroyModule(long _id)
		{
			if (modules.ContainsKey (_id))
			{
				modules[_id].Destroy ();
				modules.Remove (_id);
			}
		}
        internal void DestroyModule<T>() where T : IModule
		{
			Type _type = typeof (T);
			List<long> _list = new List<long> ();
			foreach (var _v in modules)
			{
				if (_v.Value.GetType () == _type)
				{
					_v.Value.Destroy ();
					_list.Add (_v.Key);
				}
			}
			for (int i = 0; i < _list.Count; i++)
			{
				modules.Remove (_list[i]);
			}
		}
        internal void ActivateModule (long _id)
		{
			if (modules.ContainsKey (_id))
			{
				modules[_id].Activate ();
			}
		}
        internal void ActivateModule<T>() where T : IModule
		{
			Type _type = typeof (T);
			foreach (var _v in modules)
			{
				if (_v.Value.GetType () == _type)
				{
					_v.Value.Activate ();
				}
			}
		}
        internal void SleepModule (long _id)
		{
			if (modules.ContainsKey (_id))
			{
				modules[_id].Sleep ();
			}
		}
        internal void SleepModule<T>() where T : IModule
		{
			Type _type = typeof (T);
			foreach (var _v in modules)
			{
				if (_v.Value.GetType () == _type)
				{
					_v.Value.Sleep ();
				}
			}
		}
		#endregion
	}
}