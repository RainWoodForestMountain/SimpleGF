using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GameFramework
{
    public class UGUIPressEventListener : MonoBase, IPointerDownHandler, IPointerUpHandler
    {
        public string eventKeyName;

        private void Awake()
        {
            if (string.IsNullOrEmpty(eventKeyName))
            {
                eventKeyName = name;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!ProjectDatas.openUIOperate) return;
            MessageModule.instance.Recevive(eventKeyName, MessageType.EventPressStart, gameObject);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!ProjectDatas.openUIOperate) return;
            MessageModule.instance.Recevive(eventKeyName, MessageType.EventPressEnd, gameObject);
        }
    }
}