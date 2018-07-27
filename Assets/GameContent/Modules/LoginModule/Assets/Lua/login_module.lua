--[[
    创建时间：2018.2.26
    创建人：伍霖峰
]]
require("login_module_controller")

login_module = {}

function login_module.New(_fileName, _moduleName)
    local this = lua_module_base.New(_fileName, "LoginModule")
    local base = this.__index

    function this.Init(_id)
        if base.Init(_id) then
            this.Activate()
            return true
        end
        this.SetController(login_module_controller.New(this))

        events.AddListener(MessageType.ServerResponsed, this)
        return false
    end

    function this.Destroy()
        base.Destroy()
        events.RemoveListener(MessageType.ServerResponsed, this)
    end

    function this.OnMessageCome(_msg)
        if _msg.msgType == MessageType.ServerResponsed then
            local _body = _msg.body.content
            this.controller.CloseLoading()
            if _body.pro_con_id == net_module_protocol_contrast.loginack.pro_con_id then
                this.controller.OnServerLoginBack(_body)
            end
            if _body.pro_con_id == net_module_protocol_contrast.enterhallack.pro_con_id then
                this.controller.OnServerEnterHallBack(_body)
            end
        end
    end

    return this
end

return login_module