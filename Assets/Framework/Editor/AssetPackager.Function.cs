using UnityEditor;
using UnityEngine;
using System.IO;
using System;

namespace GameFramework
{
	public partial class AssetPackager : EditorWindow
	{
	    public static List<string> FilterExtensions = new List<string> {".meta"};

        private static List<AssetBundleBuild> buildmap = new List<AssetBundleBuild>();
        private static List<AssetModuleConfig> resBundleModuleName = new List<AssetModuleConfig>();
        private static AssetModuleConfig currentBundleModuleName = null;

        private static void Clear ()
        {
            buildmap.Clear();
            resBundleModuleName.Clear();
            currentBundleModuleName = null;
        }

        [MenuItem("工具/资源/快速打包 &B")]
        public static void PackAssetBundles()
        {
            PackAssetBundles(false);
        }
        public static void PackAssetBundles(bool _force = false)
	    {
	        if (EditorApplication.isCompiling && !_force)
	        {
	            EditorUtility.DisplayDialog("警告", "请等待代码编译完成", "Yes");
	            return;
	        }
            Clear();

            //清理临时文件
            CleanTempFiles();
            //检查目录
            CheckPath();
            //构建所有资源
            FindAllModule();
            //构建配置文件
            RecordConfigFiles();
            //打包配置文件
            BundleCommonAssets();
            //对比上一次资源
            List<string> _cp = Comparison();
            //版本控制文件
            BuidleVersionFiles();
            //打包zip文件
            BuidleZipFiles(_cp);
            //清理临时文件
            CleanTempFiles();

            if (windows) windows.Close();
            AssetDatabase.Refresh();
            Utility.Log("资源打包完成！");
        }
        /// <summary>
        /// 检查路径
        /// </summary>
        private static void CheckPath ()
        {
            if (!Directory.Exists(ProjectDatas.EDITOR_ASSET_SAVE_ROOT)) Directory.CreateDirectory(ProjectDatas.EDITOR_ASSET_SAVE_ROOT);
            if (!Directory.Exists(ProjectDatas.EDITOR_ASSET_SAVE_CURRENT_ROOT)) Directory.CreateDirectory(ProjectDatas.EDITOR_ASSET_SAVE_CURRENT_ROOT);

        }
        /// <summary>
        /// 记录公共文件
        /// </summary>
        private static void RecordConfigFiles ()
        {
            //string _dir = ProjectDatas.EDITOR_GAME_CONTENT_ROOT + ProjectDatas.PATH_COMMON_NODE + ProjectDatas.PATH_CONFIG_NODE + "/";
            string _dirRes = ProjectDatas.EDITOR_RESOURCE_ROOT + ProjectDatas.PATH_COMMON_NODE + ProjectDatas.PATH_CONFIG_NODE + "/";
            //string _path = _dir + ProjectDatas.NAME_ASSET_BUNDLE_INFO_FILE;
            //if (!Directory.Exists (_dir))  Directory.CreateDirectory(_dir);

            string _content = LitJson.JsonMapper.ToJson(resBundleModuleName);
            //File.WriteAllText(_path, _content);

            //写一份在Resources目录下，作为默认资源
            _dirRes = _dirRes.ToLower();
            if (!Directory.Exists(_dirRes)) Directory.CreateDirectory(_dirRes);
            File.WriteAllText(_dirRes + ProjectDatas.NAME_ASSET_BUNDLE_INFO_FILE, _content);

            AssetDatabase.Refresh();
        }
        /// <summary>
        /// 打包公共资源 （配置资源、）
        /// </summary>
        private static void BundleCommonAssets()
        {
            AddOneModuleToMap(Path.GetFileName(ProjectDatas.PATH_COMMON_NODE), ProjectDatas.EDITOR_GAME_CONTENT_ROOT + ProjectDatas.PATH_COMMON_NODE);
        }
        /// <summary>
        /// 找到所有Module
        /// </summary>
        private static void FindAllModule ()
        {
            //找到所有Moduel（或者资源文件夹）
            string[] _dirs = Directory.GetDirectories(ProjectDatas.EDITOR_MODULE_ROOT);
            foreach (var _pair in _dirs)
            {
                string _assetpath = _pair + ProjectDatas.PATH_ASSETS_NODE;
                AddOneModuleToMap(Path.GetFileNameWithoutExtension (_pair), _assetpath);
            }
        }
        /// <summary>
        /// 将一个Module添加入map
        /// </summary>
        private static void AddOneModuleToMap (string _moduleName, string _rootAssetPath)
        {
            if (!Directory.Exists(_rootAssetPath)) return;
            //整包模式
            if (wholeModule)
            {
                DoMakeAssetBundleBuildTotal(_moduleName, _rootAssetPath);
                return;
            }
            //获取子文件夹
            string[] _dirs = Directory.GetDirectories(_rootAssetPath);
            for (int i = 0; i < _dirs.Length; i++)
            {
                string _one = Utility.MakeUnifiedDirectory (_dirs[i]);
                //整个文件夹打成一个包模式
                if (_one.ToLower().EndsWith("_one"))
                {
                    DoMakeAssetBundleBuildTotal(_moduleName, _one);
                }
                else if (_one.ToLower().EndsWith("_child"))
                {
                    DoMakeAssetBundleBuildChild(_moduleName, _one);
                }
                else if (_one.ToLower().EndsWith("_single"))
                {
                    DoMakeAssetBundleBuildSingle(_moduleName, _one);
                }
                else if (_one.ToLower().IndexOf('_') < 0)
                {
                    DoMakeAssetBundleBuildTotal(_moduleName, _one);
                }
            }
        }
        /// <summary>
        /// 文件夹下面的所有资源打成一个整包
        /// </summary>
        private static void DoMakeAssetBundleBuildTotal (string _module, string _src, bool _relation = true)
        {
            string[] _files = Directory.GetFiles(_src, "*", SearchOption.AllDirectories);
            string _dirName = Path.GetFileName(_src);
            string _abname = MakeAssetBundleName(_module, _dirName);
            if (_relation) RecordResourceRelationship(_module, _abname, _files);
            buildmap.AddRange(MakeAssetBundleBuild(_module, _abname, _files));
        }
        /// <summary>
        /// 文件夹下面的子文件夹内的所有资源打成整包，无视指定根目录的子文件
        /// </summary>
        private static void DoMakeAssetBundleBuildChild(string _module, string _src, bool _relation = true)
        {
            //所有子文件夹
            string[] _dirs = Directory.GetDirectories(_src);
            for (int i = 0; i < _dirs.Length; i++)
            {
                string[] _files = Directory.GetFiles(_dirs[i], "*", SearchOption.AllDirectories);
                string _dirName = Path.GetFileNameWithoutExtension(_dirs[i]);
                string _abname = MakeAssetBundleName(_module, _dirName);
                if (_relation) RecordResourceRelationship(_module, _abname, _files);
                buildmap.AddRange (MakeAssetBundleBuild(_module, _abname, _files));
            }
        }
        /// <summary>
        /// 文件夹下面的子文件打成单一包，无视指定根目录的子文件夹
        /// </summary>
        private static void DoMakeAssetBundleBuildSingle(string _module, string _src, bool _relation = true)
        {
            //所有子文件
            string[] _files = Directory.GetFiles(_src);
            for (int i = 0; i < _files.Length; i++)
            {
                if (_files[i].EndsWith(".meta")) continue;
                string _dirName = Path.GetFileNameWithoutExtension(_files[i]);
                string _abname = MakeAssetBundleName(_module, _dirName);
                if (_relation) RecordResourceRelationship(_module, _abname, new string[] { _files[i] });
                buildmap.AddRange(MakeAssetBundleBuild(_module, _abname, new string[] { _files[i] }));
            }
        }
        /// <summary>
        /// 将文件打入bundle
        /// </summary>
        private static List<AssetBundleBuild> MakeAssetBundleBuild(string _module, string _abname, string[] _files)
        {
            _module = _module.ToLower();
            _abname = _abname.ToLower();

            List<AssetBundleBuild> _bundles = new List<AssetBundleBuild>();
            List<string> _luas = new List<string>();
            List<string> _list = new List<string>();
            for (int i = 0; i < _files.Length; i++)
            {
                string _oneFile = _files[i].ToLower();
                string _ext = Path.GetExtension(_oneFile);
                if (FilterExtensions.IndexOf(_ext) != -1) continue;
                string _one = Utility.MakeUnifiedDirectory(_oneFile);
                //改变lua文件格式，assetbundle不识别.lua后缀名
                if (_one.EndsWith(".lua"))
                {
                    string _luaToTxt = _one.Replace(".lua", ".bytes");
                    //写一份lua的临时文件
                    byte[] _rp = File.ReadAllBytes(_one);
                    _rp = Utility.Encrypt(_rp, ProjectDatas.CODEKEY);
                    File.WriteAllBytes(_luaToTxt, _rp);
                    
                    _luas.Add(_luaToTxt);
                }
                //协议文件暂时不做加密
                else if (_one.EndsWith(".pb"))
                {
                    string _luaToTxt = _one.Replace(".pb", ".bytes");
                    //写一份lua的临时文件
                    byte[] _rp = File.ReadAllBytes(_one);
                    File.WriteAllBytes(_luaToTxt, _rp);
                    
                    _luas.Add(_luaToTxt);
                }
                else
                {
                    _list.Add(_one);
                }
            }

            AssetBundleBuild _build;

            if (_list.Count > 0)
            {
                _build = new AssetBundleBuild();
                _build.assetBundleName = (_module + "/" + _abname); ;
                _build.assetNames = _list.ToArray();
                _bundles.Add(_build);
            }

            if (_luas.Count > 0)
            {
                _build = new AssetBundleBuild();
                _build.assetBundleName = (_module + "/" + _abname); ;
                _build.assetNames = _luas.ToArray();
                _bundles.Add(_build);
            }

            return _bundles;
        }
        /// <summary>
        /// 构建bundle名称
        /// </summary>
        private static string MakeAssetBundleName(string _module, string _dir)
        {
            string _abname = _module + "_" + _dir + ProjectDatas.NAME_ASSETBUNDLE_EXTENSION;
            return _abname.ToLower();
        }
        /// <summary>
        /// 构建资源关系
        /// </summary>
        private static void RecordResourceRelationship (string _module, string _bundle, string[] _file)
        {
            _module = _module.ToLower();
            _bundle = _bundle.ToLower();

            if (currentBundleModuleName == null)
            {
                currentBundleModuleName = new AssetModuleConfig(_module, _bundle);
                resBundleModuleName.Add(currentBundleModuleName);
            }
            else
            {
                if (currentBundleModuleName.mN != _module || currentBundleModuleName.bN != _bundle)
                {
                    currentBundleModuleName = new AssetModuleConfig(_module, _bundle);
                    resBundleModuleName.Add(currentBundleModuleName);
                }
            }
            
            for (int i = 0; i < _file.Length; i++)
            {
                string _oneFile = _file[i].ToLower();
                string _fileExt = Path.GetExtension(_oneFile);
                if (_fileExt.EndsWith("meta")) continue;
                string _fileName = Path.GetFileNameWithoutExtension(_oneFile);
                //assetbundle不识别lua后缀，修改为txt后缀
                _fileExt = _fileExt.Replace(".lua", ".bytes");
                _fileExt = _fileExt.Replace(".pb", ".bytes");
                if (!currentBundleModuleName.Add (_fileName, _fileExt))
                {
                    Utility.LogError("模块", _module, "下发现重名资源：", _fileName, "   路径：", _file[i]);
                }
            }
        }
        /// <summary>
        /// 对比上一次资源
        /// </summary>
        private static List<string> Comparison ()
        {
            //输出的manifest文件名称
            string _outputName = Path.GetDirectoryName(ProjectDatas.EDITOR_ASSET_SAVE_CURRENT_ROOT);
            //输出的manifest文件路径
            string _manifestPath = ProjectDatas.EDITOR_ASSET_SAVE_CURRENT_ROOT + Path.GetFileName(_outputName);
            //改名后的manifest文件路径
            string _manifestChangePath = ProjectDatas.EDITOR_ASSET_SAVE_CURRENT_ROOT + ProjectDatas.NAME_MANIFESET_FILE;
            //加载上一次的文件
            //AssetBundleManifest _last = null;
            //if (File.Exists(_manifestChangePath))
            //{
            //    AssetBundle _ab = AssetBundle.LoadFromFile(_manifestChangePath);
            //    _last = _ab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            //    _ab.Unload(false);
            //}
            Dictionary<string, long> _old = LoadedCRCValue(Directory.GetFiles(ProjectDatas.EDITOR_ASSET_SAVE_CURRENT_ROOT, "*.manifest", SearchOption.AllDirectories));
            //删除老资源
            Directory.Delete(ProjectDatas.EDITOR_ASSET_SAVE_CURRENT_ROOT, true);
            Directory.CreateDirectory(ProjectDatas.EDITOR_ASSET_SAVE_CURRENT_ROOT);
            //构建新资源
            BuildPipeline.BuildAssetBundles(ProjectDatas.EDITOR_ASSET_SAVE_CURRENT_ROOT, buildmap.ToArray(),
                BuildAssetBundleOptions.ChunkBasedCompression | BuildAssetBundleOptions.DeterministicAssetBundle,
                EditorUserBuildSettings.activeBuildTarget);
            //改名生成的manifest文件
            File.Copy(_manifestPath, _manifestChangePath, true);
            File.Delete(_manifestPath);
            File.Copy(_manifestPath + ".manifest", _manifestChangePath + ".manifest", true);
            File.Delete(_manifestPath + ".manifest");
            //比较两次的文件
            return CompareManifestFile(_old);
        }
        /// <summary>
        /// 比较Manifest文件
        /// </summary>
        private static List<string> CompareManifestFile(Dictionary<string, long> _oldCRC)
        {
            List<string> _changed = new List<string>();
            string[] _abnames2 = Directory.GetFiles(ProjectDatas.EDITOR_ASSET_SAVE_CURRENT_ROOT, "*.manifest", SearchOption.AllDirectories);
            Dictionary<string, long> _newCRC = LoadedCRCValue(_abnames2);

            foreach (var _k in _newCRC)
            {
                string _name = _k.Key.Replace(ProjectDatas.EDITOR_ASSET_SAVE_CURRENT_ROOT, string.Empty).Replace(".manifest", string.Empty);
                if (!_oldCRC.ContainsKey(_k.Key))
                {
                    Utility.Log("<color=green>添加了资源包: ", _name, "</color>");
                    _changed.Add(_name);
                    continue;
                }
                if (_k.Value != _oldCRC[_k.Key])
                {
                    Utility.Log("<color=yellow>改变了资源包: ", _name, "</color>");
                    _changed.Add(_name);
                }
            }
            return _changed;
        }
        private static Dictionary<string, long> LoadedCRCValue(string[] _mfs)
        {
            Dictionary<string, long> _dic = new Dictionary<string, long>();
            if (_mfs == null) return _dic;

            for (int i = 0; i < _mfs.Length; i++)
            {
                //获取CRC校验码
                string[] _matline = File.ReadAllLines(_mfs[i]);
                for (int j = 0; j < _matline.Length; j++)
                {
                    if (_matline[j].Contains("CRC:"))
                    {
                        string _m = _matline[j].Replace("CRC:", string.Empty).Replace(" ", string.Empty);
                        _dic.Add(_mfs[i], long.Parse(_m));
                        break;
                    }
                }
            }
            return _dic;
        }
        /// <summary>
        /// 构造版本信息文件
        /// </summary>
        private static void BuidleVersionFiles ()
        {
            string _ver = version.ToString();
            //只记录bundle包
            string[] _allFile = Directory.GetFiles(ProjectDatas.EDITOR_ASSET_SAVE_CURRENT_ROOT, "*", SearchOption.AllDirectories);
            List<AssetBundleVersion> _list = new List<AssetBundleVersion>();

            for (int i = 0; i < _allFile.Length; i++)
            {
                string _ext = Path.GetExtension(_allFile[i]);
                //只识别资源包
                if (!string.IsNullOrEmpty(_ext) && !_ext.Equals(ProjectDatas.NAME_ASSETBUNDLE_EXTENSION)) continue;

                string _one = Utility.MakeUnifiedDirectory(_allFile[i]);
                string _mat = _one + ProjectDatas.NAME_MANIFEST_EXTENSION;
                //获取CRC校验码
                string[] _matline = File.ReadAllLines(_mat);
                for (int j = 0; j < _matline.Length; j++)
                {
                    if (_matline[j].Contains("CRC:"))
                    {
                        string _m = _matline[j].Replace("CRC:", string.Empty).Replace(" ", string.Empty);
                        _one = _one.Replace(ProjectDatas.EDITOR_ASSET_SAVE_CURRENT_ROOT, string.Empty);
                        _list.Add(new AssetBundleVersion(_one, _ver, _m));
                        break;
                    }
                }
            }

            File.WriteAllText(ProjectDatas.EDITOR_ASSET_SAVE_CURRENT_ROOT + "/" + ProjectDatas.NAME_ASSET_BUNDLE_VERSION_FILE, LitJson.JsonMapper.ToJson(_list));
            //写一份在resource目录，作为初始包记录
            string _dirRes = ProjectDatas.EDITOR_RESOURCE_ROOT + ProjectDatas.PATH_COMMON_NODE + ProjectDatas.PATH_CONFIG_NODE + "/";
            File.WriteAllText(_dirRes + ProjectDatas.NAME_ASSET_BUNDLE_VERSION_FILE, LitJson.JsonMapper.ToJson(_list));
        }
        /// <summary>
        /// 将有变化的资源打包成zip
        /// </summary>
        private static void BuidleZipFiles (List<string> _cp)
        {
            if (_cp == null || _cp.Count == 0) return;
            string _path = ProjectDatas.EDITOR_ASSET_SAVE_ROOT + "/Zips/";
            if (!Directory.Exists(_path)) Directory.CreateDirectory(_path);
            string _name = _path + version.ToString() + ".zip";

            for (int i = 0; i < _cp.Count; i++)
            {
                _cp[i] = ProjectDatas.EDITOR_ASSET_SAVE_CURRENT_ROOT + _cp[i];
            }
            //加入版本文件
            _cp.Add(ProjectDatas.EDITOR_ASSET_SAVE_CURRENT_ROOT + "/" + ProjectDatas.NAME_ASSET_BUNDLE_VERSION_FILE);
            //加入总控文件
            _cp.Add(ProjectDatas.EDITOR_ASSET_SAVE_CURRENT_ROOT + "/" + ProjectDatas.NAME_MANIFESET_FILE);

            Zip.ZipFiles(_cp.ToArray(), ProjectDatas.EDITOR_ASSET_SAVE_CURRENT_ROOT, _name);
        }
        /// <summary>
        /// 清理临时文件
        /// </summary>
        private static void CleanTempFiles ()
        {
            string[] _bytes = Directory.GetFiles(ProjectDatas.EDITOR_MODULE_ROOT, "*.bytes", SearchOption.AllDirectories);
            for (int i = 0; i < _bytes.Length; i++)
            {
                File.Delete(_bytes[i]);
            }
        }
    }
}