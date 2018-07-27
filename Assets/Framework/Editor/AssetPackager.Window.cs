using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace GameFramework
{
	public partial class AssetPackager : EditorWindow
	{
        static AssetPackager ()
        {
            GetUserSetting();
        }

        private static AssetPackager windows;
        [MenuItem("工具/资源/资源打包 #&B")]
        private static void OpenWindow ()
		{
			if (EditorApplication.isCompiling)
			{
				EditorUtility.DisplayDialog("警告", "请等待代码编译完成！", "确定");
				return;
			}
            if (windows != null) windows.Close();
            windows = GetWindow<AssetPackager> ("资源打包器", false);
            windows.titleContent = new GUIContent("资源打包器");
            windows.Start();
			windows.Show ();
        }

        //操作数
        private static bool wholeModule;
        private static Version version;
        private static Vector2 svposition;
        private static string[] changeds;
        private static int buidle = 0;


        public void Start ()
        {
            svposition = Vector2.zero;
        }
        private void OnGUI ()
		{
			GetUserSetting ();
			EditorGUILayout.BeginVertical ();
			{
				TIPS ();
				Propertys ();
				Functions ();

            }
			EditorGUILayout.EndVertical ();
			SetUserSetting ();
		}
		private static void GetUserSetting ()
		{
            wholeModule = PersistenceData.GetPrefsDataBool("ASSET_PACKAGER_WHOLE_MODULE");

            string _v = RunningTimeData.GetRunningData(CommonKey.VERSION_LOCAL, "2.0.0.0");
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
        private static void SetUserSetting ()
        {
            PersistenceData.SavePrefsData("ASSET_PACKAGER_WHOLE_MODULE", wholeModule);

            RunningTimeData.SetRunningData(CommonKey.VERSION_LOCAL, version.ToString());
            RunningTimeData.Record();
        }
		private void TIPS ()
		{
			EditorGUILayout.Space ();
			EditorGUILayout.LabelField ("该编辑器使用到的所有路径，请在文件\"ProjectDatas\"中修改！");
			EditorGUILayout.Space ();

			EditorGUILayout.LabelField ("资源路径：");
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Space(10);
                EditorGUILayout.LabelField(ProjectDatas.EDITOR_MODULE_ROOT);
            }
            EditorGUILayout.EndHorizontal();

			EditorGUILayout.LabelField ("打包规则文件：（如果不存在将使用默认打包方式）");
			EditorGUILayout.BeginHorizontal ();
			{
				GUILayout.Space (10);
				EditorGUILayout.LabelField (ProjectDatas.EDITOR_ASSET_MODEL_FILE);
			}
			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.LabelField ("资源保存路径：");
			EditorGUILayout.BeginHorizontal ();
			{
				GUILayout.Space (10);
				EditorGUILayout.LabelField (ProjectDatas.EDITOR_ASSET_SAVE_CURRENT_ROOT);
			}
			EditorGUILayout.EndHorizontal ();


			GUILayout.Space (20);
		}
		private void Propertys ()
		{
			EditorGUILayout.BeginHorizontal ();
			{
                wholeModule = EditorGUILayout.Toggle(wholeModule, GUILayout.Width(12));
                EditorGUILayout.LabelField("使用模块整包");
            }
			EditorGUILayout.EndHorizontal ();

            GUILayout.Space(10);

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

            GUILayout.Space(20);
        }
		private void Functions ()
		{
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button ("打包资源", GUILayout.Width(120)))
                {
                    changeds = null;
                    buidle++;
                    PersistenceData.SavePrefsData("LOCAL_VERSION_BUIDLE", buidle);
                    PackAssetBundles();
                }
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(20);
        }
	}
}