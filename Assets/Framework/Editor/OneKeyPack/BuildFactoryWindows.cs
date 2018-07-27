using UnityEditor;

namespace GameFramework
{
	public class BuildFactoryWindows : BuildFactory
    {
        public override void OnSetting(OneKeyBuildPackage _ok)
        {
            base.OnSetting(_ok);
            PlayerSettings.runInBackground = true;
            PlayerSettings.allowedAutorotateToLandscapeLeft = true;
            PlayerSettings.allowedAutorotateToLandscapeRight = true;
            PlayerSettings.allowedAutorotateToPortraitUpsideDown = false;
            PlayerSettings.allowedAutorotateToPortrait = false;
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.Mono2x);
            PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Android, ApiCompatibilityLevel.NET_2_0);
            PlayerSettings.defaultInterfaceOrientation = UIOrientation.LandscapeLeft | UIOrientation.LandscapeRight;
            PlayerSettings.SplashScreen.show = false;
            PlayerSettings.SplashScreen.showUnityLogo = false;
        }
    }
}