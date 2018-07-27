--[[
    创建时间：2018.2.26
    创建人：伍霖峰
]]
require("common.randoms")

utils = {}

function utils.isNull(_o)
	if _o then
		return false
	end
	utils.LogWarning("物体为空，请检查是否为异常参数\n！")
	return true
end
--以下为Unity操作
--激活GameObject，自动判空 (参数 ce：可以为空)
function utils.ActiviteGameObject(_g, _a, _ce)
	if not _ce and utils.isNull(_g) then
		return
	end
	UtilityUnity.ActiviteGameObject(_g, _a)
end
--删除GameObject，自动判空
function utils.DestroyGameObject(_g, _ce)
	if not _ce and utils.isNull(_g) then
		return
	end
	UtilityUnity.DestroyGameObject(_g)
end
--删除组件，自动判空
function utils.DestroyComponent(_g, _ce)
	if not _ce and utils.isNull(_g) then
		return
	end
	UtilityUnity.DestroyComponent(_g)
end
--删除所有子物体，自动判空
function utils.DestroyAllChilds(_g, _ce)
	if not _ce and utils.isNull(_g) then
		return
	end
	UtilityUnity.DestroyAllChilds(_g)
end
--隐藏所有子物体
function utils.HideAllChilds(_g, _ce)
	if not _ce and utils.isNull(_g) then
		return
	end
	UtilityUnity.HideAllChilds(_g)
end
--删除子物体，按名称保留，自动判空
function utils.DestroyChildsExceptNames(_g, ...)
	if utils.isNull(_g) then
		return
	end
	UtilityUnity.DestroyChildsExceptNames(_g, ...)
end
--移除UI的物理点击事件
function utils.OperateRaycastTarget(_g, _op, _ce)
	if not _ce and utils.isNull(_g) then
		return
	end
	UtilityUnity.OperateRaycastTarget(_g, _op)
end
--查找子物体
function utils.FindChild(_g, _name, _ce)
	if not _ce and utils.isNull(_g) then
		return
	end
	return UtilityUnity.FindChild(_g, _name)
end
--获取所有子物体
function utils.GetAllChild(_g, _ce)
	if not _ce and utils.isNull(_g) then
		return
	end
	return UtilityUnity.GetAllChild(_g)
end
--设置父级
function utils.SetParent(_g, _parent, _ce)
	if not _ce and utils.isNull(_g) then
		return
	end
	return UtilityUnity.SetParent(_g, _parent)
end
--使用该种方式添加的点击，不会广播事件。同时这种监听方式可以设置为不受UI按钮限制的控制，可以作为新手引导的按钮点击方式。
function utils.AddButtonClick(_g, _cb, _nolimit, _ce)
	if not _ce and utils.isNull(_g) then
		return
	end
	UtilityUnity.AddButtonClick(_g, _cb, _nolimit)
end
--按名称查找父级，直到找到或者没有更上层的父级
function utils.FindParentByName (_g, _name, _ce)
	if not _ce and utils.isNull(_g) then
		return
	end
	UtilityUnity.FindParentByName(_g, _name)
end
function utils.SetText (_obj, _v, _ce)
	if not _ce and utils.isNull(_obj) then
		return
	end
	UtilityUnity.SetText(_obj, _v)
end
function utils.SetFont (_obj, _v, _ce)
	if not _ce and utils.isNull(_obj) then
		return
	end
	UtilityUnity.SetFont(_obj, _v)
end
function utils.SetSprite (_obj, _v, _ce)
	if not _ce and utils.isNull(_obj) then
		return
	end
	UtilityUnity.SetSprite(_obj, _v)
end
function utils.SetMaterial (_obj, _v, _ce)
	if not _ce and utils.isNull(_obj) then
		return
	end
	UtilityUnity.SetMaterial(_obj, _v)
end
function utils.SetGray(_obj, _cacle)
	common_manager.GrayObject(_obj, _cacle)
end
function utils.SetGrayAll(_obj, _cacle)
	common_manager.GrayObjectAndChild(_obj, _cacle)
end

function utils.SetSpriteByUrl (_obj, _url, _width, _heigth, _force)
	UtilityUnity.SetSpriteByUrl(_obj, _url, _width, _heigth, _force and true or false)
end
function utils.SetColor (_obj, _v, _ce)
	if not _ce and utils.isNull(_obj) then
		return
	end
	UtilityUnity.SetColor(_obj, _v)
end
function utils.SetAlpha (_obj, _v, _ce)
	if not _ce and utils.isNull(_obj) then
		return
	end
	UtilityUnity.SetAlpha(_obj, _v)
end
function utils.SetPosition (_obj, _v, _ce)
	if not _ce and utils.isNull(_obj) then
		return
	end
	_v = _v or Vector3.zero
	UtilityUnity.SetPosition(_obj, _v)
end
function utils.SetLocalPosition (_obj, _v, _ce)
	if not _ce and utils.isNull(_obj) then
		return
	end
	_v = _v or Vector3.zero
	UtilityUnity.SetLocalPosition(_obj, _v)
end
function utils.SetEulerAngles (_obj, _v, _ce)
	if not _ce and utils.isNull(_obj) then
		return
	end
	_v = _v or Vector3.zero
	UtilityUnity.SetEulerAngles(_obj, _v)
end
function utils.SetLocalEulerAngles (_obj, _v, _ce)
	if not _ce and utils.isNull(_obj) then
		return
	end
	_v = _v or Vector3.zero
	UtilityUnity.SetLocalEulerAngles(_obj, _v)
end
function utils.SetScale (_obj, _v, _ce)
	if not _ce and utils.isNull(_obj) then
		return
	end
	_v = _v or Vector3.one
	UtilityUnity.SetScale(_obj, _v)
end
function utils.SetUIPosition (_obj, _v, _ce)
	if not _ce and utils.isNull(_obj) then
		return
	end
	UtilityUnity.SetUIPosition(_obj, _v)
end
function utils.SetUIScale (_obj, _v, _ce)
	if not _ce and utils.isNull(_obj) then
		return
	end
	UtilityUnity.SetUIScale(_obj, _v)
end
function utils.SetUIAnchorMin (_obj, _v, _ce)
	if not _ce and utils.isNull(_obj) then
		return
	end
	UtilityUnity.SetUIAnchorMin(_obj, _v)
end
function utils.SetUIAnchorMax (_obj, _v, _ce)
	if not _ce and utils.isNull(_obj) then
		return
	end
	UtilityUnity.SetUIAnchorMax(_obj, _v)
end
function utils.SetUIPivot (_obj, _v, _ce)
	if not _ce and utils.isNull(_obj) then
		return
	end
	UtilityUnity.SetUIPivot(_obj, _v)
end
function utils.SetNativeSize (_obj, _ce)
	if not _ce and utils.isNull(_obj) then
		return
	end
	UtilityUnity.SetNativeSize(_obj)
end
function utils.GetText(_obj)
	if utils.isNull(_obj) then
		return
	end
	
	local _txtobj = _obj:GetComponent("InputField")
	if _txtobj ~= nil then
		return _txtobj.text
	end
	
	_txtobj = _obj:GetComponent("Text")
	if _txtobj ~= nil then
		return _txtobj.text
	end
end

--以下为动画操作
--本地旋转动画
function utils.ToLocalRotation(_target, _end, _time, _ease)
	return UtilityUnity.ToLocalRotation(_target, _end, _time, _ease or Ease.Linear)
end
--旋转动画
function utils.ToRotation(_target, _end, _time, _ease)
	return UtilityUnity.ToRotation(_target, _end, _time, _ease or Ease.Linear)
end
--本地欧拉角动画
function utils.ToLocalEulerAngles(_target, _end, _time, _ease)
	return UtilityUnity.ToLocalEulerAngles(_target, _end, _time, _ease or Ease.Linear)
end
--欧拉角动画
function utils.ToEulerAngles(_target, _end, _time, _ease)
	return UtilityUnity.ToEulerAngles(_target, _end, _time, _ease or Ease.Linear)
end
--缩放动画
function utils.ToScale(_target, _end, _time, _ease)
	return UtilityUnity.ToScale(_target, _end, _time, _ease or Ease.Linear)
end
--UGUI缩放动画
function utils.ToUGUIScale(_target, _end, _time, _ease)
	return UtilityUnity.ToUGUIScale(_target, _end, _time, _ease or Ease.Linear)
end
--移动动画
function utils.ToMove(_target, _end, _time, _ease)
	return UtilityUnity.ToMove(_target, _end, _time, _ease or Ease.Linear)
end
--本地移动动画
function utils.ToMoveLocal(_target, _end, _time, _ease)
	return UtilityUnity.ToMoveLocal(_target, _end, _time, _ease or Ease.Linear)
end
--UGUI移动动画
function utils.ToMoveUGUI(_target, _end, _time, _ease)
	return UtilityUnity.ToMoveUGUI(_target, _end, _time, _ease or Ease.Linear)
end
--UGUI显示Int型数字动画
function utils.ToUGUIShowInt(_target, _start, _end, _time, _ease)
	return UtilityUnity.ToUGUIShowInt(_target, _start, _end, _time, _ease or Ease.Linear)
end
--UGUI显示数字动画，限定小数位
function utils.ToUGUIShowNumber(_target, _start, _end, _time, _showLimit, _ease)
	return UtilityUnity.ToUGUIShowNumber(_target, _start, _end, _time, _showLimit or 2, _ease or Ease.Linear)
end
--颜色变化动画
function utils.ToUGUIColor(_target, _start, _end, _time, _ease)
	return UtilityUnity.ToUGUIColor(_target, _start, _end, _time, _ease or Ease.Linear)
end
--透明通道变化动画
function utils.ToAlpha(_target, _start, _end, _time, _ease)
	return UtilityUnity.ToAlpha(_target, _start, _end, _time, _ease or Ease.Linear)
end

--以下为Utility操作
--消息，正式版发布会自动屏蔽
function utils.Log (...)
	if ProjectDatas.isDebug then
		Utility.Log(Utility.MergeString("Lua log Msg: ", ...), "\n", debug.traceback())
	end
end
--警告，正式版发布会自动屏蔽
function utils.LogWarning (...)
	if ProjectDatas.isDebug then
		Utility.LogWarning(Utility.MergeString("Lua log Msg: ", ...), "\n", debug.traceback())
	end
end
--错误，永远不会屏蔽，并且永远带有调用栈
function utils.LogError (...)
	Utility.LogError(Utility.MergeString("Lua log Msg: ", ...), "\n", debug.traceback())
end

--计时器操作
--生成计时器，返回id
function utils.CreateTimer(_func, _loopCount, _loopSpace, _delay)
	if not _func then
		utils.Log("添加计时器失败，请指定接收调用函数")
		return
	end
	-- -1默认值表示无限循环
	_loopCount = _loopCount or -1
	-- -1默认值表示每帧循环（间隔时间不生效，最短时间为一帧）
	_loopSpace = _loopSpace or -1
	_delay     = _delay or 0
	return Utility.CreateTimer(_func, _loopCount, _loopSpace, _delay)
end
--创建延时调用
function utils.CreateDelayInvok(_func, _delay)
	return Utility.CreateDelayInvok(_func, _delay)
end
--改变某个计时器的间隔时间
function utils.ChangedTimerSpecaTime(_id, _v)
	Utility.ChangedTimerSpecaTime(_id, _v)
end
--改变一个计时器的循环次数
function utils.ChangedTimerLoopCount(_id, _v)
	Utility.ChangedTimerLoopCount(_id, _v)
end
--播放动画，并在完成后自动关闭
function utils.PlayerAnimationAndCloseOnComplete(_obj, _ce)
	if not _ce and utils.isNull(_obj) then
		return
	end
	Utility.PlayerAnimationAndCloseOnComplete(_obj)
end
--按id移除计时器
function utils.RemoveTimer(_id)
	if not _id then
		return
	end
	Utility.RemoveTimer(_id)
end

--md5
--生成字符串的MD5值
function utils.MD5 (_source)
	return Utility.MD5(_source)
end
--按路径生成文件的MD5值
function utils.MD5File (_file)
	return Utility.MD5File(_file)
end

--make
--规格化路径或者名称
function utils.MakeUnifiedDirectory (_dir)
	return Utility.MakeUnifiedDirectory(_dir)
end
--获取不带后缀的文件名
function utils.GetFileNameWithoutExtension(_path)
	return Utility.GetFileNameWithoutExtension(_path)
end

--数据
function utils.SavePrefsData (_key, _value)
	PersistenceData.SavePrefsData(_key, _value)
end
function utils.GetPrefsData (_key, _default)
	return PersistenceData.GetPrefsData(_key, _default)
end
function utils.GetPrefsDataNumber (_key, _default)
	return PersistenceData.GetPrefsDataDouble(_key, _default)
end
function utils.GetPrefsDataBool (_key, _default)
	return PersistenceData.GetPrefsDataBool(_key, _default)
end
function utils.SetRunningData (_key, _value, _force)
	RunningTimeData.SetRunningData(_key, tostring(_value), _force)
end
function utils.SetRunningDataByFile (_file)
	RunningTimeData.SetRunningDataByFile(_file)
end
function utils.GetRunningData (_key, _default)
	return RunningTimeData.GetRunningData(_key, _default)
end
function utils.GetRunningDataNumber (_key, _default)
	return RunningTimeData.GetRunningDataFloat(_key, _default)
end
function utils.GetRunningDataBool (_key, _default)
	return RunningTimeData.GetRunningDataBool(_key, _default)
end

--池
function utils.PoolPush(_key, _obj)
	Utility.PoolPush(_key, _obj)
end
function utils.PoolPop(_key)
	return Utility.PoolPop(_key)
end

--以下为功能性

--打开等待界面(_text为显示文字，_time为关闭时间)
function utils.OpenLoading(_text, _time, _timeOutMsg, _symbol)
	local _c = {}
	_c.text  = _text
	_c.time  = _time
	_c.tomsg = _timeOutMsg
	_c.symbol = _symbol
	events.Brocast(CreateMessage(MessageType.ModuleOpen, "loading_module", _c))
end
function utils.CloseLoading(_symbol)
	events.Brocast(CreateMessage(MessageType.CloseLoading, nil, _symbol))
end

--打开对话框 (信息，确认按钮回调)
function utils.OpenDialog(_msg, _sureAc, _closeAc, _type)
	local _c = {}
	_c.str   = _msg
	_c.sure  = _sureAc
	_c.close = _closeAc
	_c.type  = _type
	
	events.Brocast(CreateMessage(MessageType.ModuleOpen, "dialog_module", _c))
end
--显示漂浮文字 （信息，持续时间）
function utils.OpenFloatTips(_msg)
	if not _msg then
		return
	end
	events.Brocast(CreateMessage(MessageType.ModuleOpen, "floattips_module", _msg))
end
--table长度，有序table直接用 #table，无序table用该接口
function utils.Tablelen(_table)
	if not _table then
		return 0
	end
	local _len = 0
	for _, t in pairs(_table) do
		_len = _len + 1
	end
	return _len
end
--打印表
function utils.Print(root)
	local cache = { [root] = "." }
	local function _dump(t, space, name)
		local temp = {}
		for k, v in pairs(t) do
			local key = tostring(k)
			if cache[v] then
				table.insert(temp, "+" .. key .. " {" .. cache[v] .. "}")
			elseif type(v) == "table" then
				local new_key = name .. "." .. key
				cache[v]      = new_key
				table.insert(temp, "+" .. key .. _dump(v, space .. (next(t, k) and "|" or " ") .. string.rep(" ", #key),
				                                       new_key))
			else
				table.insert(temp, "+" .. key .. " [" .. tostring(v) .. "]")
			end
		end
		return table.concat(temp, "\n" .. space)
	end
	utils.Log("\n", _dump(root, "", ""))
end
--为一个表插入表 (必须有序)
function utils.TableAppend(_ta, _tb)
	_ta = _ta or {}
	if utils.isEmptyTable(_tb) then
		return _ta
	end
	for i, v in ipairs(_tb) do
		table.insert(_ta, v)
	end
	return _ta
end
--判断空表
function utils.isEmptyTable(_table)
	return _table == nil or (type(_table) == "table" and next(_table) == nil)
end
--克隆数据，包括原表数据
function utils.Clone(_table)
	local _new = {}
	if type(_table) ~= "table" then
		return _table
	end
	if utils.isEmptyTable(_table) then
		return _new
	end
	--克隆表
	for _k, _v in pairs(_table) do
		if type(_v) == "table" and _k ~= "__index" then
			_new[_k] = utils.Clone(_v)
		else
			_new[_k] = _v
		end
	end
	--检查元表
	local _meta = getmetatable(_table)
	if _meta then
		local _newMeta   = utils.Clone(_meta)
		_newMeta.__index = _newMeta
		_new             = setmetatable(_new, _newMeta)
	end
	
	return _new
end
--如果tableB中存在于tableA中相同的键，则复制到tableA，并选择是否清理掉tableB中的值
function utils.CloneBy(_tableA, _tableB, _clean)
	if type(_tableA) ~= "table" or type(_tableB) ~= "table" then
		return _tableA
	end
	for k, v in pairs(_tableA) do
		if _tableB[k] then
			_tableA[k] = utils.Clone(_tableB[k])
		end
		if _clean then
			_tableB[k] = nil
		end
	end
	local _metaA = getmetatable(_tableA)
	if _metaA then
		for k, v in pairs(_metaA) do
			if _tableB[k] then
				_tableA[k] = utils.Clone(_tableB[k])
			end
			if _clean then
				_tableB[k] = nil
			end
		end
	end
	return _tableA
end
--克隆一个tableB的所有数据到tableA
function utils.CloneTo(_tableA, _tableB)
	if _tableA == nil then
		_tableA = {}
	end
	if _tableB == nil then
		return _tableA
	end
	if type(_tableB) ~= "table" then
		return _tableA
	end
	--克隆表
	for _k, _v in pairs(_tableB) do
		if type(_v) == "table" and _k ~= "__index" then
			_tableA[_k] = utils.CloneTo(_tableA[_k], _v)
		else
			_tableA[_k] = _v
		end
	end
	--检查元表
	local _metaB = getmetatable(_tableB)
	if _metaB then
		local _newMeta = utils.Clone(_metaB)
		_tableA        = utils.CloneTo(_tableA, _newMeta)
	end
	
	return _tableA
end
--字符串是否为空
function utils.IsNullOrEmptyString(_s)
	if type(_s) == "string" then
		return _s == nil or string.len(_s) == 0
	else
		return true
	end
end
--字符串分割
function utils.Split(_szFullString, _szSeparator)
	local _nFindStartIndex = 1
	local _nSplitIndex     = 1
	local _nSplitArray     = {}
	while true do
		local _nFindLastIndex = string.find(_szFullString, _szSeparator, _nFindStartIndex)
		if not _nFindLastIndex then
			_nSplitArray[_nSplitIndex] = string.sub(_szFullString, _nFindStartIndex, string.len(_szFullString))
			break
		end
		_nSplitArray[_nSplitIndex] = string.sub(_szFullString, _nFindStartIndex, _nFindLastIndex - 1)
		_nFindStartIndex           = _nFindLastIndex + string.len(_szSeparator)
		_nSplitIndex               = _nSplitIndex + 1
	end
	return _nSplitArray
end
--将秒转换成天、时、分、秒
function utils.SecondToDHMS(_second)
	if _second <= 0 then
		return 0, 0, 0, 0
	end
	local _d = math.floor(_second / 86400)
	_second  = _second - _d * 86400
	
	local _h = math.floor(_second / 3600)
	_second  = _second - _h * 3600
	
	local _m = math.floor(_second / 60)
	_second  = _second - _m * 60
	
	return _d, _h, _m, _second
end
--将秒数转换成 00:00:00 
function utils.SecondToTimer(_second)
	local _d, _h, _m, _s = utils.SecondToDHMS(_second)
	return string.format("%02d:%02d:%02d", _h, _m, _s)
end
--字符串长度，根据需要自行做了一些处理
function utils.StringUtf8Len(_str)
	local _lenInByte = #_str
	local _width     = 0
	local _count     = 1
	while (_count <= _lenInByte) do
		local curByte   = string.byte(_str, _count)
		local byteCount = 1;
		if curByte > 0 and curByte <= 127 then
			byteCount = 1
			_width    = _width + 1                                               --1字节字符
		elseif curByte >= 192 and curByte < 223 then
			byteCount = 2
			_width    = _width + 2                                            --双字节字符
		elseif curByte >= 224 and curByte < 239 then
			byteCount = 3
			_width    = _width + 2                                           --汉字
		elseif curByte >= 240 and curByte <= 247 then
			byteCount = 4
			_width    = _width + 2                                           --4字节字符
		end
		
		local char = string.sub(_str, _count, _count + byteCount - 1)
		_count     = _count + byteCount                                              -- 重置下一字节的索引
		-- width = width + byteCount                                             -- 字符的个数（长度）
	end
	return _width
end
--按单位升级数字
function utils.UpgradeNumber(_num, _limit, _unit, _ex)
	if _num < 100000000 then
		_limit = _limit or 10000
		_unit = _unit or 10000
		_ex = _ex or "万"
	else
		_limit = _limit or 100000000
		_unit = _unit or 100000000
		_ex = _ex or "亿"
	end

	if _num == nil then
		return 0
	end
	if _num < _limit then
		return math.floor(_num)
	else
		_num = string.format( "%.2f", _num / _unit)
	end
	return _num.._ex
end
--获取随机数
function utils.Random(_min, _max)
	return randoms.Range(_min, _max)
end
--匹配汉字
function utils.FilterSpecChars(s)
	local ss = {}
	local k  = 1
	while true do
		if k > #s then
			break
		end
		local c = string.byte(s, k)
		if not c then
			break
		end
		if c < 192 then
			if (c >= 48 and c <= 57) or (c >= 65 and c <= 90) or (c >= 97 and c <= 122) then
				table.insert(ss, string.char(c))
			end
			k = k + 1
		elseif c < 224 then
			k = k + 2
		elseif c < 240 then
			if c >= 228 and c <= 233 then
				local c1 = string.byte(s, k + 1)
				local c2 = string.byte(s, k + 2)
				if c1 and c2 then
					local a1, a2, a3, a4 = 128, 191, 128, 191
					if c == 228 then
						a1 = 184
					elseif c == 233 then
						a2, a4 = 190, c1 ~= 190 and 191 or 165
					end
					if c1 >= a1 and c1 <= a2 and c2 >= a3 and c2 <= a4 then
						table.insert(ss, string.char(c, c1, c2))
					end
				end
			end
			k = k + 3
		elseif c < 248 then
			k = k + 4
		elseif c < 252 then
			k = k + 5
		elseif c < 254 then
			k = k + 6
		end
	end
	return table.concat(ss)
end


local shield = require("common.shield")
--字符串中是否含有敏感字
function utils.IsWarningInPutStr(_inputStr)
	return shield.IsWarningInPutStr(_inputStr)
end
--将字符串中敏感字用*替换返回
function utils.WarningStrGsub(_inputStr)
	return shield.WarningStrGsub(_inputStr)
end