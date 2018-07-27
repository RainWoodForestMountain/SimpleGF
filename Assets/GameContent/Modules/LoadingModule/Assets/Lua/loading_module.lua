--[[
    创建时间：2018-04-03
    创建人：李密
    内容简介：
        等待界面
]]
require("loading_module_controller")

loading_module = {}

function loading_module.New(_fileName, _propertys)
    local this = lua_module_base.New(_fileName, "LoadingModule")
    local base = this.__index
    local symbol = nil

    function this.Init(_id)
        if not this.IsInit(_id) then
            this.SetController(loading_module_controller.New(this))
            events.AddListener(MessageType.CloseLoading, this.OnCloseing)
        end
        this.Activate(_propertys)
    end
    function this.Destroy()
        base.Destroy()
        events.RemoveListener(MessageType.CloseLoading, this.OnCloseing)
    end

    function this.Activate(_propertys)
        symbol = _propertys.symbol
        this.controller.Activate(_propertys)
    end

    function this.OnCloseing(_msg)
        local _sb = nil
        if _msg then
            _sb = _msg.body.content
        end
        --如果存在标志，则两次都相等才关闭
        if symbol and _sb then
            if symbol == _sb then
                this.Kill()
            end
        end
        --如果一个标志都没有，直接关闭
        if (not symbol) and (not _sb) then
            this.Kill()
        end
    end

    return this
end

return loading_module