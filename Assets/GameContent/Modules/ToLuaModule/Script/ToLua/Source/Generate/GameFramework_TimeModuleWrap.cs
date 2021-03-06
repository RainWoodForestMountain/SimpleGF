﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class GameFramework_TimeModuleWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(GameFramework.TimeModule), typeof(GameFramework.ModuleBase));
		L.RegFunction("Init", Init);
		L.RegFunction("Destroy", Destroy);
		L.RegFunction("Activate", Activate);
		L.RegFunction("Sleep", Sleep);
		L.RegFunction("SetTimeSpeed", SetTimeSpeed);
		L.RegFunction("Register", _Register);
		L.RegFunction("RegisterChain", RegisterChain);
		L.RegFunction("RemoveTimeNodeByID", RemoveTimeNodeByID);
		L.RegFunction("RunIEnumerator", RunIEnumerator);
		L.RegFunction("ChangedTimerSpecaTime", ChangedTimerSpecaTime);
		L.RegFunction("ChangedTimerLoopCount", ChangedTimerLoopCount);
		L.RegFunction("New", _CreateGameFramework_TimeModule);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("instance", get_instance, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateGameFramework_TimeModule(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				GameFramework.TimeModule obj = new GameFramework.TimeModule();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: GameFramework.TimeModule.New");
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
			ToLua.CheckArgsCount(L, 2);
			GameFramework.TimeModule obj = (GameFramework.TimeModule)ToLua.CheckObject<GameFramework.TimeModule>(L, 1);
			long arg0 = LuaDLL.tolua_checkint64(L, 2);
			obj.Init(arg0);
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
			GameFramework.TimeModule obj = (GameFramework.TimeModule)ToLua.CheckObject<GameFramework.TimeModule>(L, 1);
			obj.Destroy();
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
			GameFramework.TimeModule obj = (GameFramework.TimeModule)ToLua.CheckObject<GameFramework.TimeModule>(L, 1);
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
			GameFramework.TimeModule obj = (GameFramework.TimeModule)ToLua.CheckObject<GameFramework.TimeModule>(L, 1);
			obj.Sleep();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetTimeSpeed(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			GameFramework.TimeModule obj = (GameFramework.TimeModule)ToLua.CheckObject<GameFramework.TimeModule>(L, 1);
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.SetTimeSpeed(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Register(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2)
			{
				GameFramework.TimeModule obj = (GameFramework.TimeModule)ToLua.CheckObject<GameFramework.TimeModule>(L, 1);
				System.Action arg0 = (System.Action)ToLua.CheckDelegate<System.Action>(L, 2);
				long o = obj.Register(arg0);
				LuaDLL.tolua_pushint64(L, o);
				return 1;
			}
			else if (count == 3)
			{
				GameFramework.TimeModule obj = (GameFramework.TimeModule)ToLua.CheckObject<GameFramework.TimeModule>(L, 1);
				System.Action arg0 = (System.Action)ToLua.CheckDelegate<System.Action>(L, 2);
				float arg1 = (float)LuaDLL.luaL_checknumber(L, 3);
				long o = obj.Register(arg0, arg1);
				LuaDLL.tolua_pushint64(L, o);
				return 1;
			}
			else if (count == 4)
			{
				GameFramework.TimeModule obj = (GameFramework.TimeModule)ToLua.CheckObject<GameFramework.TimeModule>(L, 1);
				System.Action arg0 = (System.Action)ToLua.CheckDelegate<System.Action>(L, 2);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
				float arg2 = (float)LuaDLL.luaL_checknumber(L, 4);
				long o = obj.Register(arg0, arg1, arg2);
				LuaDLL.tolua_pushint64(L, o);
				return 1;
			}
			else if (count == 5)
			{
				GameFramework.TimeModule obj = (GameFramework.TimeModule)ToLua.CheckObject<GameFramework.TimeModule>(L, 1);
				System.Action arg0 = (System.Action)ToLua.CheckDelegate<System.Action>(L, 2);
				int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
				float arg2 = (float)LuaDLL.luaL_checknumber(L, 4);
				float arg3 = (float)LuaDLL.luaL_checknumber(L, 5);
				long o = obj.Register(arg0, arg1, arg2, arg3);
				LuaDLL.tolua_pushint64(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: GameFramework.TimeModule.Register");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RegisterChain(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 3)
			{
				GameFramework.TimeModule obj = (GameFramework.TimeModule)ToLua.CheckObject<GameFramework.TimeModule>(L, 1);
				System.Action[] arg0 = ToLua.CheckObjectArray<System.Action>(L, 2);
				float[] arg1 = ToLua.CheckNumberArray<float>(L, 3);
				long o = obj.RegisterChain(arg0, arg1);
				LuaDLL.tolua_pushint64(L, o);
				return 1;
			}
			else if (count == 4)
			{
				GameFramework.TimeModule obj = (GameFramework.TimeModule)ToLua.CheckObject<GameFramework.TimeModule>(L, 1);
				System.Action[] arg0 = ToLua.CheckObjectArray<System.Action>(L, 2);
				float[] arg1 = ToLua.CheckNumberArray<float>(L, 3);
				float arg2 = (float)LuaDLL.luaL_checknumber(L, 4);
				long o = obj.RegisterChain(arg0, arg1, arg2);
				LuaDLL.tolua_pushint64(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: GameFramework.TimeModule.RegisterChain");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveTimeNodeByID(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			GameFramework.TimeModule obj = (GameFramework.TimeModule)ToLua.CheckObject<GameFramework.TimeModule>(L, 1);
			long arg0 = LuaDLL.tolua_checkint64(L, 2);
			obj.RemoveTimeNodeByID(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RunIEnumerator(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			GameFramework.TimeModule obj = (GameFramework.TimeModule)ToLua.CheckObject<GameFramework.TimeModule>(L, 1);
			System.Collections.IEnumerator arg0 = ToLua.CheckIter(L, 2);
			obj.RunIEnumerator(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ChangedTimerSpecaTime(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			GameFramework.TimeModule obj = (GameFramework.TimeModule)ToLua.CheckObject<GameFramework.TimeModule>(L, 1);
			long arg0 = LuaDLL.tolua_checkint64(L, 2);
			float arg1 = (float)LuaDLL.luaL_checknumber(L, 3);
			obj.ChangedTimerSpecaTime(arg0, arg1);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ChangedTimerLoopCount(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			GameFramework.TimeModule obj = (GameFramework.TimeModule)ToLua.CheckObject<GameFramework.TimeModule>(L, 1);
			long arg0 = LuaDLL.tolua_checkint64(L, 2);
			int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
			obj.ChangedTimerLoopCount(arg0, arg1);
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
			ToLua.PushObject(L, GameFramework.TimeModule.instance);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

