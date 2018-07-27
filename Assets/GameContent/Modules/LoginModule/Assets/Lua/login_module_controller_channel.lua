login_module_controller_channel = {}

local channelid = ChannelData.current.channelID

function login_module_controller_channel.GetChannelLoginPrefab()
    local _basename = "log_pre_page"

    if channelid == 0 then
    end

    return _basename
end

function login_module_controller_channel.Login(_key, _cb)
    if channelid == 0 then
    end
    --目前默认，微信登录
    third_plantforms.WechatLogin(_cb)
end

return login_module_controller_channel