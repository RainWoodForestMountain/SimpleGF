--[[
    创建时间：2018.2.26
    创建人：伍霖峰
    内容简介：
        View 的UI层级，需要根据每一个view单独来设置，这里写在New里面只是标注View必须有一个层级，否则将会按照默认层级（窗口层级）来显示
]]
lua_view_base = {}

function lua_view_base.New(_controller, _uiLayer)
    local this = {}
    local layer = _uiLayer or UILayers.Window
    local isInit = false
    this.__index = this
    this.controller = _controller
    --这里应该写在每一个单独的view里
    this.gameObject = nil

    function this.IsInit()
        if isInit then
            return isInit
        end
        isInit = true
        this.Init()
        return false
    end

    function this.Init()
    end
    function this.Activate()
        utils.ActiviteGameObject(this.gameObject, true)
        lua_bridge.Recevive(tostring(layer), MessageType.UILayersAddObject, this.gameObject)
    end
    function this.Sleep()
        utils.ActiviteGameObject(this.gameObject)
    end
    function this.Destroy()
        utils.DestroyGameObject(this.gameObject, true)
        lua_bridge.Recevive(tostring(layer), MessageType.UILayersRefresh, nil)
    end

    --一些资源加载的快速接口
    function this.LoadInstantiateGameObject(_resName)
        return this.controller.LoadInstantiateGameObject(_resName)
    end
    function this.LoadGameObject(_resName)
        return this.controller.LoadGameObject(_resName)
    end
    function this.LoadSprite(_resName)
        return this.controller.LoadSprite(_resName)
    end
    function this.LoadText(_resName)
        return this.controller.LoadText(_resName)
    end
    function this.LoadMaterial(_resName)
        return this.controller.LoadMaterial(_resName)
    end
    function this.LoadAudioClip(_resName)
        return this.controller.LoadAudioClip(_resName)
    end
    function this.LoadTexture(_resName)
        return this.controller.LoadTexture(_resName)
    end
    function this.LoadTexture2D(_resName)
        return this.controller.LoadTexture2D(_resName)
    end
    function this.SetSpriteByName(_obj, _resName)
        utils.SetSprite(_obj, this.LoadSprite(_resName))
    end

    function this.CreateRootObject(_res)
        this.gameObject = this.LoadInstantiateGameObject(_res)
        this.Activate()
    end

    function this.ServerRequest(_cmd)
        if not _cmd or not _cmd.cmd then
            utils.Log("请求为空")
            return
        end
        events.Brocast(CreateMessage(MessageType.ServerRequested, nil, _cmd))
    end

    local view = setmetatable({}, this)
    return view
end

return lua_view_base