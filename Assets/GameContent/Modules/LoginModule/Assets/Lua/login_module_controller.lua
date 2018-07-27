require("login_module_data")
require("login_module_view")
require("login_module_controller_channel")

login_module_controller = {}

function login_module_controller.New(_module)
    local this = lua_controller_base.New(_module)
    local base = this.__index

    function this.Init()
        if this.IsInit() then
            this.Activate()
            return
        end

        this.SetData(login_module_data.New(this))
        this.SetView(login_module_view.New(this, UILayers.Page, login_module_controller_channel.GetChannelLoginPrefab()))

        events.AddListener(MessageType.EventButtonClick, this)

        --如果是debug，打开输入
        utils.ActiviteGameObject(this.view.input, ProjectDatas.isDebug)    
        --打开比赛监听模块（全球模块）
        events.Brocast(CreateMessage(MessageType.ModuleOpen, "gamematchwaiting_module"))

        --判断是否有登录记录，如果有，则直接登录
        this.AutoLogin()
    end

    function this.Destroy()
        base.Destroy()
        events.RemoveListener(MessageType.EventButtonClick, this)
    end

    --OnMessageCome的实现可以检查meg的key来判断执行的功能
    --或者msg的key就是一个功能的名称，直接通过名称来调用
    function this.OnMessageCome(_msg)
        if _msg.msgType == MessageType.EventButtonClick then
            if this[_msg.body.key] and type(this[_msg.body.key]) == "function" then
                this[_msg.body.key](_msg.body.content)
                return
            end
        end
    end

    --其他自定义内容写在这里
    --微信登录
    function this.LoginPage_Login(_obj)
        if ProjectDatas.isDebug then
            local _code = utils.GetText(this.view.input)
            if utils.IsNullOrEmptyString(_code) then
                this.WechatLogin(_obj)
            else
                this.OnThirdLoginBack(_code)
            end
        else
            this.WechatLogin(_obj)
        end
    end
    
    --微信登录
    function this.WechatLogin(_obj)
        login_module_controller_channel.Login(_obj.name, this.OnThirdLoginBack)
    end

    --登录请求返回
    function this.OnThirdLoginBack(_code)
        local _cmd = utils.Clone(net_module_protocol_contrast.loginreq)
        _cmd.wxverifycode = _code
        _cmd.deviceInfo = SystemInfo.deviceModel.."-"..SystemInfo.operatingSystem.."-"..SystemInfo.processorType.."- game client:"..RunningTimeData.GetRunningData(CommonKey.VERSION_LOCAL, "2.0.0.0");
        _cmd.channel_id = ChannelData.current.channelID
        this.OpenLoadingWithTimeOut("正在登录...", "登录超时")
        events.Brocast(CreateMessage(MessageType.ServerRequested, nil, _cmd))
    end
    --login的玩家数据已经被数据层劫持，表现层无需处理数据
    function this.OnServerLoginBack(_body)
        if _body.isSuccess then
            --保存登录记录到本地
            utils.SavePrefsData(CommonKey.LOGIN_RECORD, true)
            --记录在线人数
            data_cache.onlineNum = _body.ref or 0
            --如果存在房间id
            if _body.roomid and _body.roomid ~= 0 then
                -- data_cache.roomId = _body.roomid
                --从id判断游戏
                local _gn = game_config.games[data_cache.GetGameIdByRoomId(_body.roomid)]
                data_cache.OnSelectedGame(_gn)
                data_cache.RequestOnEnterRoom(_body.roomid, "home_module")

                events.Brocast(CreateMessage(MessageType.ModuleOpen, "homebackground_module"))
                events.Brocast(CreateMessage(MessageType.ModuleOpen, "home_module"))
    
                --请求比赛列表信息
                data_cache.CacheMatchList()
                this.module.Kill()
            else
                this.OpenLoadingWithTimeOut("正在进入大厅...", "进入大厅超时")
            end
            local _cmd = utils.Clone(net_module_protocol_contrast.enterhallreq)
            events.Brocast(CreateMessage(MessageType.ServerRequested, nil, _cmd))
            --第三方统计
            third_plantforms.TalkingdataOnLogin()
        else
            utils.OpenFloatTips("登录失败！")
        end
    end
    --服务器进入大厅回调
    function this.OnServerEnterHallBack(_body)
        if _body.isSuccess then
            events.Brocast(CreateMessage(MessageType.ModuleOpen, "homebackground_module"))
            events.Brocast(CreateMessage(MessageType.ModuleOpen, "home_module"))
            this.module.Kill()
            events.Brocast(CreateMessage(MessageType.ModuleClose, "loginbackground_module"))
            --判断是否打开首充界面
            local _tab = json.decode(data_cache.mine.first_charge)
            if type(_tab) == "table" and _tab.isFirst then
                events.Brocast(CreateMessage(MessageType.ModuleOpen, "firstcharge_module", "FirstChargeModule"))
            end
        else
            utils.OpenDialog("进入大厅失败！", nil, nil, 1)
        end
    end
    --打开用户协议
    function this.LoginPage_UserAgreement()
        events.Brocast(CreateMessage(MessageType.ModuleOpen, "useragreement_module"))
    end

    --判断是否自动登录
    function this.AutoLogin()
        if utils.GetPrefsDataBool(CommonKey.LOGIN_RECORD) and (not ProjectDatas.isDebug) then
            local _tab = {}
            _tab.name = "BtnLogin"
            this.LoginPage_Login(_tab)
        else

        end
    end
    --自定义内容结束

    return this
end

return login_module_controller
