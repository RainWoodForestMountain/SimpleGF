--敏感词库
local warning = require "common/shield_data_base"
local shield = {}

--树节点创建
local function CreateNode(_c, _flag, _nodes)
    local _node = {}
    _node.c = _c or nil           --字符
    _node.flag = _flag or 0       --是否结束标志，0：继续，1：结尾
    _node.nodes = _nodes or {}    --保存子节点
    return _node
end

--字符串转换为字符数组
local function GetCharArray(_str)
    local _array = {}
    local _len = string.len(_str)
    while _str do
        local _fontUTF = string.byte(_str, 1)

        if _fontUTF == nil then
            break
        end

        --lua中字符占1byte,中文占3byte
        if _fontUTF > 127 then 
            local _tmp = string.sub(_str, 1, 3)
            table.insert(_array, _tmp)
            _str = string.sub(_str, 4, _len)
        else
            local _tmp = string.sub(_str, 1, 1)
            table.insert(_array, _tmp)
            _str = string.sub(_str, 2, _len)
        end
    end
    return _array
end

--初始化树结构
local function CreateTree()
    shield.rootNode = CreateNode('R')  --根节点  

    for i,v in ipairs(warning) do
        local _wordList = utils.Split(v,",")
        for _, _w in ipairs(_wordList) do
            local _chars = GetCharArray(_w)
            if #_chars > 0 then
                shield.InsertNode(shield.rootNode, _chars, 1)
            end
        end
    end
end

--插入节点
function shield.InsertNode(_node, _cs, _index)
    local _n = shield.FindNode(_node, _cs[_index])
    if _n == nil then
        _n = CreateNode(_cs[_index])
        table.insert(_node.nodes, _n)
    end

    if _index == #_cs then
        _n.flag = 1
    end

    _index = _index + 1
    if _index <= #_cs then
        shield.InsertNode(_n, _cs, _index)
    end
end

--节点中查找子节点
function shield.FindNode(_node, _c)
    local _nodes = _node.nodes
    local _rn = nil
    for _i,_v in ipairs(_nodes) do
        if _v.c == _c then
            _rn = _v
            break
        end
    end
    return _rn
end

--将字符串中敏感字用*替换返回
function shield.WarningStrGsub(_inputStr)
    local _chars = GetCharArray(_inputStr)
    local _index = 1
    local _node = shield.rootNode
    local _word = {}

    while #_chars >= _index do
        --遇空格节点树停止本次遍历[习 近  平 -> ******]
        if _chars[_index] ~= ' ' then
            _node = shield.FindNode(_node, _chars[_index])
        end

        if _node == nil then
            _index = _index - #_word 
            _node = shield.rootNode
            _word = {}
        elseif _node.flag == 1 then
            table.insert(_word, _index)
            for i,v in ipairs(_word) do
                _chars[v] = '*'
            end
            _node = shield.rootNode
            _word = {}
        else
            table.insert(_word, _index)
        end
        _index = _index + 1
    end

    local _str = ''
    for i,v in ipairs(_chars) do
        _str = _str .. v
    end

    return _str
end

--字符串中是否含有敏感字
function shield.IsWarningInPutStr(_inputStr)
    local _chars = GetCharArray(_inputStr)
    local _index = 1
    local _node = shield.rootNode
    local _word = {}

    while #_chars >= _index do
        if _chars[_index] ~= ' ' then
            _node = shield.FindNode(_node, _chars[_index])
        end

        if _node == nil then
            _index = _index - #_word 
            _node = shield.rootNode
            _word = {}
        elseif _node.flag == 1 then
            return true
        else
            table.insert(_word, _index)
        end
        _index = _index + 1
    end

    return false
end

CreateTree()
return shield