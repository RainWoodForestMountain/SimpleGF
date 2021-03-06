﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class GameFramework_ChannelDataWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(GameFramework.ChannelData), typeof(System.Object));
		L.RegFunction("Refresh", Refresh);
		L.RegFunction("SetByBundleid", SetByBundleid);
		L.RegFunction("SetChannelByIndex", SetChannelByIndex);
		L.RegFunction("New", _CreateGameFramework_ChannelData);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("current", get_current, set_current);
		L.RegVar("channels", get_channels, set_channels);
		L.RegVar("channel", get_channel, set_channel);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateGameFramework_ChannelData(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				GameFramework.ChannelData obj = new GameFramework.ChannelData();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: GameFramework.ChannelData.New");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Refresh(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 0);
			GameFramework.ChannelData.Refresh();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetByBundleid(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string arg0 = ToLua.CheckString(L, 1);
			GameFramework.ChannelData.SetByBundleid(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetChannelByIndex(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 1);
			GameFramework.ChannelData.SetChannelByIndex(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_current(IntPtr L)
	{
		try
		{
			ToLua.PushObject(L, GameFramework.ChannelData.current);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_channels(IntPtr L)
	{
		try
		{
			ToLua.Push(L, GameFramework.ChannelData.channels);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_channel(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ChannelData.channel);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_current(IntPtr L)
	{
		try
		{
			GameFramework.ChannelDataPackage arg0 = (GameFramework.ChannelDataPackage)ToLua.CheckObject<GameFramework.ChannelDataPackage>(L, 2);
			GameFramework.ChannelData.current = arg0;
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_channels(IntPtr L)
	{
		try
		{
			string[] arg0 = ToLua.CheckStringArray(L, 2);
			GameFramework.ChannelData.channels = arg0;
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_channel(IntPtr L)
	{
		try
		{
			string arg0 = ToLua.CheckString(L, 2);
			GameFramework.ChannelData.channel = arg0;
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

