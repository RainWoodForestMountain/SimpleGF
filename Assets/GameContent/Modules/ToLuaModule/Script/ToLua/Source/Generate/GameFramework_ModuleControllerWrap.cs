﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class GameFramework_ModuleControllerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(GameFramework.ModuleController), typeof(System.Object));
		L.RegFunction("Init", Init);
		L.RegFunction("Activate", Activate);
		L.RegFunction("Sleep", Sleep);
		L.RegFunction("Destroy", Destroy);
		L.RegFunction("New", _CreateGameFramework_ModuleController);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("instance", get_instance, set_instance);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateGameFramework_ModuleController(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				GameFramework.ModuleController obj = new GameFramework.ModuleController();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: GameFramework.ModuleController.New");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Init(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			GameFramework.ModuleController obj = (GameFramework.ModuleController)ToLua.CheckObject<GameFramework.ModuleController>(L, 1);
			obj.Init();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Activate(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			GameFramework.ModuleController obj = (GameFramework.ModuleController)ToLua.CheckObject<GameFramework.ModuleController>(L, 1);
			obj.Activate();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Sleep(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			GameFramework.ModuleController obj = (GameFramework.ModuleController)ToLua.CheckObject<GameFramework.ModuleController>(L, 1);
			obj.Sleep();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Destroy(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			GameFramework.ModuleController obj = (GameFramework.ModuleController)ToLua.CheckObject<GameFramework.ModuleController>(L, 1);
			obj.Destroy();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_instance(IntPtr L)
	{
		try
		{
			ToLua.PushObject(L, GameFramework.ModuleController.instance);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_instance(IntPtr L)
	{
		try
		{
			GameFramework.ModuleController arg0 = (GameFramework.ModuleController)ToLua.CheckObject<GameFramework.ModuleController>(L, 2);
			GameFramework.ModuleController.instance = arg0;
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

