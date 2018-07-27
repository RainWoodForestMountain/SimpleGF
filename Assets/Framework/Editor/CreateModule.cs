using System.IO;
using UnityEngine;
using UnityEditor;

namespace GameFramework
{
    public class CreateModule : EditorWindow
    {
        private const string CREATETIME = "CREATTIME";
        private const string CREATER = "CREATER";
        private const string CONTENT = "CONTENT";

        private const string MODULENAME = "MODULE";
        private const string MODULE_NAME = "MODULE_NAME";
        private const string CONTROLLERNAME = "MODULE_CONTROLLER";
        private const string VIEWNAME = "MODULE_VIEW";
        private const string VIEWLAYER = "MODULE_VIEW_LAYER";
        private const string DATANAME = "MODULE_DATA";
        private const string MODULE_PAGE = "PAGES";

        private static string MOBAN_PATH = ProjectDatas.EDITOR_MODULE_ROOT + "ToLuaModule/Model/";
        private static string GAMEMOBAN_PATH = ProjectDatas.EDITOR_MODULE_ROOT + "ToLuaModule/Model/GAMEMODEL/";

        private static EditorWindow windows;
        [MenuItem("工具/创建/创建模块 &N")]
        private static void Creater()
        {
            if (windows != null) windows.Close();
            windows = GetWindow<CreateModule>("模块创建", false);
            windows.titleContent = new GUIContent("模块创建");
            windows.Show();
        }

        private bool isGameModule = false;
        private bool useMVC;

        private bool useView = true;
        private bool useView_Prefab = true;

        private bool useData = true;
        private bool isUseNode = true;

        private List<string> useNodes = new List<string>(new string[] { "GameResult", "GameStart", "WaitPlayerEnter", "WaitPlayerReady" });
        
        private Developer creaters;
        private UILayers layer = UILayers.Window;
        private string contents;

        private string createModuleName;

        private void GetUserSetting()
        {
            useMVC = PersistenceData.GetPrefsDataBool("CREATE_MODULE_USE_MVC");
            string _creaters = PersistenceData.GetPrefsData("CREATE_MODULE_CREATER");
            if (string.IsNullOrEmpty(_creaters)) creaters = Developer.未知;
            else creaters = (Developer)System.Enum.Parse(typeof(Developer), _creaters);
        }
        private void SetUserSetting()
        {
            PersistenceData.SavePrefsData("CREATE_MODULE_USE_MVC", useMVC);
            PersistenceData.SavePrefsData("CREATE_MODULE_CREATER", creaters.ToString());
        }
        private void OnGUI()
        {
            GetUserSetting();

            EditorGUILayout.BeginVertical();
            {
                Property();
                Bottons();
            }
            EditorGUILayout.EndVertical();
            SetUserSetting();
        }
        
        private void Property()
        {
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("创建者：", GUILayout.Width(50));
                creaters = (Developer)EditorGUILayout.EnumPopup(creaters, GUILayout.Width(150));
                GUILayout.Space(50);

                EditorGUILayout.LabelField("是游戏模块", GUILayout.Width(80));
                isGameModule = EditorGUILayout.Toggle(isGameModule, GUILayout.Width(50));
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            if (!isGameModule)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    useMVC = EditorGUILayout.Toggle(useMVC, GUILayout.Width(12));
                    EditorGUILayout.LabelField("使用MVC", GUILayout.Width(100));
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                if (useMVC)
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.BeginVertical();
                        {
                            EditorGUILayout.BeginHorizontal();
                            {
                                EditorGUILayout.LabelField("开启View", GUILayout.Width(100));
                                useView = EditorGUILayout.Toggle(useView, GUILayout.Width(150));
                            }
                            EditorGUILayout.EndHorizontal();
                            if (useView)
                            {
                                EditorGUILayout.BeginHorizontal();
                                {
                                    EditorGUILayout.LabelField("UI层级：", GUILayout.Width(100));
                                    layer = (UILayers)EditorGUILayout.EnumPopup(layer, GUILayout.Width(150));
                                }
                                EditorGUILayout.EndHorizontal();
                                EditorGUILayout.BeginHorizontal();
                                {
                                    EditorGUILayout.LabelField("生成Prefab模板", GUILayout.Width(100));
                                    useView_Prefab = EditorGUILayout.Toggle(useView_Prefab, GUILayout.Width(150));
                                }
                                EditorGUILayout.EndHorizontal();
                            }
                        }
                        EditorGUILayout.EndVertical();

                        EditorGUILayout.BeginVertical();
                        {
                            useData = EditorGUILayout.Toggle("开启Data", useData, GUILayout.Width(200));
                        }
                        EditorGUILayout.EndVertical();
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
            else
            {
                EditorGUILayout.LabelField("UI层级：");
                layer = (UILayers)EditorGUILayout.EnumPopup(layer, GUILayout.Width(200));
                EditorGUILayout.Space();
                isUseNode = EditorGUILayout.Toggle("使用节点：", isUseNode);
                if (isUseNode)
                {
                    for (int i = 0; i < useNodes.Count; i++)
                    {
                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField("节点名称：", GUILayout.Width(80));
                            useNodes[i] = EditorGUILayout.TextField(useNodes[i], GUILayout.Width(300));
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    EditorGUILayout.BeginHorizontal();
                    {
                        if (GUILayout.Button("添加", GUILayout.Width(100)))
                        {
                            useNodes.Add(string.Empty);
                        }
                        if (GUILayout.Button("移除", GUILayout.Width(100)))
                        {
                            if (useNodes.Count > 0) useNodes.RemoveAt(useNodes.Count - 1);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space();
                }
            }

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("模块名称：", GUILayout.Width(100));
                createModuleName = EditorGUILayout.TextField(createModuleName);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("内容简介：");
            contents = EditorGUILayout.TextArea(contents, GUILayout.MinHeight(150));

            GUILayout.Space(20);
        }
        private void Bottons()
        {
            if (GUILayout.Button("创建模块", GUILayout.Width(120)))
            {
                if (Directory.Exists(ProjectDatas.EDITOR_MODULE_ROOT + createModuleName))
                {
                    EditorUtility.DisplayDialog("警告", Utility.MergeString("模块：", createModuleName, "已经存在，请更换名称或者删除已存在的模块！"), "确定");
                    return;
                }
                if (isGameModule) CreateGameModules();
                else CreateModules();
            }
            GUILayout.Space(20);
        }

        private void CreateGameModules()
        {
            string _module = createModuleName.ToLower();
            _module.Replace("_module", string.Empty).Replace("module", string.Empty);
            if (!createModuleName.EndsWith("Module"))
            {
                createModuleName += "Module";
            }
            //规划路径
            Directory.CreateDirectory(ProjectDatas.EDITOR_MODULE_ROOT + createModuleName);
            Directory.CreateDirectory(ProjectDatas.EDITOR_MODULE_ROOT + createModuleName + "/Assets/Lua");
            Directory.CreateDirectory(ProjectDatas.EDITOR_MODULE_ROOT + createModuleName + "/Assets/Prefab");
            Directory.CreateDirectory(ProjectDatas.EDITOR_MODULE_ROOT + createModuleName + "/Assets/Sprite");
            Directory.CreateDirectory(ProjectDatas.EDITOR_MODULE_ROOT + createModuleName + "/Assets/Audio");
            Directory.CreateDirectory(ProjectDatas.EDITOR_MODULE_ROOT + createModuleName + "/Assets/Animation");
            Directory.CreateDirectory(ProjectDatas.EDITOR_MODULE_ROOT + createModuleName + "/Assets/Material_Shader");

            //节点文件，先处理
            string _nodeFiles = string.Empty;
            string _nodeNames = string.Empty;
            string _nodeSetting = string.Empty;
            string _nodeDirt = GAMEMOBAN_PATH + "node_NODENAME";
            string _newNodeDirt = ProjectDatas.EDITOR_MODULE_ROOT + createModuleName + "/Assets/Lua/node_NODENAME/";
            _newNodeDirt = _newNodeDirt.Replace("GAMEMODEL", _module);
            for (int i = 0; i < useNodes.Count; i++)
            {
                string _oneNode = useNodes[i];
                if (string.IsNullOrEmpty(_oneNode)) continue;
                string[] _baseNodeFiles = Directory.GetFiles(_nodeDirt, "*.lua");
                string _writePath = _newNodeDirt.Replace("NODENAME", _oneNode.ToLower());
                Directory.CreateDirectory(_writePath);
                for (int j = 0; j < _baseNodeFiles.Length; j++)
                {
                    string _oneNodeContent = File.ReadAllText(_baseNodeFiles[j]);
                    _oneNodeContent = _oneNodeContent.Replace("GAMEMODEL", _module);
                    _oneNodeContent = _oneNodeContent.Replace("NODENAME", _oneNode.ToLower());
                    _baseNodeFiles[j] = _baseNodeFiles[j].Replace("GAMEMODEL", _module).Replace("NODENAME", _oneNode.ToLower());

                    string _fileName = Path.GetFileName(_baseNodeFiles[j]);
                    _fileName = _fileName.Replace("GAMEMODEL", _module).Replace("NODENAME", _oneNode.ToLower());
                    File.WriteAllText(_writePath + _fileName, _oneNodeContent);
                }

                //处理一些引用
                _nodeFiles += "require(\"node_" + _oneNode.ToLower() + "." + _module + "_module_node_" + _oneNode.ToLower() + "\")\n";
                _nodeNames += _module + "_module_game_state." + _oneNode + " = \"" + _oneNode + "\"\n";
                _nodeSetting += "\t\tthis.SetNode(" + _module + "_module_game_state." + _oneNode + ", " + _module + "_module_node_" + _oneNode.ToLower() + ".New(this, this.data, this.view).GetNode())\n";
            }

            //普通文件
            string[] _files = Directory.GetFiles(GAMEMOBAN_PATH, "*.lua");
            for (int i = 0; i < _files.Length; i++)
            {
                string _cons = File.ReadAllText(_files[i]);
                string _name = Path.GetFileName(_files[i]);
                _cons = _cons.Replace("GAMEMODEL", _module);
                
                _cons = _cons.Replace(CREATETIME, System.DateTime.Now.ToString("yyyy-MM-dd"));
                _cons = _cons.Replace(CREATER, creaters.ToString());
                _cons = _cons.Replace(CONTENT, contents);
                _cons = _cons.Replace(VIEWLAYER, "UILayers." + layer.ToString());
                //节点相关
                _cons = _cons.Replace("--[rp require node files /rp]", _nodeFiles);
                _cons = _cons.Replace("--[rp require node names /rp]", _nodeNames);
                _cons = _cons.Replace("--[rp require node setting /rp]", _nodeSetting);

                _name = _name.Replace("GAMEMODEL", _module);
                File.WriteAllText(ProjectDatas.EDITOR_MODULE_ROOT + createModuleName + "/Assets/Lua/" + _name, _cons);
            }

            AssetDatabase.Refresh();
        }

        private void CreateModules()
        {
            string _moduleName = createModuleName.ToLower();
            if (!createModuleName.EndsWith("Module"))
            {
                createModuleName += "Module";
                _moduleName += "_module";
            }
            //规划路径
            Directory.CreateDirectory(ProjectDatas.EDITOR_MODULE_ROOT + createModuleName);
            Directory.CreateDirectory(ProjectDatas.EDITOR_MODULE_ROOT + createModuleName + "/Assets/Lua");
            Directory.CreateDirectory(ProjectDatas.EDITOR_MODULE_ROOT + createModuleName + "/Assets/Prefab");
            Directory.CreateDirectory(ProjectDatas.EDITOR_MODULE_ROOT + createModuleName + "/Assets/Sprite");
            Directory.CreateDirectory(ProjectDatas.EDITOR_MODULE_ROOT + createModuleName + "/Assets/Audio");
            Directory.CreateDirectory(ProjectDatas.EDITOR_MODULE_ROOT + createModuleName + "/Assets/Animation");
            Directory.CreateDirectory(ProjectDatas.EDITOR_MODULE_ROOT + createModuleName + "/Assets/Material_Shader");

            string[] _files = new string[4];
            string _ex = ".lua";
            _files[0] = File.ReadAllText(MOBAN_PATH + MODULENAME + _ex);
            _files[1] = File.ReadAllText(MOBAN_PATH + CONTROLLERNAME + _ex);
            _files[2] = File.ReadAllText(MOBAN_PATH + VIEWNAME + _ex);
            _files[3] = File.ReadAllText(MOBAN_PATH + DATANAME + _ex);

            //只有模块文件有记录信息
            _files[0] = _files[0].Replace(CREATETIME, System.DateTime.Now.ToString("yyyy-MM-dd"));
            _files[0] = _files[0].Replace(CREATER, creaters.ToString());
            _files[0] = _files[0].Replace(CONTENT, contents);
            //只有controller有UI层级
            _files[1] = _files[1].Replace(VIEWLAYER, "UILayers." + layer.ToString());

            //替换
            string _controllerName = _moduleName + "_controller";
            string _viewName = _moduleName + "_view";
            string _dataName = _moduleName + "_data";

            for (int i = 0; i < _files.Length; i++)
            {
                _files[i] = _files[i].Replace(CONTROLLERNAME, _controllerName);
                _files[i] = _files[i].Replace(VIEWNAME, _viewName);
                _files[i] = _files[i].Replace(DATANAME, _dataName);
                _files[i] = _files[i].Replace(MODULE_NAME, createModuleName);
                _files[i] = _files[i].Replace(MODULENAME, _moduleName);
            }

            //写文件
            File.WriteAllText(ProjectDatas.EDITOR_MODULE_ROOT + createModuleName + "/Assets/Lua/" + _moduleName + _ex, _files[0]);
            if (useMVC)
            {
                File.WriteAllText(ProjectDatas.EDITOR_MODULE_ROOT + createModuleName + "/Assets/Lua/" + _controllerName + _ex, _files[1]);
                File.WriteAllText(ProjectDatas.EDITOR_MODULE_ROOT + createModuleName + "/Assets/Lua/" + _viewName + _ex, _files[2]);
                File.WriteAllText(ProjectDatas.EDITOR_MODULE_ROOT + createModuleName + "/Assets/Lua/" + _dataName + _ex, _files[3]);

                if (!useView)
                {
                    File.Delete(ProjectDatas.EDITOR_MODULE_ROOT + createModuleName + "/Assets/Lua/" + _viewName + _ex);
                }
                else
                {
                    if (useView_Prefab)
                    {
                        string _prefabName = ProjectDatas.EDITOR_MODULE_ROOT + "ToLuaModule/Model/MODULE_page.prefab";
                        string _newPrefabName = (_moduleName.Substring (0, 3) + "_pre_page.prefab").ToLower();
                        _newPrefabName = ProjectDatas.EDITOR_MODULE_ROOT + createModuleName + "/Assets/Prefab/" + _newPrefabName;
                        File.Copy(_prefabName, _newPrefabName);

                        _files[2] = _files[2].Replace(MODULE_PAGE, Path.GetFileNameWithoutExtension(_newPrefabName));
                        File.WriteAllText(ProjectDatas.EDITOR_MODULE_ROOT + createModuleName + "/Assets/Lua/" + _viewName + _ex, _files[2]);
                    }
                }
                if (!useData)
                {
                    File.Delete(ProjectDatas.EDITOR_MODULE_ROOT + createModuleName + "/Assets/Lua/" + _dataName + _ex);
                }

                string[] _allLine = File.ReadAllLines(ProjectDatas.EDITOR_MODULE_ROOT + createModuleName + "/Assets/Lua/" + _controllerName + _ex);
                List<string> _list = new List<string>(_allLine);
                for (int i = 0; i < _list.Count; i++)
                {
                    if (!useView)
                    {
                        //移除一行
                        if (_list[i].Contains(_viewName))
                        {
                            _list.RemoveAt(i);
                            i--;
                            continue;
                        }
                    }
                    if (!useData)
                    {
                        //移除一行
                        if (_list[i].Contains(_dataName))
                        {
                            _list.RemoveAt(i);
                            i--;
                            continue;
                        }
                    }
                }

                File.WriteAllLines(ProjectDatas.EDITOR_MODULE_ROOT + createModuleName + "/Assets/Lua/" + _controllerName + _ex, _list.ToArray());
            }
            else
            {
                string[] _allLine = File.ReadAllLines(ProjectDatas.EDITOR_MODULE_ROOT + createModuleName + "/Assets/Lua/" + _moduleName + _ex);
                List<string> _list = new List<string>(_allLine);
                for (int i = 0; i < _list.Count; i++)
                {
                    //移除一行
                    if (_list[i].Contains(_controllerName))
                    {
                        _list.RemoveAt(i);
                        i--;
                        continue;
                    }
                }
                File.WriteAllLines(ProjectDatas.EDITOR_MODULE_ROOT + createModuleName + "/Assets/Lua/" + _moduleName + _ex, _list.ToArray());
            }

            AssetDatabase.Refresh();
        }
    }
}