using UnityEditor;
using System;
using System.IO;
using System.Reflection;

namespace GameFramework
{
    public class VSCodeLuaCodeSnippetsCreater : EditorWindow
    {
        private const string BASE_FILE = @"
{
	// Place your snippets for lua here. Each snippet is defined under a snippet name and has a prefix, body and 
	// description. The prefix is what is used to trigger the snippet and the body will be expanded and inserted. Possible variables are:
	// $1, $2 for tab stops, $0 for the final cursor position, and ${1:label}, ${2:another} for placeholders. Placeholders with the 
	// same ids are connected.
	// Example:
	// ""Print to console"": {
	// 	""prefix"": ""log"",
	// 	""body"": [
	// 		""console.log(${1:aaa});"",
	// 		""$2""
	// 	],
	// 	""description"": ""Log output to console""
	// }

	// add here
}
";
        private const string BASE_PLAYER_SETTING = @"{
    ""files.exclude"": {
        ""**/.git"": true,
        ""**/.svn"": true,
        ""**/.hg"": true,
        ""**/CVS"": true,
        ""**/.DS_Store"": true,
        ""**/*.meta"": true,
        ""**/*.cs"": true,
        ""**/*.prefab"": true,
        ""**/*.png"": true
        ""**/Prefab"": true,
        ""**/Sprite"": true,
        ""**/Animation"": true,
        ""**/Font"": true,
        ""**/Audio"": true,
        ""**/Material_Shader"": true
      },

    ""files.associations"": {
        ""*.lua.txt"": ""lua""
    },
    ""window.zoomLevel"": 0,
    ""git.ignoreMissingGitWarning"": true,
    ""extensions.ignoreRecommendations"": true,
    ""workbench.startupEditor"": ""newUntitledFile"",
    ""explorer.confirmDragAndDrop"": false
}";

        private static string[] SPECIAL = new string[] { "MessageType" };
        private static List<string> FILE_NAMES = null;
        static VSCodeLuaCodeSnippetsCreater()
        {
            BASE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            BASE_PATH = Path.GetDirectoryName(BASE_PATH);
            BASE_PATH = BASE_PATH + "/Roaming/Code/User/snippets/lua.json";
            if (!Directory.Exists(Path.GetDirectoryName(BASE_PATH)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(BASE_PATH));
            }
            FILE_NAMES = new List<string>();
            FILE_NAMES.AddRange(new string[] {
                "utils", "define", "lua_bridge", "events", "CommonKey", "MessageType", "game_state", "shield", "randoms", "data_cache", "pool_type", "poker_type",
                "poker", "game_player", "game_type", "net_cmd", "plantform_visit", "net_module_protocol_contrast", "third_plantforms", "common_manager", "webrequest",
                "lua_module_manager",
            });
            BASE_SETTING_PATH = Path.GetDirectoryName(Path.GetDirectoryName(BASE_PATH)) + "/settings.json";
        }

        private static string BASE_PATH;
        private static string BASE_SETTING_PATH;
        private const string MOBAN = @"	
        ""NAME"": {
		    ""prefix"": ""NAME"",
		    ""body"": [
			    ""BODY""
		    ],
		    ""description"": ""DESCRIPTION""
	    }, 
        ";
        
        [MenuItem("工具/操作/VSCode lua代码片段 &V")]
        private static void Open()
        {
            List<string> _write = new List<string>();
            List<string> _collects = Collecte();
            _write.AddRange (Record(_collects));
            _write.AddRange(Special());
            _write.AddRange(CPP());
            
            WriteFile(_write);
        }

        private static List<string> Collecte()
        {
            List<string> _coll = new List<string>();
            string[] _files = Directory.GetFiles(ProjectDatas.EDITOR_GAME_CONTENT_ROOT, "*.lua", SearchOption.AllDirectories);
            for (int i = 0; i < _files.Length; i++)
            {
                string _one = _files[i];
                if (FILE_NAMES.IndexOf(Path.GetFileNameWithoutExtension(_one)) >= 0)
                {
                    _coll.Add(_one);
                }
            }
            return _coll;
        }
        private static List<string> Record (List<string> _collects)
        {
            List<string> _record = new List<string>();
            while (_collects.Count > 0)
            {
                string _one = _collects[0];
                _collects.RemoveAt(0);
                string _name = Path.GetFileNameWithoutExtension(_one);

                string[] _lines = File.ReadAllLines(_one);
                for (int i = 0; i < _lines.Length; i++)
                {
                    //每一行检查
                    string _oneLine = Utility.MakeUnifiedDirectory(_lines[i]);
                    //只查找以 文件名.方法名 的全局方法或者参数
                    if (_oneLine.Contains (_name + "."))
                    {
                        _record.Add(DepartOneLine(_name, _oneLine, i > 0 ? _lines[i - 1] : string.Empty));
                    }
                    for (int j = 0; j < SPECIAL.Length; j++)
                    {
                        if (_oneLine.Contains(SPECIAL[j]))
                        {
                            _record.Add(DepartOneLine(SPECIAL[j], _oneLine, i > 0 ? _lines[i-1] : string.Empty));
                        }
                    }
                }
            }
            return _record;
        }
        private static string DepartOneLine (string _name, string _line, string _des)
        {
            string _one = MOBAN;
            _des = Utility.MakeUnifiedDirectory(_des);
            //移除分号
            _line = _line.Replace(";",string.Empty);
            int _indexName = _line.IndexOf(_name);
            int _indexDian = _line.IndexOf(".");
            int _indexQK = _line.IndexOf("(");
            int _indexHK = _line.IndexOf(")");
            int _indexDeng = _line.IndexOf("=");
            int _indexFunc = _line.IndexOf("function");

            //有等号，赋值语句，且不是初始值，忽略
            if (_indexDeng >= 0 && _indexDeng < _indexDian)
            {
                return string.Empty;
            }
            //没有点号，忽略
            if (_indexDian < 0)
            {
                return string.Empty;
            }
            
            //非函数
            if (_indexFunc < 0 && _indexQK < 0)
            {
                int _last = _indexDeng < 0 ? _line.Length : _indexDeng;
                _one = _one.Replace("NAME", _name + "." + _line.Substring(_indexDian + 1, _last - 1 - _indexDian));
                _one = _one.Replace("BODY", _name + "." + _line.Substring(_indexDian + 1, _last - 1 - _indexDian));
                //注释符号
                if (_des.StartsWith("--")) _one = _one.Replace("DESCRIPTION", _des.Substring (2, _des.Length - 2));
                return _one;
            }
            //是函数
            if (_indexFunc >= 0 && _indexFunc < _indexName)
            {
                _one = _one.Replace("NAME", _line.Substring(_indexName, _indexQK - _indexName));
                //分号检测，参数替换
                string _q = _line.Substring(_indexName, _indexQK - _indexName + 1);
                string _h = ")";
                string _z = _line.Substring(_indexQK + 1, _indexHK - 1 - _indexQK);
                string[] _pros = _z.Split(',');

                for (int i = 0; i < _pros.Length; i++)
                {
                    _q += "${" + (i + 1) + ":" + _pros[i] + "}, ";
                }
                _q = _q.Substring(0, _q.Length - 2) + _h;
                _one = _one.Replace("BODY", _q);
                //注释符号
                if (_des.StartsWith("--")) _one = _one.Replace("DESCRIPTION", _des.Substring(2, _des.Length - 2));

                return _one;
            }

            return string.Empty;
        }
        private static List<string> Special ()
        {
            string[] _base = new string[] { "this.controller", "this.view", "this.data", "CreateMessage(${1:type}, ${2:key}, ${3:content})", "require_manager.Unload(${1:name})",
            "GetComponent(${1:name})", "this.IsInit()", "this.Init()", "this.Activate()", "this.Sleep()", "this.Destroy()", "this.LoadInstantiateGameObject(${1:res})",
            "this.SetView( ${1:view} ${2:[, not_init]})", "this.SetData( ${1:data} ${2:[, not_init]})", "this.CreateRootObject(${1:res})",
            "this.LoadGameObject(${1:res})", "this.LoadInstantiateGameObject(${1:res})", "this.LoadSprite(${1:res})", "this.LoadText(${1:res})",
            "this.LoadMaterial(${1:res})", "this.LoadAudioClip(${1:res})", "this.Kill()", "this.SetController( ${1:cont} ${2:[, not_init]})", "class(${1:name})",
            "json.encode(${1:tab})", "json.decode(${1:string})", "class_model.New()", "this.SetSpriteByName(${1:obj}, ${2:resName} )" };
            List<string> _sp = new List<string>();

            for (int i = 0; i < _base.Length; i++)
            {
                string _one = _base[i];
                string _name = _one.Substring(0, _one.IndexOf("(") < 0 ? _one.Length : _one.IndexOf("("));
                _one = MOBAN.Replace("NAME", _name).Replace("BODY", _base[i]);
                _sp.Add(_one);
            }

            _sp.Add(@"	
        ""zhushihang"": {
		    ""prefix"": ""zhushihang"",
		    ""body"": [
			    ""--创建时间：${1:时间}  创建人：${2:名称}""
		    ],
		    ""description"": ""创建时间&人""
	    }, 
        ");
            _sp.Add(@"	
        ""zhushikuai"": {
		    ""prefix"": ""zhushikuai"",
		    ""body"": [
			    ""--[[\n\t\t创建时间：${1:时间}  创建人：${2:名称}\n\t\t主要功能：${3:功能}\n]]""
		    ],
		    ""description"": ""创建时间&人""
	    }, 
        ");

            return _sp;
        }
        private static List<string> CPP ()
        {
            List<string> _cpp = new List<string>();

            _cpp.AddRange(AddEnum("MessageType", typeof(MessageType)));
            _cpp.AddRange(AddEnum("UILayers", typeof(UILayers)));
            _cpp.AddRange(AddEnum("Ease", typeof(DG.Tweening.Ease)));
            _cpp.AddRange(AddClass("PersistenceData", typeof(PersistenceData)));
            _cpp.AddRange(AddClass("RunningTimeData", typeof(RunningTimeData)));
            _cpp.AddRange(AddClass("CommonKey", typeof(CommonKey)));
            _cpp.AddRange(AddClass("ProjectDatas", typeof(ProjectDatas)));

            return _cpp;
        }
        private static List<string> AddEnum (string _name, Type _type)
        {
            List<string> _list = new List<string>();
            string[] _ss = Enum.GetNames(_type);
            for (int i = 0; i < _ss.Length; i++)
            {
                string _one = MOBAN;
                _one = _one.Replace("NAME", _name + "." + _ss[i]);
                _one = _one.Replace("BODY", _name + "." + _ss[i]);
                _list.Add(_one);
            }
            return _list;
        }
        private static List<string> AddClass(string _name, Type _t)
        {
            List<string> _list = new List<string>();

            FieldInfo[] _methods = _t.GetFields(BindingFlags.Public | BindingFlags.Static);
            for (int i = 0; i < _methods.Length; i++)
            {
                string _one = MOBAN;
                _one = _one.Replace("NAME", _name + "." + _methods[i].Name);
                _one = _one.Replace("BODY", _name + "." + _methods[i].Name);
                _list.Add(_one);
            }
            return _list;
        }

        private static void WriteFile (List<string> _cols)
        {
            string _s = string.Empty;
            for (int i = 0; i < _cols.Count; i++)
            {
                _s += _cols[i];
            }

            _s = BASE_FILE.Replace("// add here", _s);
            File.WriteAllText(BASE_PATH, _s);
            File.WriteAllText(BASE_SETTING_PATH, BASE_PLAYER_SETTING);
        }
    }
}