login_module_data = {}

function login_module_data.New(_controller)
    local this = lua_data_base.New(_controller)

    function this.Init()
        if this.__index.Init() then
            return true
        end

        return false
    end

    function this.Activate()
        this.__index.Activate()
    end

    function this.Sleep()
        this.__index.Sleep()
    end

    function this.Destroy()
        this.__index.Destroy()
    end

    --其他自定义内容写在这里

    --自定义内容结束
    
    return this
end

return login_module_data