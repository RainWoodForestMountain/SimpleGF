#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace GameFramework
{
	public class MainMono : MonoBehaviour
	{
		private void Awake ()
        {
#if UNITY_EDITOR
            if (!Check()) return;
#endif
            if (Main.instance == null)
            {
                Main.instance = new Main();
                Main.instance.Init ();
			}
            if (ProjectDatas.isDebug)
            {
                DebugTools.Init();
            }
            Destroy (this.gameObject);

            Application.targetFrameRate = 30;
		}

#if UNITY_EDITOR
        private bool Check ()
        {
            if (!System.IO.Directory.Exists(Application.dataPath + "/AssetsPackages"))
            {
                EditorApplication.isPlaying = false;
                EditorUtility.DisplayDialog("错误", "没有找到编辑器运行所需的资源包，请打包资源！（快捷键 alt+b）", "确定");
                return false;
            }
            return true;
        }
#endif
    }
	internal class Main
	{
        internal static Main instance;
        internal void Init ()
		{
			ModuleController.instance = new ModuleController ();
            ModuleController.instance.Init();
        }
        internal void Destroy ()
		{
			ModuleController.instance.Destroy ();
			ModuleController.instance = null;
		}
	}
}