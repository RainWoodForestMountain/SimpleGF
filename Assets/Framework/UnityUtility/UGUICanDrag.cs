using UnityEngine;
using UnityEngine.EventSystems;

namespace GameFramework
{
    public class UGUICanDrag : UGUIEventListener, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private GameObject lastFather;
        private GameObject tempFather;

        private void Awake()
        {
            if (string.IsNullOrEmpty(eventKeyName))
            {
                eventKeyName = name;
            }
            tempFather = UtilityUnity.FindParentByName(gameObject, "UI");
        }
        /// <summary>
        /// 屏蔽点击事件
        /// </summary>
        public override void OnPointerClick(PointerEventData eventData) { }
        public void OnBeginDrag(PointerEventData eventData)
        {
            MessageModule.instance.Recevive(eventKeyName, MessageType.EventBeginDrag, gameObject);
            lastFather = parent.gameObject;
            UtilityUnity.SetParent(gameObject, tempFather);
        }
        public void OnDrag(PointerEventData eventData)
        {
            position = eventData.position;
            //MessageModule.instance.Recevive(eventKeyName, MessageType.EventDragging, gameObject);
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            UtilityUnity.SetParent(gameObject, lastFather);
            MessageModule.instance.Recevive(eventKeyName, MessageType.EventEndDrag, gameObject);
        }
    }
}