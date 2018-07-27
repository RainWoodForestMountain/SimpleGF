using UnityEngine;
using UnityEngine.EventSystems;

namespace GameFramework
{
    public class UGUISlidingSelection : UGUIEventListener, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
    {
        private static bool onPointerDown = false;

        public void OnPointerDown(PointerEventData eventData)
        {
            onPointerDown = true;
            MessageModule.instance.Recevive(eventKeyName, MessageType.EventBeginSliding, gameObject);
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            onPointerDown = false;
            MessageModule.instance.Recevive(eventKeyName, MessageType.EventEndSliding, gameObject);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (onPointerDown)
            {
                MessageModule.instance.Recevive(eventKeyName, MessageType.EventOnSliding, gameObject);
            }
        }
    }
}