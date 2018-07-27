--[[
    创建时间：2018.5.7
    创建人：龚明东
    内容简介：
		游戏内广播的管理
]]

game_broadcast      = {}

---广播消息模版
game_broadcast_data = {
	privaty      = false, --为false则广播给所有人，否则以一对一方式
	from_seat_id = 1, --int32,发送放位置索引，从1开始
	to_seat_id   = 1, --int32,接收方位置索引，从1开始
	type         = "", -- 类型标识
	data         = nil, -- 广播内容
}

function game_broadcast.New()
	---@class game_broadcast @游戏广播
	local this         = class_model.New()
	this.__index       = this
	
	local _action      = 100
	local _handler_map = {}
	
	---Init 初始化
	function this.Init()
		events.AddListener(MessageType.ServerResponsed, this)
	end
	
	---Destroy 销毁
	function this.Destroy()
		events.RemoveListener(MessageType.ServerResponsed, this)
	end
	
	function this.OnMessageCome(_msg)
		if _msg.msgType == MessageType.ServerResponsed then
			local _body = _msg.body.content
			if _body.pro_con_id == net_module_protocol_contrast.pushmessages.pro_con_id then
				controller.CloseLoading()
				if _body.isSuccess and _body ~= "" then
					local messages = json.decode(_body.messages)
					for k, v in pairs(messages) do
						if v.cmd == _action then
							this._onBroadCast(v)
						end
					end
				end
			end
		end
	end
	function this._onBroadCast(_msg)
		local handler = _handler_map[_msg.type]
		if handler then
			for k, v in pairs(handler) do
				if type(v) == "function" then
					v(_msg.type, _msg)
				elseif type(v) == "table" and v.OnBroadCast then
					v.OnBroadCast(_msg.type, _msg)
				else
					utils.LogError("没有该类型的监听! type:", _msg.type)
				end
			end
		else
			utils.LogError("没有该类型的监听! type:", _msg.type)
		end
	end
	
	---AddListener 添加监听
	---@param _type string @类型标识
	---@param _carrier function|class @监听方法或包含OnBroadCast(type,data)方法的实例
	function this.AddListener(_type, _carrier)
		if not _type or not _carrier then
			utils.LogError("参数错误!")
			return
		end
		
		if not _handler_map[_type] then
			_handler_map[_type] = {}
		end
		for k, v in pairs(_handler_map[_type]) do
			if v == _carrier then
				utils.LogError(_type, "游戏广播类型的监听列表中发现重复监听！请检查代码，确认是否需要重复监听！")
			end
		end
		table.insert(_handler_map[_type], _carrier)
	end
	---RemoveListener 移除监听
	---@param _type string @类型标识
	---@param _carrier function|class @监听方法或包含OnBroadCast(type,data)方法的实例
	function this.RemoveListener(_type, _carrier)
		if not _type or not _carrier then
			utils.LogError("参数错误!")
			return
		end
		
		if not _handler_map[_type] then
			return
		end
		local remove_list = {}
		for k, v in pairs(_handler_map[_type]) do
			if v == _carrier then
				table.insert(remove_list, k)
			end
		end
		for k, v in pairs(remove_list) do
			_handler_map[_type][k] = nil
		end
	end
	
	---BroadCast 广播消息
	---@param _privaty bool @是否广播给所有人
	---@param _to_seat_id number @目标座位id
	---@param _type string @类型标识
	---@param _data table @广播内容
	function this.BroadCast(_privaty, _to_seat_id, _type, _data)
		local _msg       = utils.Clone(net_module_protocol_contrast.actionreq)
		_msg.roomid      = data_cache.roomId
		_msg.action      = _action
		local data       = {}
		data.cmd         = _action
		data.privaty     = _privaty
		data.to_seat_id  = _to_seat_id
		data.type        = _type
		data.data        = _data
		_msg.action_data = json.encode(data)
		
		events.Brocast(CreateMessage(MessageType.ServerRequested, nil, _msg))
	end
	
	return this
end

return gamechat_module