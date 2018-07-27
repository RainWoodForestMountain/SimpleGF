using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace GameFramework
{
    public class BatchDisableRaycastTarget : Editor
    {
        [MenuItem("工具/操作/去掉选定UGUI控件的事件监测（raycast target） &C")]
        private static void OnBatchDisableRaycastTarget()
        {
            Object[] _objects = Selection.GetFiltered(typeof(GameObject), SelectionMode.DeepAssets);
            for (int i = 0; i < _objects.Length; i++)
            {
                GameObject _go = _objects[i] as GameObject;
                FindChildAndDo(_go.transform);
            }
        }
        private static void FindChildAndDo (Transform _root)
        {
            int _cc = _root.childCount;
            for (int i = 0; i < _cc; i++)
            {
                Graphic _g = _root.GetChild(i).GetComponent<Graphic>();
                if (_g != null)
                {
                    _g.raycastTarget = false;
                }
                FindChildAndDo(_root.GetChild(i));
            }
        }
    }


}