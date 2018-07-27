using System.IO;

using LuaInterface;

namespace GameContent
{
    /// <summary>
    /// 集成自LuaFileUtils，重写里面的ReadFile，
    /// </summary>
    public class ToLuaLoader : LuaFileUtils
    {
        private ToLuaModule module;
        public ToLuaLoader(ToLuaModule _module)
        {
            instance = this;
            module = _module;
        }

        /// <summary>
        /// 添加打入Lua代码的AssetBundle
        /// </summary>
        public void AddBundle(string bundleName)
        {
            //string url = Util.DataPath + bundleName.ToLower();
            //if (File.Exists(url))
            //{
            //    var bytes = File.ReadAllBytes(url);
            //    AssetBundle bundle = AssetBundle.LoadFromMemory(bytes);
            //    if (bundle != null)
            //    {
            //        bundleName = bundleName.Replace("lua/", "").Replace(".unity3d", "");
            //        base.AddSearchBundle(bundleName.ToLower(), bundle);
            //    }
            //}
        }

        /// <summary>
        /// 当LuaVM加载Lua文件的时候，这里就会被调用，
        /// 用户可以自定义加载行为，只要返回byte[]即可。
        /// </summary>
        public override byte[] ReadFile(string fileName)
        {
            string[] _depart = fileName.Split('-');
            if (_depart.Length > 1)
            {
                return module.ReadFile(_depart[0], _depart[1]);
            }
            else
            {
                return module.ReadFile(fileName);
            }
        }
    }
}