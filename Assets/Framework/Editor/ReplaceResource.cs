using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
namespace GameFramework
{
    public class ReplaceResource : EditorWindow
    {
        enum ResType
        {
            UI图片,
            字体,
            音频,
            动画,
        }

        private static EditorWindow windows;
        [MenuItem("工具/操作/替换资源 %&P")]
        private static void OnOpen()
        {
            if (windows != null) windows.Close();
            windows = GetWindow<ReplaceResource>("替换资源", false);
            windows.titleContent = new GUIContent("替换资源");
            windows.Show();
        }


        private ResType resType = ResType.UI图片;
        private string searchPath;
        private bool deleateOld = false;

        private Object target;
        private List<Object> needReplace = new List<Object>();

        private void OnGUI()
        {
            Running();
            SelectedGameObjects();
            Buttons();
        }

        private void Running ()
        {
            EditorGUILayout.LabelField("选择需要替换的资源类型");
            resType = (ResType)EditorGUILayout.EnumPopup(resType, GUILayout.Width(300));
            deleateOld = EditorGUILayout.Toggle("删除被替换的资源文件", deleateOld, GUILayout.Width(200));
            EditorGUILayout.Space();
            switch (resType)
            {
                case ResType.UI图片:
                    ShowUIImage();
                    break;
                case ResType.字体:
                    ShowFont();
                    break;
                case ResType.音频:
                    break;
                case ResType.动画:
                    break;
            }
        }
        
        private void ShowFont()
        {
            EditorGUILayout.HelpBox("选择资源，右侧列表中的资源将会被替换为左侧目标资源", UnityEditor.MessageType.Info);
            EditorGUILayout.BeginHorizontal();
            {
                target = EditorGUILayout.ObjectField(target, typeof(Font), false);
                EditorGUILayout.BeginVertical();
                {
                    if (needReplace.Count == 0 || needReplace[needReplace.Count - 1] != null) needReplace.Add(null);

                    for (int i = 0; i < needReplace.Count; i++)
                    {
                        if (needReplace[i] == null && i < needReplace.Count - 1)
                        {
                            needReplace.RemoveAt(i);
                            i--;
                        }
                        needReplace[i] = EditorGUILayout.ObjectField(needReplace[i], typeof(Font), false);
                    }
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
        }

        private void ShowUIImage ()
        {
            EditorGUILayout.HelpBox("选择资源，右侧列表中的资源将会被替换为左侧目标资源", UnityEditor.MessageType.Info);
            EditorGUILayout.BeginHorizontal();
            {
                target = EditorGUILayout.ObjectField(target, typeof(Sprite), false);
                EditorGUILayout.BeginVertical();
                {
                    if (needReplace.Count == 0 || needReplace[needReplace.Count - 1] != null) needReplace.Add(null);
                    
                    for (int i = 0; i < needReplace.Count; i++)
                    {
                        if (needReplace[i] == null && i < needReplace.Count - 1)
                        {
                            needReplace.RemoveAt(i);
                            i--;
                        }
                        needReplace[i] = EditorGUILayout.ObjectField(needReplace[i], typeof(Sprite), false);
                    }
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
        }
        private void SelectedGameObjects()
        {
            EditorGUILayout.HelpBox("如果路径不生效，则不执行任何功能！", UnityEditor.MessageType.Warning);
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("搜索路径:", GUILayout.Width(120));
                if (GUILayout.Button("选择", GUILayout.Width(100)))
                {
                    searchPath = EditorUtility.OpenFolderPanel("选择搜索路径", Application.dataPath, string.Empty);
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.LabelField(searchPath);

            EditorGUILayout.Space();
        }
        private void Buttons ()
        {
            if (GUILayout.Button("开始", GUILayout.Width (400)))
            {
                OnStartReplace();
            }
        }

        private void OnStartReplace ()
        {
            string[] _ss = Directory.GetFiles(searchPath, "*.prefab", SearchOption.AllDirectories);

            OnStartReplace(_ss);

            AssetDatabase.Refresh();
        }

        private void OnStartReplace(string[] _ss)
        {
            if (target != null)
            {
                string _tguid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(target));
                List<string> _guid = new List<string>();
                for (int i = 0; i < needReplace.Count; i++)
                {
                    if (needReplace[i] == null) continue;
                    string _path = AssetDatabase.GetAssetPath(needReplace[i]);
                    _guid.Add(AssetDatabase.AssetPathToGUID(_path));

                    if (deleateOld)
                    {
                        AssetDatabase.DeleteAsset(_path);
                    }
                }

                for (int i = 0; i < _ss.Length; i++)
                {
                    string _file = _ss[i];
                    _file = Utility.MakeUnifiedDirectory(_file);
                    string _name = Path.GetFileNameWithoutExtension(_file);

                    string _txt = File.ReadAllText(_file);
                    for (int j = 0; j < _guid.Count; j++)
                    {
                        if (_txt.Contains(_guid[j]))
                        {
                            _txt = _txt.Replace(_guid[j], _tguid);
                            Utility.Log("资源 ", _name, " 被修改！");
                        }
                    }
                    File.WriteAllText(_file, _txt);
                }
            }

            target = null;
            needReplace.Clear();
        }
    }
}