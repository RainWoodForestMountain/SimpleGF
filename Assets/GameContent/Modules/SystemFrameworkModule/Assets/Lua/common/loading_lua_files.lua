loading_lua_files = {}
local this = loading_lua_files

this.queue = {}
this.current = nil

local function OnLoading()
    --仅为第一次启动的时候会执行这一句
    if this.current == nil then
        this.current = table.remove( this.queue, 1 )
    end
    local _count = 0
    while #this.current.files > 0 or _count >= 5 do
        _count = _count + 1
        local _f = table.remove( this.current.files, 1 )
        require(_f)
    end

    if utils.isEmptyTable(this.current.files) or #this.current.files <= 0 then
        this.OnEnd()
    end
end

function this.Load(_files, _callback)
    if type(_files) ~= "table" then
        _callback()
        return
    end

    local _new = {}
    _new.files = _files
    _new.callBack = _callback
    table.insert( this.queue, _new )
    if this.lpID == nil then
        this.lpID = utils.CreateTimer(OnLoading)
    end
end
function this.OnEnd()
    this.current.callBack()
    --队列没有了就结束计时器
    if utils.isEmptyTable(this.queue) or #this.queue <= 0 then
        utils.RemoveTimer(this.lpID)
        this.lpID = nil
        this.current = nil
    else
        this.current = table.remove( this.queue, 1 )
    end
end

return loading_lua_files