--[[
    创建时间：2018.2.26
    创建人：伍霖峰
]]

define = {}

function define.Init(_withoutHotupdata)
    --unity
    SpriteRenderer   = UnityEngine.SpriteRenderer
    Application      = UnityEngine.Application
    SystemInfo       = UnityEngine.SystemInfo
    WWW              = UnityEngine.WWW
    GameObject       = UnityEngine.GameObject
    Screen           = UnityEngine.Screen
    SleepTimeout     = UnityEngine.SleepTimeout
    Ping             = UnityEngine.Ping
    UnityWebRequest  = UnityEngine.Networking.UnityWebRequest
    NetworkReachability = UnityEngine.NetworkReachability
    --工具
    ToLuaModule      = GameFramework.ToLuaModule
    Utility          = GameFramework.Utility
    UtilityUnity     = GameFramework.UtilityUnity
    DoTweenAnimation = GameFramework.DoTweenAnimation
    CheckingString   = GameFramework.CheckingString
    --模型
    Message          = GameFramework.Message
    AssetLoadRecord  = GameFramework.AssetLoadRecord
    IDModel          = GameFramework.IDModel
    ByteBuffer       = GameFramework.ByteBuffer
    --数据
    ChannelData      = GameFramework.ChannelData
    ChannelDataPackage = GameFramework.ChannelDataPackage
    ProjectDatas     = GameFramework.ProjectDatas
    PersistenceData  = GameFramework.PersistenceData
    RunningTimeData  = GameFramework.RunningTimeData
    __CommonKey      = GameFramework.CommonKey
    --枚举
    __MessageType    = GameFramework.MessageType
    __UILayers       = GameFramework.UILayers
    Ease             = DG.Tweening.Ease
    --其他
    UIEffectDepth    = GameFramework.UIEffectDepth
    --lua桥
    ToLuaBridge      = GameContent.ToLuaBridge

    --加载lua代码
    json = require "cjson"
    require("common.events")
    require("common.lua_bridge")
    require("common.utils")
    require("common.class_model")
    require("common.MessageType")
    require("common.UILayers")
    require("common.CommonKey")
    require("common.data_cache")
    require("common.common_manager")
    require("common.common_config")
    require("common.third_plantforms")
    require("luamodulebase.lua_module_manager")
    require("game_config_manager")

    --设置一些默认参数
    Screen.sleepTimeout = SleepTimeout.NeverSleep
    Application.targetFrameRate = RunningTimeData.GetRunningDataInt(CommonKey.GAME_FRAME_RATE, 30)

    --各种初始化函数
	lua_module_manager.Init()
	lua_bridge.Init()
	data_cache.Init()
	game_config_manager.Init()
    common_manager.Init()
    third_plantforms.Init()
    
    events.Brocast(CreateMessage(MessageType.ModuleOpen, "net_module"))
    -- if (utils.GetRunningDataBool("SWITCH_USE_HOTUPDATE") or (not ProjectDatas.isDebug)) and (not _withoutHotupdata) then
    --     events.Brocast(CreateMessage(MessageType.ModuleOpen, "hotupdate_module"))
    -- else
    --     events.Brocast(CreateMessage(MessageType.ModuleOpen, "loginbackground_module"))
    --     events.Brocast(CreateMessage(MessageType.ModuleOpen, "login_module"))
    -- end
    events.Brocast(CreateMessage(MessageType.ModuleOpen, "hotupdate_module"))

    -- require("gdoudizhu_strategy")
    -- local _get = gdoudizhu_strategy.FoundBiger(0, {0x4}, {0x4, 0x5, 0x6})
    -- utils.LogError("-------------------get = ", json.encode(_get))
end

function define.Destroy()
	lua_module_manager.Destroy()
	lua_bridge.Destroy()
	data_cache.Destroy()
	game_config_manager.Destroy()
    common_manager.Destroy()
    third_plantforms.Destroy()

	if poker then
		poker.Destroy()
	end
	if game_player then
		game_player.Destroy()
    end
    
    require_manager.UnloadAll()
end

return define