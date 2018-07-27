loginbackground_module_view = {}

function loginbackground_module_view.New(_controller, _uiLayer)
    local this = lua_view_base.New(_controller, _uiLayer)

    function this.Init()
        if this.IsInit() then
            this.Activate()
            return
        end
        this.CreateRootObject("log_pre_bg")
    end

    --其他自定义内容写在这里

    --自定义内容结束
    
    return this
end

return loginbackground_module_view