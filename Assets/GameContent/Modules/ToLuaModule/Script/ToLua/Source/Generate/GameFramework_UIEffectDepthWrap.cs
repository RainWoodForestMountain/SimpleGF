﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class GameFramework_UIEffectDepthWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(GameFramework.UIEffectDepth), typeof(UnityEngine.MonoBehaviour));
		L.RegFunction("Refresh", Refresh);
		L.RegFunction("DestroySelf", DestroySelf);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("order", get_order, set_order);
		L.RegVar("relyon", get_relyon, set_relyon);
		L.RegVar("destroyObj", get_destroyObj, set_destroyObj);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Refresh(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1)
			{
				GameFramework.UIEffectDepth obj = (GameFramework.UIEffectDepth)ToLua.CheckObject<GameFramework.UIEffectDepth>(L, 1);
				obj.Refresh();
				return 0;
			}
			else if (count == 2)
			{
				GameFramework.UIEffectDepth obj = (GameFramework.UIEffectDepth)ToLua.CheckObject<GameFramework.UIEffectDepth>(L, 1);
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
				obj.Refresh(arg0);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: GameFramework.UIEffectDepth.Refresh");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DestroySelf(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			GameFramework.UIEffectDepth obj = (GameFramework.UIEffectDepth)ToLua.CheckObject<GameFramework.UIEffectDepth>(L, 1);
			obj.DestroySelf();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
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
	static int get_order(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.UIEffectDepth obj = (GameFramework.UIEffectDepth)o;
			int ret = obj.order;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index order on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_relyon(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.UIEffectDepth obj = (GameFramework.UIEffectDepth)o;
			GameFramework.UILayers ret = obj.relyon;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index relyon on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_destroyObj(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.UIEffectDepth obj = (GameFramework.UIEffectDepth)o;
			UnityEngine.GameObject ret = obj.destroyObj;
			ToLua.PushSealed(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index destroyObj on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_order(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.UIEffectDepth obj = (GameFramework.UIEffectDepth)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.order = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index order on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_relyon(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.UIEffectDepth obj = (GameFramework.UIEffectDepth)o;
			GameFramework.UILayers arg0 = (GameFramework.UILayers)ToLua.CheckObject(L, 2, typeof(GameFramework.UILayers));
			obj.relyon = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index relyon on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_destroyObj(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.UIEffectDepth obj = (GameFramework.UIEffectDepth)o;
			UnityEngine.GameObject arg0 = (UnityEngine.GameObject)ToLua.CheckObject(L, 2, typeof(UnityEngine.GameObject));
			obj.destroyObj = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index destroyObj on a nil value");
		}
	}
}

