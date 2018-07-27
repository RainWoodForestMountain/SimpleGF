--[[
    创建时间：2018.2.26
    创建人：伍霖峰
]]
require("player_module_controller")

player_module = {}

function player_module.New(_fileName, _moduleName)
    local this = lua_module_base.New(_fileName, "PlayerModule")
    this.__moduleType = lua_module_manager.moduleType.system

    function this.Init(_id)
        this.SetController(player_module_controller.New(this))

        events.AddListener(MessageType.ServerDataIntercept, this)
        return false
    end

    function this.Destroy()
        events.RemoveListener(MessageType.ServerDataIntercept, this)
    end

    function this.OnMessageCome(_msg)
        --劫持消息
        if _msg.msgType == MessageType.ServerDataIntercept then
            local _body = _msg.body.content
            local _player = {}
            --登录消息劫持
            if _body.pro_con_id == net_module_protocol_contrast.loginack.pro_con_id then
                data_cache.mine = nil
                this.controller.Clean()
                _player = utils.Clone(_body)
            --劫持到游戏中的数据
            elseif _body.pro_con_id == "in_games" then
                _player = utils.Clone(_body)
            --存钱、取钱消息劫持
            elseif _body.pro_con_id == net_module_protocol_contrast.savemoneyack.pro_con_id or _body.pro_con_id == net_module_protocol_contrast.withdrawmoneyack.pro_con_id then
                _body.insurance = _body.insurance or 0
                _player = utils.Clone(_body)
            --修改交友微信号消息劫持
            elseif _body.pro_con_id == net_module_protocol_contrast.alertsignack.pro_con_id then
                _player = utils.Clone(_body)
            --获取背包物品配置消息劫持
            elseif _body.pro_con_id == net_module_protocol_contrast.getgoodsconfigack.pro_con_id then
                _player = utils.Clone(_body)
            --获取兑换奖品消息劫持
            elseif _body.pro_con_id == net_module_protocol_contrast.useitemack.pro_con_id then
                _player = utils.Clone(_body)
            --获取玩家货币信息消息劫持
            elseif _body.pro_con_id == net_module_protocol_contrast.getcurrencyack.pro_con_id then
                _player = utils.Clone(_body)
            --获取玩家领取附件消息劫持
            elseif _body.pro_con_id == net_module_protocol_contrast.getattachmentack.pro_con_id then
                _player = utils.Clone(_body)
            --比赛消息劫持
            elseif _body.pro_con_id == net_module_protocol_contrast.broadcastntf.pro_con_id then
                local _body = json.decode(_body.message)
                local _mine = data_cache.mine
                local _player = {}
                --比赛结束推送
                if _body.cmd == 202 then
                    local _rewards = _body.data.rewards
                    _player.money = _mine.money + (_rewards.gold or 0)
                    _player.coupon = _mine.coupon + (_rewards.coupon or 0)
                    _player.coin = _mine.coin + (_rewards.coin or 0)
                    _player.match = _mine.match + (_rewards.match or 0)
                    this.controller.ChangeSelf(_player)
                --充值成功推送
                elseif _body.cmd == 206 then
                    _player.money = gold
                    this.controller.ChangeSelf(_player)
                end
            end
            
            this.controller.ChangeSelf(_player)
        end
    end

    return this
end

return player_module