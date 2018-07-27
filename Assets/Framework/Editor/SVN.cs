using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using System.IO;

namespace GameFramework
{
	public class SVN
    {
		private const string COMMIT = "commit";
		private const string UPDATE = "update";

		private const string SHELL_CODE = @"
#!/bin/bash
svn update {0}
echo EndWorking";

		/// <summary>
		/// 创建一个SVN的cmd命令
		/// </summary>
		/// <param name="_command">命令(可在help里边查看)</param>
		/// <param name="_path">命令激活路径</param>
		public static void SVNCommand(string _command, string _path)
		{
#if UNITY_IOS || UNITY_IPHONE
			string _c = Application.dataPath + "/Tools/editor/SVN/svnupdate.sh";
			string _content = string.Format (SHELL_CODE, _path);
			File.WriteAllText(_c, _content);
			ProcessStartInfo _info = new ProcessStartInfo("/bin/bash", _c);
#else
            //closeonend 2 表示假设提交没错，会自动关闭提交界面返回原工程，详细描述可在
            //TortoiseSVN/help/TortoiseSVN/Automating TortoiseSVN里查看
            string _c = "/c tortoiseproc.exe /command:{0} /path:\"{1}\" /closeonend 2";
			_c = string.Format(_c, _command, _path);
			ProcessStartInfo _info = new ProcessStartInfo("cmd.exe", _c);
#endif
            _info.WindowStyle = ProcessWindowStyle.Hidden;
            Process _p = Process.Start(_info);
            _p.WaitForExit();
        }
        /// <summary>
        /// 提交选中内容
        /// </summary>
        [MenuItem("工具/SVN/提交选中的 %&C", false, 100)]
        public static void SVNCommit()
		{
			SVNCommand(COMMIT, GetSelectedObjectPath());
		}
        /// <summary>
        /// 更新选中内容
        /// </summary>
        [MenuItem("工具/SVN/更新选中的", false, 100)]
        public static void SVNUpdate()
		{
			SVNCommand(UPDATE, GetSelectedObjectPath());
		}
		/// <summary>
		/// 更新全部内容
		/// </summary>
		[MenuItem("工具/SVN/更新所有 %&U", false, 100)]
		public static void SVNUpdateAll()
		{
			SVNCommand(UPDATE, Path.GetDirectoryName(Application.dataPath));
		}
		public static void SVNUpdateAll (string _path)
		{
			SVNCommand(UPDATE, _path);
		}

		/// <summary>
		/// 获取全部选中物体的路径
		/// 包括meta文件
		/// </summary>
		private static string GetSelectedObjectPath()
		{
			string _path = string.Empty;

			for (int i = 0; i < Selection.objects.Length; i++)
			{
				_path += AssetsPathToFilePath(AssetDatabase.GetAssetPath(Selection.objects[i]));
				//路径分隔符
				_path += "*";
				//meta文件
				_path += AssetsPathToFilePath(AssetDatabase.GetAssetPath(Selection.objects[i])) + ".meta";
				//路径分隔符
				_path += "*";
			}

			return _path;
		}
        /// <summary>
        /// 将Assets路径转换为File路径
        /// </summary>
        private static string AssetsPathToFilePath(string _path)
		{
			string _new_path = Path.GetDirectoryName (Application.dataPath) + "/";
			_new_path += _path;

			return _new_path;
		}

	}
}