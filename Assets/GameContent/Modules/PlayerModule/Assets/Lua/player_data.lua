player_data = {}

function player_data.New()
    local this = class_model.New()
    this.__index = this

    this.wxid = 0
    this.token = 0
    this.userid = 0
    this.nickname = 0
    this.wxheadurl = 0
    this.sex = 0
    
    --钱
    this.money = 0
    --保险箱
    this.insurance = 0
    --签名
    this.description = 0
    --代理商昵称
    this.agentnick = 0
    --手机号 -- 字符串
    this.mobile = 0
    this.remote_ip = 0
    this.open_id = 0
    this.location = 0
    --背包物品配置
    this.items = {}
    --奖券
    this.coupon = 0
    --钻石
    this.coin = 0
    --魅力值
    this.charm = 0
    --风流值
    this.dissolute = 0
    --游戏时长
    this.playing_time = 0
    --赢得金币
    this.win_gold = 0
    --参赛券
    this.match = 0
    --是否首充(服务器传过来的是json字符串)
    this.first_charge = 0

    function this.Equals(_player)
        return _player and this.wxid == _player.wxid
    end

    local cla = setmetatable({}, this)
    return cla
end

return player_data