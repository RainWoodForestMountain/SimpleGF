require("gamebase_module_data")
require("GAMEMODEL_module_data_seat")

GAMEMODEL_module_data = {}

function GAMEMODEL_module_data.New(_controller)
    local this = gamebase_data.New(_controller)
    local base = this.__index

    --其他自定义内容写在这里
    function this.Clean()
        base.Clean()
    end
    --自定义内容结束
    
    return this
end

return GAMEMODEL_module_data
