require("net_module_controller")

net_module = {}

function net_module.New(_fileName, _moduleName)
    local this = lua_module_base.New(_fileName, "NetModule")
    this.__moduleType = lua_module_manager.moduleType.system

    function this.Init(_id)
        if this.__index.Init(_id) then
            this.Activate()
            return true
        end
        this.SetController(net_module_controller.New(this))

        --C#端通讯
        events.AddListener(MessageType.ServerResponse, this)
        --lua端通讯
        events.AddListener(MessageType.ServerRequested, this)

        return false
    end

    function this.Destroy()
        this.controller.Destroy()
        
        --lua端通讯
        events.RemoveListener(MessageType.ServerRequested, this)
        --C#端通讯
        events.RemoveListener(MessageType.ServerResponse, this)
    end

    function this.OnMessageCome(_msg)
        local _key = tostring(_msg.msgType)
        if this[_key] and type(this[_key]) == "function" then
            this[_key](_msg)
        end
    end
    function this.ServerRequested(_msg)
        assert(_msg.body.content, "消息体不能为空！")
        this.controller.ServerRequested(_msg.body.content)
    end
    function this.ServerResponse(_msg)
        assert(_msg.body.content, "消息体不能为空！")
        this.controller.ServerResponse(_msg.body.content)
    end

    function this.OnServerResponse(_content)
        --成功才会劫持
        if _content.isSuccess then
            events.Brocast(CreateMessage(MessageType.ServerDataIntercept, nil, _content))
        end
        events.Brocast(CreateMessage(MessageType.ServerResponsed, nil, _content))
    end

    return this
end

return net_module