C#端常用API见 "c# API.txt"
Lua端常用API见 "lua API.txt"

这里只简单介绍一下lua端的编码规则和与C#端交互的方式和内容

1.交互
	1.1：lua端仅通过 lua_bridge.lua 和 utils.lua 来与C#框架交互；其他任何形式的交互均被禁止（以下提到的ProjectDatas类交互除外）
	1.2：C#中ProjectDatas类包含了项目只读/静态常量或者其他不会变动的参数，可以直接调用
	1.3：lua与Unity组件交互不受限制
	1.4：lua_bridge.lua主要负责一些消息类的功能，汇聚C#端的消息转发给lua，汇聚lua端的消息转发给C#
	1.5：utils.lua 包含了所有C#端的公共功能

2.模块
	2.1：lua使用自身独立的模块系统
	2.2：模块与模块之间无法交互，外部可以通过命令的方式打开或者关闭模块，模块内部可以自关闭
	2.3：一个模块无法直接获取另一个模块的数据内容，仅能发送申请消息，然后等待回应消息
	2.4：模块彻底关闭后，要清理掉消息监听

3.消息
	3.1：lua端使用自己的消息机制 events
	3.2：lua桥负责接收C#端消息然后反馈给events再分发，如果有需求，lua的消息可以直接通过lua桥分发，C#端能够直接收到
	3.3：在注册消息监听的时候，要注意不要重复注册（添加重复监听时系统有提示）

4.常见lua错误
	LuaException: [string "xxx/xxxx"]:145: field or property xxxx does not exist
		定义的内容为找到，如果是调用C#端，则刷新warp文件
		如果是lua端，则查找定义
	LuaException: [string "xxxxx"]:16: attempt to index field 'xxxx' (a nil value)
		空值，检查赋值

5.良好的lua编码
	多用assert断言语句，发现错误能够清晰明了
	避免使用 . 和 ：调用混编
	注意table有序和无序的区别