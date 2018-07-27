require "3rd.pbc.protobuf"
require("net_module_protocol_contrast")

net_module_protocol = {}
net_module_protocol.protocols = { "Common", "FR_Common", "Hall", "Game_Common", "Game", "Rpc", "CS_Msg" }

local assetLoadRecord = AssetLoadRecord.New("NetModule")

function net_module_protocol.New()
    local this = class_model.New()

    function this.Init()
        for i, v in ipairs(net_module_protocol.protocols) do
            local _f = assetLoadRecord:LoadLuaFileBytes(v)
            protobuf.register(_f)
        end
    end

    local function DecodeTable(_tab)
        if type(_tab) ~= "table" then
            return _tab
        end
        for k, v in pairs(_tab) do
            if type(v) == "table" then
                if v[1] and v[2] then
                    local _pc = pcall(function()
                        local _tryDecode = protobuf.decode(v[1], v[2])
                        if _tryDecode then
                            _tab[k] = DecodeTable(_tryDecode)
                        end
                    end)
                    if not _pc then
                        _tab[k] = DecodeTable(v)
                    end
                else
                    _tab[k] = DecodeTable(v)
                end
            end
        end
        return _tab
    end
    function this.Decode(_byteBuffer)
        local _id = _byteBuffer:ReadInt()
        local _sign = _byteBuffer:ReadInt()
        --特殊消息，心跳返回
        if _id == 100003 then
            local _heart = protobuf.decode("fr_common.HeartBeatAck", _byteBuffer:ReadBuffer())
            _heart.id = _id
            return _heart
        end
        --认证返回
        if _id == 100001 then
            local _heart = protobuf.decode("fr_common.AuthACK", _byteBuffer:ReadBuffer())
            _heart.id = _id
            return _heart
        end
        local _protocol = net_module_protocol_contrast.GetProtocolById(_id)
        utils.Log("<color=yellow>", "解析服务器消息： id = ", _protocol.pro_con_id, "   notes = ", _protocol.pro_con_notes, "    name = ", _protocol.pro_con_name, "</color>")

        local _tab = protobuf.decode("cs_msg.CSMsg", _byteBuffer:ReadBuffer())
        _tab = _tab[_protocol.pro_con_name]
        _tab = protobuf.decode(_tab[1], _tab[2])
        _tab = DecodeTable(_tab)
        for k, v in pairs(_tab) do
            _protocol[k] = v
        end
        return utils.Clone(_protocol)
    end
    function this.Eecode(_tab)
        local _byteBuffer = ByteBuffer.New()
        local _proName = _tab.pro_con_name
        local _proId = _tab.pro_con_id
        local _notes = _tab.pro_con_notes
        local _pro = net_module_protocol_contrast[_proName]
        --清理不必要信息
        for k, v in pairs(_pro) do
            _tab[k] = nil
        end
        --添加公共数据
        if data_cache.mine and _proId ~= 102 and _proId ~= 100 then
            _tab.token = data_cache.mine.token
        end
        local _head = {}
        _head.head = { senderrouterid = 0, sessionid = 0, roomid = 0 }
        --游戏服识别码
        if _proId >= 10001 and _proId <= 100000 then
            if _proId >= 10020 then
                _head.head.roomid = _tab.roomid or data_cache.roomId or data_cache.mine.roomid or 0
            end
            local _kind
            local _gid
            local _roomid = _head.head.roomid 
            if _roomid and _roomid ~= 0 then
                _kind = data_cache.GetGameKind(_roomid)
                _gid = data_cache.GetGameIdByRoomId(_roomid)
            else
                _kind = data_cache.curGameKind or 0
                _gid = data_cache.curSelGame.id
            end
            _head.head.destservertype = _gid * 5 + _kind
        end
        _head[_proName] = _tab
        utils.Log("<color=yellow>", "向服务器发送消息： id = ", _proId, "   notes = ", _notes, "    name = ", _proName, "</color>")
        utils.Print(_tab)
        _byteBuffer:WriteInt(_proId)
        --sign
        _byteBuffer:WriteInt(0)
        _byteBuffer:WriteBytes(protobuf.encode("cs_msg.CSMsg", _head))
        return _byteBuffer
    end

    return this
end

return net_module_protocol