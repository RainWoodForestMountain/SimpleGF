net_cmd = {}

--请求模板
--登录请求
net_cmd.Login = {
    --命令唯一标志
    cmd = test_1_0.Login,
    code = nil,
}
--进入大厅请求
net_cmd.EnterHall = {
    cmd = test_1_0.EnterHall,
}
--登出请求
net_cmd.Logout = {
    cmd = test_1_0.Logout,
}
--存钱
net_cmd.SaveMoney = {
    cmd = test_1_0.SaveMoney,
    money = 1,
}
--取钱
net_cmd.GetMoney = {
    cmd = test_1_0.GetMoney,
    money = 0,
    pwd = "",
}
--储存定位信息
net_cmd.SaveLocation = {
    cmd = test_1_0.SaveLocation,
    location = "位置信息",
}
--战绩
net_cmd.GetGameRecord = {
    cmd = test_1_0.GetGameRecord,
    offset = 0,
    count = 30,
    gameid = 0,
}
--排行榜
net_cmd.GetRank = {
    cmd = test_1_0.GetRank,
    id = 0,
}
--公告
net_cmd.GetNotice = {
    cmd = test_1_0.GetNotice,
    limit = 30,
    game_id = 0,
}
--充值核对
net_cmd.GetRecharge = {
    cmd = test_1_0.GetRecharge,
    order_sn = "order_sn",
}
--获取邮件
net_cmd.GetMail = {
    cmd = test_1_0.GetMail,
    userid = 0,
}
--获取邮件附件
net_cmd.TakeAttachment = {
    cmd = test_1_0.TakeAttachment,
    mailid = "",
    attachment = "",
}
--设置邮件已读
net_cmd.SetMailRead = {
    cmd = test_1_0.SetMailRead,
    mailid = "",
}
--获取商城列表
net_cmd.GetStoreList = {
    cmd = test_1_0.GetStoreList,
}
--设置个性签名
net_cmd.ModifySign = {
    cmd = test_1_0.ModifySign,
    sign = "sign",
}
--获取验证码
net_cmd.GetVerifyCodeRequest = {
    cmd = test_1_0.GetVerifyCodeRequest,
    mobile = "186",
}
--修改保险箱密码
net_cmd.ModifyPassWordRequest = {
    cmd = test_1_0.ModifyPassWordRequest,
    mobile = "186",
    code = 0,
    pwd = "pwd",
}
--信道设置
net_cmd.GameServerChange = {
    cmd = test_1_0.GameServerChange,
}
--创建房间
net_cmd.CreateGameRoom = {
    cmd = test_1_0.CreateGameRoom,
    usersetup = {
        ["抢庄方式"] = "看牌抢庄",
        ["牌型倍数"] = "普通模式",
        ["下注倍数"] = "小倍",
        ["特殊牌型"] = "同花顺,葫芦,同花,顺子",
        ["底注"] = 50,
        ["入场"] = 500,
        ["离场"] = 250,
        ["禁止陌生人加入"] = false,
    }
}
--进入房间
net_cmd.EnterGameRoom = {
    cmd = test_1_0.EnterGameRoom,
    roomid = roomid,
}
--离开房间
net_cmd.LeaveGameRoom = {
    cmd = test_1_0.LeaveGameRoom,
    roomid = roomid,
}

--玩家操作
net_cmd.PlayerOperation = {
    cmd = test_1_0.PlayerOperation,
    subcmd = 0,
    argcount = 0,--参数数量
}
--发送聊天消息
net_cmd.PlayerChat = {
    cmd = test_1_0.SendChat,
    msg = msg,
}
--发送语音消息
net_cmd.PlayerVoice = {
    cmd = test_1_0.SendVoice,
    seatid = 0,
    files = files,
}






--服务器返回模板
net_cmd.LoginBack = {
    cmd = test_1_0.LoginBack,
    token = "token",
    wxid = "wxid",
    nickname = "微信昵称",
    wxheadurl = "微信头像下载地址",
    sex = 0, --性别
    tkn = "tkn",
    roomid = 0, --如果是断线重连，这个值大于0
}
net_cmd.EnterHallBack = {
    cmd = test_1_0.EnterHallBack,
    money = 0, --身上的金币
    insurance = 0, --保险箱内的金币
    description = "个性签名",
    userid = 0,
    agentnick = "代理商/人",
    mobile = "电话号码",
    remote_ip = "IP地址",
    open_id = "open_id",
    game_id_count = 0, --数量
    games = {
        [1] = {
            gid = 0, --子游戏id，如斗地主的id，牛牛的id
            ip = "子游戏的服务器地址",
            port = 8080, --子游戏的端口
        },
    }
}

net_cmd.LogoutBack = {
    cmd = test_1_0.LogoutBack
}
net_cmd.SaveMoneyBack = {
    cmd = test_1_0.SaveMoneyBack,
}
net_cmd.GetMoneyBack = {
    cmd = test_1_0.GetMoneyBack,
}
net_cmd.SaveLocationBack = {
    cmd = test_1_0.SaveLocationBack,
}
net_cmd.GetGameRecordBack = {
    cmd = test_1_0.GetGameRecordBack,
    game_count = 0, --数量
    games = {
        [0] = {
            game_id = 0,
            win_status = 1,  --胜利状态
            amount = 200,    --
            create_time = "2018-02-05 10:26:41",
            userid = 0,    --为0为没有对手，不为0是猜拳
            setup = {
                ["抢庄方式"] = "看牌抢庄",
                ["牌型倍数"] = "普通模式",
                ["下注倍数"] = "小倍",
                ["特殊牌型"] = "同花顺,葫芦,同花,顺子",
                ["底注"] = 100,
                ["入场"] = 3000,
                ["离场"] = 3000,
                ["禁止陌生人加入"] = false,
                ["服务费比例"] = 0.3,
            }
        }
    }
}
net_cmd.GetRankBack = {
    cmd = test_1_0.GetRankBack,
    user_count = 30, --数量
    users = {
        [0] = {
            userid = 0,
            reg_ip = "注册ip地址",
            location = "",
            nickname = "昵称",
            gender = 1, --性别
            wxheadurl = "head url",
            user_sign = "个性签名",
            agent_id = "1",
            win_gold = 0,
            gold = 0,
        }
    }
}
net_cmd.GetNoticeBack = {
    cmd = test_1_0.GetNoticeBack,
    count = 2, --数量
    contents = {
        [0] = {
            content = "内容",
        }
    }
}
net_cmd.GetRechargeBack = {
    cmd = test_1_0.GetRechargeBack,
}
net_cmd.GetMailBack = {
    cmd = test_1_0.GetMailBack,
    mailcount = 0, --数量
    mails = {},
}
net_cmd.SetMailReadBack = {
    cmd = test_1_0.SetMailReadBack,
}
net_cmd.GetStoreListBack = {
    cmd = test_1_0.GetStoreListBack,
    pay_limit_everyday = 2000,   --单日支付上线
    pay_everyday = 0,    --今日已支付
    goods_count = 0,      --商品数量
    goods = {       --商品列表
        [1] = {
            goods_id = 0,     --id
            state = 0,  --状态
            goods_name = nil,
            goods_price = 0,  --花费的人民币
            goods_num = 0, --得到的金币
            reasons = nil, --简介
        },
    }
}
net_cmd.ModifySignBack = {
    cmd = test_1_0.ModifySignBack,
}
net_cmd.GetVerifyCodeCallBack = {
    cmd = test_1_0.GetVerifyCodeCallBack,
}
net_cmd.ModifyPassWordCallBack = {
    cmd = test_1_0.ModifyPassWordCallBack,
}
net_cmd.GameServerChangeBack = {
    cmd = test_1_0.GameServerChangeBack,
}
net_cmd.CreateGameRoomBack = {
    cmd = test_1_0.CreateGameRoomBack,
    roomid = roomid,
}
net_cmd.EnterGameRoomBack = {
    cmd = test_1_0.EnterGameRoomBack,
    roomid = 114920,
    room_user_setup = "json格式字符串",
}
net_cmd.ServerPush = {
    cmd = test_1_0.ServerPush,
}



return net_cmd