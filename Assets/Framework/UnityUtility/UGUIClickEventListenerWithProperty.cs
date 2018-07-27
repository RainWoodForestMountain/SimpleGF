using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GameFramework
{
    [System.Serializable]
    struct Propertys
    {
        public string key;
        public string value;

        public Propertys (string _k, string _v)
        {
            key = _k;
            value = _v;
        }
    }
    public class UGUIClickEventListenerWithProperty : UGUIClickEventListener
    {
        [SerializeField]
        private System.Collections.Generic.List<Propertys> list = new System.Collections.Generic.List<Propertys>();

        public void Addproperty (string _key, string _value)
        {
            list.Add(new Propertys(_key, _value));
        }
        public override void OnPointerClick(PointerEventData eventData)
        {
            //检查限制UI事件
            if (!ProjectDatas.openUIOperate) return;
            if (!doubleClick)
            {
                MessageModule.instance.Recevive(eventKeyName, MessageType.EventButtonClick, LitJson.JsonMapper.ToJson(list));
            }
            else if (eventData.clickCount >= 2)
            {
                MessageModule.instance.Recevive(eventKeyName, MessageType.EventButtonDoubleClick, LitJson.JsonMapper.ToJson(list));
            }
        }
    }
}