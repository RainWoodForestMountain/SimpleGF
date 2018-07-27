require("gamebase_view")
require("GAMEMODEL_module_view_seat")

GAMEMODEL_module_view = {}

function GAMEMODEL_module_view.New(_controller, _uiLayer)
    local this = gamebase_view.New(_controller, _uiLayer)
    local base = this.__index

    function this.Init()
        base.Init()

        this.CreateRootObject("PAGES")
        base.InitSeats(this.InitSeats())

        --基础动画，通用
        base.winAnimation = utils.FindChild(this.gameObject, "Center/animation/win")
        base.loseAnimation = utils.FindChild(this.gameObject, "Center/animation/lose")
    end

    --其他自定义内容写在这里
    function this.Clean()
        base.Clean()
        
    end
    --初始化座位
    function this.InitSeats()
        local _s = {}
        for i=0, GAMEMODEL_config.maxSeat - 1 do
            _s[i] = GAMEMODEL_module_view_seat.New(utils.FindChild(this.gameObject, "Seat/"..i))
            _s[i].Init(this)
        end
        return _s
    end
    --自定义内容结束
    
    return this
end

return GAMEMODEL_module_view