using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;

namespace GameFramework
{
    public class FindProject 
	{
#if UNITY_EDITOR_OSX

		[MenuItem("Assets/Find References In Project", false, 2000)]
		private static void FindProjectReferences()
		{
		string appDataPath = Application.dataPath;
		string output = "";
		string selectedAssetPath = AssetDatabase.GetAssetPath (Selection.activeObject);
		List<string> references = new List<string>();

		string guid = AssetDatabase.AssetPathToGUID (selectedAssetPath);

		var psi = new System.Diagnostics.ProcessStartInfo();
		psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
		psi.FileName = "/usr/bin/mdfind";
		psi.Arguments = "-onlyin " + Application.dataPath + " " + guid;
		psi.UseShellExecute = false;
		psi.RedirectStandardOutput = true;
		psi.RedirectStandardError = true;

		System.Diagnostics.Process process = new System.Diagnostics.Process();
		process.StartInfo = psi;

		process.OutputDataReceived += (sender, e) => {
		if(string.IsNullOrEmpty(e.Data))
		return;

		string relativePath = "Assets" + e.Data.Replace(appDataPath, "");

		// skip the meta file of whatever we have selected
		if(relativePath == selectedAssetPath + ".meta")
		return;

		references.Add(relativePath);

		};
		process.ErrorDataReceived += (sender, e) => {
		if(string.IsNullOrEmpty(e.Data))
		return;

		output += "Error: " + e.Data + "\n";
		};
		process.Start();
		process.BeginOutputReadLine();
		process.BeginErrorReadLine();

		process.WaitForExit(2000);

		foreach(var file in references){
		output += file + "\n";
		Debug.Log(file, AssetDatabase.LoadMainAssetAtPath(file));
		}

		Debug.LogWarning(references.Count + " references found for object " + Selection.activeObject.name + "\n\n" + output);
		}
#elif UNITY_EDITOR
		private static List<Object> objs = new List<Object>();
        private static List<string> noQuote = new List<string>();
        private static string selectedPath = string.Empty;

		[MenuItem("Assets/Find References", false, 10)]
        private static void Find_References()
		{
			EditorSettings.serializationMode = SerializationMode.ForceText;
			objs = new List<Object> (Selection.objects);
            noQuote.Clear();
            selectedPath = string.IsNullOrEmpty (selectedPath) ? Application.dataPath : selectedPath;
			selectedPath = EditorUtility.OpenFolderPanel ("选择搜索的路径", selectedPath, "");
			StartFind ();
		}
		private static void StartFind ()
		{
			Object _temp = null;
			if (objs.Count > 0)
			{
				_temp = objs [0];
				objs.RemoveAt (0);
			}
			else
			{
                if (EditorUtility.DisplayDialog("对空引用文件处理","是否删除所有没有任何引用的文件？", "删除", "不删除"))
                {
                    for (int i = 0; i < noQuote.Count; i++)
                    {
                        AssetDatabase.DeleteAsset(noQuote[i]);
                    }
                    AssetDatabase.Refresh();
                }
				return;
			}
			string _path = AssetDatabase.GetAssetPath (_temp);
			string _name = _temp.name;
			if (!string.IsNullOrEmpty (_path))
			{
                int _count = 0;
				string guid = AssetDatabase.AssetPathToGUID (_path);
				List<string> withoutExtensions = new List<string> (new string[]{ ".prefab", ".unity", ".mat", ".asset" });

                string[] _files = Directory.GetFiles (string.IsNullOrEmpty (selectedPath) ? Application.dataPath : selectedPath, "*.*", SearchOption.AllDirectories).Where (s => withoutExtensions.Contains (Path.GetExtension (s).ToLower ())).ToArray();
                //string[] _luas = Directory.GetFiles(ProjectDatas.EDITOR_GAME_CONTENT_ROOT, "*.lua", SearchOption.AllDirectories);
                int _fileIndex = 0;

				EditorApplication.update = delegate()
                {
                    bool _isCancel = true;
                    if (_fileIndex < _files.Length)
                    {
                        string _file = _files[_fileIndex];

                        _isCancel = EditorUtility.DisplayCancelableProgressBar("匹配资源" + _name, _file, (float)_fileIndex / (float)_files.Length);

                        if (Regex.IsMatch(File.ReadAllText(_file), guid))
                        {
                            Debug.Log(_name + "匹配到引用：" + _file, AssetDatabase.LoadAssetAtPath<Object>(GetRelativeAssetsPath(_file)));
                            _count++;
                        }
                    }
                    //else
                    //{
                    //    int _li = _fileIndex - _files.Length;
                    //    string _resName = Path.GetFileNameWithoutExtension(_path);
                    //    string _alltxt = File.ReadAllText(_luas[_li]);

                    //    _isCancel = EditorUtility.DisplayCancelableProgressBar("匹配资源" + _name, _luas[_li], (float)_li / (float)_luas.Length);
                    //    if (_alltxt.Contains ("\"" + _resName + "\""))
                    //    {
                    //        Debug.Log(_name + "匹配到lua文件引用：" + _luas[_li]);
                    //        _count++;
                    //    }
                    //}

					_fileIndex++;
					if (_isCancel)
					{
						EditorUtility.ClearProgressBar ();
						EditorApplication.update = null;
						_fileIndex = 0;
						Debug.Log ("取消匹配");
                    }
                    //if (_fileIndex >= _files.Length + _luas.Length)
                    if (_fileIndex >= _files.Length)
                    {
                        EditorUtility.ClearProgressBar();
                        EditorApplication.update = null;
                        _fileIndex = 0;
                        if (_count == 0)
                        {
                            noQuote.Add(_path);
                        }
                        Debug.Log(_name + "匹配结束");
                        if (!_isCancel)
                        {
                            StartFind();
                        }
                    }

                    //TODO：查询lua文件
                };
			}
		}

		[MenuItem("Assets/Find References", true)]
		static private bool VFind()
		{
			string path = AssetDatabase.GetAssetPath(Selection.activeObject);
			return (!string.IsNullOrEmpty(path));
		}

		static private string GetRelativeAssetsPath(string path)
		{
			return "Assets" + Path.GetFullPath(path).Replace(Path.GetFullPath(Application.dataPath), "").Replace('\\', '/');
		}
#endif
	}
}