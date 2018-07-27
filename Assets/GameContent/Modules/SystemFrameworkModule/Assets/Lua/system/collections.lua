Queue = {}
function Queue.New(_single)
    local this = {}
    local datas = {}
    local single = _single

    function this.Pop()
        if #datas > 0 then
            return table.remove( datas, 1 )
        else
            return nil
        end
    end
    function this.Push(_item)
        if single then
            this.Remove(_item)
        end
        table.insert( datas, _item )
    end
    function this.Peek()
        return datas[1]
    end
    function this.Remove(_item)
        for i=1, #datas do
            if datas[i] == _item then
                table.remove( datas, i )
                i = i - 1
            end
        end
    end

    return this
end

Stack = {}
function Stack.New(_single)
    local this = {}
    local datas = {}
    local single = _single

    function this.Pop()
        if #datas > 0 then
            return table.remove( datas, 1 )
        else
            return nil
        end
    end
    function this.Push(_item)
        if single then
            this.Remove(_item)
        end
        table.insert( datas, 1, _item )
    end
    function this.Peek()
        return datas[1]
    end
    function this.Remove(_item)
        for i=1, #datas do
            if datas[i] == _item then
                table.remove( datas, i )
                i = i - 1
            end
        end
    end

    return this
end