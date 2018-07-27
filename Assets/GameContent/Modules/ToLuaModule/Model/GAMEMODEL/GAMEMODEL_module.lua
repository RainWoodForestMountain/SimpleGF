--[[
    创建时间：2018-03-02
    创建人：伍霖峰
    内容简介：
        
]]
require("gamebase_module")
require("GAMEMODEL_module_controller")
require("GAMEMODEL_module_config")

GAMEMODEL_module = {}

function GAMEMODEL_module.New(_fileName, _moduleName)
    local this = gamebase_module.New(_fileName, "GAMEMODELModule")
    local base = this.__index

    function this.Init(_id)
        if base.Init(_id) then
            this.Activate()
            return true
        end
        this.SetController(GAMEMODEL_controller.New(this))
        return false
    end

    return this
end

return GAMEMODEL_module