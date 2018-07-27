--[[
    创建时间：2018.2.26
    创建人：伍霖峰
]]
require("luamvc.lua_data_base")
require("luamvc.lua_view_base")

lua_controller_base = {}

function lua_controller_base.New(_module)
    local this = {}
    local isInit = false
    this.module = _module
    this.__index = this

    local child_modules = {}

    function this.IsInit()
        if isInit then
            return isInit
        end
        isInit = true
        this.Init()
        return false
    end

    function this.Init()
        this.view = lua_view_base.New(this)
        this.data = lua_data_base.New(this)

        this.view.Init()
        this.data.Init()
    end

    function this.Activate()
        if this.view then
            this.view.Activate()
        end
        if this.data then
            this.data.Activate()
        end
    end

    function this.Sleep()
        if this.view then
            this.view.Sleep()
        end
        if this.data then
            this.data.Sleep()
        end
    end

    function this.Destroy()
        if this.view then
            this.view.Destroy()
        end
        if this.data then
            this.data.Destroy()
        end

        for k,v in pairs(child_modules) do
            events.Brocast(CreateMessage(MessageType.ModuleClose, v))
        end
    end

    function this.OpenChildModule(_mdName, _pro)
        table.insert( child_modules, _mdName )
        events.Brocast(CreateMessage(MessageType.ModuleOpen, _mdName, _pro))
    end
    function this.SetView(_view, _notInit)
        this.view = _view
        if not _notInit then
            this.view.Init()
        end
    end
    function this.SetData(_data, _notInit)
        this.data = _data
        if not _notInit then
            this.data.Init()
        end
    end
    function this.LoadInstantiateGameObject(_resName)
        return this.module.LoadInstantiateGameObject(_resName)
    end
    function this.LoadGameObject(_resName)
        return this.module.LoadGameObject(_resName)
    end
    function this.LoadSprite(_resName)
        return this.module.LoadSprite(_resName)
    end
    function this.LoadText(_resName)
        return this.module.LoadText(_resName)
    end
    function this.LoadMaterial(_resName)
        return this.module.LoadMaterial(_resName)
    end
    function this.LoadAudioClip(_resName)
        return this.module.LoadAudioClip(_resName)
    end
    function this.LoadTexture(_resName)
        return this.module.LoadTexture(_resName)
    end
    function this.LoadTexture2D(_resName)
        return this.module.LoadTexture2D(_resName)
    end

    --一些view的快捷操作
    function this.SetSpriteByName(_obj, _resName)
        utils.SetSprite(_obj, this.LoadSprite(_resName))
    end

    --打开loading后的限时操作
    local onCLose = false
    local onLoadingEndAct = nil
    local function OnLoadingEnd()
        utils.CloseLoading("request_net_loading")
        if onCLose then
            return
        end
        --如果需要执行功能
        if type(onLoadingEndAct) == "function" then
            onLoadingEndAct()
            onLoadingEndAct = nil
        end
    end
    function this.OpenLoadingWithTimeOut(_msg, _timeOutMsg, _onEnd, _timeOut)
        onCLose = false
        onLoadingEndAct = _onEnd
        _timeOut = _timeOut or 5

        utils.OpenLoading(_msg, _timeOut, _timeOutMsg, "request_net_loading")
        utils.CreateDelayInvok(OnLoadingEnd, _timeOut)
    end
    function this.CloseLoading()
        onCLose = true
        OnLoadingEnd()
    end
    function this.Kill()
        this.module.Kill()
    end

    local controller = setmetatable({}, this)
    return controller
end

return lua_controller_base