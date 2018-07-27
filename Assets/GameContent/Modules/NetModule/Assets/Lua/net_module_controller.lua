require("net_module_connect")
require("net_module_protocol")
require("net_connect_check")

net_module_controller = {}

function net_module_controller.New(_module)
    local this = lua_controller_base.New(_module)
    local base = this.__index
    local heartid = nil
    local protocol = net_module_protocol.New()
    this.controllers = {}
    this.connect = net_module_connect.New(this)

    function this.Init()
        if base.Init() then
            return
        end
        protocol.Init()
        net_connect_check.Init()
        if not heartid then
            heartid = utils.CreateTimer(this.Heart, -1, 10)
        end
    end
    function this.Destroy()
        utils.RemoveTimer(heartid)
        net_connect_check.Destroy()
        this.connect.Destroy()
    end

    --注册命令表
    function this.ServerCmdRegist(_cmds)
        this.data.SetCmdList(_cmds)
    end
    --请求服务器
    function this.ServerRequested(_content)
        if _content == "Auth" then
            this.Auth()
            return
        end
        local _bb = protocol.Eecode(_content)
        lua_bridge.Recevive(CommonKey.SERVER_HALL, MessageType.ServerRequest, _bb)
    end
    --当服务器数据到达，注意这里接收的是ByteBuffer结构
    function this.ServerResponse(_content)
        local _tab = protocol.Decode(_content)
        --特殊心跳消息
        if _tab.id == 100003 then
            this.HeartBack(_tab)
            return
        end
        if _tab.id == 100001 then
            utils.Log("<color=green>认证返回", _tab.result, "</color>")
            return
        end
        utils.Print(_tab)

        --账号重复登录
        if _tab.result == "NEEDLOGIN" then
            utils.OpenDialog("账号重复登录！该账号已被强制下线!\n如果不是本人操作，请及时联系客服！", nil, nil, 1)
            events.Brocast(CreateMessage(MessageType.GlobalLogout))
            return
        end

        _tab.isSuccess = _tab.result == "OK" or _tab.result == "" or _tab.result == nil
        if not _tab.isSuccess then
            utils.Log("<color=red>服务器返回错误信息：", _tab.result, "</color>")
            if _tab.result == "MONEY" then
                utils.OpenFloatTips("所持金额不足！")
            end
        end
        this.module.OnServerResponse(_tab)
    end
    --连接服务器
    function this.ServerConnect(_content)
        this.ServerClose()
        lua_bridge.Recevive(nil, MessageType.ServerConnect, json.encode(_content))
    end
    function this.ServerClose()
        lua_bridge.Recevive(nil, MessageType.ServerClose, json.encode({name = CommonKey.SERVER_HALL}))
    end

    --认证
    function this.Auth()
        utils.Log("<color=green>发送认证</color>")
        local _byteBuffer = ByteBuffer.New()
        _byteBuffer:WriteInt(100000)
        _byteBuffer:WriteInt(0)
        _byteBuffer:WriteBytes(protobuf.encode("fr_common.AuthReq", {password="53A9F9114025B0028A209ECE6B4115B6", selfdescription = data_cache.mine and data_cache.mine.token or ""}))
        lua_bridge.Recevive(CommonKey.SERVER_HALL, MessageType.ServerRequest, _byteBuffer)
    end
    --心跳相关
    local pushCount = 0
    function this.Heart()
        local _byteBuffer = ByteBuffer.New()
        _byteBuffer:WriteInt(100002)
        _byteBuffer:WriteInt(0)
        _byteBuffer:WriteBytes(protobuf.encode("fr_common.HeartBeatReq", {}))
        lua_bridge.Recevive(CommonKey.SERVER_HALL, MessageType.ServerRequest, _byteBuffer)
        pushCount = pushCount + 1
        --五次发送心跳没有响应
        if pushCount == 5 then
            utils.LogWarning("<color=red>----------------------------心跳中断</color>")
            events.Brocast(CreateMessage(MessageType.ServerConnecting, nil, nil))
            events.Brocast(CreateMessage(MessageType.ReConnectServer, nil))
        end
    end
    function this.HeartBack(_tab)
        utils.LogWarning("<color=red>----------------------------心跳返回</color>")
        data_cache.serverTime = _tab.timenow
        pushCount = 0
    end

    return this
end

return net_module_controller