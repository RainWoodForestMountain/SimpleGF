using System;

namespace GameFramework
{
    public class AssetBundleVersion
    {
        public string n;
        public string v;
        public string m;
        public uint crc;

        private Version ver = null;
        public Version version
        {
            get
            {
                if (ver == null) ver = new Version(v);
                return ver;
            }
        }

        public AssetBundleVersion() { }
        public AssetBundleVersion(string _name, string _version, string _md5)
        {
            n = _name;
            v = _version;
            m = _md5;
            crc = uint.Parse(m);
        }

        public bool NeedDownloadThan (AssetBundleVersion _c)
        {
            bool _need = true;
            _need &= !IsNewThan(_c);
            _need &= !IsEquals(_c);
            return _need;
        }
        /// <summary>
        /// 对比传入的信息，判断是否是更新的文件
        /// </summary>
        public bool IsNewThan(AssetBundleVersion _c)
        {
            return this.version > _c.version;
        }

        public bool IsEquals(AssetBundleVersion _c)
        {
            return crc == _c.crc;
        }
        public static bool operator >=(AssetBundleVersion _abv1, AssetBundleVersion _abv2)
        {
            return _abv1.version >= _abv2.version;
        }
        public static bool operator <=(AssetBundleVersion _abv1, AssetBundleVersion _abv2)
        {
            return _abv1.version <= _abv2.version;
        }
    }
}