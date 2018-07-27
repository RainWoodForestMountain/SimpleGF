using UnityEngine;

namespace GameFramework
{
    public class AssetData
    {
        private const int maxIdleTime = 10;
        public string key;
        public AssetBundle assetbundle;
        private ulong reference = 0;
        private float idleTime = 0;

        private Dictionary<string, Object> loadeds = new Dictionary<string, Object>();

        public bool canRelease
        {
            get
            {
                return Time.time - idleTime > maxIdleTime;
            }
        }
        public bool waitForRelease
        {
            get
            {
                idleTime = Time.time;
                return true;
            }
        }
        public void Used()
        {
            reference++;
        }
        public bool waitRelease
        {
            get
            {
                reference--;
                return reference == 0 && waitForRelease;
            }
        }
        public T Load<T>(string _res) where T : Object
        {
            T _t;
            if (!loadeds.ContainsKey(_res))
            {
                _t = assetbundle.LoadAsset<T>(_res);
                loadeds.Add(_res, _t);
            }
            else _t = loadeds[_res] as T;
            return _t;
        }
        public bool NeedRelease()
        {
            if (canRelease)
            {
                Utility.Log("卸载资源: ", key);
                assetbundle.Unload(true);
            }
            return canRelease;
        }
    }
}
