--[[
    创建时间：2018.2.26
    创建人：伍霖峰
        注：lua端使用时，如果想要直接通过命令掉函数，命令值用字符串。若无需要，可以为任何值类型值
        若使用数字，值请不要重复，并且值大小在1000以后
]]
MessageType = setmetatable({}, { __index = __MessageType })

MessageType.CloseAllUIModule = "CloseAllUIModule"
MessageType.CloseAllImportentModule = "CloseAllImportentModule"
MessageType.ReConnectServer = "ReConnectServer"
MessageType.ExitCurrentGame = "ExitCurrentGame"

--data_cache中监听的刷新自身消息
MessageType.RefreshSelfPlayerData = "RefreshSelfPlayerData"
MessageType.RefreshGameDataCache = "RefreshGameDataCache"

--后缀为ed的消息类型为lua端发送给lua net module进行消息封装；不带ed的为lua net module传给C#端专用通道
MessageType.ServerRequested = "ServerRequested"
--后缀为ed的消息类型为lua接收到参数后进行分发;不带ed的为C#端传给lua net module专用通道
MessageType.ServerResponsed = "ServerResponsed"
--服务器数据拦截，在某条命令中取部分相关数据
MessageType.ServerDataIntercept = "ServerDataIntercept"
--连接服务器
MessageType.ServerConnecting = "ServerConnecting"

MessageType.CloseLoading = "CloseLoading"

MessageType.GlobalLogout = "GlobalLogout"

--刷新比赛报名信息
MessageType.RefreshMatchInfo = "RefreshMatchInfo"
--刷新实名信息
MessageType.RefreshPlayerRealName = "RefreshPlayerRealName"

return MessageType