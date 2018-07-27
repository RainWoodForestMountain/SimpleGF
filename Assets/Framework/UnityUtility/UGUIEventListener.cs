using UnityEngine;
using UnityEngine.EventSystems;

namespace GameFramework
{
    public class UGUIEventListener : MonoBase, IPointerClickHandler
    {
        public string eventKeyName;
        private System.Action<GameObject> acllback;
        private bool limit = true;

        private void Awake()
        {
            if (string.IsNullOrEmpty(eventKeyName))
            {
                eventKeyName = name;
            }
        }
        public void AddClickCallback (System.Action<GameObject> _acllback, bool _limit = true)
        {
            limit = _limit;
            acllback = _acllback;
        }
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (limit && !ProjectDatas.openUIOperate) return;
            if (acllback != null) acllback(gameObject);
        }
    }
}