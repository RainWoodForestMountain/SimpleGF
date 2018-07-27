using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEditor.iOS.Xcode;
using System.Diagnostics;

namespace GameFramework
{
	public class BuildFactoryIOS : BuildFactory
    {
        protected string projPath = string.Empty;
		protected PBXProject proj = null;
		protected string target = string.Empty;
        protected string[] customPaths = null;

        public override void OnSetting(OneKeyBuildPackage _ok)
        {
            base.OnSetting(_ok);
            PlayerSettings.logObjCUncaughtExceptions = true;
            PlayerSettings.accelerometerFrequency = 0;
            PlayerSettings.muteOtherAudioSources = false;
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.iOS, ScriptingImplementation.IL2CPP);
            PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.iOS, ApiCompatibilityLevel.NET_2_0);
            PlayerSettings.iOS.targetDevice = iOSTargetDevice.iPhoneAndiPad;
            PlayerSettings.iOS.sdkVersion = iOSSdkVersion.DeviceSDK;
            PlayerSettings.iOS.targetOSVersionString = "9.0";
            PlayerSettings.iOS.useOnDemandResources = true;
            PlayerSettings.iOS.allowHTTPDownload = true;
            PlayerSettings.iOS.appInBackgroundBehavior = iOSAppInBackgroundBehavior.Suspend;
            PlayerSettings.iOS.appleEnableAutomaticSigning = true;
            PlayerSettings.iOS.appleDeveloperTeamID = ChannelData.current.ocAppTeamID;
            PlayerSettings.stripEngineCode = false;
        }
        public override void OnPostProcessBuildEnd (string _pathToBuiltProject) 
		{
            base.OnPostProcessBuildEnd(_pathToBuiltProject);
            projPath = PBXProject.GetPBXProjectPath(_pathToBuiltProject);
			proj = new PBXProject();

			proj.ReadFromString(File.ReadAllText(projPath));
            target = proj.TargetGuidByName("Unity-iPhone");

            //添加文件，这里直接放到plugins下面，不再拷贝，若多渠道则使用外部拷贝
            //AddFiles();
            //修改PList文件
            ChangePList(_pathToBuiltProject + "/Info.plist");
            //添加系统framsworks
            AddSystemFramworks();
            //设置属性
            SetProjectProperty();

			File.WriteAllText (projPath, proj.WriteToString ());

            //调试模式只打出xcode
            if (!UnityEngine.Debug.isDebugBuild)
            {
                //拷贝shell文件，执行shell指令
                CopyShellFilesAndRun(projPath);
            }
        }
        protected virtual void CopyShellFilesAndRun (string _projectPath)
        {
            string _shellHandle = savePath + "shellHandle/";
            if (!Directory.Exists(_shellHandle)) Directory.CreateDirectory(_shellHandle);
            string _path = Application.dataPath + "/Framework/Editor/OneKeyPack/shell/";
            //读取shell文件
            string _shell = File.ReadAllText(_path + "shell.sh");
            //debug模式
            _shell = _shell.Replace("[rp isDebug /rp]", isDebug ? "Debug" : "Release");
            //替换参数
            //解析两层路径
            string _proPath = Path.GetDirectoryName(_projectPath);
            _proPath = Path.GetDirectoryName(_proPath);
            _shell = _shell.Replace("[rp project_path /rp]", _proPath);
            //工程名称
            string _proName = PlayerSettings.productName;
            _shell = _shell.Replace("[rp project_name /rp]", _proName);
            //ipa保存路径
            string _saveIPA = savePath + "ipaPackages";
            if (!Directory.Exists(_saveIPA)) Directory.CreateDirectory(_saveIPA);
            _shell = _shell.Replace("[rp exportIpaPath /rp]", _saveIPA);
            //app保存路径
            string _saveAPP = savePath + "appPackages";
            if (!Directory.Exists(_saveAPP)) Directory.CreateDirectory(_saveAPP);
            _shell = _shell.Replace("[rp build_path /rp]", _saveAPP);
            //重命名后的app名称
            string _renameAPP = Path.GetFileName(_proPath);
            if (!Directory.Exists(_renameAPP)) Directory.CreateDirectory(_renameAPP);
            _shell = _shell.Replace("[rp package_name /rp]", _renameAPP);
            //exportOptionsPath路径
            string _expplistPath = _shellHandle + "exportOptions.plist";
            _shell = _shell.Replace("[rp exportOptionsPath /rp]", _expplistPath);
            //重写一份shell文件到shellHandle目录
            File.WriteAllText(_shellHandle + "shell.sh", _shell);
            //拷贝一份exportOptions.plist到shellHandle目录
            File.Copy(_path + "exportOptions.plist", _expplistPath, true);

            //运行shell代码
            string _shellCode = _shellHandle + "shell.sh";
            Process process = new Process();
            process.StartInfo.FileName = "osascript";
            process.StartInfo.Arguments = string.Format("-e 'tell application \"Terminal\" \n activate \n do script \"chmod a+x {0} && sh {1}\" \n end tell'", _shellCode, _shellCode);
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();
        }
        protected virtual void AddFiles()
        {
            string _target = Path.GetDirectoryName(projPath);
            _target = Path.GetDirectoryName(_target);
            _target = Utility.MakeUnifiedDirectory(_target) + "/";
            string _source = Path.GetDirectoryName(Application.dataPath) + "/ios";
            _source = Utility.MakeUnifiedDirectory(_source) + "/";

            //copy文件
            string[] _all = Directory.GetFiles(_source, "*", SearchOption.AllDirectories);
            for (int i = 0; i < _all.Length; i++)
            {
                string _one = Utility.MakeUnifiedDirectory(_all[i]);
                string _file = _one.Replace(_source, string.Empty);
                string _dirs = Path.GetDirectoryName(_target + _file);
                if (!Directory.Exists(_dirs)) Directory.CreateDirectory(_dirs);
                File.Copy(_one, _target + _file);
            }
            //引用组
            customPaths = Directory.GetDirectories(_source);
            for (int i = 0; i < customPaths.Length; i++)
            {
                string _one = Path.GetFileNameWithoutExtension(customPaths[i]);
                proj.AddFileToBuild(target, proj.AddFile(_one, _one, PBXSourceTree.Source));
            }
        }
        protected virtual void ChangePList(string _path)
        {
            PlistDocument _plist = new PlistDocument();
            _plist.ReadFromString(File.ReadAllText(_path));
            PlistElementDict _rootDict = _plist.root;
            PlistElementArray _tempList = new PlistElementArray();
            PlistElementDict _tempDict = new PlistElementDict();

            //设置 CFBundleURLTypes
            PlistElementArray _plistArray = _rootDict.CreateArray("CFBundleURLTypes");
            if (_plistArray == null) _plistArray = new PlistElementArray();

            //微信
            _tempDict = _plistArray.AddDict();
            _tempDict.SetString("CFBundleTypeRole", "Editor");
            _tempDict.SetString("CFBundleURLName", "weixin");
            _tempList = _tempDict.CreateArray("CFBundleURLSchemes");
            _tempList.AddString(ChannelData.current.wxappID);

            //白名单
            _plistArray = _rootDict.CreateArray("LSApplicationQueriesSchemes");
            if (_plistArray == null) _plistArray = new PlistElementArray();
            //微信
            _plistArray.AddString("weixin");

            //rootDict.SetString("CFBundleVersion", "11");

            File.WriteAllText(_path, _plist.WriteToString());
        }
        protected virtual void AddSystemFramworks ()
		{
            proj.AddFrameworkToProject(target, "libc++.tbd", false);
			proj.AddFrameworkToProject(target, "libz.tbd", false);
			proj.AddFrameworkToProject(target, "libstdc++.6.tbd", false);
			proj.AddFrameworkToProject(target, "libsqlite3.tbd", false);

            proj.AddFrameworkToProject(target, "Accounts.framework", false);
			proj.AddFrameworkToProject(target, "AdSupport.framework", false);
			proj.AddFrameworkToProject(target, "AssetsLibrary.framework", false);
			proj.AddFrameworkToProject(target, "CFNetwork.framework", false);
			proj.AddFrameworkToProject(target, "CoreGraphics.framework", false);
			proj.AddFrameworkToProject(target, "CoreMotion.framework", false);
			proj.AddFrameworkToProject(target, "CoreTelephony.framework", false);
			proj.AddFrameworkToProject(target, "Foundation.framework", false);
			proj.AddFrameworkToProject(target, "iAd.framework", false);
			proj.AddFrameworkToProject(target, "ImageIO.framework", false);
			proj.AddFrameworkToProject(target, "JavaScriptCore.framework", false);
			proj.AddFrameworkToProject(target, "MobileCoreServices.framework", false);
			proj.AddFrameworkToProject(target, "QuartzCore.framework", false);
			proj.AddFrameworkToProject(target, "Security.framework", false);
			proj.AddFrameworkToProject(target, "StoreKit.framework", false);
			proj.AddFrameworkToProject(target, "SystemConfiguration.framework", false);
			proj.AddFrameworkToProject(target, "UIKit.framework", false);
			proj.AddFrameworkToProject(target, "CoreFoundation.framework", false);
			proj.AddFrameworkToProject(target, "CoreText.framework", false);
			proj.AddFrameworkToProject(target, "AudioToolbox.framework", false);
			proj.AddFrameworkToProject(target, "MediaPlayer.framework", false);
			proj.AddFrameworkToProject(target, "AVFoundation.framework", false);
			proj.AddFrameworkToProject(target, "GameController.framework", false);
		}
		protected virtual void SetProjectProperty ()
		{
			proj.SetBuildProperty(target, "ENABLE_BITCODE", "No");
			proj.AddBuildProperty(target, "OTHER_LDFLAGS", "-ObjC");
			proj.AddBuildProperty(target, "LIBRARY_SEARCH_PATHS", "$(SRCROOT)/Libraries");

            //证书
            //if (isDebug)
            //{
            //    proj.SetBuildProperty(target, "CODE_SIGN_IDENTITY", "iPhone Developer: hengjia@hengjiahudong.com (AFHG22PLYK)");
            //}
            //else
            //{
            //    proj.SetBuildProperty(target, "CODE_SIGN_IDENTITY", "iPhone Distribution: Chengdu Hengjia Interaction Technology Co., Ltd. (E8RLJ3YHUJ)");
            //}

            if (customPaths != null)
            {
                for (int i = 0; i < customPaths.Length; i++)
                {
                    string _one = Path.GetFileNameWithoutExtension(customPaths[i]);
                    proj.AddBuildProperty(target, "FRAMEWORK_SEARCH_PATHS", "$(PROJECT_DIR)/" + _one);
                    proj.AddBuildProperty(target, "LIBRARY_SEARCH_PATHS", "$(PROJECT_DIR)/" + _one);
                }
            }
		}
	}
}