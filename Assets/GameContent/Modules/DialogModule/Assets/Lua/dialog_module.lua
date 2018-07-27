--[[
    内容简介：
    启动模块时候传一个表 包括2个字段 str btn 和一个方法 fun
	str字段表示显示文字内容 btn字段表示是否显示按钮0不显示 1显示（不显示按钮时候会在1.5s后自动关闭模块）
	fun代表为确定按钮绑定的方法
]]
require("dialog_module_controller")

dialog_module = {}
dialog_module.more = true

function dialog_module.New(_fileName, _info)
    local this = lua_module_base.New(_fileName,  "DialogModule")
    local base = this.__index

    function this.Init(_id)
        base.Init(_id)
		this.Info = _info
        this.SetController(dialog_module_controller.New(this))
        return false
    end

    return this
end

return dialog_module