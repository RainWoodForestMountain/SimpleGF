--[[
    创建时间：2018.2.26
    创建人：伍霖峰
    内容简介：
        使用以下形式对module进行操作 三个参数分别为 消息类型 模块名称 lua文件路径
        注：所有lua文件均以模块下面的Assets/Lua文件夹为根开始，如路径为 Assets/Lua/Lua1/mylua.lua 则调用路径为 Lua1/mylua
        events.Brocast(CreateMessage(MessageType.ModuleOpen, "lua_module_base", "luamodulebase/lua_module_base"))
]]
require("luamodulebase.lua_module_base")

lua_module_manager = {}

lua_module_manager.moduleType = {
    ui = 1,         --default, nil equals ui
    system = 2,
    importent = 3,
}

local idModle = IDModel.New()
local modules = {}

local function CloseModule(_name)
    utils.Log("准备关闭模块", _name)
    if modules[_name] then
        modules[_name].Destroy()
    else
        utils.Log("<color=red>", "名称为", _name, "的模块并未初始化", "</color>")
    end
    modules[_name] = nil
end
local function ActivateModule(_name, _moduleProperty)
    utils.Log("准备激活模块", _name)
    if modules[_name] then
        modules[_name].Activate(_moduleProperty)
    else
        utils.Log("<color=red>", "名称为", _name, "的模块并未初始化", "</color>")
    end
end
local function SleepModule(_name)
    utils.Log("准备休眠模块", _name)
    if modules[_name] then
        modules[_name].Sleep()
    else
        utils.Log("<color=red>", "名称为", _name, "的模块并未初始化", "</color>")
    end
end
local function OpenModule(_name, _moduleProperty)
    utils.Log("准备打开模块", _name)
    if not modules[_name] then
        local _b = nil
        --调试版本，暴露错误
        if ProjectDatas.isDebug then
            _b = require(_name)
        else
            local _e = pcall(function ()
                _b = require(_name)
            end )
            if not _e then
                utils.Log("<color=red>", "没有找到名称为".._name.."的模块", "</color>")
                return
            end
        end
        modules[_name] = _b.New(utils.GetFileNameWithoutExtension(_name), _moduleProperty)
        modules[_name].Init(idModle.GetLongId)
        if _b.more then
            modules[_name] = nil
        end
    else
        ActivateModule(_name, _moduleProperty)
    end
end
local function CloseAllUIModule()
    for k,v in pairs(modules) do
        --空默认认为是UI
        if not v.__moduleType or v.__moduleType == lua_module_manager.moduleType.ui then
            v.Destroy()
            modules[k] = nil
        end
    end
end
local function CloseAllImportentModule()
    for k,v in pairs(modules) do
        if v.__moduleType == lua_module_manager.moduleType.importent then
            v.Destroy()
            modules[k] = nil
        end
    end
end

local function OperateModule(_msg)
    if _msg.msgType == MessageType.ModuleOpen then
        OpenModule(_msg.body.key, _msg.body.content)
        return
    end
    if _msg.msgType == MessageType.ModuleClose then
        CloseModule(_msg.body.key)
        return
    end
    if _msg.msgType == MessageType.ModuleActivate then
        ActivateModule(_msg.body.key, _msg.body.content)
        return
    end
    if _msg.msgType == MessageType.ModuleSleep then
        SleepModule(_msg.body.key)
        return
    end
end

function lua_module_manager.OpenModule(_name, _pro)
    OpenModule(_name, _pro)
end
function lua_module_manager.CloseModule(_name)
    CloseModule(_name)
end
function lua_module_manager.ActivateModule(_name, _pro)
    ActivateModule(_name, _pro)
end
function lua_module_manager.SleepModule(_name)
    SleepModule(_name)
end
function lua_module_manager.IsModuleOpen(_name)
    return modules[_name] ~= nil
end
function lua_module_manager.IsModuleActivate(_name)
    return modules[_name] ~= nil and modules[_name].IsActive()
end
function lua_module_manager.Init()
    --事件
    events.AddListener(MessageType.CloseAllUIModule, CloseAllUIModule)
    events.AddListener(MessageType.CloseAllImportentModule, CloseAllImportentModule)
    events.AddListener(MessageType.ModuleOpen, OperateModule)
    events.AddListener(MessageType.ModuleClose, OperateModule)
    events.AddListener(MessageType.ModuleActivate, OperateModule)
    events.AddListener(MessageType.ModuleSleep, OperateModule)

    OpenModule("player_module")
end
function lua_module_manager.Destroy()
    events.RemoveListener(MessageType.CloseAllUIModule, CloseAllUIModule)
    events.RemoveListener(MessageType.CloseAllImportentModule, CloseAllImportentModule)
    events.RemoveListener(MessageType.ModuleOpen, OperateModule)
    events.RemoveListener(MessageType.ModuleClose, OperateModule)
    events.RemoveListener(MessageType.ModuleActivate, OperateModule)
    events.RemoveListener(MessageType.ModuleSleep, OperateModule)
end

return lua_module_manager