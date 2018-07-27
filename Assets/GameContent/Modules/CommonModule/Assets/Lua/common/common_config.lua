common_config={}

--性别
common_config.sex={
    Unknown = 0,
    Male = 1,
    Famale = 2,
}
--仅作对比，暂无引用
common_config.channelid = {
    Default = 0,
    szhy = 1,
    aysdp = 2,
}
--比赛类型
common_config.Matching = {
    Normal = 1,
    Match = 2,
}

function common_config.IsAndroid ()
    return ProjectDatas.PATH_PLATFORM_NODE == "/android"
end 
function common_config.IsIOS ()
    return ProjectDatas.PATH_PLATFORM_NODE == "/ios"
end 

return common_config