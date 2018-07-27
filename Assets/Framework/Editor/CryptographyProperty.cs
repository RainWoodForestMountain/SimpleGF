using UnityEngine;
using UnityEditor;

namespace GameFramework
{
    public class CryptographyProperty : EditorWindow
    {
        [MenuItem("工具/操作/加密参数")]
        private static void OnOpen()
        {
            EditorWindow _win = CreateInstance<CryptographyProperty>();
            _win.titleContent = new GUIContent("加密参数");
            _win.Show();
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("设置Lua文件加密Key：(至少16位，不足自动填充0)");
            string _keyLua = EditorGUILayout.TextField(PersistenceData.GetPrefsData("CRYPT_KEY", "abcdefghijklmnop12345678"));
            while (_keyLua.Length < 16)
            {
                _keyLua += "0";
            }
            PersistenceData.SavePrefsData("CRYPT_KEY", _keyLua);
        }
    }
}