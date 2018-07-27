using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GameFramework
{
    public class UGUIClickEventListener : UGUIEventListener
    {
        public bool doubleClick = false;

        public override void OnPointerClick(PointerEventData eventData)
        {
            //检查限制UI事件
            if (!ProjectDatas.openUIOperate) return;
            if (!doubleClick)
            {
                MessageModule.instance.Recevive(eventKeyName, MessageType.EventButtonClick, gameObject);
            }
            else if (eventData.clickCount >= 2)
            {
                MessageModule.instance.Recevive(eventKeyName, MessageType.EventButtonDoubleClick, gameObject);
            }
        }
    }
}