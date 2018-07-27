loading_module_view = {}

function loading_module_view.New(_controller, _uiLayer)
    local this = lua_view_base.New(_controller, _uiLayer)
    local base = this.__index
    local time=0
    
    function this.Init()
        if this.IsInit() then
            this.Activate()
            return
        end

        this.CreateRootObject("loa_pre_node")
        this.LoadingText=utils.FindChild(this.gameObject,"LoadingPnl/Describe")
        this.mask = utils.FindChild(this.gameObject,"mask")
    end
    --设置等待界面文字显示
    function this.SetLoading_moduleText(_text)
        utils.SetText(this.LoadingText,_text)
    end
    
    return this
end

return loading_module_view