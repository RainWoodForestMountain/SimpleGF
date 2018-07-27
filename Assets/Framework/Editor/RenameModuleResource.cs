using UnityEngine;
using UnityEditor;

using System.IO;

namespace GameFramework
{
    public class RenameModuleResource : ScriptableObject
    {
        [MenuItem("Assets/规范化命名", false, 10)]
        private static void ReName()
        {
            EditorSettings.serializationMode = SerializationMode.ForceText;
            Object[] arr = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            string _path = Path.GetDirectoryName(Application.dataPath) + "/" + AssetDatabase.GetAssetPath(arr[0]);
            while (!_path.EndsWith ("Module"))
            {
                _path = Path.GetDirectoryName(_path);
            }
            _path = _path.Replace(Application.dataPath + "/", "Assets/");
            RenameAllModuleResource.RenameOneModule(Utility.MakeUnifiedDirectory(_path));
        }
    }
}