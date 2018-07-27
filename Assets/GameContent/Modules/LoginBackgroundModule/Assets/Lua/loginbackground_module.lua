--[[
    内容简介：
]]
require("loginbackground_module_controller")

loginbackground_module = {}

function loginbackground_module.New(_fileName, _moduleName)
    --fileName：lua文件名
    --moduleName：模块名称（主要指模块文件夹名称）
    local this = lua_module_base.New(_fileName, "LoginBackgroundModule")

    function this.Init(_id)
        if this.__index.Init(_id) then
            this.Activate()
            return true
        end
        this.SetController(loginbackground_module_controller.New(this))
        return false
    end


    return this
end

return loginbackground_module