--[[
    创建时间：2018.2.26
    创建人：伍霖峰
]]
lua_data_base = {}

function lua_data_base.New(_controller)
    local this = {}
    local isInit = false
    this.__index = this
    this.controller = _controller

    function this.IsInit()
        if isInit then
            return isInit
        end
        isInit = true
        this.Init()
        return false
    end

    function this.Init()
    end

    function this.Activate()
    end

    function this.Sleep()
    end

    function this.Destroy()
    end

    local data = setmetatable({}, this)
    return data
end

return lua_data_base