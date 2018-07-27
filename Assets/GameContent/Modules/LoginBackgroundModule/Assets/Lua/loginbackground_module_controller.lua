require("loginbackground_module_view")

loginbackground_module_controller = {}

function loginbackground_module_controller.New(_module)
    local this = lua_controller_base.New(_module)

    function this.Init()
        if this.IsInit() then
            this.Activate()
            return
        end
        
        this.SetView(loginbackground_module_view.New(this, UILayers.Backgroud))
    end

    --其他自定义内容写在这里

    --自定义内容结束

    return this
end

return loginbackground_module_controller
