﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class GameFramework_MessageBody_StringWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(GameFramework.MessageBody_String), typeof(GameFramework.MessageBody));
		L.RegFunction("New", _CreateGameFramework_MessageBody_String);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("content", get_content, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateGameFramework_MessageBody_String(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2)
			{
				string arg0 = ToLua.CheckString(L, 1);
				string arg1 = ToLua.CheckString(L, 2);
				GameFramework.MessageBody_String obj = new GameFramework.MessageBody_String(arg0, arg1);
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: GameFramework.MessageBody_String.New");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_content(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.MessageBody_String obj = (GameFramework.MessageBody_String)o;
			string ret = obj.content;
			LuaDLL.lua_pushstring(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index content on a nil value");
		}
	}
}
