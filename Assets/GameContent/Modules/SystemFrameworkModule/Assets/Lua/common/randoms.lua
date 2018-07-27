local bit = require "bit"

randoms = {}

function randoms.New(_seed)
    local this = class_model.New()
    local seed = _seed
    local L_RANDMAX = 0x7fff

    local function _rand()
        seed = bit.band(seed * 214013 + 2531011, 0xffffffff)
        return bit.band(bit.rshift(seed, 16), L_RANDMAX)
    end

    function this.Rand( _min, _max)
        local _temp = _rand()
        local _r =  _temp * (1 / (L_RANDMAX + 1))
        if _min then
            if not _max then
                _max = _min
                _min = 1
            end
            _r = math.floor(_r * ((_max - _min) + 1))
            return  _r + _min
        else
            return _r
        end
    end

    return this
end
--获取随机数
function randoms.Range(_min, _max, _seed)
    --防止出现异常数据，先进行一次随机将异常数据跑掉
    math.random()
    local _r = randoms.New(_seed or math.random())
    _r.Rand()
    return _r.Rand(_min, _max)
end

return randoms