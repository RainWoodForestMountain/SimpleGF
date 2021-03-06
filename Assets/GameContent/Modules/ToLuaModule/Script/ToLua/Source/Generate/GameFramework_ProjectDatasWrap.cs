﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class GameFramework_ProjectDatasWrap
{
	public static void Register(LuaState L)
	{
		L.BeginStaticLibs("ProjectDatas");
		L.RegVar("CODEKEY", get_CODEKEY, set_CODEKEY);
		L.RegVar("LOG_SYMBOL", get_LOG_SYMBOL, null);
		L.RegVar("NAME_MANIFESET_FILE", get_NAME_MANIFESET_FILE, null);
		L.RegVar("NAME_ASSETBUNDLE_EXTENSION", get_NAME_ASSETBUNDLE_EXTENSION, null);
		L.RegVar("NAME_MANIFEST_EXTENSION", get_NAME_MANIFEST_EXTENSION, null);
		L.RegVar("NAME_RUNNING_TIME_DATA", get_NAME_RUNNING_TIME_DATA, null);
		L.RegVar("NAME_CHANNEL_FILE", get_NAME_CHANNEL_FILE, null);
		L.RegVar("NAME_ASSET_BUNDLE_INFO_FILE", get_NAME_ASSET_BUNDLE_INFO_FILE, null);
		L.RegVar("NAME_ASSET_BUNDLE_VERSION_FILE", get_NAME_ASSET_BUNDLE_VERSION_FILE, null);
		L.RegVar("NAME_ASSET_BUNDLE_TOTAL_FILE", get_NAME_ASSET_BUNDLE_TOTAL_FILE, null);
		L.RegVar("NAME_TEMP_VOICE_FILE", get_NAME_TEMP_VOICE_FILE, null);
		L.RegVar("PATH_CONFIG_NODE", get_PATH_CONFIG_NODE, null);
		L.RegVar("PATH_FRAMEWORK_NODE", get_PATH_FRAMEWORK_NODE, null);
		L.RegVar("PATH_ASSETS_NODE", get_PATH_ASSETS_NODE, null);
		L.RegVar("PATH_COMMON_NODE", get_PATH_COMMON_NODE, null);
		L.RegVar("PATH_CACHE_NODE", get_PATH_CACHE_NODE, null);
		L.RegVar("PATH_CACHE_SPRITE", get_PATH_CACHE_SPRITE, null);
		L.RegVar("EDITOR_ASSET_SAVE_ROOT", get_EDITOR_ASSET_SAVE_ROOT, null);
		L.RegVar("EDITOR_ASSET_SAVE_CURRENT_ROOT", get_EDITOR_ASSET_SAVE_CURRENT_ROOT, null);
		L.RegVar("EDITOR_ASSET_SAVE_TEMP_ROOT", get_EDITOR_ASSET_SAVE_TEMP_ROOT, null);
		L.RegVar("EDITOR_ASSET_MODEL_FILE", get_EDITOR_ASSET_MODEL_FILE, null);
		L.RegVar("EDITOR_MODULE_ROOT", get_EDITOR_MODULE_ROOT, null);
		L.RegVar("EDITOR_GAME_CONTENT_ROOT", get_EDITOR_GAME_CONTENT_ROOT, null);
		L.RegVar("EDITOR_RESOURCE_ROOT", get_EDITOR_RESOURCE_ROOT, null);
		L.RegVar("openUIOperate", get_openUIOperate, set_openUIOperate);
		L.RegVar("PATH_PLATFORM_NODE", get_PATH_PLATFORM_NODE, null);
		L.RegVar("PATH_CACHE", get_PATH_CACHE, null);
		L.RegVar("PATH_ASSETS", get_PATH_ASSETS, null);
		L.RegVar("PATH_CACHE_PERSISTENT", get_PATH_CACHE_PERSISTENT, null);
		L.RegVar("PATH_CACHE_STREAMING", get_PATH_CACHE_STREAMING, null);
		L.RegVar("EDITOR_CODE_MODEL_PATH", get_EDITOR_CODE_MODEL_PATH, null);
		L.RegVar("isDebug", get_isDebug, null);
		L.EndStaticLibs();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_CODEKEY(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.CODEKEY);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LOG_SYMBOL(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.LOG_SYMBOL);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_NAME_MANIFESET_FILE(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.NAME_MANIFESET_FILE);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_NAME_ASSETBUNDLE_EXTENSION(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.NAME_ASSETBUNDLE_EXTENSION);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_NAME_MANIFEST_EXTENSION(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.NAME_MANIFEST_EXTENSION);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_NAME_RUNNING_TIME_DATA(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.NAME_RUNNING_TIME_DATA);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_NAME_CHANNEL_FILE(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.NAME_CHANNEL_FILE);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_NAME_ASSET_BUNDLE_INFO_FILE(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.NAME_ASSET_BUNDLE_INFO_FILE);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_NAME_ASSET_BUNDLE_VERSION_FILE(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.NAME_ASSET_BUNDLE_VERSION_FILE);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_NAME_ASSET_BUNDLE_TOTAL_FILE(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.NAME_ASSET_BUNDLE_TOTAL_FILE);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_NAME_TEMP_VOICE_FILE(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.NAME_TEMP_VOICE_FILE);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PATH_CONFIG_NODE(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.PATH_CONFIG_NODE);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PATH_FRAMEWORK_NODE(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.PATH_FRAMEWORK_NODE);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PATH_ASSETS_NODE(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.PATH_ASSETS_NODE);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PATH_COMMON_NODE(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.PATH_COMMON_NODE);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PATH_CACHE_NODE(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.PATH_CACHE_NODE);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PATH_CACHE_SPRITE(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.PATH_CACHE_SPRITE);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EDITOR_ASSET_SAVE_ROOT(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.EDITOR_ASSET_SAVE_ROOT);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EDITOR_ASSET_SAVE_CURRENT_ROOT(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.EDITOR_ASSET_SAVE_CURRENT_ROOT);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EDITOR_ASSET_SAVE_TEMP_ROOT(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.EDITOR_ASSET_SAVE_TEMP_ROOT);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EDITOR_ASSET_MODEL_FILE(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.EDITOR_ASSET_MODEL_FILE);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EDITOR_MODULE_ROOT(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.EDITOR_MODULE_ROOT);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EDITOR_GAME_CONTENT_ROOT(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.EDITOR_GAME_CONTENT_ROOT);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EDITOR_RESOURCE_ROOT(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.EDITOR_RESOURCE_ROOT);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_openUIOperate(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushboolean(L, GameFramework.ProjectDatas.openUIOperate);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PATH_PLATFORM_NODE(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.PATH_PLATFORM_NODE);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PATH_CACHE(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.PATH_CACHE);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PATH_ASSETS(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.PATH_ASSETS);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PATH_CACHE_PERSISTENT(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.PATH_CACHE_PERSISTENT);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PATH_CACHE_STREAMING(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.PATH_CACHE_STREAMING);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EDITOR_CODE_MODEL_PATH(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushstring(L, GameFramework.ProjectDatas.EDITOR_CODE_MODEL_PATH);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isDebug(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushboolean(L, GameFramework.ProjectDatas.isDebug);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_CODEKEY(IntPtr L)
	{
		try
		{
			string arg0 = ToLua.CheckString(L, 2);
			GameFramework.ProjectDatas.CODEKEY = arg0;
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_openUIOperate(IntPtr L)
	{
		try
		{
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			GameFramework.ProjectDatas.openUIOperate = arg0;
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

