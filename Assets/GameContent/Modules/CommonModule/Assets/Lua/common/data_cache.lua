require("player_data")

data_cache                   = {}
local this                   = data_cache

--玩家自身数据
data_cache.mine              = nil
data_cache.other             = nil
--服务器时间
data_cache.serverTime        = 0
--玩家当前选择的游戏类型
data_cache.curSelGame        = nil
data_cache.curGameKind = 0
--商城列表
data_cache.storeList         = {}

--房间ID
data_cache.roomId            = nil
--是否在游戏内部
data_cache.isInGame			 = false

--自己的座位ID
data_cache.selfSeatId        = nil
--房间信息
data_cache.roomSetup         = nil

--游戏
data_cache.gameFightLandlrod = nil

--玩家id和座位映射
data_cache.seatid2userid     = {}

--比赛列表信息
data_cache.matchList         = {}

--在线人数
data_cache.onlineNum		 = 0

local function DealGames(_games)
	--以游戏名称为依赖，方便调用
	for k, v in pairs(_games) do
		data_cache[v.name] = v
	end
end
function this.OnMessageCome(_msg)
	--监听游戏配置表消息
	if _msg.msgType == MessageType.RefreshGameDataCache then
		DealGames(_msg.body.content)
	end
	--监听玩家数据刷新消息
	if _msg.msgType == MessageType.RefreshSelfPlayerData then
		if not data_cache.mine then
			data_cache.mine = player_data.New()
		end
		data_cache.mine = _msg.body.content
	end
	--监听进入房间消息
	if _msg.msgType == MessageType.ServerResponsed then
		local _body = _msg.body.content
		if _body.pro_con_id == net_module_protocol_contrast.enterroomack.pro_con_id then
			this.OnEnterRoom(_body)
		end
	end
	--退出登录消息
	if _msg.msgType == MessageType.GlobalLogout then
		this.GlobalLogout()
		return
	end
	
	--监听比赛列表消息
	if _msg.msgType == MessageType.ServerResponsed then
		local _body = _msg.body.content
		if _body.pro_con_id == net_module_protocol_contrast.getmatchlistack.pro_con_id then
			this.matchList = _body.items
		end
	end
	
	--监听比赛信息刷新
	if _msg.msgType == MessageType.RefreshMatchInfo then
		local _body = _msg.body.content
		local id = tonumber(_body.matchid)
		
		for k,v in pairs(this.matchList) do
			if v.matchid == id then
				utils.CloneTo(v, _body)
				break
			end
		end
	end

	--当断线重连
	if _msg.msgType == MessageType.ReConnectServer then
		--如果没有在游戏中，则做退出登录处理
		--如果在游戏中，则有 gamebase自行处理
		if not this.isInGame then
			utils.OpenDialog("由于您长时间处于非活跃状态，请重新登录")
			this.GlobalLogout()
		end
	end
end
function this.Init()
	events.AddListener(MessageType.RefreshSelfPlayerData, this)
	events.AddListener(MessageType.RefreshGameDataCache, this)
	events.AddListener(MessageType.ServerResponsed, this)
	events.AddListener(MessageType.GlobalLogout, this)
	events.AddListener(MessageType.ReConnectServer, this)
end
function this.Destroy()
	events.RemoveListener(MessageType.RefreshSelfPlayerData, this)
	events.RemoveListener(MessageType.RefreshGameDataCache, this)
	events.RemoveListener(MessageType.ServerResponsed, this)
	events.RemoveListener(MessageType.GlobalLogout, this)
	events.RemoveListener(MessageType.ReConnectServer, this)
end
--对比是否是自己
function data_cache.IsMine(_player)
	if not _player or not data_cache.mine then
		return false
	end
	local _muid = data_cache.mine.userid
	local _tuid = tonumber(_player.userid or _player.user_id)
	return _muid == _tuid
end
--对比是否是自己d
function data_cache.IsMineId(_uid)
	if not data_cache.mine then
		return false
	end
	local _muid = data_cache.mine.userid
	return _muid == _uid
end
--获取语音文件临时存放地址
function data_cache.GetTempVoicePathWithSeat(_seat)
	return ProjectDatas.PATH_CACHE .. _seat .. ProjectDatas.NAME_TEMP_VOICE_FILE
end

--解析房间json配置
function data_cache.ParseRoomSetup(_setup)
	if not utils.IsNullOrEmptyString(_setup) then
		data_cache.roomSetup = json.decode(_setup)
	end
end

function data_cache.OnSelectedGame(_cur, _kind)
	data_cache.curSelGame = _cur
end

--退出登录
function this.GlobalLogout()
	--清除本地登录记录
	utils.SavePrefsData(CommonKey.LOGIN_RECORD, false)
	events.Brocast(CreateMessage(MessageType.CloseAllUIModule))
	events.Brocast(CreateMessage(MessageType.CloseAllImportentModule))
	events.Brocast(CreateMessage(MessageType.ModuleOpen, "loginbackground_module", "LoginBackgroundModule"))
	events.Brocast(CreateMessage(MessageType.ModuleOpen, "login_module", "LoginModule"))
end
--请求进入房间
function data_cache.RequestOnEnterRoom(_roomid, _return)
	local _cmd        = utils.Clone(net_module_protocol_contrast.enterroomreq)
	_cmd.roomid       = _roomid
	data_cache.gameReturnModuel = _return or "roomlist_module"
	events.Brocast(CreateMessage(MessageType.ServerRequested, nil, _cmd))
end
--进入房间
function data_cache.OnEnterRoom(_msg)
	if _msg.isSuccess then
		--防止重复拉入信息
		if this.roomId == _msg.roomid then
			return
		end
		this.roomId = _msg.roomid
		this.ParseRoomSetup(_msg.setup)

		--关闭所有UI界面
		events.Brocast(CreateMessage(MessageType.CloseAllUIModule))
		local _gm = this.GetGameModuleByRoomId(_msg.roomid)
		if _gm then
			events.Brocast(CreateMessage(MessageType.ModuleOpen, _gm))
		else
			utils.OpenDialog("找不到指定的游戏类型，进入房间失败！")
		end
	else
		if _msg.result == "FULL" then
			utils.OpenFloatTips("房间已满")
		elseif _msg.result == "NOTFOUND" then
			utils.OpenFloatTips("房间不存在")
		elseif _msg.result == "MONEY" then
			utils.OpenFloatTips("金币不足")
		elseif _msg.result == "FAILED" then 
			utils.OpenFloatTips("进入房间失败失败")
		elseif _msg.result == "PLAYING" then 
			utils.OpenFloatTips("在游戏中")	
		end
		this.roomId = nil
	end
end
--通过房间id查询游戏类型
function data_cache.GetGameModuleByRoomId(_roomid)
	local _gameid = this.GetGameIdByRoomId(_roomid) or -1
	local _gc = game_config.games[_gameid]
	if _gc then
		return _gc.module
	else
		return nil
	end
end
--通过房间id获取游戏类型
function data_cache.GetGameIdByRoomId(_roomid)
	_roomid       = _roomid or data_cache.roomId
	local _gameid = nil
	if _roomid then
		_gameid = math.floor(_roomid / 100000)
	end
	return _gameid
end
--获取游戏kind
function data_cache.GetGameKind(_roomid)
	_roomid = _roomid or data_cache.roomId
	if _roomid then
		local _gid  = math.floor(_roomid / 100000)
		local _t2   = math.floor(_roomid / 20000)
		local _kind = _t2 - (_gid * 5)
		return _kind
	end
	return 0
end

--通过座位id获取玩家id
function data_cache.GetUserIdBySeatId(_seat_id)
	return data_cache.seatid2userid[_seat_id]
end

--通过玩家id获取座位id
function data_cache.GetSeatIdByUserId(_user_id)
	for k, v in pairs(data_cache.seatid2userid) do
		if v == _user_id then
			return k
		end
	end
	return nil
end

function data_cache.CacheMatchList()
	local _cmd = utils.Clone(net_module_protocol_contrast.getmatchlistreq)
	_cmd.category = "";
	events.Brocast(CreateMessage(MessageType.ServerRequested, nil, _cmd))
end

function data_cache.GetMatchByMatchId(_match_id)
	return data_cache.matchList[_match_id]
end

return data_cache