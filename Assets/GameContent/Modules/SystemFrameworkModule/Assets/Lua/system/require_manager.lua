--[[
    创建时间：2018.2.26
    创建人：伍霖峰
]]
require_manager = {}

local crequire = require
local luaF = {}

--重载require入口
function require(_name, _module)
    return require_manager.GetLua(_name)
end

function require_manager.GetLua(_name)
    if luaF[_name] == nil then
        luaF[_name] = crequire (_name)
    end
    return luaF[_name]
end

function require_manager.UnloadAll()
	for k,v in pairs(luaF) do
        package.loaded[k] = nil
	end
    luaF = {}
end

function require_manager.Unload(_name)
    luaF[_name] = nil
    package.loaded[_name] = nil
end

return require_manager