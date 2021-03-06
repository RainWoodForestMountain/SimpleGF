﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class GameFramework_GameObjectLayoutGridGroupWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(GameFramework.GameObjectLayoutGridGroup), typeof(UnityEngine.MonoBehaviour));
		L.RegFunction("Refresh", Refresh);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("ChildAlignmentXAxis", get_ChildAlignmentXAxis, set_ChildAlignmentXAxis);
		L.RegVar("ChildAlignmentYAxis", get_ChildAlignmentYAxis, set_ChildAlignmentYAxis);
		L.RegVar("EnableXAxisInverse", get_EnableXAxisInverse, set_EnableXAxisInverse);
		L.RegVar("EnableYAxisInverse", get_EnableYAxisInverse, set_EnableYAxisInverse);
		L.RegVar("EnableZAxisInverse", get_EnableZAxisInverse, set_EnableZAxisInverse);
		L.RegVar("XAxisLayoutElementCount", get_XAxisLayoutElementCount, set_XAxisLayoutElementCount);
		L.RegVar("YAxisLayoutElementCount", get_YAxisLayoutElementCount, set_YAxisLayoutElementCount);
		L.RegVar("XAxisSpace", get_XAxisSpace, set_XAxisSpace);
		L.RegVar("YAxisSpace", get_YAxisSpace, set_YAxisSpace);
		L.RegVar("ZAxisSpace", get_ZAxisSpace, set_ZAxisSpace);
		L.RegVar("LayoutSize", get_LayoutSize, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Refresh(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			GameFramework.GameObjectLayoutGridGroup obj = (GameFramework.GameObjectLayoutGridGroup)ToLua.CheckObject<GameFramework.GameObjectLayoutGridGroup>(L, 1);
			obj.Refresh();
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
	static int get_ChildAlignmentXAxis(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.GameObjectLayoutGridGroup obj = (GameFramework.GameObjectLayoutGridGroup)o;
			GameFramework.GameObjectLayoutGridGroup.LayoutAlignmentXAxis ret = obj.ChildAlignmentXAxis;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index ChildAlignmentXAxis on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ChildAlignmentYAxis(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.GameObjectLayoutGridGroup obj = (GameFramework.GameObjectLayoutGridGroup)o;
			GameFramework.GameObjectLayoutGridGroup.LayoutAlignmentYAxis ret = obj.ChildAlignmentYAxis;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index ChildAlignmentYAxis on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EnableXAxisInverse(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.GameObjectLayoutGridGroup obj = (GameFramework.GameObjectLayoutGridGroup)o;
			bool ret = obj.EnableXAxisInverse;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index EnableXAxisInverse on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EnableYAxisInverse(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.GameObjectLayoutGridGroup obj = (GameFramework.GameObjectLayoutGridGroup)o;
			bool ret = obj.EnableYAxisInverse;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index EnableYAxisInverse on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EnableZAxisInverse(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.GameObjectLayoutGridGroup obj = (GameFramework.GameObjectLayoutGridGroup)o;
			bool ret = obj.EnableZAxisInverse;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index EnableZAxisInverse on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_XAxisLayoutElementCount(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.GameObjectLayoutGridGroup obj = (GameFramework.GameObjectLayoutGridGroup)o;
			int ret = obj.XAxisLayoutElementCount;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index XAxisLayoutElementCount on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_YAxisLayoutElementCount(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.GameObjectLayoutGridGroup obj = (GameFramework.GameObjectLayoutGridGroup)o;
			int ret = obj.YAxisLayoutElementCount;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index YAxisLayoutElementCount on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_XAxisSpace(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.GameObjectLayoutGridGroup obj = (GameFramework.GameObjectLayoutGridGroup)o;
			float ret = obj.XAxisSpace;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index XAxisSpace on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_YAxisSpace(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.GameObjectLayoutGridGroup obj = (GameFramework.GameObjectLayoutGridGroup)o;
			float ret = obj.YAxisSpace;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index YAxisSpace on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ZAxisSpace(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.GameObjectLayoutGridGroup obj = (GameFramework.GameObjectLayoutGridGroup)o;
			float ret = obj.ZAxisSpace;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index ZAxisSpace on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LayoutSize(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.GameObjectLayoutGridGroup obj = (GameFramework.GameObjectLayoutGridGroup)o;
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
	static int set_ChildAlignmentXAxis(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.GameObjectLayoutGridGroup obj = (GameFramework.GameObjectLayoutGridGroup)o;
			GameFramework.GameObjectLayoutGridGroup.LayoutAlignmentXAxis arg0 = (GameFramework.GameObjectLayoutGridGroup.LayoutAlignmentXAxis)ToLua.CheckObject(L, 2, typeof(GameFramework.GameObjectLayoutGridGroup.LayoutAlignmentXAxis));
			obj.ChildAlignmentXAxis = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index ChildAlignmentXAxis on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ChildAlignmentYAxis(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.GameObjectLayoutGridGroup obj = (GameFramework.GameObjectLayoutGridGroup)o;
			GameFramework.GameObjectLayoutGridGroup.LayoutAlignmentYAxis arg0 = (GameFramework.GameObjectLayoutGridGroup.LayoutAlignmentYAxis)ToLua.CheckObject(L, 2, typeof(GameFramework.GameObjectLayoutGridGroup.LayoutAlignmentYAxis));
			obj.ChildAlignmentYAxis = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index ChildAlignmentYAxis on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_EnableXAxisInverse(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.GameObjectLayoutGridGroup obj = (GameFramework.GameObjectLayoutGridGroup)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.EnableXAxisInverse = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index EnableXAxisInverse on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_EnableYAxisInverse(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.GameObjectLayoutGridGroup obj = (GameFramework.GameObjectLayoutGridGroup)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.EnableYAxisInverse = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index EnableYAxisInverse on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_EnableZAxisInverse(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.GameObjectLayoutGridGroup obj = (GameFramework.GameObjectLayoutGridGroup)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.EnableZAxisInverse = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index EnableZAxisInverse on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_XAxisLayoutElementCount(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.GameObjectLayoutGridGroup obj = (GameFramework.GameObjectLayoutGridGroup)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.XAxisLayoutElementCount = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index XAxisLayoutElementCount on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_YAxisLayoutElementCount(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.GameObjectLayoutGridGroup obj = (GameFramework.GameObjectLayoutGridGroup)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.YAxisLayoutElementCount = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index YAxisLayoutElementCount on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_XAxisSpace(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.GameObjectLayoutGridGroup obj = (GameFramework.GameObjectLayoutGridGroup)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.XAxisSpace = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index XAxisSpace on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_YAxisSpace(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.GameObjectLayoutGridGroup obj = (GameFramework.GameObjectLayoutGridGroup)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.YAxisSpace = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index YAxisSpace on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ZAxisSpace(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GameFramework.GameObjectLayoutGridGroup obj = (GameFramework.GameObjectLayoutGridGroup)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.ZAxisSpace = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index ZAxisSpace on a nil value");
		}
	}
}

