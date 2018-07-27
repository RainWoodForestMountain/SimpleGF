using UnityEditor;

using System.IO;

namespace GameFramework
{
    public class RenameAllModuleResource : Editor
    {
        private static string[] DISTINGUISH = new string[] { ".png", ".jpg", ".tga", ".prefab", ".anim", ".mat", ".shader", ".mp3", ".ogg", ".ttf", ".TTF", ".fontsettings" };

        [MenuItem("工具/操作/重命名所有模块资源 &R")]
        private static void OnRenameAllModuleResource()
        {
            string _basePath = ProjectDatas.EDITOR_MODULE_ROOT;
            string[] _modules = Directory.GetDirectories(_basePath);

            for (int i = 0; i < _modules.Length; i++)
            {
                if (_modules[i].Contains("ToLuaModule")) continue;
                RenameOneModule(_modules[i]);
            }
            AssetDatabase.Refresh();
        }
        public static void RenameOneModule(string _module)
        {
            List<string> _dis = new List<string>(DISTINGUISH);
            string _module_name1 = Path.GetFileNameWithoutExtension(_module).ToLower();
            string _module_name2 = _module_name1.Replace("module", string.Empty);
            string _module_name3 = _module_name1.Substring(0, 3) + "_";

            string[] _files = Directory.GetFiles(_module, "*", SearchOption.AllDirectories);
            for (int i = 0; i < _files.Length; i++)
            {
                string _fileName = Utility.MakeUnifiedDirectory(Path.GetFileNameWithoutExtension(_files[i]).ToLower());
                string _extName = Utility.MakeUnifiedDirectory(Path.GetExtension(_files[i]).ToLower());
                string _dirs = Utility.MakeUnifiedDirectory(Path.GetDirectoryName(_files[i])) + "/";

                if (!_dis.Contains(_extName)) continue;
                _extName = _extName.Substring(1, 3);
                string[] _ns = _fileName.Split(new string[] { _extName }, System.StringSplitOptions.RemoveEmptyEntries);
                //已经被重命名过
                if (_ns.Length > 1)
                {
                    _fileName = _ns[1];
                }
                if (_fileName.StartsWith("_")) _fileName = _fileName.Substring(1, _fileName.Length - 1);

                string _newName = _fileName.Replace(_module_name1, string.Empty).Replace(_module_name2, string.Empty).Replace(_module_name3, string.Empty);
                if (_newName.StartsWith("_")) _newName = _newName.Substring(1, _newName.Length - 1);
                _newName = _module_name3 + _extName + "_" + _newName;
                //修改lua文件中的对应名称
                ChangeLuaRely(_module, Path.GetFileNameWithoutExtension(_files[i]), _newName);
                AssetDatabase.RenameAsset(Utility.MakeUnifiedDirectory(_files[i]), _newName);
                AssetDatabase.SaveAssets();
            }
        }
        private static void ChangeLuaRely (string _module, string _oldName, string _newName)
        {
            _oldName = "\"" + _oldName + "\"";
            _newName = "\"" + _newName + "\"";
            string[] _files = Directory.GetFiles(_module, "*.lua", SearchOption.AllDirectories);
            for (int i = 0; i < _files.Length; i++)
            {
                string _allText = File.ReadAllText(_files[i]);
                if (_allText.Contains(_oldName))
                {
                    File.WriteAllText(_files[i], _allText.Replace(_oldName, _newName));
                }
            }
        }
    }
}