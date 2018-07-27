using UnityEngine;
using UnityEditor;
using System;
using System.IO;

namespace GameFramework
{
    public partial class OneKeyBuildPackage : EditorWindow
    {
        private static string[] bundleIndetfi = new string[] { "com.hjhd.szfy", "com.hjhd.aysdp", "com.tencent.tmgp.mmddszfy" };
        private static string[] channels = ChannelData.channels;
        public Version version;
        public string bundleidf;
        private int usebundleidCount = 0;
        private int useChannle = 0;
        private int buidle = 0;

        private void ComponentDataGetBundleIdentify()
        {
            //版本
            string _v = RunningTimeData.GetRunningData(CommonKey.VERSION_LOCAL, "0.0.0.0");
            version = new Version(_v);
            string _bu = DateTime.Now.ToString("MMdd");
            buidle = int.Parse(_bu);
            if (buidle > version.Build)
            {
                buidle = 0;
                PersistenceData.SavePrefsData("LOCAL_VERSION_BUIDLE", buidle);
            }

            buidle = PersistenceData.GetPrefsDataInt("LOCAL_VERSION_BUIDLE", 0);
        }
        private void ComponentDataSetBundleIdentify()
        {
            //版本
            RunningTimeData.SetRunningData(CommonKey.VERSION_LOCAL, version.ToString());
        }
        private void ComponentOnGUIBundleIdentify()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            {
                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("应用标识: ", GUILayout.Width(80));
                    usebundleidCount = EditorGUILayout.Popup(usebundleidCount, bundleIndetfi, GUILayout.Width(300));
                    if (usebundleidCount >= 0)
                    {
                        bundleidf = bundleIndetfi[usebundleidCount];
                        ChannelData.SetByBundleid(bundleidf);
                    }
                    else bundleidf = string.Empty;
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("应用名称: ", GUILayout.Width(80));
                    EditorGUILayout.LabelField(ChannelData.current.appName, GUILayout.Width(300));
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("使用渠道: ", GUILayout.Width(80));
                    useChannle = EditorGUILayout.Popup(useChannle, channels, GUILayout.Width(300));
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("版本号: ", GUILayout.Width(40));
                    int _ma = EditorGUILayout.IntField(version.Major, GUILayout.Width(36));
                    EditorGUILayout.LabelField(".", GUILayout.Width(7));
                    int _mi = EditorGUILayout.IntField(version.Minor, GUILayout.Width(36));
                    EditorGUILayout.LabelField(".", GUILayout.Width(7));
                    string _bu = DateTime.Now.ToString("MMdd");
                    EditorGUILayout.LabelField(_bu, GUILayout.Width(36));
                    EditorGUILayout.LabelField(".", GUILayout.Width(7));
                    EditorGUILayout.LabelField(buidle.ToString(), GUILayout.Width(36));

                    version = new Version(_ma + "." + _mi + "." + _bu + "." + buidle);
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
            }
            EditorGUILayout.EndVertical();
        }

        private void ComponentOnBuildBundleIdentify()
        {
            CopyChannelFiles();
            ChannelData.SetByBundleid(bundleidf);
            if (useChannle > 0)
            {
                ChannelData.SetChannelByIndex(useChannle);
            }
            AssetDatabase.Refresh();

            if (!isDebug)
            {
                buidle++;
                PersistenceData.SavePrefsData("LOCAL_VERSION_BUIDLE", buidle);
            }
        }

        private void CopyChannelFiles()
        {
            string _needReplace = "/bundle_and_channel";
            string _assetRoot = "/Assets";
            string _bundleidfRoot = "/" + bundleidf;
            string _sourePath = Path.GetDirectoryName(Application.dataPath) + _needReplace + _bundleidfRoot;
            string[] _files = Directory.GetFiles(_sourePath, "*", SearchOption.AllDirectories);
            for (int i = 0; i < _files.Length; i++)
            {
                string _cpPath = _files[i].Replace(_needReplace, _assetRoot).Replace(_bundleidfRoot, string.Empty);
                File.Copy(_files[i], _cpPath, true);
            }
        }
    }
}