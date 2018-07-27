namespace GameFramework
{
	public enum MessageType
	{
		Normal = 0,

        //模块操作消息
        ModuleOpen = 1,
        ModuleClose,
        ModuleActivate,
        ModuleSleep,

        //触发器消息
        EventButtonClick = 100,
        EventButtonDoubleClick = 101,
        EventOnTouchEnter = 102,
        EventOnTouchExit = 103,
        EventOnTouchDown = 104,
        EventOnTouchUp = 105,
        EventBeginDrag = 106,
        EventDragging = 107,
        EventEndDrag = 108,
        EventPressStart,
        EventPressEnd,
        EventBeginSliding,
        EventEndSliding,
        EventOnSliding,

        //服务器消息
        ServerConnect = 200,
        ServerRequest,
        ServerResponse,
        ServerConnected,
        ServerDisconnect,
        ServerClose,

        //UI物体操作消息
        UILayersAddObject = 300,
        UILayersRemoveObject,
        UILayersRefresh,

        //控制器消息
        OnMusicStateChanged = 400,
        PlayBGM,
        PlayAudio,

        //热更
        StartHotupdate = 500,
        StartFileListHotupdate,
        OnHotupdateStep,
        OnHotupdateFailed,
        OnTotalHotupdateComplete,

        //第三方
        PlatformRequest = 600,
        PlatformResponse,

        Custom = 1000,
	}
}