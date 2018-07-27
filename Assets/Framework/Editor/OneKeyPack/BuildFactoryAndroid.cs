using System.IO;
using UnityEditor;
using UnityEngine;

namespace GameFramework
{
	public class BuildFactoryAndroid : BuildFactory
    {
        public override void OnSetting(OneKeyBuildPackage _ok)
        {
            base.OnSetting(_ok);
            PlayerSettings.keyaliasPass = "a123456";
            PlayerSettings.keystorePass = "a123456";
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.Mono2x);
            PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Android, ApiCompatibilityLevel.NET_2_0);
            PlayerSettings.Android.preferredInstallLocation = AndroidPreferredInstallLocation.Auto;
            PlayerSettings.Android.bundleVersionCode = _ok.isDebug ? PlayerSettings.Android.bundleVersionCode : PlayerSettings.Android.bundleVersionCode + 1;
            PlayerSettings.Android.keystoreName = _ok.KEY_STORE_PATH;
            PlayerSettings.Android.keyaliasName = "bwgame";
            PlayerSettings.Android.useAPKExpansionFiles = _ok.buildDepartPackage;
            PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel21;
            PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevel21;
            PlayerSettings.SplashScreen.show = false;
            PlayerSettings.SplashScreen.showUnityLogo = false;
            //自定义结束

            //处理AndroidManifest文件
            DealXMLFile();

            AssetDatabase.Refresh();
        }
        /// <summary>
        /// 处理XML文件
        /// </summary>
        private void DealXMLFile()
        {
            //固定路径
            string _xmlPath = Application.dataPath + "/Plugins/Android/AndroidManifest.xml";
            if (!File.Exists(_xmlPath)) return;

            //package chang
            XMLHelper.CreateOrUpdateXmlAttributeByXPath(_xmlPath, "manifest", "package", PlayerSettings.applicationIdentifier);
            //VersionCode
            XMLHelper.CreateOrUpdateXmlAttributeByXPath(_xmlPath, "manifest", "android:versionCode", PlayerSettings.Android.bundleVersionCode.ToString());
            //bundleVersion
            XMLHelper.CreateOrUpdateXmlAttributeByXPath(_xmlPath, "manifest", "android:versionName", PlayerSettings.bundleVersion);
            //appname
            XMLHelper.CreateOrUpdateXmlAttributeByXPath(_xmlPath, "manifest/application", "android:label", PlayerSettings.productName);
            //use-sdk
            XMLHelper.CreateOrUpdateXmlAttributeByXPath(_xmlPath, "manifest/uses-sdk", "android:minSdkVersion", ((int)PlayerSettings.Android.minSdkVersion).ToString());
            XMLHelper.CreateOrUpdateXmlAttributeByXPath(_xmlPath, "manifest/uses-sdk", "android:targetSdkVersion", ((int)PlayerSettings.Android.targetSdkVersion).ToString());
        }
    }
}