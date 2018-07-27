require "3rd.pbc.protobuf"
parser = require "3rd.pbc.parser"

-- path = Application.dataPath.."/GameContent/Modules/ToLuaModule/Assets/Lua/3rd/pbc/addressbook.proto"
-- t = parser.register("addressbook.proto","../../test")
-- t = parser.register(path)
t = parser.register(Application.dataPath.."/GameContent/Modules/ToLuaModule/Assets/Lua/3rd/pbc/Common.proto")
t = parser.register(Application.dataPath.."/GameContent/Modules/ToLuaModule/Assets/Lua/3rd/pbc/FR_Common.proto")
t = parser.register(Application.dataPath.."/GameContent/Modules/ToLuaModule/Assets/Lua/3rd/pbc/Hall.proto")
t = parser.register(Application.dataPath.."/GameContent/Modules/ToLuaModule/Assets/Lua/3rd/pbc/CS_Msg.proto")

-- addressbook = {
-- 	name = "Alice",
-- 	id = 12345,
-- 	phone = {
-- 		{ number = "1301234567" },
-- 		{ number = "87654321", type = "WORK" },
-- 	}
-- }

-- code = protobuf.encode("tutorial.Person", addressbook)

-- local buf = ByteBuffer.New()
-- buf:Write(code)

-- decode = protobuf.decode("tutorial.Person" , code)

-- print(decode.name)
-- print(decode.id)
-- for _,v in ipairs(decode.phone) do
-- 	print("\t"..v.number, v.type)
-- end

-- buffer = protobuf.pack("tutorial.Person name id", "Alice", 123)
-- print(protobuf.unpack("tutorial.Person name id", buffer))