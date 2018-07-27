﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class GameFramework_VideoPlayerEventWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(GameFramework.VideoPlayerEvent), typeof(GameFramework.MonoBase));
		L.RegFunction("SetPlayerEnd", SetPlayerEnd);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("IsPlayEnd", get_IsPlayEnd, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetPlayerEnd(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			GameFramework.VideoPlayerEvent obj = (GameFramework.VideoPlayerEvent)ToLua.CheckObject<GameFramework.VideoPlayerEvent>(L, 1);
			System.Action arg0 = (System.Action)ToLua.CheckDelegate<System.Action>(L, 2);
			obj.SetPlayerEnd(arg0);
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
	static int get_IsPlayEnd(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.VideoPlayerEvent obj = (GameFramework.VideoPlayerEvent)o;
			bool ret = obj.IsPlayEnd;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index IsPlayEnd on a nil value");
		}
	}
}

