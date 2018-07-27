using System.IO;

namespace GameFramework
{
    public static class RunningTimeData
    {
        /// <summary>
        /// 默认配置
        /// </summary>
        static RunningTimeData()
        {
            string _content = Utility.LoadConfigFile(ProjectDatas.NAME_RUNNING_TIME_DATA);
            if (string.IsNullOrEmpty(_content)) return;
            runningDatas = Utility.DepartDictionaryByLines(_content);
        }

        private static Dictionary<string, string> runningDatas = new Dictionary<string, string>();
        public static string GetRunningData(string _key)
        {
            return GetRunningData(_key, string.Empty);
        }
        public static string GetRunningData(string _key, string _default = "")
        {
            if (runningDatas.ContainsKey(_key))
            {
                return runningDatas[_key];
            }
            else
            {
                return _default;
            }
        }
        public static int GetRunningDataInt(string _key, int _default = 0)
        {
            try
            {
                return int.Parse(GetRunningData(_key));
            }
            catch
            {
                return _default;
            }
        }
        public static float GetRunningDataFloat(string _key, float _default = 0)
        {
            try
            {
                return float.Parse(GetRunningData(_key));
            }
            catch
            {
                return _default;
            }
        }
        public static bool GetRunningDataBool(string _key, bool _default = false)
        {
            string _value = GetRunningData(_key);
            if (string.IsNullOrEmpty(_value)) return _default;
            _value = _value.ToLower();
            return _value.Equals("true") || _value.Equals("yes") || _value.Equals("1");
        }
        /// <summary>
        /// 设置单一键值对
        /// </summary>
        /// <param name="_force">强制设置，无论是否合理</param>
        public static void SetRunningData(string _key, string _value, bool _force = false)
        {
            if (string.IsNullOrEmpty(_key) || string.IsNullOrEmpty(_value)) return;
            if (_key.Contains("Version") || _key.Contains("Ver") || _key.Contains("V."))
            {
                if (runningDatas.ContainsKey(_key))
                {
                    int _c = runningDatas[_key].CompareTo(_value);
                    if (_c == 1 && !_force)
                    {
                        Utility.Log("当前存在的运行数据", _key, "的值大于新设定值，如果要强行设定，请设置参数_force的值为true");
                        return;
                    }
                }
            }
            runningDatas.Add(_key, _value);
        }
        /// <summary>
        /// 通过字典设置
        /// </summary>
        public static void SetRunningData(Dictionary<string, string> _dic)
        {
            if (_dic == null) return;

            foreach(var _kvp in _dic)
            {
                SetRunningData(_kvp.Key, _kvp.Value);
            }
        }

        public static void RemoveRunningData(string _key)
        {
            runningDatas.Remove(_key);
        }

        public static void Record ()
        {
            Utility.RecordConfigTextFile(ProjectDatas.NAME_RUNNING_TIME_DATA, runningDatas);
        }
    }
}
