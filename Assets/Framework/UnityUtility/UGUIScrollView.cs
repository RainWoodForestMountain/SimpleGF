using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GameFramework
{
    public class UGUIScrollView : MonoBehaviour
    {
        private const string MODEL = "model";

        public ScrollRect scorll;
        public RectTransform model;
        public int offset = 200;
        public int length
        {
            get
            {
                return content.childCount;
            }
        }

        private System.Action<GameObject, int> onNeedShow;
        private RectTransform view;
        private RectTransform content;
        private Transform pool;
        private int startIndex = 0;
        private Rect rect;
        private Vector3 basePos = Vector3.zero;

        private void Awake()
        {
            if (!scorll) scorll = GetComponent<ScrollRect>();
            if (scorll)
            {
                scorll.onValueChanged.AddListener(OnDrag);
                view = scorll.viewport;
                content = scorll.content;
                GameObject _temp = new GameObject();
                _temp.name = "pool";
                _temp.SetActive(false);
                _temp.transform.SetParent(gameObject.transform);
                pool = _temp.transform;
            }
        }
        private void OnDrag(Vector2 _pos)
        {
            Refresh();
        }
        private void Refresh()
        {
            //rect = view.rect;
            Vector2 _v2 = scorll.transform.position;
            RectTransform _t = scorll.GetComponent<RectTransform>();
            _v2 = new Vector2(_v2.x - (_t.pivot.x * _t.rect.width), _v2.y - (_t.pivot.y * _t.rect.height));
            rect = new Rect(_v2.x - offset, _v2.y - offset, _t.rect.width + offset * 2, _t.rect.height + offset * 2);
            //basePos = _v2;
            //_temp = new GameObject();
            //_temp.transform.position = basePos;

            RectTransform _rt = null;
            int _cc = content.childCount;
            for (int i = 0; i < _cc; i++)
            {
                _rt = content.GetChild(i).GetComponent<RectTransform>();
                IsInShow(_rt);
            }
        }
        private void IsInShow(RectTransform _child)
        {
            Rect _size = _child.rect;
            Vector2 _p = _child.pivot;
            Vector2 _base = _child.transform.position;
            Vector2 _lt = _base + new Vector2(-_size.width * _p.x, _size.height * _p.y);
            Vector2 _lb = _base + new Vector2(-_size.width * _p.x, -_size.height * _p.y);
            Vector2 _rt = _base + new Vector2(_size.width * _p.x, _size.height * _p.y);
            Vector2 _rb = _base + new Vector2(_size.width * _p.x, -_size.height * _p.y);

            Transform _t = _child.Find(MODEL);

            if (rect.Contains(_lt) || rect.Contains(_lb) || rect.Contains(_rt) || rect.Contains(_rb))
            {
                if (_t) return;
                GameObject _temp = pool.childCount > 0 ? pool.GetChild(0).gameObject : null;
                if (_temp == null) _temp = Instantiate<GameObject>(model.gameObject);
                _temp.name = MODEL;
                _temp.transform.SetParent(_child.transform);
                _temp.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
                _temp.transform.localScale = Vector3.one;
                int _index = int.Parse(_child.name);
                //需要设置数据
                if (onNeedShow != null) onNeedShow(_temp, _index);
            }
            else
            {
                if (!_t) return;
                _t.SetParent(pool);
            }
        }
        public void OnRefresh()
        {
            Invoke("Refresh", Time.deltaTime);
        }
        public void SetStartIndex(int _index)
        {
            if (_index == startIndex) return;

            int _cc = content.childCount;
            for (int i = 0; i < _cc; i++)
            {
                content.GetChild(i).name = (i + _index).ToString();
            }
        }
        public void SetListenter(System.Action<GameObject, int> _cb)
        {
            onNeedShow = _cb;
        }
        public void SetModel(GameObject _item)
        {
            model = _item.GetComponent<RectTransform>();
        }
        public void Clean ()
        {
            while (content.childCount > 0)
            {
                Transform _t = content.GetChild(0);
                _t.SetParent(null);
                if (_t.childCount > 0)
                {
                    Transform _child = _t.Find(MODEL);
                    _child.SetParent(pool);
                }
                Destroy(_t.gameObject);
            }
        }
        public void AddChild(int _count)
        {
            int _cc = content.childCount;
            GameObject _temp = null;
            Graphic _g = null;
            RectTransform _rect;

            if (_count > 0)
            {
                for (int i = 0; i < _count; i++)
                {
                    _temp = new GameObject();
                    _temp.name = (_cc + startIndex + i).ToString();
                    _g = _temp.AddComponent<Image>();
                    _g.color = Color.clear;
                    _g.raycastTarget = true;
                    _rect = _g.GetComponent<RectTransform>();
                    _rect.sizeDelta = model.sizeDelta;
                    _temp.transform.SetParent(content.transform);
                    _temp.transform.localScale = Vector3.one;
                }
                OnRefresh();
                return;
            }

            if (_count < 0)
            {
                _count = Mathf.Abs(_count);
                while (_count > 0)
                {
                    Transform _t = content.GetChild(_count - 1);
                    if (_t.childCount > 0)
                    {
                        _t.SetParent(pool);
                    }
                    Destroy(_t.gameObject);
                    _count--;
                }
                OnRefresh();
            }
        }
    }
}