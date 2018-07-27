require("node_NODENAME.GAMEMODEL_module_node_NODENAME_view_seat")

GAMEMODEL_module_node_NODENAME_view = {}

function GAMEMODEL_module_node_NODENAME_view.New(_view)
    local this = class_model.New(_view)

    function this.OnOpenNode()
        local _seats = {}
        for i,v in ipairs(this.seats) do
            _seats[i] = GAMEMODEL_module_node_NODENAME_view_seat.New(v)
        end
        this.seats = _seats
    end
    function this.OnCloseNode()
    end

    return this
end

return GAMEMODEL_module_node_NODENAME_view