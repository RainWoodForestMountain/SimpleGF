using System.IO;
using UnityEngine;
using LuaInterface;
using GameFramework;

namespace GameContent
{
    public class ToLuaModule : ModuleBase
    {
        private MonoBase mono;

        public override void Init(long _id)
        {
            base.Init(_id);

            GameObject _temp = new GameObject();
            Object.DontDestroyOnLoad(_temp);
            _temp.name = name;
            mono = _temp.AddComponent<MonoBase>();

            InitStart();
            StartMain();

            //MessageModule.instance.AddListener(MessageType.OnTotalHotupdateComplete, ReStart);
        }
        public override void Destroy()
        {
            base.Destroy();

            CloseMain();
            LuaGC();
            loop.Destroy();
            loop = null;

            lua.Dispose();
            lua = null;
            loader = null;

            bridge.Destroy();

            Object.Destroy(mono.gameObject);
            //MessageModule.instance.RemoveListener(MessageType.OnTotalHotupdateComplete, ReStart);
        }

        private LuaState lua;
        private ToLuaLoader loader;
        private LuaLooper loop = null;
        private INeedDestroy bridge = null;

        // Use this for initialization
        private void InitStart()
        {
            DelegateFactory.Init();
            bridge = new ToLuaBridge();
            loader = new ToLuaLoader(this);
            lua = new LuaState();

            OpenLibs();
            lua.LuaSetTop(0);

            LuaBinder.Bind(lua);
            LuaCoroutine.Register(lua, mono);

            InitLuaPath();
            lua.Start();    //启动LUAVM
            StartLooper();
        }

        private void StartLooper()
        {
            loop = mono.gameObject.AddComponent<LuaLooper>();
            loop.luaState = lua;
        }

        //cjson 比较特殊，只new了一个table，没有注册库，这里注册一下
        private void OpenCJson()
        {
            lua.LuaGetField(LuaIndexes.LUA_REGISTRYINDEX, "_LOADED");
            lua.OpenLibs(LuaDLL.luaopen_cjson);
            lua.LuaSetField(-2, "cjson");

            lua.OpenLibs(LuaDLL.luaopen_cjson_safe);
            lua.LuaSetField(-2, "cjson.safe");
        }

        private void StartMain()
        {
            lua.DoFile("Main.lua");
            LuaFunction main = lua.GetFunction("Main");
            main.Call();
            main.Dispose();
            main = null;
        }
        //private void StartMainWithOutUpdata()
        //{
        //    lua.DoFile("Main.lua");
        //    LuaFunction main = lua.GetFunction("MainWithoutUpdata");
        //    main.Call();
        //    main.Dispose();
        //    main = null;
        //}
        private void CloseMain ()
        {
            LuaFunction main = lua.GetFunction("MainDestroy");
            main.Call();
            main.Dispose();
            main = null;
        }

        /// <summary>
        /// 初始化加载第三方库
        /// </summary>
        private void OpenLibs()
        {
            lua.OpenLibs(LuaDLL.luaopen_pb);
            lua.OpenLibs(LuaDLL.luaopen_sproto_core);
            lua.OpenLibs(LuaDLL.luaopen_protobuf_c);
            lua.OpenLibs(LuaDLL.luaopen_lpeg);
            lua.OpenLibs(LuaDLL.luaopen_bit);
            lua.OpenLibs(LuaDLL.luaopen_socket_core);

            this.OpenCJson();
        }

        /// <summary>
        /// 初始化Lua代码加载路径
        /// </summary>
        private void InitLuaPath()
        {
#if UNITY_EDITOR
            string _path = Application.dataPath + "/GameContent/Modules";
            string[] _dirs = System.IO.Directory.GetDirectories(_path, "*", System.IO.SearchOption.AllDirectories);
            for (int i = 0; i < _dirs.Length; i++)
            {
                string _one = _dirs[i].ToLower();
                _one = Utility.MakeUnifiedDirectory(_one);
                if (_one.EndsWith("/")) _one = _one.Substring(0, _one.Length - 1);
                if (_one.EndsWith ("lua") || _one.EndsWith ("tolua"))
                {
                    lua.AddSearchPath(_dirs[i]);
                }
            }
#endif
        }

        public object[] DoFile(string filename)
        {
            return lua.DoFile(filename);
        }

        // Update is called once per frame
        public object[] CallFunction(string funcName, params object[] args)
        {
            LuaFunction func = lua.GetFunction(funcName);
            if (func != null)
            {
                return func.LazyCall(args);
            }
            return null;
        }

        //public void ReStart (Message _msg)
        //{
        //    lua.Dispose();
        //    InitStart();
        //    StartMainWithOutUpdata();
        //}
        public void LuaGC()
        {
            lua.LuaGC(LuaGCOptions.LUA_GCCOLLECT);
        }

        public byte[] ReadFile(string _fileName)
        {
            byte[] _str = null;
#if UNITY_EDITOR
            string path = loader.FindFile(_fileName);

            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                _str = File.ReadAllBytes(path);
            }
            return _str;
#endif
            if (_str == null)
            {
                string _one = Path.GetFileNameWithoutExtension(_fileName);
                TextAsset _ta = ResourceModule.instance.LoadAsset<TextAsset>(string.Empty, _one);

                _str = _ta == null ? null : _ta.bytes;
                _str = Utility.Decrypt(_str, ProjectDatas.CODEKEY);
            }
            return _str;
        }

        public byte[] ReadFile(string _moduleName, string _fileName)
        {
            byte[] _str = null;
#if UNITY_EDITOR
            string path = loader.FindFile(_fileName);

            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                _str = File.ReadAllBytes(path);
            }
            return _str;
#endif
            if (_str == null)
            {
                string _one = Path.GetFileNameWithoutExtension(_fileName);
                TextAsset _ta = ResourceModule.instance.LoadAsset<TextAsset>(_moduleName, _one);

                _str = _ta == null ? null : _ta.bytes;
                _str = Utility.Decrypt(_str, ProjectDatas.CODEKEY);
            }
            return _str;
        }
    }
}