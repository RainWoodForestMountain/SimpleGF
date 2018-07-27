using UnityEngine;
using UnityEditor;

namespace GameFramework
{
    public partial class OneKeyBuildPackage : EditorWindow
    {
        private string[] hotServers = new string[]
        {
            "http://192.168.1.137",
            "http://192.168.1.137",
            "http://192.168.1.137",
            "http://192.168.1.137",
            "http://119.27.178.186/root/game",
            "http://119.27.178.186/root/game",
        };
        private string[] gameServers = new string[]
        {
            "192.168.1.220",
            "192.168.1.223",
            "192.168.1.226",
            "192.168.1.137",
            "120.79.137.29",
            "qp.shizhougame.com",
        };

        //内网服务器组
        private string[] serverGroup = new string[]
        {
            "默认机器",
            "李亦机器",
            "小熊机器",
            "老谢机器",
            "爱游耍大牌",
            "十州风云",
        };
        private string[] chooseServers = null;
        private int chooseServerCount = 0;
        //是否使用热更新
        public bool useHotupdate = true;

        private void ComponentDataGetServer()
        {
            useHotupdate = RunningTimeData.GetRunningDataBool("SWITCH_USE_HOTUPDATE", true);
        }
        private void ComponentDataSetServer()
        {
            RunningTimeData.SetRunningData("SWITCH_USE_HOTUPDATE", useHotupdate.ToString());
        }

        private void ComponentOnGUIServer ()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            {
                EditorGUILayout.Space();
                useHotupdate = EditorGUILayout.Toggle("使用热更新", useHotupdate);
                EditorGUILayout.LabelField("选择服务器组");
                chooseServerCount = EditorGUILayout.Popup(chooseServerCount, serverGroup, GUILayout.Width(300));
                EditorGUILayout.LabelField("热更服：" + hotServers[chooseServerCount]);
                EditorGUILayout.LabelField("游戏服：" + gameServers[chooseServerCount]);
                EditorGUILayout.Space();
            }
            EditorGUILayout.EndVertical();
        }

        private void ComponentOnBuildWorkServer()
        {
            RunningTimeData.SetRunningData("DEFAULT_SERVER", gameServers[chooseServerCount]);
            RunningTimeData.SetRunningData("URL_HOTUPDATE", hotServers[chooseServerCount]);
            RunningTimeData.Record();
        }
    }
}
