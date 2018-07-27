GAMEMODEL_module_node_NODENAME_controller = {}

function GAMEMODEL_module_node_NODENAME_controller.New(_controller)
    local this = class_model.New(_controller)
    this.view = GAMEMODEL_module_node_NODENAME_view.New(this.view)

    function this.OnOpenNode()
        events.AddListener(MessageType.EventButtonClick, this)

        this.view.OnOpenNode()
    end
    function this.OnCloseNode()
        events.RemoveListener(MessageType.EventButtonClick, this)

        this.view.OnCloseNode()
    end
    function this.OnMessageCome(_msg)
        if _msg.msgType == MessageType.EventButtonClick then
            if this[_msg.body.key] and type(this[_msg.body.key]) == "function" then
                this[_msg.body.key](_msg.body.content)
            end
        end
    end
    return this
end

return GAMEMODEL_module_node_NODENAME_controller