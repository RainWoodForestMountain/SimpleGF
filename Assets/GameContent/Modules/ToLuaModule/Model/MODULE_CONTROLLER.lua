require("MODULE_DATA")
require("MODULE_VIEW")

MODULE_CONTROLLER = {}

function MODULE_CONTROLLER.New(_module)
    local this = lua_controller_base.New(_module)
    local base = this.__index

    function this.Init()
        if this.IsInit() then
            this.Activate()
            return
        end
        
        this.SetData(MODULE_DATA.New(this))
        this.SetView(MODULE_VIEW.New(this, MODULE_VIEW_LAYER))

        --这里监听UI操作事件，可以用类实例监听，也可以用单个函数监听
        --类实例监听的话必须包含统一接口函数 OnMessageCome(_msg)
        --具体实现参见events.lua
        events.AddListener(MessageType.EventButtonClick, this)
    end

    --无覆写需要请删除
    function this.Activate()
        base.Activate()
    end
    function this.Sleep()
        base.Sleep()
    end
    --无覆写需要请删除 end

    function this.Destroy()
        base.Destroy()

        events.RemoveListener(MessageType.EventButtonClick, this)
    end

    --OnMessageCome的实现可以检查meg的key来判断执行的功能
    function this.OnMessageCome(_msg)
        if _msg.msgType == MessageType.EventButtonClick then
            if this[_msg.body.key] and type(this[_msg.body.key]) == "function" then
                this[_msg.body.key](_msg.body.content)
            end
            return
        end
    end

    --其他自定义内容写在这里

    --自定义内容结束

    return this
end

return MODULE_CONTROLLER