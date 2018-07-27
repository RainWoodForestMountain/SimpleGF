--[[
    创建时间：2018.2.26
    创建人：伍霖峰
]]
require("luamvc.lua_controller_base")

lua_module_base = {}

function lua_module_base.New(_fileName, _moduleName)
    local this = {}
    local isInit = false
    local isActive = false
    this.__index = this
    this.__mFileName = _fileName
    this.__mModuleName = _moduleName

    function this.IsInit(_id)
        if isInit then
            return isInit
        end
        isInit = true
        this.Init(_id)
        return false
    end

    function this.Init(_id)
        this.id = _id
        this.assetLoadRecord = AssetLoadRecord.New(this.__mModuleName)
        this.SetController(lua_controller_base.New(this))
        isActive = true
    end
    function this.SetController(_con, _notInit)
        this.controller = _con
        if not _notInit then
            this.controller.Init()
        end
    end

    function this.Activate()
        this.controller.Activate()
        isActive = true
    end

    function this.Sleep()
        this.controller.Sleep()
        isActive = false
    end

    function this.IsActive()
        return isActive
    end

    function this.Destroy()
        this.assetLoadRecord:Destroy()
        this.controller.Destroy()
        isActive = false
    end
    --自关闭调用，自关闭请不要调用Destroy
    function this.Kill()
        events.Brocast(CreateMessage(MessageType.ModuleClose, this.__mFileName))
    end

    function this.LoadGameObject(_resName)
        return this.assetLoadRecord:LoadGameObject(_resName)
    end
    function this.LoadInstantiateGameObject(_resName)
        return this.assetLoadRecord:LoadInstantiateGameObject(_resName)
    end
    function this.LoadSprite(_resName)
        return this.assetLoadRecord:LoadSprite(_resName)
    end
    function this.LoadText(_resName)
        return this.assetLoadRecord:LoadText(_resName)
    end
    function this.LoadMaterial(_resName)
        return this.assetLoadRecord:LoadMaterial(_resName)
    end
    function this.LoadAudioClip(_resName)
        return this.assetLoadRecord:LoadAudioClip(_resName)
    end
    function this.LoadTexture(_resName)
        return this.assetLoadRecord:LoadTexture(_resName)
    end
    function this.LoadTexture2D(_resName)
        return this.assetLoadRecord:LoadTexture2D(_resName)
    end

    local module = setmetatable({}, this)
    return module
end

return lua_module_base