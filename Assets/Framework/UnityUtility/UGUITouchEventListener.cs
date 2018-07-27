using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GameFramework
{
    public class UGUITouchEventListener : UGUIEventListener, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            //检查限制UI事件
            if (!ProjectDatas.openUIOperate) return;
            MessageModule.instance.Recevive(eventKeyName, MessageType.EventOnTouchEnter, gameObject);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            //检查限制UI事件
            if (!ProjectDatas.openUIOperate) return;
            MessageModule.instance.Recevive(eventKeyName, MessageType.EventOnTouchExit, gameObject);
        }
    }
}