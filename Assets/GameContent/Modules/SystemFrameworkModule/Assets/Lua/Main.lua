--[[
    注意：请不要在ToLua路径下编写自己的lua文件，这是系统lua文件存放路径
	lua端出除了由lua_bridge和utils与C#端交互外，不直接交互C#，如果非要交互，另外再写一个bridge，原则上做到lua与C#的交互为单线节点交互
	Unity端的GameObject物体以及系统Component组件可以由Lua自由交互
]]
--main只加载system中的文件
require "system.class"
require "system.message"
--define中启动了大部分必要文件
require "system.define"
--重载require入口
require "system.require_manager"
--数据结构
-- require("system.collections")

--主入口函数。从这里开始lua逻辑
--main函数不被第二次调用，main文件也不被第二次加载，所以整个程序中只有一次性加载的可以放在这儿加载
--如果LuaModule被销毁的话，必须调用Main的MainDestroy函数
function Main()
	MainInit()
end
--这里加入一些系统的初始化和销毁功能
function MainInit()
	define.Init()
end
function MainDestroy()
	define.Destroy()
end