--[[
    创建时间：2018.2.26
    创建人：伍霖峰
]]
lua_bridge = {}

function lua_bridge.Init()
	lua_bridge.AddListener(events.Brocast)
end
function lua_bridge.Destroy()
	lua_bridge.RemoveListener(events.Brocast)
end
--lua_beidgr主要用于与C#端的交互，若需要发送消息给C#端或者接受C#端的消息，用lua_bridge
--如果消息仅仅在lua生效，用events
--添加C#事件监听函数
function lua_bridge.AddListener(_ac)
    ToLuaBridge.AddListener(_ac)
end
--移除C#事件监听函数
function lua_bridge.RemoveListener(_ac)
    ToLuaBridge.RemoveListener(_ac)
end
--向C#端发送消息命令
function lua_bridge.Recevive(_key, _type, _content)
    ToLuaBridge.Recevive(_key, _type, _content);
end

return lua_bridge