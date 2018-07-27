require("node_NODENAME.GAMEMODEL_module_node_NODENAME_view")
require("node_NODENAME.GAMEMODEL_module_node_NODENAME_controller")

GAMEMODEL_module_node_NODENAME = {}

function GAMEMODEL_module_node_NODENAME.New(_con, _data, _view)
    local this = gamebase_node.New(GAMEMODEL_module_node_NODENAME_controller.New(_con))
    local base = this.__index
    
    function this.GetNode()
        return base.GetNode(this.GetLeafs(), this.OnComplete)
    end
    function this.GetLeafs()
        local _leafs = {}
        table.insert( _leafs, game_state_machine_leaf_single.New(this.Leaf_CleanOtherAndReadySelf) )

        return _leafs
    end

    function this.Leaf_CleanOtherAndReadySelf()
        this.controller.OnOpenNode()
    end
    --结束时的工作
    function this.OnComplete()
        this.controller.OnCloseNode()
    end

    return this
end

return GAMEMODEL_module_node_NODENAME