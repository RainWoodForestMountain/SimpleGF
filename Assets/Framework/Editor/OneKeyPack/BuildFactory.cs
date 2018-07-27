using System.IO;
using UnityEditor;
using UnityEngine;

namespace GameFramework
{
    public abstract class BuildFactory : IBuildFactory
    {

        protected string savePath = string.Empty;
        protected bool isDebug = false;
        public virtual void OnSetting(OneKeyBuildPackage _ok)
        {
            savePath = _ok.SAVE_PATH;
            isDebug = _ok.isDebug;
            PlayerSettings.runInBackground = true;
            PlayerSettings.allowedAutorotateToLandscapeLeft = true;
            PlayerSettings.allowedAutorotateToLandscapeRight = false;
            PlayerSettings.allowedAutorotateToPortraitUpsideDown = false;
            PlayerSettings.allowedAutorotateToPortrait = false;
            PlayerSettings.bundleVersion = _ok.version.ToString();
            PlayerSettings.applicationIdentifier = _ok.bundleidf;
            PlayerSettings.productName = ChannelData.current.appName;
            PlayerSettings.companyName = "恒嘉互动";
            PlayerSettings.defaultInterfaceOrientation = UIOrientation.LandscapeLeft | UIOrientation.LandscapeRight;
            PlayerSettings.enableCrashReportAPI = false;
            PlayerSettings.actionOnDotNetUnhandledException = ActionOnDotNetUnhandledException.Crash;
        }
        public virtual void OnPostProcessBuildEnd(string _pathToBuiltProject)
        {
            System.Diagnostics.Process.Start(Path.GetDirectoryName(savePath));
        }
    }
}
