require("dialog_module_view")

dialog_module_controller = {}

function dialog_module_controller.New(_module)
    local this = lua_controller_base.New(_module)
    local base = this.__index

    function this.Init()
        if base.IsInit() then
            this.Activate()
            return
        end
        this.id = this.module.id
        this.SetView(dialog_module_view.New(this, UILayers.Popup))
		this.Setdialog_moduleText(_module.Info.str)
        this.Setdialog_moduleBtn(_module.Info)
        
        events.AddListener(MessageType.EventButtonClick, this)
    end

    function this.Destroy()
        base.Destroy()
        events.RemoveListener(MessageType.EventButtonClick, this)
    end

    --OnMessageCome的实现可以检查meg的key来判断执行的功能
    --或者msg的key就是一个功能的名称，直接通过名称来调用
    function this.OnMessageCome(_msg)
        if _msg.msgType == MessageType.EventButtonClick then
            if tostring(this.id) ~= _msg.body.content.name then
                return
            end
            if this[_msg.body.key] and type(this[_msg.body.key]) == "function" then
                this[_msg.body.key]()
            end
            return
        end
    end

    --其他自定义内容写在这里
	function this.dialog_module_Sure()
        if _module.Info.sure and type(_module.Info.sure) == "function" then
            _module.Info.sure()
        end
        this.dialog_module_Close()
    end
    function this.dialog_module_Cacle()
        if _module.Info.close and type(_module.Info.close) == "function" then
            _module.Info.close()
        end
        this.dialog_module_Close()
    end
    function this.dialog_module_Close()
        --对话框窗口是可以多个存在的，无法使用kill的发送消息模式删除
        this.module.Destroy()
	end
	function this.Setdialog_moduleText(_str)
		this.view.Setdialog_moduleText(_str)		
	end
    function this.Setdialog_moduleBtn(_tab)
        local _type = 0
        if _tab.sure then
            if _tab.close then
                _type = 2
            else
                _type = 1
            end
        end
        _type = _tab.type or _type
		this.view.Setdialog_moduleBtn(_type)
	end
    --自定义内容结束

    return this
end

return dialog_module_controller
