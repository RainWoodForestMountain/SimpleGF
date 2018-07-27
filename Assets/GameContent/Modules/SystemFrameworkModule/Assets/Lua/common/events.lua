--[[
    创建时间：2018.2.26
    创建人：伍霖峰
    内容简介：
		事件监听类型，请参照C#事件监听类型格式
		msgType 为MessageType类型值或者int类型参数
		carrier 为接受参数为Message类型的函数或者包含接受函数 OnMessageCome 的table
]]
events = {}

local recEvents = {}

function events.AddListener(_msgType, _carrier)
	if _msgType == nil then
		utils.Log ("请指定监听消息类型")
		return
	end
	if _carrier == nil then
		utils.Log ("请指定监听函数或者类结构")
		return
	end

	if not recEvents[_msgType] then
		recEvents[_msgType] = {}
	end

	if recEvents[_msgType] then
		for _i,_v in ipairs(recEvents[_msgType]) do
			if _v == _carrier then
				utils.LogError(_msgType, "类型的监听列表中发现重复监听！请检查代码，确认是否需要重复监听！")
			end
		end
	end
	
	table.insert( recEvents[_msgType], _carrier )
end

--这里的_msg是C#内的Message的派生类，也可以为同一格式的自定义lua tab
function events.Brocast(_msg)
	_msgType = _msg.msgType
	-- utils.Log("开始广播类型为", _msgType, "的消息")

	if not recEvents[_msgType] then
		utils.Log("没有找到监听类型为", _msgType, "的监听器")
	else
		local _tab = recEvents[_msgType]
		for _i,_v in ipairs(_tab) do
			if _v then
				if type(_v) == "function" then
					_v(_msg)
				elseif type(_v) == "table" then
					if _v.OnMessageCome then
						_v.OnMessageCome(_msg)
					end
				end
			else
				utils.LogError(_msgType, "类型的监听列表中发现空监听，已移除，请检查代码，规避风险")
				table.remove( _tab, _i )
			end
		end
	end
end

function events.RemoveListener(_msgType, _carrier)
	if _msgType == nil then
		utils.Log ("请指定监听消息类型")
		return
	end
	if _carrier == nil then
		utils.Log ("请指定监听函数或者类结构")
		return
	end

	if not recEvents[_msgType] then
		utils.Log("没有找到监听类型为", _msgType, "的监听")
	else
		local _tab = recEvents[_msgType]
		for _k,_v in pairs(_tab) do
			if _v == _carrier then
				table.remove( recEvents[_msgType] , _k)
			end
		end
	end
end