--[[
    创建时间：2018.2.26
    创建人：伍霖峰
]]
CommonKey = setmetatable({}, { __index = __CommonKey })

--大厅服务器
CommonKey.SERVER_HALL = "SERVER_HALL"
--当前网络状态
CommonKey.CURRENT_NET_STATE = "CURRENT_NET_STATE"
--渠道id
CommonKey.CHANNEL_ID = "CHANNEL_ID"
--渠道名称
CommonKey.CHANNEL_NAME = "CHANNEL_NAME"
--系统配置文件
CommonKey.SYSTEM_CONFIG = "SYSTEM_CONFIG"
--登录记录
CommonKey.LOGIN_RECORD = "LOGIN_RECORD"

return CommonKey