using System.IO;

using UnityEngine;
using UnityEditor;

namespace GameFramework
{
    class RunningDatas
    {
        public string dis;
        public string key;
        public string val;
        public string totaldes
        {
            get
            {
                return "#" + dis;
            }
        }
        public string total
        {
            get
            {
                return key + " = " + val;
            }
        }

        public RunningDatas ()
        {
            dis = "这里写注释";
            key = "key";
            val = "val";
        }
    }
    public class RunningDataEditor : EditorWindow
    {
        private static EditorWindow windows;
        [MenuItem("工具/编辑/编辑默认运行数据配置表 &Y")]
        private static void Creater()
        {
            if (windows != null) windows.Close();
            windows = GetWindow<RunningDataEditor>("运行数据配置表", false);
            windows.titleContent = new GUIContent("运行数据配置表");
            windows.Show();
        }

        private static string baseResPath = ProjectDatas.EDITOR_RESOURCE_ROOT + ProjectDatas.PATH_COMMON_NODE + ProjectDatas.PATH_CONFIG_NODE + "/" + ProjectDatas.NAME_RUNNING_TIME_DATA;

        private List<RunningDatas> lines;
        private int addCount = 0;
        private Vector2 pos;

        private void OnEnable()
        {
            baseResPath = Utility.MakeUnifiedDirectory(baseResPath);

            lines = new List<RunningDatas>();
            string[] _list = File.ReadAllLines(baseResPath);

            RunningDatas _one = new RunningDatas();
            for (int i = 0; i < _list.Length; i++)
            {
                string _l = Utility.MakeUnifiedDirectory(_list[i], true);
                if (string.IsNullOrEmpty(_l)) continue;
                if (_l.StartsWith("#")) _one.dis = _l.Substring(1, _l.Length - 1);
                else
                {
                    if (_l.IndexOf("=") > 0 && _l.IndexOf("=") < _l.Length - 1)
                    {
                        string[] _ss = _l.Split('=');
                        _one.key = _ss[0];
                        _one.val = _ss[1];
                        lines.Add(_one);
                        _one = new RunningDatas();
                    }
                }
            }
        }

        private void OnGUI()
        {
            pos = EditorGUILayout.BeginScrollView(pos);
            {
                Running();
            }
            EditorGUILayout.EndScrollView();
        }
        private void Running ()
        {
            for (int i = 0; i < lines.Count; i++)
            {
                //EditorGUILayout.BeginHorizontal();
                //{
                //    EditorGUILayout.LabelField("注释：", GUILayout.Width(50));
                //    lines[i].dis = EditorGUILayout.TextField(lines[i].dis, GUILayout.Width(300));
                //}
                //EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("key：", GUILayout.Width(50));
                    lines[i].key = EditorGUILayout.TextField(lines[i].key, GUILayout.Width(300));
                }
                EditorGUILayout.EndHorizontal();


                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("value：", GUILayout.Width(50));
                    lines[i].val = EditorGUILayout.TextField(lines[i].val, GUILayout.Width(300));
                }
                EditorGUILayout.EndHorizontal();

                if (GUILayout.Button("移除", GUILayout.Width(100)))
                {
                    lines.RemoveAt(i);
                    i--;
                }
                EditorGUILayout.Space();
            }

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("添加", GUILayout.Width(150)))
                {
                    for (int i = 0; i < addCount; i++)
                    {
                        lines.Add(new RunningDatas());
                    }
                    addCount = 0;
                }
                addCount = EditorGUILayout.IntField(addCount, GUILayout.Width(20));
                EditorGUILayout.LabelField("个");
            }
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("保存", GUILayout.Width(150)))
            {
                List<string> _all = new List<string>();
                for (int i = 0; i < lines.Count; i++)
                {
                    if (!string.IsNullOrEmpty(lines[i].dis)) _all.Add(lines[i].totaldes);
                    _all.Add(lines[i].total);
                }

                File.WriteAllLines(baseResPath, _all.ToArray());
                AssetDatabase.Refresh();
            }
        }
    }
}