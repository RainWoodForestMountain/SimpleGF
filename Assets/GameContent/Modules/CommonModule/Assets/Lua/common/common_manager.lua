common_manager = {}
local this = common_manager
local assetLoadRecord = nil
local grayMat = nil
local btnAudio = nil

function this.Init()
    events.AddListener(MessageType.EventButtonClick, this)
    assetLoadRecord = AssetLoadRecord.New("CommonModule")
    grayMat = assetLoadRecord:LoadMaterial("com_mat_uigray")
    btnAudio = assetLoadRecord:LoadAudioClip("com_mp3_btnclick")
end

function this.Destroy()
    events.RemoveListener(MessageType.EventButtonClick, this)
    assetLoadRecord:Destroy()
end

function this.OnMessageCome(_msg)
    if _msg.msgType == MessageType.EventButtonClick then
        this.PlayBtnClickAudio()
    end
end

function this.PlayBtnClickAudio()
    lua_bridge.Recevive(nil, MessageType.PlayAudio, btnAudio)
end
function common_manager.GrayObject(_obj, _cacle)
    local _m = nil
    if not _cacle then
        _m = grayMat
    end
    utils.SetMaterial(_obj, _m)
end
function common_manager.GrayObjectAndChild(_obj, _cacle)
    local _all = utils.GetAllChild(_obj)
    for i=0, _all.Length-1 do
        this.GrayObject(_all[i], _cacle)
    end
end

function common_manager.LoadSprite(_resName)
    return assetLoadRecord:LoadSprite(_resName)
end

return common_manager