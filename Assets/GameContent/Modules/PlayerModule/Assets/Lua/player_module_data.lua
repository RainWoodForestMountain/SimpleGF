player_module_data = {}

function player_module_data.New(_controller)
    local this = lua_data_base.New(_controller)
    local mine = player_data.New()

    function this.Clean()
        mine = player_data.New()
    end
    --玩家自身的数据
    function this.ChangeSelf(_data)
        mine = utils.CloneBy(mine, _data, true)
    end
    function this.GetSelf()
        return mine
    end

    return this
end

return player_module_data