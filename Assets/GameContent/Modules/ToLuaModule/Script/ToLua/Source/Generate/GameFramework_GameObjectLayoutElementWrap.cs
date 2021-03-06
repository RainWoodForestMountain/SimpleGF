﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class GameFramework_GameObjectLayoutElementWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(GameFramework.GameObjectLayoutElement), typeof(UnityEngine.MonoBehaviour));
		L.RegFunction("PrefactSize", PrefactSize);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("LayoutSize", get_LayoutSize, set_LayoutSize);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PrefactSize(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			GameFramework.GameObjectLayoutElement obj = (GameFramework.GameObjectLayoutElement)ToLua.CheckObject<GameFramework.GameObjectLayoutElement>(L, 1);
			obj.PrefactSize();
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
	static int get_LayoutSize(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.GameObjectLayoutElement obj = (GameFramework.GameObjectLayoutElement)o;
			UnityEngine.Vector3 ret = obj.LayoutSize;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index LayoutSize on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_LayoutSize(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.GameObjectLayoutElement obj = (GameFramework.GameObjectLayoutElement)o;
			UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
			obj.LayoutSize = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index LayoutSize on a nil value");
		}
	}
}

