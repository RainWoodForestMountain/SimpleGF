using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using UnityEngine;

namespace GameFramework
{
	public static class Utility
	{
		public static void Log (params string[] _msg)
		{
			if (ProjectDatas.isDebug)
			{
				Debug.Log (ProjectDatas.LOG_SYMBOL + Utility.MergeString (_msg));
			}
		}
		public static void Log (params object[] _msg)
		{
			if (ProjectDatas.isDebug)
			{
				Debug.Log (ProjectDatas.LOG_SYMBOL + Utility.MergeString (_msg));
			}
		}
		public static void LogWarning (params string[] _msg)
		{
			if (ProjectDatas.isDebug)
			{
				Debug.LogWarning (ProjectDatas.LOG_SYMBOL + Utility.MergeString (_msg));
			}
		}
		public static void LogWarning (params object[] _msg)
		{
			if (ProjectDatas.isDebug)
			{
				Debug.LogWarning (ProjectDatas.LOG_SYMBOL + Utility.MergeString (_msg));
			}
		}
		public static void LogError (params string[] _msg)
		{
			Debug.LogError (ProjectDatas.LOG_SYMBOL + Utility.MergeString (_msg));
		}
		public static void LogError (params object[] _msg)
		{
			Debug.LogError (ProjectDatas.LOG_SYMBOL + Utility.MergeString (_msg));	
		}

        /// <summary>
        /// 优先读取持久化路径的图片资源，找不到则加载Resource路径资源并写入缓存目录
        /// </summary>
        public static string RecordTexture2DFile(string _name)
        {
            string _cfFile = MergeString(ProjectDatas.PATH_CACHE, ProjectDatas.PATH_COMMON_NODE, ProjectDatas.PATH_CACHE_SPRITE,"/" +  _name).ToLower();
            _cfFile = MakeUnifiedDirectory(_cfFile);
            if (!File.Exists(_cfFile))
            {
                // 缓存目录下无此文件，则从Resource路径加载
                string _path = Path.GetFileName(ProjectDatas.PATH_COMMON_NODE);
                _path = Utility.MergeString(_path, ProjectDatas.PATH_CACHE_SPRITE, "/", Path.GetFileNameWithoutExtension(_name)).ToLower();
                _path = MakeUnifiedDirectory(_path);
                Texture2D tt = Resources.Load<Texture2D>(_path);

                byte[] bytes = tt.EncodeToPNG();
                string recordPath = MergeString(ProjectDatas.PATH_CACHE, ProjectDatas.PATH_COMMON_NODE, ProjectDatas.PATH_CACHE_SPRITE).ToLower();
                //判断目录是否存在，不存在则会创建目录  
                if (!Directory.Exists(recordPath))
                {
                    Directory.CreateDirectory(recordPath);
                }
                string savePath = recordPath + "/" + _name;
                //存图片  
                System.IO.File.WriteAllBytes(savePath, bytes);
            }
            return _cfFile;
        }

        /// <summary>
        /// 优先读取持久化路径的配置文件，找不到则加载Resource默认路径资源
        /// </summary>
        public static string LoadConfigFile(string _name)
        {
            string _content = LoadPersistentConfigFile(_name);
            if (string.IsNullOrEmpty(_content))
            {
                _content = LoadResourceConfigFile(_name);
            }
            return _content;
        }
        public static string LoadPersistentConfigFile (string _name)
        {
            string _cfFile = MergeString(ProjectDatas.PATH_ASSETS, _name).ToLower();
            string _content = string.Empty;
            if (File.Exists(_cfFile))
            {
                _content = File.ReadAllText(_cfFile);
            }
            return _content;
        }
        public static string LoadResourceConfigFile(string _name)
        {
            string _content = string.Empty;
            string _path = Path.GetFileName(ProjectDatas.PATH_COMMON_NODE);
            _path = Utility.MergeString(_path, "/", ProjectDatas.PATH_CONFIG_NODE, "/", Path.GetFileNameWithoutExtension(_name)).ToLower();
            _path = MakeUnifiedDirectory(_path);
            TextAsset _ta = Resources.Load<TextAsset>(_path);
            if (_ta) _content = _ta.text;
            return _content;
        }
        /// <summary>
        /// 写入持久化路径的配置文件
        /// </summary>
        public static void RecordConfigTextFile(string _name, string _content)
        {
            string _dir = ProjectDatas.PATH_ASSETS.ToLower();
#if UNITY_EDITOR
            _dir = MergeString(ProjectDatas.EDITOR_RESOURCE_ROOT, ProjectDatas.PATH_COMMON_NODE, ProjectDatas.PATH_CONFIG_NODE).ToLower();
#endif
            string _cfFile = MergeString(_dir, "/", _name).ToLower();
            _cfFile = MakeUnifiedDirectory(_cfFile);
            if (!Directory.Exists(_dir)) Directory.CreateDirectory(_dir);
            File.WriteAllText(_cfFile, _content);
        }
        public static void RecordConfigTextFile(string _name, Dictionary<string, string> _content)
        {
            string _dir = ProjectDatas.PATH_ASSETS.ToLower();
#if UNITY_EDITOR
            _dir = MergeString(ProjectDatas.EDITOR_RESOURCE_ROOT, ProjectDatas.PATH_COMMON_NODE, ProjectDatas.PATH_CONFIG_NODE).ToLower();
#endif
            string _cfFile = MergeString(_dir, "/", _name).ToLower();
            _cfFile = MakeUnifiedDirectory(_cfFile);
            if (!Directory.Exists(_dir)) Directory.CreateDirectory(_dir);

            List<string> _lines = new List<string>();
            
            foreach(var _kvp in _content)
            {
                _lines.Add(MergeString(_kvp.Key, " = ", _kvp.Value));
            }

            File.WriteAllLines(_cfFile, _lines.ToArray());
        }

        /// <summary>
        /// 解析用等号和换行格式的键值对文本
        /// </summary>
        public static Dictionary<string, string> DepartDictionaryByLines (string _content)
        {
            Dictionary<string, string> _dic = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(_content)) return _dic;
            string[] _lines = _content.Split('\n');

            for (int i = 0; i < _lines.Length; i++)
            {
                if (string.IsNullOrEmpty(_lines[i])) continue;
                string _one = MakeUnifiedDirectory(_lines[i], true);
                //#开头认为是注释
                if (_one.StartsWith("#")) continue;
                if (_one.IndexOf("=") < 0) continue;
                string[] _kv = _one.Split('=');
                _dic.Add(_kv[0], _kv[1]);
            }

            return _dic;
        }
        /// <summary>
        /// 通用判空，当为空时会提示错误。如果出现空值是合理或者可控制的，不必要使用该函数
        /// </summary>
        public static bool isNull<T> (T _t) where T : UnityEngine.Object
        {
            bool _isNull = _t == null;
            return _isNull;
        }

        /// 计算字符串的MD5值
        public static string MD5(string _source)
		{
			MD5CryptoServiceProvider _md5 = new MD5CryptoServiceProvider();
			byte[] _data = Encoding.UTF8.GetBytes(_source);
			byte[] _md5Data = _md5.ComputeHash(_data, 0, _data.Length);
			_md5.Clear();

			string _destString = "";
			for (int i = 0; i < _md5Data.Length; i++)
			{
				_destString = Utility.MergeString (_destString, System.Convert.ToString(_md5Data[i], 16).PadLeft(2, '0'));
			}
			_destString = _destString.PadLeft(32, '0');
			return _destString;
		}
        /// 计算文件的MD5值
        public static string MD5File(byte[] _data)
        {
            MD5 _md5 = new MD5CryptoServiceProvider();
            byte[] _retVal = _md5.ComputeHash(_data);

            StringBuilder _sb = new StringBuilder();
            for (int i = 0; i < _retVal.Length; i++)
            {
                _sb.Append(_retVal[i].ToString("x2"));
            }
            return _sb.ToString();
        }
        public static string MD5File(string _file)
		{
			try
			{
				FileStream _fs = new FileStream(_file, FileMode.Open);
				MD5 _md5 = new MD5CryptoServiceProvider();
				byte[] _retVal = _md5.ComputeHash(_fs);
				_fs.Close();

				StringBuilder _sb = new StringBuilder();
				for (int i = 0; i < _retVal.Length; i++)
				{
					_sb.Append(_retVal[i].ToString("x2"));
				}
				return _sb.ToString();
			}
			catch (Exception ex)
			{
				throw new Exception("md5file() fail, error:" + ex.Message);
			}
		}
        /// <summary>
        /// 构造统一格式的路径
        /// </summary>
        public static string MakeUnifiedDirectory(string _dir)
        {
            return MakeUnifiedDirectory(_dir, false);
        }
        public static string MakeUnifiedDirectory(string _dir, bool _saveDouble)
        {
            _dir = _dir.Replace("\\", "/").Replace(" ", string.Empty).Replace("\t", string.Empty).Replace("\r", string.Empty);
            if (!_saveDouble)
            {
                _dir = _dir.Replace("//", "/");
            }
            if (_dir.EndsWith("/"))
            {
                _dir = _dir.Substring(0, _dir.Length - 1);
            }
            return _dir;
        }
        /// <summary>
        /// 计时器
        /// </summary>
        public static long CreateTimer (Action _end, int _loopCount, float _loopSpace, float _delay)
        {
            return TimeModule.instance.Register(_end, _loopCount, _loopSpace, _delay);
        }
        public static long CreateDelayInvok(Action _end, float _delay)
        {
            return TimeModule.instance.Register(_end, 1, 0, _delay);
        }
        public static void RemoveTimer (long _id)
        {
            TimeModule.instance.RemoveTimeNodeByID(_id);
        }
        public static void ChangedTimerSpecaTime(long _id, float _speca)
        {
            TimeModule.instance.ChangedTimerSpecaTime(_id, _speca);
        }
        public static void ChangedTimerLoopCount(long _id, int _count)
        {
            TimeModule.instance.ChangedTimerLoopCount(_id, _count);
        }
        //只支持单一动画的
        public static void PlayerAnimationAndCloseOnComplete(GameObject _obj)
        {
            if (isNull(_obj)) return;
            Animation _ani = _obj.GetComponent<Animation>();
            if (isNull(_ani)) return;

            _obj.SetActive(true);
            _ani.Play();

            foreach (AnimationState _state in _ani)
            {
                CreateDelayInvok(() => { if (_obj)_obj.SetActive(false); }, _state.length);
                break;
            }
        }
        /// <summary>
        /// 获取所有组件，包括自身（包括被关闭的物体）
        /// </summary>
        public static List<T> GetComponentsInChild<T>(Transform _t) where T :Component
        {
            List<T> _cs = new List<T>();
            for (int i = 0; i < _t.childCount; i++)
            {
                _cs.AddRange(GetComponentsInChild<T>(_t.GetChild(i)));
            }
            T _tt = _t.GetComponent<T>();
            if (_tt != null)
            {
                _cs.Add(_tt);
            }
            return _cs;
        }


        /// <summary>
        /// 字符串操作
        /// </summary>
        private static StringBuilder sb = new StringBuilder();
        public static string MergeString(string _s, long _l)
        {
            lock (sb)
            {
                sb.Length = 0;
                sb.Append(_s);
                sb.Append(_l);
                return sb.ToString();
            }
        }
        public static string MergeString(params string[] _ss)
        {
            lock (sb)
            {
                sb.Length = 0;
                for (int i = 0; i < _ss.Length; i++)
                {
                    sb.Append(_ss[i]);
                }
                return sb.ToString();
            }

        }
        public static string MergeString(params object[] _ss)
        {
            lock (sb)
            {
                sb.Length = 0;
                for (int i = 0; i < _ss.Length; i++)
                {
                    sb.Append(_ss[i]);
                }
                return sb.ToString();
            }
        }
        /// <summary>
        /// 获取不带后缀的文件名
        /// </summary>
        public static string GetFileNameWithoutExtension(string _path)
        {
            return Path.GetFileNameWithoutExtension(_path);
        }

        //池
        public static void PoolPush(string _key, GameObject _obj)
        {
            PoolModule.instance.Push(_key, _obj);
        }
        public static GameObject PoolPop(string _key)
        {
            return PoolModule.instance.Pop<GameObject>(_key);
        }

        public static float RealtimeSinceStarGetTime()
        {
            return Time.realtimeSinceStartup;
        }

        public static byte[] Encrypt (byte[] _datas, string _key)
        {
            return Cryptography.Encrypt(_datas, _key);
        }
        public static byte[] Decrypt(byte[] _datas, string _key)
        {
            return Cryptography.Decrypt(_datas, _key);
        }
    }
}