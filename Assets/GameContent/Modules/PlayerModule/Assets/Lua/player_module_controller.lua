require("player_module_data")

player_module_controller = {}

function player_module_controller.New(_module)
    local this = lua_controller_base.New(_module)

    function this.Init()
        if this.__index.Init() then
            return true
        end

        this.SetData(player_module_data.New(this))

        return false
    end

    function this.Clean()
        this.data.Clean()
    end
    function this.ChangeSelf(_data)
        this.data.ChangeSelf(_data)
        events.Brocast(CreateMessage(MessageType.RefreshSelfPlayerData, nil, this.GetSelf()))
    end
    function this.GetSelf()
        return utils.Clone(this.data.GetSelf())
    end

    return this
end

return player_module_controller
