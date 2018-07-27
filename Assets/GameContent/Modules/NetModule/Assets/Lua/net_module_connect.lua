net_module_connect = {}

function net_module_connect.New(_net_c)
    local this = class_model.New()
    local net_controller = _net_c
    local url = nil

    function this.OnMessageCome(_msg)
        if _msg.msgType == MessageType.ServerConnecting then
            this.ServerConnecting(_msg.body.content)
        end
    end
    function this.Destroy()
        events.RemoveListener(MessageType.ServerConnecting, this)
    end
    --连接大厅
    function this.ServerConnecting(_url)
        url = _url or url
        --连接大厅服务器
        local _content = {}
        _content.type = "socket"
        _content.name = CommonKey.SERVER_HALL
        _content.ip = url
        utils.Log("<color=yellow>=========登录服务器=", url, "</color>")
        _content.port = 20002
        net_controller.ServerConnect(_content)
    end

    events.AddListener(MessageType.ServerConnecting, this)

    return this
end

return net_module_connect