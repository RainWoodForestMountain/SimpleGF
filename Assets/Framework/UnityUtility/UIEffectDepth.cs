using UnityEngine;

namespace GameFramework
{
    [ExecuteInEditMode]
    public class UIEffectDepth : MonoBehaviour
    {
        public int order = 0;
        public UILayers relyon = UILayers.Base;
        public GameObject destroyObj = null;

        private void OnEnable() {
            if (destroyObj == null) {
                destroyObj = gameObject;
            }

            Refresh();
        }
        public void Refresh(int _order)
        {
            order = _order;
            Refresh();
        }
        public void DestroySelf()
        {
            Destroy(destroyObj);
        }  
        public void Refresh()
        {
            GameObject[] _gs = UtilityUnity.GetAllChild(this.gameObject);
            Renderer _render = null;
            for (int i = 0; i < _gs.Length; i++)
            {
                _render = _gs[i].GetComponent<Renderer>();
                if (_render)
                {
                    if (relyon != UILayers.Base)
                    {
                        _render.sortingOrder = (int)relyon + order;
                    }
                    else
                    {
                        _render.sortingOrder = order;
                    }
                }
            }
        }
    }
}