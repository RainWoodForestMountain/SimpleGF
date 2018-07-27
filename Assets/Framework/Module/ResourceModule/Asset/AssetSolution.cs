using System.IO;
using UnityEngine;

namespace GameFramework
{
	public partial class AssetSolution : IAssetLoader
    {
		private AssetBundleManifest abManifest;
		private Dictionary<string, string> dAssetIndex;
		private Dictionary<string, AssetData> dAssetCache;
		private Dictionary<string, AssetData> dWaitForRelease;

        private Dictionary<string, AssetBundleVersion> localversion;
        private Dictionary<string, AssetBundleVersion> serverversion;

        private long unloadTimerID;

		public AssetSolution()
        {
            Refresh();
        }

        public void Refresh ()
        {
            Clean(false);
            localversion = CreateVersionDictionary(Utility.LoadResourceConfigFile(ProjectDatas.NAME_ASSET_BUNDLE_VERSION_FILE));
            serverversion = CreateVersionDictionary(Utility.LoadPersistentConfigFile(ProjectDatas.NAME_ASSET_BUNDLE_VERSION_FILE));

            dAssetCache = new Dictionary<string, AssetData>();
            dWaitForRelease = new Dictionary<string, AssetData>();
            dAssetIndex = new Dictionary<string, string>();

            abManifest = LoadAssetManifest();
            InitConfigDic();
            unloadTimerID = TimeModule.instance.Register(UnloadWaitForReleaseAssetBundle, -1, 1);
        }
        private void Clean (bool _force = true)
        {
            abManifest = null;
            if (dAssetCache != null && dAssetCache.Count > 0)
            {
                foreach (var pair in dAssetCache)
                {
                    pair.Value.assetbundle.Unload(_force);
                }
                dAssetCache.Clear();
                dAssetCache = null;
            }
            if (dWaitForRelease != null && dWaitForRelease.Count > 0)
            {
                foreach (var pair in dWaitForRelease)
                {
                    pair.Value.assetbundle.Unload(_force);
                }
                dWaitForRelease.Clear();
                dWaitForRelease = null;
            }
        }

		public void Destroy()
		{
            Clean();
            TimeModule.instance.RemoveTimeNodeByID(unloadTimerID);
		}

        private Dictionary<string, AssetBundleVersion> CreateVersionDictionary(string _config)
        {
            Dictionary<string, AssetBundleVersion> _dic = new Dictionary<string, AssetBundleVersion>();
            AssetBundleVersion[] _abvlist = LitJson.JsonMapper.ToObject<AssetBundleVersion[]>(_config);
            if (_abvlist != null)
            {
                for (int i = 0; i < _abvlist.Length; i++)
                {
                    _dic.Add(_abvlist[i].n, _abvlist[i]);
                }
            }
            return _dic;
        }
        private AssetBundle LoadAssetBundle(string _fileName)
        {
#if UNITY_EDITOR
            string _ediPath = Utility.MergeString(ProjectDatas.EDITOR_ASSET_SAVE_CURRENT_ROOT, _fileName);
            return AssetBundle.LoadFromFile(_ediPath);
#endif
            string _localPath = Utility.MergeString(ProjectDatas.PATH_CACHE_STREAMING, _fileName);
            string _presentPath = Utility.MergeString(ProjectDatas.PATH_CACHE_PERSISTENT, _fileName);
            string _path = string.Empty;
            uint _crc = 0;

            bool _allexistence = true;
            //如果下载资源中没有该资源直接读取本地资源
            if (!serverversion.ContainsKey(_fileName))
            {
                _path = _localPath;
                _crc = localversion[_fileName].crc;
                _allexistence = false;
            }
            //如果本地不存在，检查下载资源
            if (!localversion.ContainsKey(_fileName))
            {
                _path = _presentPath;
                _crc = serverversion[_fileName].crc;
                _allexistence = false;
            }
            //如果都不存在，那还玩蛋？直接抛出错误就行
            if (_allexistence)
            {
                //比较资源版本
                if (serverversion[_fileName] >= localversion[_fileName])
                {
                    //如果下载版本更新，但是又找不到资源的情况下（如被恶意修改），则读取本地资源
                    if (File.Exists(_presentPath))
                    {
                        _path = _presentPath;
                        _crc = serverversion[_fileName].crc;
                    }
                    else
                    {
                        //TODO:其他处理方式
                        _path = _localPath;
                        _crc = localversion[_fileName].crc;
                    }
                }
                else
                {
                    //本地版本大于下载版本（安装更新的包，则读取本地资源）
                    _path = _localPath;
                    _crc = localversion[_fileName].crc;
                }
            }
            
            try
            {
                return AssetBundle.LoadFromFile(_path, _crc);
            }
            catch (System.Exception _e)
            {
                Utility.LogError("资源", _fileName, "加载失败！error = ", _e);
                return null;
            }
        }
        /// <summary>
        /// 加载manifest文件
        /// </summary>
        private AssetBundleManifest LoadAssetManifest()
		{
            AssetBundle _ab = LoadAssetBundle(ProjectDatas.NAME_MANIFESET_FILE);
            AssetBundleManifest _manifest = _ab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
			_ab.Unload(false);
			return _manifest;
		}
        /// <summary>
        /// 加载源资源
        /// </summary>
		private AssetData LoadSourceAssetBundle(string _abname)
		{
			if (dWaitForRelease.ContainsKey (_abname))
            {
                AssetData _data = dWaitForRelease[_abname];
				_data.Used();
				dWaitForRelease.Remove (_abname);
				dAssetCache.Add (_abname, _data);
				return _data;
			}
            
			AssetBundle _ab = LoadAssetBundle(_abname);
            if (_ab != null)
			{
				AssetData _asset = new AssetData ();
				_asset.key = _abname;
				_asset.assetbundle = _ab;
				_asset.Used();
				dAssetCache.Add (_abname, _asset);
				return _asset;
			}
            
            Utility.LogError("加载资源失败: ", _abname);
			return null;
		}
        /// <summary>
        /// 卸载源资源，加入待卸载资源，保留30秒，避免快速重复加载
        /// </summary>
		private void UnloadSourceAssetBundle(string _abname)
		{
            if (dAssetCache.ContainsKey(_abname))
            {
                AssetData _asset = dAssetCache[_abname];
                dAssetCache.Remove(_abname);
                dWaitForRelease.Add(_abname, _asset);
            }
		}
        /// <summary>
        /// 卸载待卸载资源，为保证ios内存不剧烈波动，这里一次只卸载10个资源包
        /// </summary>
        private void UnloadWaitForReleaseAssetBundle ()
        {
            lock (dWaitForRelease)
            {
                if (dWaitForRelease.Count == 0) return;
                int _count = 0;
                List<string> _unloadKey = new List<string>();
                foreach (System.Collections.Generic.KeyValuePair<string, AssetData> _kvp in dWaitForRelease)
                {
                    if (_kvp.Value == null)
                    {
                        _unloadKey.Add(_kvp.Key);
                        continue;
                    }
                    else
                    {
                        if (_kvp.Value.NeedRelease())
                        {
                            _unloadKey.Add(_kvp.Key);
                            _count++;
                        }
                    }
                    if (_count > 10) break;
                }
                for (int i = 0; i < _unloadKey.Count; i++)
                {
                    dWaitForRelease.Remove(_unloadKey[i]);
                }
            }
        }
        /// <summary>
        /// 加载依赖资源
        /// </summary>
		private void LoadDependencies(string _abname)
		{
			string[] _dependencies = abManifest.GetAllDependencies(_abname);
			if (_dependencies.Length == 0) return;

			for (int i = 0; i < _dependencies.Length; i++)
			{
				AssetData _asset = null;
				if (dAssetCache.TryGetValue (_dependencies[i], out _asset))
				{
					_asset.Used();
				}
				else
				{
					LoadSourceAssetBundle (_dependencies[i]);
				}
			}
		}
        /// <summary>
        /// 卸载依赖资源
        /// </summary>
		private void UnloadDependencies(string _abname)
		{
			string[] _dependencies = abManifest.GetAllDependencies(_abname);
			if (_dependencies.Length == 0) return;

			for (int i = 0; i < _dependencies.Length; i++)
			{
				AssetData _asset = null;
				if (dAssetCache.TryGetValue (_dependencies[i], out _asset) && _asset.waitRelease)
				{
					UnloadSourceAssetBundle (_dependencies[i]);
				}
			}
		}
        /// <summary>
        /// 卸载指定资源，外部接口
        /// </summary>
		private void UnloadAssetBundle(string _abname)
		{
			AssetData _asset = null;
			if (dAssetCache.TryGetValue(_abname, out _asset) && _asset.waitRelease)
			{
				UnloadDependencies(_abname);
				UnloadSourceAssetBundle(_abname);
			}
		}
        /// <summary>
        /// 加载AssetBundle包
        /// </summary>
		private T LoadAssetBundle<T>(string _abname, string _res) where T : UnityEngine.Object
        {
			AssetData _asset = null;
			if (dAssetCache.TryGetValue(_abname, out _asset))
			{
				_asset.Used();
			}
			else
			{
				LoadDependencies(_abname);
                _asset = LoadSourceAssetBundle(_abname);
			}
            return _asset.Load<T>(_res);
        }

        public byte[] LoadAssetBytes<T>(string _module, string _res) where T : UnityEngine.Object
        {
            byte[] _bs = null;
            if (typeof(T) == typeof(TextAsset))
            {
#if UNITY_EDITOR
                _bs = EditorLoadTextFile(_module, _res);
#else
                TextAsset _textAsset = LoadAsset<TextAsset>(_module, _res);
                if (_textAsset != null) _bs = _textAsset.bytes;
#endif
            }
            return _bs;
        }
        public T LoadAsset<T>(string _module, string _res) where T : UnityEngine.Object
        {
            T _t = null;
            _module = _module.ToLower();
            _res = _res.ToLower();
#if UNITY_EDITOR
            if (!string.IsNullOrEmpty(_module))
            {
                _t = EditorLoad<T>(_module, _res);
            }
#endif
            if (_t == null)
            {
                AssetModuleConfig _amc = GetAssetModuleConfigByModule(_module, _res);
                if (_amc == null) return null;
                _t = LoadAssetBundle<T>(_amc.GetBundleName(_res), _amc.GetResourceName(_res));
            }
            if (_t == null)
            {
                Utility.LogError("模块", _module, "中的资源", _res, "加载失败！");
            }
            return _t;
		}
        public void UnloadAsset(string _module, string _res)
        {
            AssetModuleConfig _amc = GetAssetModuleConfigByModule(_module, _res);
            if (_amc == null) return;
            UnloadAssetBundle(_amc.GetBundleName(_res));
        }

#if UNITY_EDITOR
        private Dictionary<string, Dictionary<string, string>> editorRely;
        private void CreateEditorRely ()
        {
            editorRely = new Dictionary<string, Dictionary<string, string>>();
            string _path = ProjectDatas.EDITOR_MODULE_ROOT;
            string[] _dirs = Directory.GetDirectories(ProjectDatas.EDITOR_MODULE_ROOT);
            for (int i = 0; i < _dirs.Length; i++)
            {
                editorRely.Add(Path.GetFileNameWithoutExtension(_dirs[i]).ToLower(), new Dictionary<string, string>());
            }

            foreach (var _kvp in editorRely)
            {
                string[] _files = Directory.GetFiles(_path + _kvp.Key, "*", SearchOption.AllDirectories);
                for (int i = 0; i < _files.Length; i++)
                {
                    string _one = _files[i];

                    if (_one.EndsWith("meta")) continue;
                    if (_one.EndsWith("lua")) continue;
                    if (_one.EndsWith("proto")) continue;

                    string _name = Path.GetFileNameWithoutExtension(_one).ToLower();
                    string _directory = Utility.MakeUnifiedDirectory(_one);
                    _directory = _directory.Replace(Utility.MakeUnifiedDirectory(Application.dataPath), string.Empty);
                    _kvp.Value.Add(_name, _directory);
                }
            }
        }
        private T EditorLoad<T>(string _module, string _res) where T : UnityEngine.Object
        {
            if (editorRely == null) CreateEditorRely();

            _module = _module.ToLower();
            _res = _res.ToLower();

            string _path = editorRely[_module][_res];

            return UnityEditor.AssetDatabase.LoadAssetAtPath<T>(_path);
        }
        public byte[] EditorLoadTextFile(string _module, string _res)
        {
            if (editorRely == null) CreateEditorRely();

            _module = _module.ToLower();
            _res = _res.ToLower();

            AssetModuleConfig _amc = GetAssetModuleConfigByModule(_module, _res);
            string _path = editorRely[_module][_res];
            _path = Path.GetDirectoryName(_path);
            _path += "/";
            string _fname = _amc.GetResourceName(_res);
            string _ext = Path.GetExtension(_fname);
            if (_ext.EndsWith("bytes"))
            {
                string _nefname = Path.GetFileNameWithoutExtension(_fname);
                while (true)
                {
                    if (File.Exists(_path + _nefname + ".lua"))
                    {
                        _path = _path + _nefname + ".lua";
                        break;
                    }
                    if (File.Exists(_path + _nefname + ".pb"))
                    {
                        _path = _path + _nefname + ".pb";
                        break;
                    }
                    break;
                }
            }
            return File.ReadAllBytes(_path);
        }
#endif
    }
}