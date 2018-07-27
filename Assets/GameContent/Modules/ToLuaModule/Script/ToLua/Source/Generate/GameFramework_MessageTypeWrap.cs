﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class GameFramework_MessageTypeWrap
{
	public static void Register(LuaState L)
	{
		L.BeginEnum(typeof(GameFramework.MessageType));
		L.RegVar("Normal", get_Normal, null);
		L.RegVar("ModuleOpen", get_ModuleOpen, null);
		L.RegVar("ModuleClose", get_ModuleClose, null);
		L.RegVar("ModuleActivate", get_ModuleActivate, null);
		L.RegVar("ModuleSleep", get_ModuleSleep, null);
		L.RegVar("EventButtonClick", get_EventButtonClick, null);
		L.RegVar("EventButtonDoubleClick", get_EventButtonDoubleClick, null);
		L.RegVar("EventOnTouchEnter", get_EventOnTouchEnter, null);
		L.RegVar("EventOnTouchExit", get_EventOnTouchExit, null);
		L.RegVar("EventOnTouchDown", get_EventOnTouchDown, null);
		L.RegVar("EventOnTouchUp", get_EventOnTouchUp, null);
		L.RegVar("EventBeginDrag", get_EventBeginDrag, null);
		L.RegVar("EventDragging", get_EventDragging, null);
		L.RegVar("EventEndDrag", get_EventEndDrag, null);
		L.RegVar("EventPressStart", get_EventPressStart, null);
		L.RegVar("EventPressEnd", get_EventPressEnd, null);
		L.RegVar("EventBeginSliding", get_EventBeginSliding, null);
		L.RegVar("EventEndSliding", get_EventEndSliding, null);
		L.RegVar("EventOnSliding", get_EventOnSliding, null);
		L.RegVar("ServerConnect", get_ServerConnect, null);
		L.RegVar("ServerRequest", get_ServerRequest, null);
		L.RegVar("ServerResponse", get_ServerResponse, null);
		L.RegVar("ServerConnected", get_ServerConnected, null);
		L.RegVar("ServerDisconnect", get_ServerDisconnect, null);
		L.RegVar("ServerClose", get_ServerClose, null);
		L.RegVar("UILayersAddObject", get_UILayersAddObject, null);
		L.RegVar("UILayersRemoveObject", get_UILayersRemoveObject, null);
		L.RegVar("UILayersRefresh", get_UILayersRefresh, null);
		L.RegVar("OnMusicStateChanged", get_OnMusicStateChanged, null);
		L.RegVar("PlayBGM", get_PlayBGM, null);
		L.RegVar("PlayAudio", get_PlayAudio, null);
		L.RegVar("StartHotupdate", get_StartHotupdate, null);
		L.RegVar("StartFileListHotupdate", get_StartFileListHotupdate, null);
		L.RegVar("OnHotupdateStep", get_OnHotupdateStep, null);
		L.RegVar("OnHotupdateComplete", get_OnHotupdateComplete, null);
		L.RegVar("OnHotupdateFailed", get_OnHotupdateFailed, null);
		L.RegVar("PlatformRequest", get_PlatformRequest, null);
		L.RegVar("PlatformResponse", get_PlatformResponse, null);
		L.RegVar("Custom", get_Custom, null);
		L.RegFunction("IntToEnum", IntToEnum);
		L.EndEnum();
		TypeTraits<GameFramework.MessageType>.Check = CheckType;
		StackTraits<GameFramework.MessageType>.Push = Push;
	}

	static void Push(IntPtr L, GameFramework.MessageType arg)
	{
		ToLua.Push(L, arg);
	}

	static bool CheckType(IntPtr L, int pos)
	{
		return TypeChecker.CheckEnumType(typeof(GameFramework.MessageType), L, pos);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Normal(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.Normal);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ModuleOpen(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.ModuleOpen);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ModuleClose(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.ModuleClose);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ModuleActivate(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.ModuleActivate);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ModuleSleep(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.ModuleSleep);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EventButtonClick(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.EventButtonClick);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EventButtonDoubleClick(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.EventButtonDoubleClick);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EventOnTouchEnter(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.EventOnTouchEnter);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EventOnTouchExit(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.EventOnTouchExit);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EventOnTouchDown(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.EventOnTouchDown);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EventOnTouchUp(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.EventOnTouchUp);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EventBeginDrag(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.EventBeginDrag);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EventDragging(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.EventDragging);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EventEndDrag(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.EventEndDrag);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EventPressStart(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.EventPressStart);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EventPressEnd(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.EventPressEnd);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EventBeginSliding(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.EventBeginSliding);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EventEndSliding(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.EventEndSliding);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EventOnSliding(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.EventOnSliding);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ServerConnect(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.ServerConnect);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ServerRequest(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.ServerRequest);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ServerResponse(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.ServerResponse);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ServerConnected(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.ServerConnected);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ServerDisconnect(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.ServerDisconnect);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ServerClose(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.ServerClose);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UILayersAddObject(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.UILayersAddObject);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UILayersRemoveObject(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.UILayersRemoveObject);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UILayersRefresh(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.UILayersRefresh);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OnMusicStateChanged(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.OnMusicStateChanged);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PlayBGM(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.PlayBGM);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PlayAudio(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.PlayAudio);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_StartHotupdate(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.StartHotupdate);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_StartFileListHotupdate(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.StartFileListHotupdate);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OnHotupdateStep(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.OnHotupdateStep);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OnHotupdateComplete(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.OnTotalHotupdateComplete);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OnHotupdateFailed(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.OnHotupdateFailed);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PlatformRequest(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.PlatformRequest);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_PlatformResponse(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.PlatformResponse);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Custom(IntPtr L)
	{
		ToLua.Push(L, GameFramework.MessageType.Custom);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		GameFramework.MessageType o = (GameFramework.MessageType)arg0;
		ToLua.Push(L, o);
		return 1;
	}
}
