using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace GameFramework
{
	public partial class OneKeyBuildPackage
    {

        private string PlatformPath = string.Empty;
        public string SAVE_PATH
        {
            get
            {
                string _path = Application.dataPath;
                _path = Path.GetDirectoryName(_path);
                _path += "/Build/" + PlatformPath;
                return Utility.MakeUnifiedDirectory(_path) + "/";
            }
        }
        public string KEY_STORE_PATH
        {
            get
            {
                string _path = Application.dataPath;
                _path = Path.GetDirectoryName(_path);
                _path += "/android";
                return Utility.MakeUnifiedDirectory(_path) + "/" + keyStoreNames[usebundleidCount];
            }
        }

        private static IBuildFactory current;
        private static bool isStartBuild = false;
        private static Action waitAc;

        /// <summary>
        /// 开始打包了
        /// </summary>
        private void StartBuild ()
		{
			//开始
			BuildReady ();
		}
        
		private void BuildReady ()
		{
            string _ex = string.Empty;
            //设置平台
            BuildTarget _bt = BuildTarget.StandaloneWindows;
            BuildTargetGroup _btg = BuildTargetGroup.Standalone;
            //判断平台
            switch (bt)
			{
			    case MBuidleTarget.IOS:
                    current = new BuildFactoryIOS();
                    _bt = BuildTarget.iOS;
                    _btg = BuildTargetGroup.iOS;
                    _ex = string.Empty;
                    PlatformPath = "/ios";
                    break;
                case MBuidleTarget.Android:
                    current = new BuildFactoryAndroid();
                    _bt = BuildTarget.Android;
                    _btg = BuildTargetGroup.Android;
                    _ex = ".apk";
                    PlatformPath = "/android";
                    break;
                case MBuidleTarget.Window:
                    current = new BuildFactoryWindows();
                    _bt = BuildTarget.StandaloneWindows;
                    _btg = BuildTargetGroup.Standalone;
                    _ex = ".exe";
                    PlatformPath = "/window";
                    break;
                default:
                    return;
			}
            //各平台的设置
            current.OnSetting(this);
            //构建生成名
            string _name = bundleidf + "-" + version.ToString() + _ex;
			BuildOptions _b = isDebug ? BuildOptions.Development | BuildOptions.AllowDebugging : BuildOptions.None;
            //切换平台
            EditorUserBuildSettings.SwitchActiveBuildTarget(_btg, _bt);
            AssetDatabase.Refresh();
            if (!oldRes)
            {
                //打包资源
                AssetPackager.PackAssetBundles(true);
                AssetDatabase.Refresh();
            }
            //删除缓存路径
            if (Directory.Exists(ProjectDatas.PATH_CACHE_STREAMING))
            {
                Directory.Delete(ProjectDatas.PATH_CACHE_STREAMING, true);
            }
            Directory.CreateDirectory(ProjectDatas.PATH_CACHE_STREAMING);
            if (!Directory.Exists(ProjectDatas.EDITOR_ASSET_SAVE_CURRENT_ROOT))
            {
                EditorUtility.DisplayDialog("错误", "没有找到打包资源，请重新构建资源包", "确定");
                return;
            }
            //测试包包含所有资源
            if (isDebug || isFull || baseModule.Count == 0)
            {
                string[] _files = Directory.GetFiles(ProjectDatas.EDITOR_ASSET_SAVE_CURRENT_ROOT, "*", SearchOption.AllDirectories);
                for (int i = 0; i < _files.Length; i++)
                {
                    string _one = ProjectDatas.PATH_CACHE_STREAMING + _files[i].Replace(ProjectDatas.EDITOR_ASSET_SAVE_CURRENT_ROOT, "/");
                    if (_one.EndsWith(".meta")) continue;
                    if (_one.EndsWith(".manifest")) continue;
                    string _dir = Path.GetDirectoryName(_one);

                    if (!Directory.Exists(_dir)) Directory.CreateDirectory(_dir);

                    File.Copy(_files[i], _one);
                }
            }
            else
            {
                //正式包只包含指定的模块资源
                string[] _files = Directory.GetFiles(ProjectDatas.EDITOR_ASSET_SAVE_CURRENT_ROOT);
                string[] _direcs = Directory.GetDirectories(ProjectDatas.EDITOR_ASSET_SAVE_CURRENT_ROOT);
                //如果是正式包，只拷贝外层文件和指定文件夹下面的内容
                for (int i = 0; i < _files.Length; i++)
                {
                    string _one = ProjectDatas.PATH_CACHE_STREAMING + _files[i].Replace(ProjectDatas.EDITOR_ASSET_SAVE_CURRENT_ROOT, "/");
                    if (_one.EndsWith(".meta")) continue;
                    if (_one.EndsWith(".manifest")) continue;
                    string _dir = Path.GetDirectoryName(_one);

                    File.Copy(_files[i], _one);
                }
                for (int i = 0; i < _direcs.Length; i++)
                {
                    string _one = Path.GetFileNameWithoutExtension (_direcs[i].ToLower());
                    if (baseModule.Contains(_one))
                    {
                        _files = Directory.GetFiles(_direcs[i], "*", SearchOption.AllDirectories);
                        for (int j = 0; j < _files.Length; j++)
                        {
                            if (_files[j].EndsWith(".meta")) continue;
                            if (_files[j].EndsWith(".manifest")) continue;
                            string _of = ProjectDatas.PATH_CACHE_STREAMING + _files[j].Replace(ProjectDatas.EDITOR_ASSET_SAVE_CURRENT_ROOT, "/");
                            string _dir = Path.GetDirectoryName(_of);

                            if (!Directory.Exists(_dir)) Directory.CreateDirectory(_dir);

                            File.Copy(_files[j], _of);
                        }
                    }
                }
            }
            AssetDatabase.Refresh();

            if (!Directory.Exists(SAVE_PATH))
            {
                Directory.CreateDirectory(SAVE_PATH);
            }
            isStartBuild = true;
            string _file = SAVE_PATH + _name;
            if (File.Exists(_file)) File.Delete(_file);
            BuildPipeline.BuildPlayer(GetScene(), _file, _bt, _b);
        }
        /// <summary>
        /// 游戏的场景获取
        /// </summary>
        private EditorBuildSettingsScene[] GetScene ()
		{
			//自定义打入的场景
			return new EditorBuildSettingsScene[] {new EditorBuildSettingsScene ("Assets/GameContent/Scene/start.unity", true)};
		}
		[PostProcessBuild (100)]
		public static void OnPostProcessBuild (BuildTarget _target, string _pathToBuiltProject)
		{
            if (!isStartBuild) return;
            isStartBuild = false;

            current.OnPostProcessBuildEnd(_pathToBuiltProject);

            if (Directory.Exists(ProjectDatas.PATH_CACHE_STREAMING))
            {
                Directory.Delete(ProjectDatas.PATH_CACHE_STREAMING, true);
            }
            AssetDatabase.Refresh();
        }
    }
}