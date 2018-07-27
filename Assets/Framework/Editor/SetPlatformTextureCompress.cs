using UnityEngine;
using UnityEditor;

using System.IO;

namespace GameFramework
{
    public class SetPlatformTextureCompress : ScriptableObject
    {
        private static string[] contans = new string[] { ".png", ".jpg", ".tga" };

        [MenuItem("工具/操作/压缩图片")]
        private static void DoWork()
        {
            List<string> _cc = new List<string>(contans);
            string _path = EditorUtility.OpenFolderPanel("选择文件夹", Application.dataPath, "");
            if (string.IsNullOrEmpty(_path)) return;

            string[] _files = Directory.GetFiles(_path, "*", SearchOption.AllDirectories);
            for (int i = 0; i < _files.Length; i++)
            {
                string _one = _files[i];
                string _ex = Path.GetExtension(_one);
                if (!_cc.Contains(_ex)) continue;
                _one = Utility.MakeUnifiedDirectory(_one);
                _one = _one.Replace(Application.dataPath, string.Empty);
                _one = "Assets" + _one;
                
                TextureImporter _ti = (TextureImporter)AssetImporter.GetAtPath(_one);

                if (_ex.Equals (".jpg"))
                {
                    SetCompressByPlantform_JPG(_ti);
                }
                else
                {
                    SetCompressByPlantform_PNG(_ti);
                }

                _ti.mipmapEnabled = false;
                _ti.npotScale = TextureImporterNPOTScale.None;
                _ti.SaveAndReimport();
            }
            AssetDatabase.Refresh();
        }
        
        private static void SetCompressByPlantform_PNG (TextureImporter _ti)
        {
            //The options for the platform string are: "Standalone", "iPhone", "Android", "WebGL", "Windows Store Apps", "PSP2", "PS4", "XboxOne", "Nintendo 3DS" and "tvOS".
            TextureImporterPlatformSettings _apc = new TextureImporterPlatformSettings();
            _apc.compressionQuality = 100;
            _apc.overridden = true;
            _apc.maxTextureSize = 512;
            _apc.allowsAlphaSplitting = true;
            _apc.crunchedCompression = true;

            _apc.name = "Android";
            _apc.format = TextureImporterFormat.ARGB32;
            //android
            _ti.SetPlatformTextureSettings(_apc);
            
            _apc.name = "iPhone";
            _apc.format = TextureImporterFormat.ARGB32;
            //ios
            _ti.SetPlatformTextureSettings(_apc);
        }

        private static void SetCompressByPlantform_JPG(TextureImporter _ti)
        {
            //The options for the platform string are: "Standalone", "iPhone", "Android", "WebGL", "Windows Store Apps", "PSP2", "PS4", "XboxOne", "Nintendo 3DS" and "tvOS".
            TextureImporterPlatformSettings _apc = new TextureImporterPlatformSettings();
            _apc.compressionQuality = 100;
            _apc.overridden = true;
            _apc.maxTextureSize = 2048;
            _apc.allowsAlphaSplitting = true;
            _apc.crunchedCompression = true;

            _apc.name = "Android";
            _apc.format = TextureImporterFormat.RGB24;
            //android
            _ti.SetPlatformTextureSettings(_apc);

            _apc.name = "iPhone";
            _apc.format = TextureImporterFormat.RGB24;
            //ios
            _ti.SetPlatformTextureSettings(_apc);
        }
    }
}