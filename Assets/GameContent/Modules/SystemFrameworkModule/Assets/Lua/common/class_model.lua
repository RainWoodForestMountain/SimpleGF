--[[
    创建时间：2018.2.26
    创建人：伍霖峰
]]
class_model = {}

function class_model.New(_tab)
    local this = _tab or {}
    this.__index = this

    local cla = setmetatable({}, this)
    return cla
end

return class_model