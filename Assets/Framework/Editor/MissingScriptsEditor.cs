using UnityEngine;
using UnityEditor;

using System.IO;

namespace GameFramework
{
    public class MissingScriptsEditor : EditorWindow
    {
        private static List<GameObject> lstTmp = new List<GameObject>();

        [MenuItem("工具/操作/删除丢失组件 %&D")]
        private static void Execute()
        {
            CleanUpSelection();
        }

        private static void CleanUpSelection()
        {
            string _path = Application.dataPath;
            string[] _files = Directory.GetFiles(Application.dataPath, "*.prefab", SearchOption.AllDirectories);

            List<GameObject> _objs = new List<GameObject>();
            for (int i = 0; i < _files.Length; i++)
            {
                string _one = "Assets" + Utility.MakeUnifiedDirectory(_files[i]).Replace(Application.dataPath, string.Empty);
                _objs.Add(AssetDatabase.LoadAssetAtPath<GameObject>(_one));
            }

            for (int i = 0; i < _objs.Count; ++i)
            {
                EditorUtility.DisplayProgressBar("Checking", _objs[i].name, (float)i / (float)_objs.Count);
                GameObject _gameObject = _objs[i] as GameObject;
                GameObject[] _gs = UtilityUnity.GetAllChild(_gameObject);
                for (int j = 0; j < _gs.Length; j++)
                {
                    CleanUpAsset(_gs[j]);
                }
                AssetDatabase.SaveAssets();
            }
            EditorUtility.ClearProgressBar();
            AssetDatabase.Refresh();
        }

        private static void CleanUpAsset(GameObject _asset)
        {
            // 创建一个序列化对象
            SerializedObject _serializedObject = new SerializedObject(_asset);
            // 获取组件列表属性
            SerializedProperty _prop = _serializedObject.FindProperty("m_Component");

            Component[] _components = _asset.GetComponents<Component>();
            int _index = 0;
            for (int i = 0; i < _components.Length; i++)
            {
                // 如果组建为null
                if (_components[i] == null)
                {
                    // 按索引删除
                    _prop.DeleteArrayElementAtIndex(i - _index);
                    _index++;
                }
            }

            // 应用修改到对象
            _serializedObject.ApplyModifiedProperties();
        }
    }
}