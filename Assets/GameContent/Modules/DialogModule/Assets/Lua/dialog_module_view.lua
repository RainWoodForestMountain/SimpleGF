
dialog_module_view = {}

function dialog_module_view.New(_controller, _uiLayer)
    local this = lua_view_base.New(_controller, _uiLayer)
    local base = this.__index

    function this.Init()
        if this.IsInit() then
            this.Activate()
            return
        end

        this.CreateRootObject("dia_pre_page")
        this.bg = utils.FindChild(this.gameObject,"Center/Panel/bg")
        this.PromptText = utils.FindChild(this.gameObject,"Center/Panel/Text")
        this.CloseBtn = utils.FindChild(this.gameObject,"Center/Panel/BtnClose")

        this.btn1 = utils.FindChild(this.gameObject,"Center/Panel/btn1")
        this.btn2 = utils.FindChild(this.gameObject,"Center/Panel/btn2")

        this.CloseBtn.name = tostring(this.controller.id)
        utils.FindChild(this.btn1,"BtnSure").name = tostring(this.controller.id)
        utils.FindChild(this.btn2,"BtnSure").name = tostring(this.controller.id)
        utils.FindChild(this.btn2,"BtnCancle").name = tostring(this.controller.id)
    end
	
    --其他自定义内容写在这里
	function this.Setdialog_moduleText(_str)
		utils.SetText(this.PromptText,_str)
    end
    
    function this.Setdialog_moduleBtn(_type)
        if _type == 0 then
            --utils.SetUIScale(this.bg, Vector2.New(700, 310))
        elseif _type == 1 then
            utils.ActiviteGameObject(this.btn1, true)
           -- utils.SetUIScale(this.bg, Vector2.New(700, 410))
        elseif _type == 2 then
            utils.ActiviteGameObject(this.btn2, true)
            --utils.SetUIScale(this.bg, Vector2.New(700, 410))
		end
	end
    --自定义内容结束
		
    return this
end

return dialog_module_view