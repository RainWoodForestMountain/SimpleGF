require("gamebase_data_seat")

GAMEMODEL_module_data_seat = {}

function GAMEMODEL_module_data_seat.New()
    local this = gamebase_data_seat.New()
    local base = this.__index

    --其他自定义内容写在这里
    function this.Clean()
        base.Clean()
    end
    --自定义内容结束
    
    return this
end

return GAMEMODEL_module_data_seat