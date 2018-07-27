third_plantforms = {}

local property = {
    wechat = {},
	appStore = {},
}

third_plantforms.loginType = {
	--匿名（游客）
	ANONYMOUS = 0,
	--自有
	REGISTERED = 1,
	--新浪
	SINA_WEIBO = 2,
	--qq
	QQ = 3,
	--腾讯微博
	TENCENT_WEIBO = 4,
	--91
	ND91 = 5,
	--微信
	WINXIN = 6,
	--自定义（1-10）
	TYPE1 = 7,
	TYPE2 = 8,
	TYPE3 = 9,
	TYPE4 = 10,
	TYPE5 = 11,
	TYPE6 = 12,
	TYPE7 = 13,
	TYPE8 = 14,
	TYPE9 = 15,
	TYPE10 = 16,
}

function third_plantforms.Destroy()
    events.RemoveListener(MessageType.PlatformResponse, third_plantforms)
end
function third_plantforms.Init()
	ChannelData.Refresh()
    events.AddListener(MessageType.PlatformResponse, third_plantforms)

	--打开第三方模块
	lua_bridge.Recevive("GameFramework.PlantformModule", MessageType.ModuleOpen , "")
	--第三方注册需要的参数
	local _tab = {}
	--关键字
	_tab.keyword = "Init"
	--以下两个参数千万不能改，改了收不到回调消息
	_tab.UnityObjName = "GameFramework.PlantformModule"
	_tab.UnityFuncName = "PlantformMsgCome"
    lua_bridge.Recevive("", MessageType.PlatformRequest, json.encode(_tab))
    
    third_plantforms.WechatInit()
	third_plantforms.TalkingDataInit()
	--正式发包才注册bugly
	if not ProjectDatas.isDebug then
		third_plantforms.BuglyInit()
	end
end
function third_plantforms.OnMessageCome(_msg)
    if _msg.msgType == MessageType.PlatformResponse then
        local _json = json.decode(_msg.body.content)
        --微信登录成功
        if _json.keyword == "OnWechatLoginSuccess" and property.wechat.onLoginCallback then
            property.wechat.onLoginCallback(_json.code)
			property.wechat.onLoginCallback = nil
			return
		end
		if _json.keyword == "OnWechatPayBack" then
			third_plantforms.WechatPayBack(_json.errCode)
			return
		end
		if _json.keyword == "OnAppStorePayBack" then
			third_plantforms.AppStoreBack(_json)
			return
		end
    end
end

--appstore
function third_plantforms.OnAppStorePay(_good_id, _goods_price, _goods_name)
	local _tab = {}
	_tab.keyword = "AppStorePay"
	_tab.pay_type = "appstore"
	_tab.app_name = ChannelData.current.appName
	_tab.user_id = data_cache.mine.userid
	_tab.goods_id = _good_id
	_tab.goods_price = _goods_price
	_tab.goods_name = _goods_name
	lua_bridge.Recevive("", MessageType.PlatformRequest, json.encode(_tab))
end
function third_plantforms.AppStoreBack(_json)

end
--appstore end

-- wechat
-- 注册微信
function third_plantforms.WechatInit()
	local _tab = {}
	_tab.keyword = "WechatInit"
	_tab.appid = ChannelData.current.wxappID
	lua_bridge.Recevive("", MessageType.PlatformRequest, json.encode(_tab))
end
--微信授权登录
function third_plantforms.WechatLogin(_cb)
    property.wechat.onLoginCallback = _cb
    --构建json，向第三方平台请求
    local _tab = {}
    _tab.keyword = "WechatLogin"
    lua_bridge.Recevive("", MessageType.PlatformRequest, json.encode(_tab))
end
function third_plantforms.WechatShare(_tab)
	_tab.title = "手指动起来，奖励领不完"
	_tab.url = "www.shizhougame.com"
	_tab.description = "我正在玩【十洲风云】，正宗棋牌，玩法多多奖励多多，诚邀和我一起对战一起赢！！"
	lua_bridge.Recevive("", MessageType.PlatformRequest, json.encode(_tab))
end
function third_plantforms.WechatShareToFriend()
	local _tab = {}
	_tab.keyword = "WechatShareToFriend"
	third_plantforms.WechatShare(_tab)
end
function third_plantforms.WechatShareToFriends()
	local _tab = {}
	_tab.keyword = "WechatShareToFriends"
	third_plantforms.WechatShare(_tab)
end
function third_plantforms.WechatPay(_good_id, _goods_price, _goods_name)
	coroutine.start( function()
		local _tab = {}
		_tab.keyword = "WechatPay"
		_tab.pay_type = "wechat"
		_tab.app_name = ChannelData.current.appName
		_tab.user_id = data_cache.mine.userid
		_tab.goods_id = _good_id
		_tab.goods_price = _goods_price
		_tab.goods_name = _goods_name
		_tab.url = "http://119.27.178.186/pay/wechatreq?goods_id=".._tab.goods_id.."&user_id=".._tab.user_id.."&app_name=".._tab.app_name
		-- _tab.url = "http://pay.1101mmdd.com/pay/wechatreq?goods_id=".._tab.goods_id.."&user_id=".._tab.user_id.."&app_name=".._tab.app_name
		
		utils.OpenLoading("获取订单号……")
		local _request = UnityWebRequest.Get(_tab.url)
		_request:SetRequestHeader("User-Agent", "Mozilla/5.0 AppleWebKit/537.36 Chrome/66.0.3359.139 Safari/537.36")
		_request:SetRequestHeader("Accept", "application / json charset = utf - 8")
		_request:SetRequestHeader("Accept-Language", "zh - CN,zh q = 0.9")
		_request:SetRequestHeader("X-Requested-With", "FSPF")
		_request:Send()
		coroutine.www(_request)
		utils.CloseLoading()
		
		local _isSuccess = not _request.isError
		local _successCode = tostring(_request.responseCode) == "200"

		if _isSuccess and _successCode then
			utils.LogWarning(_request.downloadHandler.text)
			local _json = json.decode(_request.downloadHandler.text)
			--appid
			_tab.appid = ChannelData.current.wxappID
			--商户id
			_tab.partnerid = _json.data.mch_id
			--预支付id（订单号）
			_tab.prepayid = _json.data.prepay_id
			--扩展字段（微信固定值）
			_tab.package = "Sign=WXPay"
			--随机字符串
			_tab.noncestr = _json.data.nonce_str
			--时间戳
			_tab.timestamp = _json.data.timestamp
			--签名
			_tab.sign = _json.data.sign

			lua_bridge.Recevive("", MessageType.PlatformRequest, json.encode(_tab))
		else
			utils.OpenFloatTips("请求订单失败！")
		end
	end)
end
function third_plantforms.WechatPayBack(_code)
	local _ec = tonumber(_code)
	if _ec == -1 then
		utils.OpenFloatTips("支付失败！")
	elseif _ec == -2 then
		utils.OpenFloatTips("支付取消！")
	end
end
-- wechat end

--bugly 
--注册bugly
function third_plantforms.BuglyInit()
	local _tab = {}
	_tab.keyword = "BuglyInit"
	if common_config.IsIOS() then
		_tab.appid = ChannelData.current.buglyIOSID
	else
		_tab.appid = ChannelData.current.buglyAndroidID
	end
	_tab.debug = ProjectDatas.isDebug
	lua_bridge.Recevive("", MessageType.PlatformRequest, json.encode(_tab))
end
--bugly end

--talkingdata
--注册talkingdata
function third_plantforms.TalkingDataInit()
	local _tab = {}
	_tab.keyword = "TalkingdataInit"
	_tab.appid = ChannelData.current.talkingDataID
	_tab.channelid = ChannelData.current.channelID
	lua_bridge.Recevive("", MessageType.PlatformRequest, json.encode(_tab))
end
--统计登录
function third_plantforms.TalkingdataOnLogin()
	local _tab = {}
	_tab.keyword = "TalkingdataOnLogin"
	_tab.account = data_cache.mine.wxid
	--登录标志 目前统一设为1
	_tab.account_type = third_plantforms.loginType.REGISTERED
	_tab.level = 0
	_tab.server = "default"
	_tab.name = data_cache.mine.nickname
	_tab.sex = data_cache.mine.sex
	lua_bridge.Recevive("", MessageType.PlatformRequest, json.encode(_tab))
end
--统计任务
-- mission：任务标志， mis_type：任务进度标志（begin，fail，complete）， msg：其他信息
function third_plantforms.TalkingdataOnMission(_mission, _mis_type, _msg)
	local _tab = {}
	_tab.keyword = "TalkingdataOnMission"
	_tab.mission = _mission
	_tab.mis_type = _mis_type
	_tab.msg = _msg
	lua_bridge.Recevive("", MessageType.PlatformRequest, json.encode(_tab))
end
--统计自定义事件
-- event：事件标志， maps：字典类型的table
function third_plantforms.TalkingdataOnCustomEvent(_event, _maps)
	local _tab = {}
	_tab.keyword = "TalkingdataOnCustomEvent"
	_tab.event = _event
	_tab.property = _maps
	lua_bridge.Recevive("", MessageType.PlatformRequest, json.encode(_tab))
end
--talkingdata end

--QQ
--点击客服按钮跳转到QQ
function third_plantforms.JumpToQQEvent(_number)
	local _tab = {}
	_tab.keyword = "JumpToQQEvent"
	_tab.number  = _number
	lua_bridge.Recevive("", MessageType.PlatformRequest, json.encode(_tab))	
end

--剪贴板
--点击复制按钮复制文本
function third_plantforms.CopyToClipboard(_words)
	local _tab = {}
	_tab.keyword = "CopyToClipboard"
	_tab.words   = _words
	lua_bridge.Recevive("", MessageType.PlatformRequest, json.encode(_tab))	
end

return third_plantforms