﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class GameFramework_MessageModuleWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(GameFramework.MessageModule), typeof(GameFramework.ModuleBase));
		L.RegFunction("Init", Init);
		L.RegFunction("Destroy", Destroy);
		L.RegFunction("Recevive", Recevive);
		L.RegFunction("AddListener", AddListener);
		L.RegFunction("RemoveListener", RemoveListener);
		L.RegFunction("New", _CreateGameFramework_MessageModule);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("instance", get_instance, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateGameFramework_MessageModule(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				GameFramework.MessageModule obj = new GameFramework.MessageModule();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: GameFramework.MessageModule.New");
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
			GameFramework.MessageModule obj = (GameFramework.MessageModule)ToLua.CheckObject<GameFramework.MessageModule>(L, 1);
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
			GameFramework.MessageModule obj = (GameFramework.MessageModule)ToLua.CheckObject<GameFramework.MessageModule>(L, 1);
			obj.Destroy();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Recevive(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2)
			{
				GameFramework.MessageModule obj = (GameFramework.MessageModule)ToLua.CheckObject<GameFramework.MessageModule>(L, 1);
				GameFramework.Message arg0 = StackTraits<GameFramework.Message>.Check(L, 2);
				obj.Recevive(arg0);
				return 0;
			}
			else if (count == 3 && TypeChecker.CheckTypes<int, GameFramework.IMessageBody>(L, 2))
			{
				GameFramework.MessageModule obj = (GameFramework.MessageModule)ToLua.CheckObject<GameFramework.MessageModule>(L, 1);
				int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
				GameFramework.IMessageBody arg1 = (GameFramework.IMessageBody)ToLua.ToObject(L, 3);
				obj.Recevive(arg0, arg1);
				return 0;
			}
			else if (count == 3 && TypeChecker.CheckTypes<GameFramework.MessageType, GameFramework.IMessageBody>(L, 2))
			{
				GameFramework.MessageModule obj = (GameFramework.MessageModule)ToLua.CheckObject<GameFramework.MessageModule>(L, 1);
				GameFramework.MessageType arg0 = (GameFramework.MessageType)ToLua.ToObject(L, 2);
				GameFramework.IMessageBody arg1 = (GameFramework.IMessageBody)ToLua.ToObject(L, 3);
				obj.Recevive(arg0, arg1);
				return 0;
			}
			else if (count == 4 && TypeChecker.CheckTypes<string, GameFramework.MessageType, GameFramework.ByteBuffer>(L, 2))
			{
				GameFramework.MessageModule obj = (GameFramework.MessageModule)ToLua.CheckObject<GameFramework.MessageModule>(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				GameFramework.MessageType arg1 = (GameFramework.MessageType)ToLua.ToObject(L, 3);
				GameFramework.ByteBuffer arg2 = (GameFramework.ByteBuffer)ToLua.ToObject(L, 4);
				obj.Recevive(arg0, arg1, arg2);
				return 0;
			}
			else if (count == 4 && TypeChecker.CheckTypes<string, int, GameFramework.ByteBuffer>(L, 2))
			{
				GameFramework.MessageModule obj = (GameFramework.MessageModule)ToLua.CheckObject<GameFramework.MessageModule>(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
				GameFramework.ByteBuffer arg2 = (GameFramework.ByteBuffer)ToLua.ToObject(L, 4);
				obj.Recevive(arg0, arg1, arg2);
				return 0;
			}
			else if (count == 4 && TypeChecker.CheckTypes<long, GameFramework.MessageType, UnityEngine.GameObject>(L, 2))
			{
				GameFramework.MessageModule obj = (GameFramework.MessageModule)ToLua.CheckObject<GameFramework.MessageModule>(L, 1);
				long arg0 = LuaDLL.tolua_toint64(L, 2);
				GameFramework.MessageType arg1 = (GameFramework.MessageType)ToLua.ToObject(L, 3);
				UnityEngine.GameObject arg2 = (UnityEngine.GameObject)ToLua.ToObject(L, 4);
				obj.Recevive(arg0, arg1, arg2);
				return 0;
			}
			else if (count == 4 && TypeChecker.CheckTypes<long, int, UnityEngine.GameObject>(L, 2))
			{
				GameFramework.MessageModule obj = (GameFramework.MessageModule)ToLua.CheckObject<GameFramework.MessageModule>(L, 1);
				long arg0 = LuaDLL.tolua_toint64(L, 2);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
				UnityEngine.GameObject arg2 = (UnityEngine.GameObject)ToLua.ToObject(L, 4);
				obj.Recevive(arg0, arg1, arg2);
				return 0;
			}
			else if (count == 4 && TypeChecker.CheckTypes<string, GameFramework.MessageType, UnityEngine.GameObject>(L, 2))
			{
				GameFramework.MessageModule obj = (GameFramework.MessageModule)ToLua.CheckObject<GameFramework.MessageModule>(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				GameFramework.MessageType arg1 = (GameFramework.MessageType)ToLua.ToObject(L, 3);
				UnityEngine.GameObject arg2 = (UnityEngine.GameObject)ToLua.ToObject(L, 4);
				obj.Recevive(arg0, arg1, arg2);
				return 0;
			}
			else if (count == 4 && TypeChecker.CheckTypes<string, int, string>(L, 2))
			{
				GameFramework.MessageModule obj = (GameFramework.MessageModule)ToLua.CheckObject<GameFramework.MessageModule>(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
				string arg2 = ToLua.ToString(L, 4);
				obj.Recevive(arg0, arg1, arg2);
				return 0;
			}
			else if (count == 4 && TypeChecker.CheckTypes<System.Type, GameFramework.MessageType, string>(L, 2))
			{
				GameFramework.MessageModule obj = (GameFramework.MessageModule)ToLua.CheckObject<GameFramework.MessageModule>(L, 1);
				System.Type arg0 = (System.Type)ToLua.ToObject(L, 2);
				GameFramework.MessageType arg1 = (GameFramework.MessageType)ToLua.ToObject(L, 3);
				string arg2 = ToLua.ToString(L, 4);
				obj.Recevive(arg0, arg1, arg2);
				return 0;
			}
			else if (count == 4 && TypeChecker.CheckTypes<System.Type, int, string>(L, 2))
			{
				GameFramework.MessageModule obj = (GameFramework.MessageModule)ToLua.CheckObject<GameFramework.MessageModule>(L, 1);
				System.Type arg0 = (System.Type)ToLua.ToObject(L, 2);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
				string arg2 = ToLua.ToString(L, 4);
				obj.Recevive(arg0, arg1, arg2);
				return 0;
			}
			else if (count == 4 && TypeChecker.CheckTypes<string, GameFramework.MessageType, string>(L, 2))
			{
				GameFramework.MessageModule obj = (GameFramework.MessageModule)ToLua.CheckObject<GameFramework.MessageModule>(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				GameFramework.MessageType arg1 = (GameFramework.MessageType)ToLua.ToObject(L, 3);
				string arg2 = ToLua.ToString(L, 4);
				obj.Recevive(arg0, arg1, arg2);
				return 0;
			}
			else if (count == 4 && TypeChecker.CheckTypes<string, int, UnityEngine.GameObject>(L, 2))
			{
				GameFramework.MessageModule obj = (GameFramework.MessageModule)ToLua.CheckObject<GameFramework.MessageModule>(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
				UnityEngine.GameObject arg2 = (UnityEngine.GameObject)ToLua.ToObject(L, 4);
				obj.Recevive(arg0, arg1, arg2);
				return 0;
			}
			else if (count == 4 && TypeChecker.CheckTypes<string, GameFramework.MessageType, UnityEngine.Object>(L, 2))
			{
				GameFramework.MessageModule obj = (GameFramework.MessageModule)ToLua.CheckObject<GameFramework.MessageModule>(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				GameFramework.MessageType arg1 = (GameFramework.MessageType)ToLua.ToObject(L, 3);
				UnityEngine.Object arg2 = (UnityEngine.Object)ToLua.ToObject(L, 4);
				obj.Recevive(arg0, arg1, arg2);
				return 0;
			}
			else if (count == 4 && TypeChecker.CheckTypes<string, int, UnityEngine.Object>(L, 2))
			{
				GameFramework.MessageModule obj = (GameFramework.MessageModule)ToLua.CheckObject<GameFramework.MessageModule>(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				int arg1 = (int)LuaDLL.lua_tonumber(L, 3);
				UnityEngine.Object arg2 = (UnityEngine.Object)ToLua.ToObject(L, 4);
				obj.Recevive(arg0, arg1, arg2);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: GameFramework.MessageModule.Recevive");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddListener(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 3 && TypeChecker.CheckTypes<GameFramework.MessageType, System.Action<GameFramework.Message>>(L, 2))
			{
				GameFramework.MessageModule obj = (GameFramework.MessageModule)ToLua.CheckObject<GameFramework.MessageModule>(L, 1);
				GameFramework.MessageType arg0 = (GameFramework.MessageType)ToLua.ToObject(L, 2);
				System.Action<GameFramework.Message> arg1 = (System.Action<GameFramework.Message>)ToLua.ToObject(L, 3);
				obj.AddListener(arg0, arg1);
				return 0;
			}
			else if (count == 3 && TypeChecker.CheckTypes<int, System.Action<GameFramework.Message>>(L, 2))
			{
				GameFramework.MessageModule obj = (GameFramework.MessageModule)ToLua.CheckObject<GameFramework.MessageModule>(L, 1);
				int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
				System.Action<GameFramework.Message> arg1 = (System.Action<GameFramework.Message>)ToLua.ToObject(L, 3);
				obj.AddListener(arg0, arg1);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: GameFramework.MessageModule.AddListener");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveListener(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 3 && TypeChecker.CheckTypes<GameFramework.MessageType, System.Action<GameFramework.Message>>(L, 2))
			{
				GameFramework.MessageModule obj = (GameFramework.MessageModule)ToLua.CheckObject<GameFramework.MessageModule>(L, 1);
				GameFramework.MessageType arg0 = (GameFramework.MessageType)ToLua.ToObject(L, 2);
				System.Action<GameFramework.Message> arg1 = (System.Action<GameFramework.Message>)ToLua.ToObject(L, 3);
				obj.RemoveListener(arg0, arg1);
				return 0;
			}
			else if (count == 3 && TypeChecker.CheckTypes<int, System.Action<GameFramework.Message>>(L, 2))
			{
				GameFramework.MessageModule obj = (GameFramework.MessageModule)ToLua.CheckObject<GameFramework.MessageModule>(L, 1);
				int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
				System.Action<GameFramework.Message> arg1 = (System.Action<GameFramework.Message>)ToLua.ToObject(L, 3);
				obj.RemoveListener(arg0, arg1);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: GameFramework.MessageModule.RemoveListener");
			}
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
			ToLua.PushObject(L, GameFramework.MessageModule.instance);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

