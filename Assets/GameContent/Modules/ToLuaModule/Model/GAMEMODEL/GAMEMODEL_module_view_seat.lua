require("gamebase_view_seat")

GAMEMODEL_module_view_seat = {}

function GAMEMODEL_module_view_seat.New(_obj)
    local this = gamebase_view_seat.New(_obj)
    local base = this.__index
    local view = nil

    local gameObject = _obj

    --这一部分是公有的部分，每个游戏通用的
    --公用部分的功能在父级也就是gamebase_view_seat处理
    base.readyIcon = utils.FindChild(gameObject, "Player/readyIcon")
    base.win = utils.FindChild(gameObject, "Player/win")
    base.lose = utils.FindChild(gameObject, "Player/lose")

    function this.Clean()
        base.Clean()
    end
    function this.Init(_view)
        base.Init()
        view = _view
    end


    --其他自定义内容写在这里
    
    --自定义内容结束

    return this
end

return GAMEMODEL_module_view_seat