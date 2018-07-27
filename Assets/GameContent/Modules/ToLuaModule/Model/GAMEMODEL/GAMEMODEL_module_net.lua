require("gamebase_net")

GAMEMODEL_module_net = {}

function GAMEMODEL_module_net.New(_con, _data)
    local this = gamebase_net.New(_con, _data)
    local base = this.__index
    local data = _data
    local controller = _con

    function this.Init()
        events.AddListener(MessageType.ServerResponsed, this)
    end
    function this.Destroy()
        events.RemoveListener(MessageType.ServerResponsed, this)
    end

    function this.OnMessageCome(_msg)

    end

    
    return this
end

return GAMEMODEL_module_net