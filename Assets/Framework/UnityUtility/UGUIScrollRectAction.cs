using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameFramework
{
    public class UGUIScrollRectAction : MonoBase, IEndDragHandler
    {
        public ScrollRect target;
        //确定横竖向
        public bool isVertical = true;

        private System.Action topCallBack;
        private System.Action bottomCallBack;

        private bool isOnTop = false;
        private bool isOnBottom = false;
        private Vector2 changed = Vector2.zero;

        public void SetTopCallback(System.Action _cb)
        {
            topCallBack = _cb;
        }
        public void SetBottomCallback(System.Action _cb)
        {
            bottomCallBack = _cb;
        }
        private void Awake()
        {
            if (target == null) target = GetComponent<ScrollRect>();
            target.onValueChanged.AddListener (OnChange);
        }

        private void OnChange (Vector2 _pos)
        {
            changed = _pos;
            Debug.LogError(_pos);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (isVertical) CheckVertical();
            else CheckHorizontal();
        }

        private void CheckVertical()
        {
            //竖直方向，changed的y值 >= 1 为上部， <= 0 为下部
            if (changed.y >= 1 && isOnTop && topCallBack != null)
            {
                topCallBack();
            }
            if (changed.y <= 0 && isOnBottom && bottomCallBack != null)
            {
                bottomCallBack();
            }
        }
        private void CheckHorizontal ()
        {
            //水平方向，，changed的x值 <=0 为左部， >= 1 为右部
            if (changed.x >= 1 && isOnBottom && bottomCallBack != null)
            {
                bottomCallBack();
            }
            if (changed.x <= 0 && isOnTop && topCallBack != null)
            {
                topCallBack();
            }
        }
    }
}