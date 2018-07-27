﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class GameFramework_MonoBaseWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(GameFramework.MonoBase), typeof(UnityEngine.MonoBehaviour));
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("transform", get_transform, null);
		L.RegVar("parent", get_parent, set_parent);
		L.RegVar("position", get_position, set_position);
		L.RegVar("eulerAngles", get_eulerAngles, set_eulerAngles);
		L.RegVar("localPosition", get_localPosition, set_localPosition);
		L.RegVar("localEulerAngles", get_localEulerAngles, set_localEulerAngles);
		L.RegVar("localScale", get_localScale, set_localScale);
		L.RegVar("rotation", get_rotation, set_rotation);
		L.RegVar("localRotation", get_localRotation, set_localRotation);
		L.RegVar("onAwakeEvent", get_onAwakeEvent, set_onAwakeEvent);
		L.RegVar("onStartEvent", get_onStartEvent, set_onStartEvent);
		L.RegVar("onEnableEvent", get_onEnableEvent, set_onEnableEvent);
		L.RegVar("onDisableEvent", get_onDisableEvent, set_onDisableEvent);
		L.RegVar("onUpdateEvent", get_onUpdateEvent, set_onUpdateEvent);
		L.RegVar("onDestroyEvent", get_onDestroyEvent, set_onDestroyEvent);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int op_Equality(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Object arg0 = (UnityEngine.Object)ToLua.ToObject(L, 1);
			UnityEngine.Object arg1 = (UnityEngine.Object)ToLua.ToObject(L, 2);
			bool o = arg0 == arg1;
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_transform(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.MonoBase obj = (GameFramework.MonoBase)o;
			UnityEngine.Transform ret = obj.transform;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index transform on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_parent(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.MonoBase obj = (GameFramework.MonoBase)o;
			UnityEngine.Transform ret = obj.parent;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index parent on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_position(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.MonoBase obj = (GameFramework.MonoBase)o;
			UnityEngine.Vector3 ret = obj.position;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index position on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_eulerAngles(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.MonoBase obj = (GameFramework.MonoBase)o;
			UnityEngine.Vector3 ret = obj.eulerAngles;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index eulerAngles on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_localPosition(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.MonoBase obj = (GameFramework.MonoBase)o;
			UnityEngine.Vector3 ret = obj.localPosition;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index localPosition on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_localEulerAngles(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.MonoBase obj = (GameFramework.MonoBase)o;
			UnityEngine.Vector3 ret = obj.localEulerAngles;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index localEulerAngles on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_localScale(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.MonoBase obj = (GameFramework.MonoBase)o;
			UnityEngine.Vector3 ret = obj.localScale;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index localScale on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rotation(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.MonoBase obj = (GameFramework.MonoBase)o;
			UnityEngine.Quaternion ret = obj.rotation;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index rotation on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_localRotation(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.MonoBase obj = (GameFramework.MonoBase)o;
			UnityEngine.Quaternion ret = obj.localRotation;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index localRotation on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onAwakeEvent(IntPtr L)
	{
		ToLua.Push(L, new EventObject("GameFramework.MonoBase.onAwakeEvent"));
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onStartEvent(IntPtr L)
	{
		ToLua.Push(L, new EventObject("GameFramework.MonoBase.onStartEvent"));
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onEnableEvent(IntPtr L)
	{
		ToLua.Push(L, new EventObject("GameFramework.MonoBase.onEnableEvent"));
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onDisableEvent(IntPtr L)
	{
		ToLua.Push(L, new EventObject("GameFramework.MonoBase.onDisableEvent"));
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onUpdateEvent(IntPtr L)
	{
		ToLua.Push(L, new EventObject("GameFramework.MonoBase.onUpdateEvent"));
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onDestroyEvent(IntPtr L)
	{
		ToLua.Push(L, new EventObject("GameFramework.MonoBase.onDestroyEvent"));
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_parent(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.MonoBase obj = (GameFramework.MonoBase)o;
			UnityEngine.Transform arg0 = (UnityEngine.Transform)ToLua.CheckObject<UnityEngine.Transform>(L, 2);
			obj.parent = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index parent on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_position(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.MonoBase obj = (GameFramework.MonoBase)o;
			UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
			obj.position = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index position on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_eulerAngles(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.MonoBase obj = (GameFramework.MonoBase)o;
			UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
			obj.eulerAngles = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index eulerAngles on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_localPosition(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.MonoBase obj = (GameFramework.MonoBase)o;
			UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
			obj.localPosition = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index localPosition on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_localEulerAngles(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.MonoBase obj = (GameFramework.MonoBase)o;
			UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
			obj.localEulerAngles = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index localEulerAngles on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_localScale(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.MonoBase obj = (GameFramework.MonoBase)o;
			UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
			obj.localScale = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index localScale on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rotation(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.MonoBase obj = (GameFramework.MonoBase)o;
			UnityEngine.Quaternion arg0 = ToLua.ToQuaternion(L, 2);
			obj.rotation = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index rotation on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_localRotation(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.MonoBase obj = (GameFramework.MonoBase)o;
			UnityEngine.Quaternion arg0 = ToLua.ToQuaternion(L, 2);
			obj.localRotation = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index localRotation on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onAwakeEvent(IntPtr L)
	{
		try
		{
			GameFramework.MonoBase obj = (GameFramework.MonoBase)ToLua.CheckObject(L, 1, typeof(GameFramework.MonoBase));
			EventObject arg0 = null;

			if (LuaDLL.lua_isuserdata(L, 2) != 0)
			{
				arg0 = (EventObject)ToLua.ToObject(L, 2);
			}
			else
			{
				return LuaDLL.luaL_throw(L, "The event 'GameFramework.MonoBase.onAwakeEvent' can only appear on the left hand side of += or -= when used outside of the type 'GameFramework.MonoBase'");
			}

			if (arg0.op == EventOp.Add)
			{
				System.Action ev = (System.Action)DelegateTraits<System.Action>.Create(arg0.func);
				obj.onAwakeEvent += ev;
			}
			else if (arg0.op == EventOp.Sub)
			{
				System.Action ev = (System.Action)LuaMisc.GetEventHandler(obj, typeof(GameFramework.MonoBase), "onAwakeEvent");
				Delegate[] ds = ev.GetInvocationList();
				LuaState state = LuaState.Get(L);

				for (int i = 0; i < ds.Length; i++)
				{
					ev = (System.Action)ds[i];
					LuaDelegate ld = ev.Target as LuaDelegate;

					if (ld != null && ld.func == arg0.func)
					{
						obj.onAwakeEvent -= ev;
						state.DelayDispose(ld.func);
						break;
					}
				}

				arg0.func.Dispose();
			}

			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onStartEvent(IntPtr L)
	{
		try
		{
			GameFramework.MonoBase obj = (GameFramework.MonoBase)ToLua.CheckObject(L, 1, typeof(GameFramework.MonoBase));
			EventObject arg0 = null;

			if (LuaDLL.lua_isuserdata(L, 2) != 0)
			{
				arg0 = (EventObject)ToLua.ToObject(L, 2);
			}
			else
			{
				return LuaDLL.luaL_throw(L, "The event 'GameFramework.MonoBase.onStartEvent' can only appear on the left hand side of += or -= when used outside of the type 'GameFramework.MonoBase'");
			}

			if (arg0.op == EventOp.Add)
			{
				System.Action ev = (System.Action)DelegateTraits<System.Action>.Create(arg0.func);
				obj.onStartEvent += ev;
			}
			else if (arg0.op == EventOp.Sub)
			{
				System.Action ev = (System.Action)LuaMisc.GetEventHandler(obj, typeof(GameFramework.MonoBase), "onStartEvent");
				Delegate[] ds = ev.GetInvocationList();
				LuaState state = LuaState.Get(L);

				for (int i = 0; i < ds.Length; i++)
				{
					ev = (System.Action)ds[i];
					LuaDelegate ld = ev.Target as LuaDelegate;

					if (ld != null && ld.func == arg0.func)
					{
						obj.onStartEvent -= ev;
						state.DelayDispose(ld.func);
						break;
					}
				}

				arg0.func.Dispose();
			}

			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onEnableEvent(IntPtr L)
	{
		try
		{
			GameFramework.MonoBase obj = (GameFramework.MonoBase)ToLua.CheckObject(L, 1, typeof(GameFramework.MonoBase));
			EventObject arg0 = null;

			if (LuaDLL.lua_isuserdata(L, 2) != 0)
			{
				arg0 = (EventObject)ToLua.ToObject(L, 2);
			}
			else
			{
				return LuaDLL.luaL_throw(L, "The event 'GameFramework.MonoBase.onEnableEvent' can only appear on the left hand side of += or -= when used outside of the type 'GameFramework.MonoBase'");
			}

			if (arg0.op == EventOp.Add)
			{
				System.Action ev = (System.Action)DelegateTraits<System.Action>.Create(arg0.func);
				obj.onEnableEvent += ev;
			}
			else if (arg0.op == EventOp.Sub)
			{
				System.Action ev = (System.Action)LuaMisc.GetEventHandler(obj, typeof(GameFramework.MonoBase), "onEnableEvent");
				Delegate[] ds = ev.GetInvocationList();
				LuaState state = LuaState.Get(L);

				for (int i = 0; i < ds.Length; i++)
				{
					ev = (System.Action)ds[i];
					LuaDelegate ld = ev.Target as LuaDelegate;

					if (ld != null && ld.func == arg0.func)
					{
						obj.onEnableEvent -= ev;
						state.DelayDispose(ld.func);
						break;
					}
				}

				arg0.func.Dispose();
			}

			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onDisableEvent(IntPtr L)
	{
		try
		{
			GameFramework.MonoBase obj = (GameFramework.MonoBase)ToLua.CheckObject(L, 1, typeof(GameFramework.MonoBase));
			EventObject arg0 = null;

			if (LuaDLL.lua_isuserdata(L, 2) != 0)
			{
				arg0 = (EventObject)ToLua.ToObject(L, 2);
			}
			else
			{
				return LuaDLL.luaL_throw(L, "The event 'GameFramework.MonoBase.onDisableEvent' can only appear on the left hand side of += or -= when used outside of the type 'GameFramework.MonoBase'");
			}

			if (arg0.op == EventOp.Add)
			{
				System.Action ev = (System.Action)DelegateTraits<System.Action>.Create(arg0.func);
				obj.onDisableEvent += ev;
			}
			else if (arg0.op == EventOp.Sub)
			{
				System.Action ev = (System.Action)LuaMisc.GetEventHandler(obj, typeof(GameFramework.MonoBase), "onDisableEvent");
				Delegate[] ds = ev.GetInvocationList();
				LuaState state = LuaState.Get(L);

				for (int i = 0; i < ds.Length; i++)
				{
					ev = (System.Action)ds[i];
					LuaDelegate ld = ev.Target as LuaDelegate;

					if (ld != null && ld.func == arg0.func)
					{
						obj.onDisableEvent -= ev;
						state.DelayDispose(ld.func);
						break;
					}
				}

				arg0.func.Dispose();
			}

			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onUpdateEvent(IntPtr L)
	{
		try
		{
			GameFramework.MonoBase obj = (GameFramework.MonoBase)ToLua.CheckObject(L, 1, typeof(GameFramework.MonoBase));
			EventObject arg0 = null;

			if (LuaDLL.lua_isuserdata(L, 2) != 0)
			{
				arg0 = (EventObject)ToLua.ToObject(L, 2);
			}
			else
			{
				return LuaDLL.luaL_throw(L, "The event 'GameFramework.MonoBase.onUpdateEvent' can only appear on the left hand side of += or -= when used outside of the type 'GameFramework.MonoBase'");
			}

			if (arg0.op == EventOp.Add)
			{
				System.Action ev = (System.Action)DelegateTraits<System.Action>.Create(arg0.func);
				obj.onUpdateEvent += ev;
			}
			else if (arg0.op == EventOp.Sub)
			{
				System.Action ev = (System.Action)LuaMisc.GetEventHandler(obj, typeof(GameFramework.MonoBase), "onUpdateEvent");
				Delegate[] ds = ev.GetInvocationList();
				LuaState state = LuaState.Get(L);

				for (int i = 0; i < ds.Length; i++)
				{
					ev = (System.Action)ds[i];
					LuaDelegate ld = ev.Target as LuaDelegate;

					if (ld != null && ld.func == arg0.func)
					{
						obj.onUpdateEvent -= ev;
						state.DelayDispose(ld.func);
						break;
					}
				}

				arg0.func.Dispose();
			}

			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onDestroyEvent(IntPtr L)
	{
		try
		{
			GameFramework.MonoBase obj = (GameFramework.MonoBase)ToLua.CheckObject(L, 1, typeof(GameFramework.MonoBase));
			EventObject arg0 = null;

			if (LuaDLL.lua_isuserdata(L, 2) != 0)
			{
				arg0 = (EventObject)ToLua.ToObject(L, 2);
			}
			else
			{
				return LuaDLL.luaL_throw(L, "The event 'GameFramework.MonoBase.onDestroyEvent' can only appear on the left hand side of += or -= when used outside of the type 'GameFramework.MonoBase'");
			}

			if (arg0.op == EventOp.Add)
			{
				System.Action ev = (System.Action)DelegateTraits<System.Action>.Create(arg0.func);
				obj.onDestroyEvent += ev;
			}
			else if (arg0.op == EventOp.Sub)
			{
				System.Action ev = (System.Action)LuaMisc.GetEventHandler(obj, typeof(GameFramework.MonoBase), "onDestroyEvent");
				Delegate[] ds = ev.GetInvocationList();
				LuaState state = LuaState.Get(L);

				for (int i = 0; i < ds.Length; i++)
				{
					ev = (System.Action)ds[i];
					LuaDelegate ld = ev.Target as LuaDelegate;

					if (ld != null && ld.func == arg0.func)
					{
						obj.onDestroyEvent -= ev;
						state.DelayDispose(ld.func);
						break;
					}
				}

				arg0.func.Dispose();
			}

			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}
