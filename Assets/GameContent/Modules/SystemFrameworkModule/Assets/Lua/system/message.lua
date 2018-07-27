--[[
    创建时间：2018.2.26
    创建人：伍霖峰
]]
function CreateMessage(_type, _key, _content)
    local _tab = {}
    _tab.msgType = _type
    _tab.id = 0
    _tab.body = {}
    _tab.body.content = _content
    _tab.body.key = _key
    return _tab
end