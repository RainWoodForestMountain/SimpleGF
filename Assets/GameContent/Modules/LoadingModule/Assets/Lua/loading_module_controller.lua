require("loading_module_view")

loading_module_controller = {}

function loading_module_controller.New(_module)
    local this = lua_controller_base.New(_module)
    local base = this.__index

    local tomsg = nil
    local delayId = nil

    function this.Init()
        base.Init()
        this.SetView(loading_module_view.New(this, UILayers.NoMaskWindow))
    end
    function this.Activate(_propertys)
        if delayId then
            utils.RemoveTimer(delayId)
        end
        this.SetLoading_moduleText(_propertys.text)
        this.SetLoading_moduleNum(_propertys.time)
        tomsg = _propertys.tomsg
    end
    function this.Destroy()
        base.Destroy()
        utils.RemoveTimer(delayId)
    end

    --其他自定义内容写在这里
    --调用View里的这个方法
    function this.SetLoading_moduleText(_text)
        this.view.SetLoading_moduleText(_text)
    end
    --调用View里的这个方法  
    function this.SetLoading_moduleNum(_num)
        if type(_num) == "number" then
            local _time = tonumber(_num)
            if _time > 0 then
                delayId = utils.CreateDelayInvok(this.OnTimeOut, _time)
            end
        end
    end
    function this.OnTimeOut()
        this.Kill()
        if tomsg then
            utils.OpenFloatTips(tomsg)
        end
    end
    --自定义内容结束
     
    return this
end

return loading_module_controller
