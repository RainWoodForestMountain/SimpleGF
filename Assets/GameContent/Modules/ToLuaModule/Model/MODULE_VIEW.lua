MODULE_VIEW = {}

function MODULE_VIEW.New(_controller, _uiLayer)
    local this = lua_view_base.New(_controller, _uiLayer)
    local base = this.__index

    function this.Init()
        if this.IsInit() then
            this.Activate()
            return
        end

        this.CreateRootObject("PAGES")
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

    --其他自定义内容写在这里

    --自定义内容结束
    
    return this
end

return MODULE_VIEW