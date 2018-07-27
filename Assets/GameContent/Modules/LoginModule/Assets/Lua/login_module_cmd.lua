login_module_cmd = {}

login_module_cmd.Login = {
    --命令唯一标志
    cmd = test_1_0.Login,
    --服务器名称
    server = CommonKey.SERVER_HALL,
    code = nil,
}
login_module_cmd.EnterHall = {
    --命令唯一标志
    cmd = test_1_0.EnterHall,
    --服务器名称
    server = CommonKey.SERVER_HALL,
}

--服务器返回模板
login_module_cmd.LoginBack = {
    cmd = test_1_0.LoginBack,

}
login_module_cmd.EnterHallBack = {
    cmd = test_1_0.EnterHallBack,
}

return login_module_cmd