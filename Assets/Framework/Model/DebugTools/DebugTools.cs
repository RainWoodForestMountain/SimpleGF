using UnityEngine;
using UnityEngine.UI;

namespace GameFramework
{
    public class DebugTools : MonoBase
    {
        private static bool isInit = false;
        public static void Init ()
        {
            if (isInit) return;
            GameObject _temp = new GameObject("DebugTools");
            DontDestroyOnLoad(_temp);
            _temp.AddComponent<DebugTools>();
        }
        
        GUIStyle backStyle;
        GUISkin skin;
        private void Awake()
        {
            int _normal = 40;

            skin = Instantiate (Resources.Load<GUISkin>("DebugTools_reporterScrollerSkin"));
            skin.label.normal.textColor = Color.black;
            skin.label.fontSize = _normal;

            skin.horizontalSlider.fixedHeight = _normal;
            skin.horizontalSliderThumb.fixedHeight = _normal;
            skin.horizontalSliderThumb.fixedWidth = _normal;

            backStyle = new GUIStyle();
            backStyle.normal.background = Resources.Load<Texture2D>("DebugTools_writh_bg");
            backStyle.clipping = TextClipping.Clip;
            backStyle.alignment = TextAnchor.UpperLeft;
            backStyle.imagePosition = ImagePosition.ImageLeft;
        }
        private void Update()
        {
            if (Input.touchCount > 0 && Input.touches[0].tapCount > 10)
            {
                OnOpen();
            }
            Run();
        }
        
        private bool isOpen = false;
        private void OnOpen()
        {
            isOpen = true;
        }
        private void OnClose()
        {
            isOpen = false;
        }
        
        private void Run ()
        {
            //CPU
            for (int i = 0; i < cup * 10; i++)
            {
                Debug.Log("------------------------test cpu------------------------------");
            }
            //memory
            int _bet = 50;
            if (memory > 0 && memory * _bet != memoryArrays.Count)
            {
                while (memoryArrays.Count > memory * _bet)
                {
                    memoryArrays.RemoveAt(0);
                }
                while (memoryArrays.Count < memory * _bet)
                {
                    memoryArrays.Add(Resources.Load<TextAsset>("DebugTools_test_memory").text);
                }
            }
        }

        private List<string> memoryArrays = new List<string>();
        
        private int cup = 0;
        private int memory = 0;
        private int net = 0;

        private void OnGUI()
        {
            if (!isOpen) return;
            GUI.skin = skin;
            GUILayout.Box("", backStyle, GUILayout.Width(800), GUILayout.Height(Screen.height));
            GUI.Label(new Rect(50, 50, 500, 50), "压力测试：");
            GUI.Label(new Rect(50, 100, 150, 50), "CPU：");
            cup = (int)GUI.HorizontalSlider(new Rect(220, 100, 500, 50), cup, 0, 100);
            GUI.Label(new Rect(50, 150, 150, 50), "内存：");
            memory = (int)GUI.HorizontalSlider(new Rect(220, 150, 500, 50), memory, 0, 100);
            GUI.Label(new Rect(50, 200, 150, 50), "网络：");
            net = (int)GUI.HorizontalSlider(new Rect(220, 200, 500, 50), net, 0, 100);


            if (GUI.Button(new Rect (800, Screen.height - 100, 100, 100), Resources.Load<Texture2D>("DebugTools_close")))
            {
                isOpen = false;
            }
        }
    }
}