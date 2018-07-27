net_connect_check = {}

local onNetStop = false
local onConnecting = false
local loopId = nil
local url = nil
local netCheckSpaceTime = 2
local isInCheck = false

local function OnNoNet()
    isInCheck = false
    if not onNetStop then
        utils.OpenLoading("设备网络连接中断！")
        utils.SetRunningData(CommonKey.CURRENT_NET_STATE, "NO")
        onNetStop = true
    end
end
local function OnDelay(_delayCount)
    isInCheck = false
    if onNetStop and not onConnecting then
        onConnecting = true
        utils.CloseLoading()
        events.Brocast(CreateMessage(MessageType.ServerConnecting, nil, url))
        events.Brocast(CreateMessage(MessageType.ReConnectServer, nil))
    end
    if Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork then
        utils.SetRunningData(CommonKey.CURRENT_NET_STATE, "WIFI")
    else
        utils.SetRunningData(CommonKey.CURRENT_NET_STATE, "DATA")
    end
    --当前延迟 _delayCount
end
local function OnConnected(_msg)
    onNetStop = false
    onConnecting = false
    utils.Log("<color=green>", _msg.body.content, "</color>")
    events.Brocast(CreateMessage(MessageType.ServerRequested, nil, "Auth"))
end
local function OnDisconnect(_msg)
    if _msg.body.content == "OnDisconnected" then
        utils.OpenFloatTips("<color=yellow>服务器连接丢失！</color>")
    end
end
local function CheckNetConnect()
    if isInCheck then
        return
    end
    coroutine.start( function ()
        isInCheck = true
        local _ping = nil
        local _ac = 0
        local _to = 10
        local _spt = netCheckSpaceTime / _to
        if Application.internetReachability == NetworkReachability.NotReachable then
            --无网络
            OnNoNet()
            return
        end
        _ping = Ping.New("www.baidu.com")
        while not _ping.isDone do
            coroutine.wait (_spt)
            _ac = _ac + 1
            if _ac > _to then
                --ping失败，无网络
                OnNoNet()
                break
            end
        end
        if _ping.isDone then
            OnDelay(_ac)
        end
        _ping:DestroyPing()
    end )
end

function net_connect_check.Init()
    url = RunningTimeData.GetRunningData("DEFAULT_SERVER")

    events.AddListener(MessageType.ServerConnected, OnConnected)
    events.AddListener(MessageType.ServerDisconnect, OnDisconnect)
    events.Brocast(CreateMessage(MessageType.ServerConnecting, nil, url))

    loopId = utils.CreateTimer(CheckNetConnect, -1, netCheckSpaceTime)
end
function net_connect_check.Destroy()
    events.RemoveListener(MessageType.ServerConnected, OnConnected)
    events.RemoveListener(MessageType.ServerDisconnect, OnDisconnect)
    utils.RemoveTimer(loopId)
end

return net_connect_check