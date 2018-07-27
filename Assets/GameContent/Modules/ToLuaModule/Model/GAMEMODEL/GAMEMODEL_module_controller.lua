require("gamebase_controller")
require("gamebase_node")

require("GAMEMODEL_module_net")
require("GAMEMODEL_module_data")
require("GAMEMODEL_module_view")

--[rp require node files /rp]

GAMEMODEL_module_controller = {}

function GAMEMODEL_module_controller.New(_module)
    local this = gamebase_controller.New(_module)
    local base = this.__index
    local net = nil

    function this.Init()
        if this.IsInit() then
            this.Activate()
            return
        end

        base.Init()

        this.SetData(GAMEMODEL_data.New(this))
        this.SetView(GAMEMODEL_view.New(this, UILayers.Page))
        
        net = GAMEMODEL_net.New(this, this.data)
        net.Init()

        this.InitAllNode()

        events.AddListener(MessageType.EventButtonClick, this)
    end
    function this.Destroy()
        base.Destroy()

        events.RemoveListener(MessageType.EventButtonClick, this)
    end
    function this.OnMessageCome(_msg)
        if _msg.msgType == MessageType.EventButtonClick then
            if this[_msg.body.key] and type(this[_msg.body.key]) == "function" then
                this[_msg.body.key]()
            end
        end
    end

    --按钮功能

    --按钮功能结束
    

    --自定义内容

    function this.InitAllNode()
--[rp require node setting /rp]
    end

    --自定义内容结束

    return this
end

return GAMEMODEL_module_controller