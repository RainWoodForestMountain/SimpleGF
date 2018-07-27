using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
    public static class PersistenceData
    {
        public static void SavePrefsData (string _key, string _value)
        {
            PlayerPrefs.SetString(_key, _value);
        }
        public static void SavePrefsData(string _key, int _value)
        {
            SavePrefsData(_key, _value.ToString());
        }
        public static void SavePrefsData(string _key, float _value)
        {
            SavePrefsData(_key, _value.ToString());
        }
        public static void SavePrefsData(string _key, double _value)
        {
            SavePrefsData(_key, _value.ToString());
        }
        public static void SavePrefsData(string _key, long _value)
        {
            SavePrefsData(_key, _value.ToString());
        }
        public static void SavePrefsData(string _key, bool _value)
        {
            SavePrefsData(_key, _value.ToString());
        }
        public static void SavePrefsData<T>(string _key, IEnumerable<T> _ie)
        {
            List<T> _list = new List<T>(_ie);
            SavePrefsData(_key, LitJson.JsonMapper.ToJson(_list));
        }
        public static string GetPrefsData (string _key, string _default = "")
        {
            return PlayerPrefs.GetString(_key, _default);
        }
        public static int GetPrefsDataInt(string _key, int _default = 0)
        {
            try
            {
                return int.Parse(GetPrefsData(_key));
            }
            catch
            {
                return _default;
            }
        }
        public static long GetPrefsDataLong(string _key, long _default = 0)
        {
            try
            {
                return long.Parse(GetPrefsData(_key));
            }
            catch
            {
                return _default;
            }
        }
        public static float GetPrefsDataFloat(string _key, float _default = 0)
        {
            try
            {
                return float.Parse(GetPrefsData(_key));
            }
            catch
            {
                return _default;
            }
        }
        public static double GetPrefsDataDouble(string _key, double _default = 0)
        {
            try
            {
                return double.Parse(GetPrefsData(_key));
            }
            catch
            {
                return _default;
            }
        }
        public static bool GetPrefsDataBool(string _key, bool _default = false)
        {
            string _value = GetPrefsData(_key);
            if (string.IsNullOrEmpty(_value)) return _default;
            _value = _value.ToLower();

            return _value.Equals("true") || _value.Equals("yes") || _value.Equals("1");
        }
        public static T[] GetPrefsDataArray<T>(string _key, T[] _default = null)
        {
            string _value = GetPrefsData(_key);
            if (string.IsNullOrEmpty(_value)) return _default;
            return LitJson.JsonMapper.ToObject<T[]>(_value);
        }
    }
}
