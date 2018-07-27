webrequest = {}

local function Request(_request, _cb, _onError)
    coroutine.start( function ()
        coroutine.wait( _request:Send() )
        if not _request.isError then
            --无错误码或者错误码等于200为正确
            if _request.responseCode == 200 or _request.responseCode == 0 then
                if _cb ~= nil and type(_cb) == "function" then
                    _cb(_request.downloadHandler.text)
                end
                return
            end
        end
        if _onError ~= nil and type(_onError) == "function" then
            _onError(_request.error)
        end
    end  )
end

function webrequest.WebGet(_url, _cb, _onError)
    local _request = UnityWebRequest.Get(_url)
    Request(_request, _cb, _onError)
end

function webrequest.WebPost(_url, _property, _cb, _onError)
    local _request = nil
    if _property ~= nil and type(_property) == "table" then
        _request = UnityWebRequest.Post(_url, _property)
    else
        _request = UnityWebRequest.Post(_url)
    end
    Request(_request, _cb, _onError)
end


return webrequest