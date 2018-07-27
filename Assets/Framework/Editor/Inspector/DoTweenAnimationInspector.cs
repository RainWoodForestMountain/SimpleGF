using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

using DG.Tweening;

namespace GameFramework
{
    [CustomEditor(typeof(DoTweenAnimation))]
    public class DoTweenAnimationInspector : Editor
    {
        private DoTweenAnis laseChoose = DoTweenAnis.No;

        private DoTweenAnimationData data;
        private DoTweenAnimation current;

        private Vector3 currentVector3
        {
            get
            {
                if (data == null) return Vector3.zero;
                switch (data.anis)
                {
                    case DoTweenAnis.ToMove:
                        return current.position;
                    case DoTweenAnis.ToLocalMove:
                        return current.localPosition;
                    case DoTweenAnis.ToEulerAngles:
                        return current.eulerAngles;
                    case DoTweenAnis.ToLocalEulerAngles:
                        return current.localEulerAngles;
                    case DoTweenAnis.ToScale:
                        return current.localScale;
                    case DoTweenAnis.ToMoveUGUI:
                        if (current.rectTransform) return current.rectTransform.anchoredPosition3D;
                        break;
                }
                return Vector3.zero;
            }
            set
            {
                if (EditorApplication.isPlaying) return;
                switch (data.anis)
                {
                    case DoTweenAnis.ToMove:
                        current.position = value;
                        break;
                    case DoTweenAnis.ToLocalMove:
                        current.localPosition = value;
                        break;
                    case DoTweenAnis.ToEulerAngles:
                        current.eulerAngles = value;
                        break;
                    case DoTweenAnis.ToLocalEulerAngles:
                        current.localEulerAngles = value;
                        break;
                    case DoTweenAnis.ToScale:
                        current.localScale = value;
                        break;
                    case DoTweenAnis.ToMoveUGUI:
                        if (current.rectTransform) current.rectTransform.anchoredPosition3D = value;
                        break;
                }
            }
        }
        private Vector2 currentVector2
        {
            get
            {
                if (data == null) return Vector2.zero;
                switch (data.anis)
                {
                    case DoTweenAnis.ToUGUIScale:
                        if (current.rectTransform) return current.rectTransform.sizeDelta;
                        break;
                }
                return Vector2.zero;
            }
            set
            {
                if (EditorApplication.isPlaying) return;
                switch (data.anis)
                {
                    case DoTweenAnis.ToUGUIScale:
                        if (current.rectTransform) current.rectTransform.sizeDelta = value;
                        break;
                }
            }
        }
        private Color currentColor
        {
            get
            {
                if (data == null) return Color.white;
                switch (data.anis)
                {
                    case DoTweenAnis.ToUGUIColor:
                        if (current.graphic) return current.graphic.color;
                        break;
                }
                return Color.white;
            }
            set
            {
                if (EditorApplication.isPlaying) return;
                switch (data.anis)
                {
                    case DoTweenAnis.ToUGUIColor:
                        if (current.graphic)
                        {
                            current.graphic.color = value;
                            current.graphic.SetNativeSize();
                        }
                        break;
                }
            }
        }
        private float currentFloat
        {
            get
            {
                if (data == null) return 0;
                switch (data.anis)
                {
                    case DoTweenAnis.ToAlpha:
                        if (current.graphic) return current.graphic.color.a;
                        if (current.canvasGroup) return current.canvasGroup.alpha;
                        break;
                    case DoTweenAnis.ToUGUIShowInt:
                        if (current.text) return int.Parse(current.text.text);
                        break;
                    case DoTweenAnis.ToUGUIShowNumber:
                        if (current.text) return float.Parse(current.text.text);
                        break;
                    case DoTweenAnis.ToFilledImage:
                        if (current.image) return current.image.fillAmount;
                        break;
                }
                return 0;
            }
            set
            {
                if (EditorApplication.isPlaying) return;
                switch (data.anis)
                {
                    case DoTweenAnis.ToAlpha:
                        if (current.graphic)
                        {
                            current.graphic.color = new Color(current.graphic.color.r, current.graphic.color.g, current.graphic.color.b, value);
                            current.graphic.SetNativeSize();
                        }
                        if (current.canvasGroup) current.canvasGroup.alpha = value;
                        break;
                    case DoTweenAnis.ToUGUIShowInt:
                        if (current.text)
                        {
                            current.text.text = ((int)value).ToString();
                            current.text.SetNativeSize();
                        }
                        break;
                    case DoTweenAnis.ToUGUIShowNumber:
                        if (current.text)
                        {
                            current.text.text = value.ToString("F" + data.limit);
                            current.text.SetNativeSize();
                        }
                        break;
                    case DoTweenAnis.ToFilledImage:
                        if (current.image)
                        {
                            if (current.image.type != Image.Type.Filled)
                            {
                                current.image.type = Image.Type.Filled;
                            }
                            current.image.fillAmount = value;
                        }
                        break;
                }
            }
        }

        private void OnEnable()
        {
            current = (DoTweenAnimation)target;
            data = current.GetData();
            laseChoose = data.anis;
            Init();
        }
        
        public override void OnInspectorGUI()
        {
            EditorGUILayout.Space();
            data.openEditor = EditorGUILayout.Toggle("编辑", data.openEditor);
            if (!data.openEditor)
            {
                return;
            }
            data.anis = (DoTweenAnis)EditorGUILayout.EnumPopup("动画类型", data.anis);
            data.autoPlayer = EditorGUILayout.Toggle("打开物体时自动播放", data.autoPlayer);
            data.autoDestroy = EditorGUILayout.Toggle("完成时自动销毁", data.autoDestroy);
            if (!data.autoDestroy)
            {
                data.autoClose = EditorGUILayout.Toggle("完成时隐藏物体", data.autoClose);
                data.autoDisable = EditorGUILayout.Toggle("完成时关闭组件", data.autoDisable);
            }
            if (laseChoose != data.anis)
            {
                data.alreadySetStart = false;
                data.alreadySetEnd = false;
                laseChoose = data.anis;
            }

            GUILayout.Space(20);
            switch (data.anis)
            {
                case DoTweenAnis.ToMove:
                case DoTweenAnis.ToLocalMove:
                case DoTweenAnis.ToEulerAngles:
                case DoTweenAnis.ToLocalEulerAngles:
                case DoTweenAnis.ToScale:
                case DoTweenAnis.ToMoveUGUI:
                    ToVector3();
                    break;
                case DoTweenAnis.ToUGUIScale:
                    ToVector2();
                    break;
                case DoTweenAnis.ToUGUIShowInt:
                case DoTweenAnis.ToUGUIShowNumber:
                case DoTweenAnis.ToAlpha:
                case DoTweenAnis.ToFilledImage:
                    ToNumber();
                    break;
                case DoTweenAnis.ToUGUIColor:
                    ToColor();
                    break;
                default:
                    return;
            }

            EditorGUILayout.Space();
            data.ease = (Ease)EditorGUILayout.EnumPopup("Tween动画模式", data.ease);
            data.loop = (DoTweenAnisLoop)EditorGUILayout.EnumPopup("循环模式", data.loop);
            data.time = EditorGUILayout.FloatField("时长", data.time);
            data.delay = EditorGUILayout.FloatField("延时", data.delay);

            GUILayout.Space(20);
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("保存", GUILayout.Width(150)))
                {
                    current.SetData(data);
                    data.openEditor = false;
                    Init();
                }
                if (GUILayout.Button("清除", GUILayout.Width(150)))
                {
                    current.SetData(null);
                    data = current.GetData();
                    Init();
                }
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(20);
        }
        private void Init ()
        {
            if (EditorApplication.isPlaying) return;
            switch (data.anis)
            {
                case DoTweenAnis.ToMove:
                    current.position = data.startVector3;
                    break;
                case DoTweenAnis.ToLocalMove:
                    current.localPosition = data.startVector3;
                    break;
                case DoTweenAnis.ToEulerAngles:
                    current.eulerAngles = data.startVector3;
                    break;
                case DoTweenAnis.ToLocalEulerAngles:
                    current.localEulerAngles = data.startVector3;
                    break;
                case DoTweenAnis.ToScale:
                    current.localScale = data.startVector3;
                    break;
                case DoTweenAnis.ToUGUIScale:
                    if (current.rectTransform) current.rectTransform.sizeDelta = data.startVector2;
                    break;
                case DoTweenAnis.ToMoveUGUI:
                    if (current.rectTransform) current.rectTransform.anchoredPosition3D = data.startVector3;
                    break;
                case DoTweenAnis.ToUGUIColor:
                    if (current.graphic) current.graphic.color = data.startColor;
                    break;
                case DoTweenAnis.ToAlpha:
                    if (current.graphic) data.startFloat = current.graphic.color.a;
                    if (current.canvasGroup) data.startFloat = current.canvasGroup.alpha;
                    break;
                default:
                    return;
            }
        }
        private void ToVector3()
        {
            if (data.alreadySetStart) data.startVector3 = EditorGUILayout.Vector3Field("起点", data.startVector3);
            if (data.pathVector3 == null) data.pathVector3 = new System.Collections.Generic.List<Vector3>();
            for (int i = 0; i < data.pathVector3.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    string _jname = "节点" + i;
                    if (i == data.pathVector3.Count - 1)
                    {
                        _jname = "终点";
                    }
                    data.pathVector3[i] = EditorGUILayout.Vector3Field(_jname, data.pathVector3[i]);
                    if (GUILayout.Button("删除", GUILayout.Width(100)))
                    {
                        data.pathVector3.RemoveAt(i);
                        i--;
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            currentVector3 = EditorGUILayout.Vector3Field("当前值", currentVector3);

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("设置当前值为起点", GUILayout.Width(150)))
                {
                    data.startVector3 = currentVector3;
                    data.alreadySetStart = true;
                }
                if (GUILayout.Button("记录路径点", GUILayout.Width(150)))
                {
                    data.pathVector3.Add(currentVector3);
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        private void ToVector2()
        {
            if (data.alreadySetStart) data.startVector2 = EditorGUILayout.Vector2Field("起点", data.startVector2);
            if (data.pathVector2 == null) data.pathVector2 = new System.Collections.Generic.List<Vector2>();
            for (int i = 0; i < data.pathVector2.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    string _jname = "节点" + i;
                    if (i == data.pathVector2.Count - 1)
                    {
                        _jname = "终点";
                    }
                    data.pathVector2[i] = EditorGUILayout.Vector2Field(_jname, data.pathVector2[i]);
                    if (GUILayout.Button("删除", GUILayout.Width(100)))
                    {
                        data.pathVector2.RemoveAt(i);
                        i--;
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            currentVector2 = EditorGUILayout.Vector2Field("当前值", currentVector2);

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("设置当前值为起点", GUILayout.Width(150)))
                {
                    data.startVector2 = currentVector2;
                    data.alreadySetStart = true;
                }
                if (GUILayout.Button("记录路径点", GUILayout.Width(150)))
                {
                    data.pathVector2.Add(currentVector2);
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        private void ToNumber()
        {
            if (data.alreadySetStart) data.startFloat = EditorGUILayout.FloatField("起点", data.startFloat);
            if (data.pathNumber == null) data.pathNumber = new System.Collections.Generic.List<float>();
            for (int i = 0; i < data.pathNumber.Count; i++)
            {
                string _jname = "节点" + i;
                if (i == data.pathNumber.Count - 1)
                {
                    _jname = "终点";
                }
                EditorGUILayout.BeginHorizontal();
                {
                    data.pathNumber[i] = EditorGUILayout.FloatField(_jname, data.pathNumber[i]);
                    if (GUILayout.Button("删除", GUILayout.Width(100)))
                    {
                        data.pathNumber.RemoveAt(i);
                        i--;
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            currentFloat = EditorGUILayout.FloatField("当前值", currentFloat);

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("设置当前值为起点", GUILayout.Width(150)))
                {
                    data.startFloat = currentFloat;
                    data.alreadySetStart = true;
                }
                if (GUILayout.Button("记录路径点", GUILayout.Width(150)))
                {
                    data.pathNumber.Add(currentFloat);
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        private void ToColor()
        {
            if (data.alreadySetStart) data.startColor = EditorGUILayout.ColorField("起点", data.startColor);
            if (data.pathColor == null) data.pathColor = new System.Collections.Generic.List<Color>();
            for (int i = 0; i < data.pathColor.Count; i++)
            {
                string _jname = "节点" + i;
                if (i == data.pathColor.Count - 1)
                {
                    _jname = "终点";
                }
                EditorGUILayout.BeginHorizontal();
                {
                    data.pathColor[i] = EditorGUILayout.ColorField(_jname, data.pathColor[i]);
                    if (GUILayout.Button("删除", GUILayout.Width(100)))
                    {
                        data.pathColor.RemoveAt(i);
                        i--;
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            currentColor = EditorGUILayout.ColorField("当前值", currentColor);

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("设置当前值为起点", GUILayout.Width(150)))
                {
                    data.startColor = currentColor;
                    data.alreadySetStart = true;
                }
                if (GUILayout.Button("记录路径点", GUILayout.Width(150)))
                {
                    data.pathColor.Add(currentColor);
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
