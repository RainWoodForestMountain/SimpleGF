--[[
    创建时间：CREATTIME
    创建人：CREATER
    内容简介：
        CONTENT
]]
require("MODULE_CONTROLLER")

MODULE = {}

function MODULE.New(_fileName, _propertys)
    --fileName：lua文件名
    --moduleName：模块名称（主要指模块文件夹名称）
    local this = lua_module_base.New(_fileName, "MODULE_NAME")
    local base = this.__index
    this.propertys = _propertys

    function this.Init(_id)
        if base.Init(_id) then
            this.Activate()
            return true
        end
        this.SetController(MODULE_CONTROLLER.New(this))

        return false
    end

    --无覆写需要请删除
    function this.Activate()
        base.Activate()
    end
    function this.Sleep()
        base.Sleep()
    end
    function this.Destroy()
        base.Destroy()
    end
    --无覆写需要请删除 end

    return this
end

return MODULE