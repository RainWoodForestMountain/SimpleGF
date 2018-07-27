using System;
using UnityEngine;
using UnityEditor;

using LitJson;

namespace GameFramework
{
    public partial class OneKeyBuildPackage : EditorWindow
    {
        private static string[] keyStoreNames = new string[] { "szhy.keystore", "aysdp.keystore", "szhy.keystore" };

        enum MBuidleTarget
        {
            IOS,
            Android,
            Window,
        }

        private static EditorWindow windows;
        [MenuItem("工具/操作/一键打包 %&B")]
        private static void Creater()
        {
            if (windows != null) windows.Close();
            windows = CreateInstance<OneKeyBuildPackage>();
            windows.titleContent = new GUIContent("一键打包");
            windows.Show();
            ChannelData.Refresh();
        }

        private string tempBaseModule = string.Empty;
        private List<string> baseModule = new List<string>();
        private MBuidleTarget bt = MBuidleTarget.Window;
        //是否debug版本
        public bool isDebug = true;
        //全包含资源
        public bool isFull = false;
        //分离体
        public bool buildDepartPackage = false;
        //不打新资源（不存在依然会打包资源）
        public bool oldRes = false;

        private void OnGUI()
        {
            GetUserSetting();

            Propertys();

            SetUserSetting();
        }
        private void GetUserSetting()
        {
            ComponentDataGetServer();
            ComponentDataGetBundleIdentify();

            //基础模块（针对业务逻辑）
            string _baseJson = RunningTimeData.GetRunningData("JSON_BASE_MODULE", string.Empty);
            if (!string.IsNullOrEmpty(_baseJson))
            {
                baseModule = JsonMapper.ToObject<List<string>>(_baseJson);
            }

            //平台
            bt = (MBuidleTarget)PersistenceData.GetPrefsDataInt("BUNDLE_PLANTFORM", 0);
            //是否debug版本
            isDebug = PersistenceData.GetPrefsDataBool("BUNDLE_ISDEBUG", true);
            //全包含资源
            isFull = PersistenceData.GetPrefsDataBool("BUNDLE_ISFULL", false);
            //分离体
            buildDepartPackage = PersistenceData.GetPrefsDataBool("BUNDLE_ISDEPART", false);
            //不打新资源（不存在依然会打包资源）
            oldRes = PersistenceData.GetPrefsDataBool("BUNDLE_ISOLDRES", false);
    }
        private void SetUserSetting()
        {
            ComponentDataSetServer();
            ComponentDataSetBundleIdentify();

            //基础模块（针对业务逻辑）
            string _baseJson = JsonMapper.ToJson(baseModule);
            RunningTimeData.SetRunningData("JSON_BASE_MODULE", _baseJson);

            //平台
            PersistenceData.SavePrefsData("BUNDLE_PLANTFORM", (int)bt);
            //是否debug版本
            PersistenceData.SavePrefsData("BUNDLE_ISDEBUG", isDebug);
            //全包含资源
            PersistenceData.SavePrefsData("BUNDLE_ISFULL", isFull);
            //分离体
            PersistenceData.SavePrefsData("BUNDLE_ISDEPART", buildDepartPackage);
            //不打新资源（不存在依然会打包资源）
            PersistenceData.SavePrefsData("BUNDLE_ISOLDRES", oldRes);
        }
        private void Propertys()
        {
            ComponentOnGUIBundleIdentify();
            ComponentOnGUIServer();

            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("平台: ", GUILayout.Width(40));
                bt = (MBuidleTarget)EditorGUILayout.EnumPopup(bt, GUILayout.Width(300));
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            buildDepartPackage = EditorGUILayout.Toggle("Android分离体", buildDepartPackage);
            isDebug = EditorGUILayout.Toggle("测试包", isDebug);
            oldRes = EditorGUILayout.Toggle("不打新资源", oldRes);
            if (!isDebug) isFull = EditorGUILayout.Toggle("包含全资源", isFull);
            if (!isDebug && !isFull)
            {
                EditorGUILayout.LabelField("需要打入的基础模块名称（无关大小写）");
                for(int i = 0; i < baseModule.Count; i++)
                {
                    baseModule[i] = baseModule[i].ToLower();
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField(baseModule[i], GUILayout.Width(300));
                        if (GUILayout.Button("删除", GUILayout.Width(60)))
                        {
                            baseModule.RemoveAt(i);
                            i--;
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.Space();
                
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("添加一个新的基础模块：", GUILayout.Width(150));
                    tempBaseModule = EditorGUILayout.TextField(tempBaseModule, GUILayout.Width(200));
                    if (GUILayout.Button("添加", GUILayout.Width(50)))
                    {
                        baseModule.Add(tempBaseModule);
                        tempBaseModule = string.Empty;
                    }
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space();
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("打包", GUILayout.Width(300)))
            {
                ComponentOnBuildBundleIdentify();
                ComponentOnBuildWorkServer();
                RunningTimeData.Record();
                StartBuild();
            }
        }
    }
}