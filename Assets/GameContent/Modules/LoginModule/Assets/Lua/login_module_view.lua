login_module_view = {}

function login_module_view.New(_controller, _uiLayer, _prename)
    local this = lua_view_base.New(_controller, _uiLayer)
    local base = this.__index
    local prename = _prename

    function this.Init()
        if base.Init() then
            return true
        end

        this.CreateRootObject(prename)
        
        this.wechatLogin = utils.FindChild(this.gameObject, "Center/WechatLogin")
        this.isAgree = utils.FindChild(this.wechatLogin, "IsAgree")
        this.btnUserProtocol = utils.FindChild(this.isAgree, "BtnUserProtocol")

        this.isAgree = this.isAgree:GetComponent("Toggle")
        this.input = utils.FindChild(this.gameObject, "Center/InputField")

        utils.SetNativeSize(utils.FindChild(this.gameObject, "logo"))

        return false
    end

    function this.Destroy()
        base.Destroy()
    end
    
    return this
end

return login_module_view