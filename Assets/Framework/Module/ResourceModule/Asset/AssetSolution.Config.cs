namespace GameFramework
{
    public class AssetModuleConfig
    {
        public string mN;
        public string bN;
        public Dictionary<string, string> rN;

        public AssetModuleConfig() { }
        public AssetModuleConfig(string _moduleName, string _bundleName)
        {
            mN = _moduleName;
            bN = _bundleName;
            rN = new Dictionary<string, string>();
        }
        public bool Add(string _res, string _ext)
        {
            if (rN.ContainsKey(_res))
            {
                rN.Add(_res, _ext);
                return false;
            }
            else
            {
                rN.Add(_res, _ext);
                return true;
            }
        }
        public string GetBundleName(string _res)
        {
            return Utility.MergeString (mN, "/", bN).ToLower();
        }
        public string GetResourceName (string _res)
        {
            return Utility.MergeString(_res, rN[_res]).ToLower();
        }
        public bool Contains (string _module, string _res)
        {
            if (!mN.Equals(_module) && !mN.ToLower().Equals(_module.ToLower())) return false;
            return rN.ContainsKey(_res);
        }
        /// <summary>
        /// 比较危险的比较方式，一般仅对全局唯一命名同时无法定位到模块的资源进行该对比
        /// </summary>
        public bool Contains(string _res)
        {
            return rN.ContainsKey(_res);
        }
    }

    public partial class AssetSolution : IAssetLoader
    {
        private List<AssetModuleConfig> amcs;

        /// <summary>
        /// 资源对照字典，格式 ： module_name : resource_name : bundle_name
        /// 没有空格，全小写
        /// </summary>
        private void InitConfigDic ()
        {
            string _content = Utility.LoadConfigFile (ProjectDatas.NAME_ASSET_BUNDLE_INFO_FILE);
            amcs = LitJson.JsonMapper.ToObject<List<AssetModuleConfig>>(_content);
        }

        private AssetModuleConfig GetAssetModuleConfigByModule (string _module, string _res)
        {
            if (string.IsNullOrEmpty (_module))
            {
                return GetAssetModuleConfigByModule(_res);
            }
            for (int i = 0; i < amcs.Count; i++)
            {
                if (amcs[i].Contains(_module, _res))
                {
                    return amcs[i];
                }
            }
            return null;
        }
        /// <summary>
        /// 比较危险的比较方式，一般仅对全局唯一命名同时无法定位到模块的资源进行该对比
        /// </summary>
        private AssetModuleConfig GetAssetModuleConfigByModule(string _res)
        {
            for (int i = 0; i < amcs.Count; i++)
            {
                if (amcs[i].Contains(_res))
                {
                    return amcs[i];
                }
            }
            return null;
        }
    }
}