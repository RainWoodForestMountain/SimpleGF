using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GameFramework
{
    public static class UtilityUnity
    {
        public static void ActiviteGameObject(Transform _g, bool _o)
        {
            if (Utility.isNull(_g)) return;
            _g.gameObject.SetActive(_o);
        }
        public static void ActiviteGameObject (GameObject _g, bool _o)
        {
            if (Utility.isNull(_g)) return;
            _g.SetActive(_o);
        }
        public static void DestroyGameObject (GameObject _g)
        {
            if (Utility.isNull(_g)) return;
            _g.transform.SetParent(null);
            Object.Destroy(_g);
        }
        public static void DestroyComponent(Component _g)
        {
            if (Utility.isNull(_g)) return;
            Object.Destroy(_g);
        }
        public static void DestroyAllChilds(GameObject _g)
        {
            if (Utility.isNull(_g)) return;
            Transform _t = _g.transform;
            while (_t.childCount > 0)
            {
                Transform _c = _t.GetChild(0);
                _c.SetParent(null);
                DestroyGameObject(_c.gameObject);
            }
        }
        public static void HideAllChilds(GameObject _g)
        {
            if (Utility.isNull(_g)) return;
            Transform _t = _g.transform;
            int _cc = _t.childCount;
            for (int i = 0; i < _cc; i++)
            {
                _t.GetChild(i).gameObject.SetActive(false);
            }
        }
        public static void DestroyChildsExceptNames(GameObject _g, params string[] _names)
        {
            if (Utility.isNull(_g)) return;
            Transform _t = _g.transform;
            int _cc = _t.childCount;
            List<string> _n = new List<string>();
            _n.AddRange(_names);
            for (int i = 0; i < _cc; i++)
            {
                Transform _c = _t.GetChild(i);
                if (_n.IndexOf(_c.name) >= 0) continue;
                _c.SetParent(null);
                DestroyGameObject(_c.gameObject);
            }
        }
        public static GameObject FindChild (GameObject _go, string _name)
        {
            if (Utility.isNull(_go)) return null;
            Transform _t = _go.transform.Find(_name);
            if (Utility.isNull(_t)) return null;
            return _t.gameObject;
        }
        public static void SetParent(GameObject _child, GameObject _parent)
        {
            if (Utility.isNull(_child)) return;
            _child.transform.SetParent (_parent == null ? null : _parent.transform);
            _child.transform.localPosition = Vector3.zero;
            _child.transform.localScale = Vector3.one;
        }
        /// <summary>
        /// 使用该种方式添加的点击，不会广播事件。
        /// 同时这种监听方式可以设置为不受UI按钮限制的控制，可以作为新手引导的按钮点击方式。
        /// </summary>
        public static void AddButtonClick (GameObject _obj, System.Action<GameObject> _cb, bool _nolimit = false)
        {
            if (Utility.isNull(_obj)) return;
            UGUIEventListener _lis = _obj.GetComponent<UGUIEventListener>();
            if (_lis == null)
            {
                _lis = _obj.AddComponent<UGUIEventListener>();
            }
            else
            {
                if (_lis.GetType() != typeof(UGUIEventListener))
                {
                    Utility.LogError("已经有其他更高层的监听器，无法继续添加底层监听器");
                    return;
                }
            }
            _lis.AddClickCallback(_cb, !_nolimit);
        }
        public static void OperateRaycastTarget (GameObject _obj, bool _op)
        {
            if (Utility.isNull(_obj)) return;
            Graphic _g = _obj.GetComponent<Graphic>();
            _g.raycastTarget = _op;
        }
        /// <summary>
        /// 根据名称查找父级，会一直查找下去，直到没有父级或者找到
        /// </summary>
        public static GameObject FindParentByName (GameObject _obj, string _name)
        {
            if (Utility.isNull(_obj)) return null;
            GameObject _f = null;
            if (string.IsNullOrEmpty (_name))
            {
                Utility.LogError("无法完成查找，请指定父级名称");
                return _f;
            }
            if (_obj.transform.parent != null)
            {
                _f = _obj.transform.parent.gameObject;
                if (_f.name.Equals(_name)) return _f;
                else return FindParentByName(_f, _name);
            }
            else return _f;
        }
        public static T GetOrAddComponent<T> (GameObject _obj) where T : Component
        {
            if (Utility.isNull(_obj)) return null;
            T _t = _obj.GetComponent<T>();
            if (!Utility.isNull(_t)) return _t;
            return _obj.AddComponent<T>();
        }
        public static Component GetOrAddComponent(GameObject _obj, string _type)
        {
            if (Utility.isNull(_obj)) return null;
            Component _t = _obj.GetComponent(System.Type.GetType(_type));
            if (!Utility.isNull(_t)) return _t;
            return _obj.AddComponent(System.Type.GetType(_type));
        }
        public static GameObject[] GetAllChild(GameObject _obj)
        {
            List<GameObject> _list = new List<GameObject>();
            if (Utility.isNull(_obj)) return _list.ToArray();
            int _cc = _obj.transform.childCount;
            for (int i = 0; i < _cc; i++)
            {
                _list.AddRange(GetAllChild(_obj.transform.GetChild(i).gameObject));
            }
            _list.Add(_obj);
            return _list.ToArray();
        }
        public static T[] GetAllComponents<T>(GameObject _obj)
        {
            List<T> _list = new List<T>();
            if (Utility.isNull(_obj)) return _list.ToArray();
            int _cc = _obj.transform.childCount;
            for (int i = 0; i < _cc; i++)
            {
                _list.AddRange(GetAllComponents<T>(_obj.transform.GetChild(i).gameObject));
            }
            T _t = _obj.GetComponent<T>();
            if (_t != null)
            {
                _list.Add(_t);
            }
            return _list.ToArray();
        }

        public static void SetText (GameObject _obj, string _v)
        {
            if (Utility.isNull(_obj)) return;
            Text _t = _obj.GetComponent<Text>();
            if (_t != null)
            {
                _t.text = _v;
                return;
            }
            InputField _i = _obj.GetComponent<InputField>();
            if (_i != null)
            {
                _i.text = _v;
                return;
            }

        }
        public static void SetFont(GameObject _obj, Font _v)
        {
            if (Utility.isNull(_obj)) return;
            Text _t = _obj.GetComponent<Text>();
            if (Utility.isNull(_t)) return;
            _t.font = _v;
        }
        public static void SetSprite(GameObject _obj, Sprite _v)
        {
            if (Utility.isNull(_obj)) return;
            Image _t = _obj.GetComponent<Image>();
            if (_t)
            {
                _t.sprite = _v;
            }
            else
            {
                SpriteRenderer _sr = _obj.GetComponent<SpriteRenderer>();
                if (_sr)
                {
                    _sr.sprite = _v;
                }
                else
                {
                    _t = _obj.AddComponent<Image>();
                    _t.sprite = _v;
                }
            }
        }
        public static void SetMaterial(GameObject _obj, Material _v)
        {
            if (Utility.isNull(_obj)) return;
            Graphic _t = _obj.GetComponent<Graphic>();
            if (Utility.isNull(_t)) return;
            _t.material = _v;
        }
        public static void SetSpriteByUrl(GameObject _obj, string _url, int _heigth, int _width, bool _force)
        {
            DownloadNetResource.LoadedSprite(_obj, _url, _heigth, _width, _force);
        }
        public static void SetColor(GameObject _obj, Color _v)
        {
            if (Utility.isNull(_obj)) return;
            Graphic _t = _obj.GetComponent<Graphic>();
            if (_t)
            {
                _t.color = _v;
            }
            else
            {
                SpriteRenderer _sr = _obj.GetComponent<SpriteRenderer>();
                if (_sr)
                {
                    _sr.color = _v;
                }
            }
        }
        public static void SetAlpha(GameObject _obj, float _v)
        {
            if (Utility.isNull(_obj)) return;
            Graphic _t = _obj.GetComponent<Graphic>();
            if (_t != null)
            {
                _t.color = new Color(_t.color.r, _t.color.g, _t.color.b, _v);
                return;
            }
            CanvasGroup _cg = _obj.GetComponent<CanvasGroup>();
            if (_cg != null)
            {
                _cg.alpha = _v;
            }
        }
        public static void SetPosition(GameObject _obj, Vector3 _v)
        {
            if (Utility.isNull(_obj)) return;
            _obj.transform.position = _v;
        }
        public static void SetLocalPosition(GameObject _obj, Vector3 _v)
        {
            if (Utility.isNull(_obj)) return;
            _obj.transform.localPosition = _v;
        }
        public static void SetEulerAngles(GameObject _obj, Vector3 _v)
        {
            if (Utility.isNull(_obj)) return;
            _obj.transform.eulerAngles = _v;
        }
        public static void SetLocalEulerAngles(GameObject _obj, Vector3 _v)
        {
            if (Utility.isNull(_obj)) return;
            _obj.transform.localEulerAngles = _v;
        }
        public static void SetScale(GameObject _obj, Vector3 _v)
        {
            if (Utility.isNull(_obj)) return;
            _obj.transform.localScale = _v;
        }
        public static void SetUIPosition(GameObject _obj, Vector2 _v)
        {
            SetUIPosition(_obj, (Vector3)_v);
        }
        public static void SetUIPosition(GameObject _obj, Vector3 _v)
        {
            if (Utility.isNull(_obj)) return;
            RectTransform _rect = _obj.GetComponent<RectTransform>();
            _rect.anchoredPosition3D = _v;
        }
        public static void SetUIScale(GameObject _obj, Vector2 _v)
        {
            if (Utility.isNull(_obj)) return;
            RectTransform _rect = _obj.GetComponent<RectTransform>();
            _rect.sizeDelta = _v;
        }
        public static void SetUIAnchorMin(GameObject _obj, Vector2 _v)
        {
            if (Utility.isNull(_obj)) return;
            RectTransform _rect = _obj.GetComponent<RectTransform>();
            _rect.anchorMin = _v;
        }
        public static void SetUIAnchorMax(GameObject _obj, Vector2 _v)
        {
            if (Utility.isNull(_obj)) return;
            RectTransform _rect = _obj.GetComponent<RectTransform>();
            _rect.anchorMax = _v;
        }
        public static void SetUIPivot(GameObject _obj, Vector2 _v)
        {
            if (Utility.isNull(_obj)) return;
            RectTransform _rect = _obj.GetComponent<RectTransform>();
            _rect.pivot = _v;
        }
        public static void SetNativeSize(GameObject _obj)
        {
            if (Utility.isNull(_obj)) return;
            Graphic _rect = _obj.GetComponent<Graphic>();
            if (_rect) _rect.SetNativeSize();
        }


        public static DoTweenAnimation ToLocalRotation(GameObject _target, Vector3 _end, float _time, Ease _ease = Ease.Linear)
        {
            return DoTweenAnimation.ToLocalRotation(_target, _end, _time, _ease);
        }
        public static DoTweenAnimation ToRotation(GameObject _target, Vector3 _end, float _time, Ease _ease = Ease.Linear)
        {
            return DoTweenAnimation.ToRotation(_target, _end, _time, _ease);
        }
        public static DoTweenAnimation ToLocalEulerAngles(GameObject _target, Vector3 _end, float _time, Ease _ease = Ease.Linear)
        {
            return DoTweenAnimation.ToLocalEulerAngles(_target, _end, _time, _ease);
        }
        public static DoTweenAnimation ToEulerAngles(GameObject _target, Vector3 _end, float _time, Ease _ease = Ease.Linear)
        {
            return DoTweenAnimation.ToEulerAngles(_target, _end, _time, _ease);
        }
        public static DoTweenAnimation ToScale(GameObject _target, Vector3 _end, float _time, Ease _ease = Ease.Linear)
        {
            return DoTweenAnimation.ToScale(_target, _end, _time, _ease);
        }
        public static DoTweenAnimation ToUGUIScale(GameObject _target, Vector2 _end, float _time, Ease _ease = Ease.Linear)
        {
            return DoTweenAnimation.ToUGUIScale(_target, _end, _time, _ease);
        }
        public static DoTweenAnimation ToMove(GameObject _target, Vector3 _end, float _time, Ease _ease = Ease.Linear)
        {
            return DoTweenAnimation.ToMove(_target, _end, _time, _ease);
        }
        public static DoTweenAnimation ToMoveLocal(GameObject _target, Vector3 _end, float _time, Ease _ease = Ease.Linear)
        {
            return DoTweenAnimation.ToMoveLocal(_target, _end, _time, _ease);
        }
        public static DoTweenAnimation ToMoveUGUI(GameObject _target, Vector2 _end, float _time, Ease _ease = Ease.Linear)
        {
            return DoTweenAnimation.ToMoveUGUI(_target, _end, _time, _ease);
        }
        public static DoTweenAnimation ToUGUIShowInt(GameObject _target, int _start, int _end, float _time, Ease _ease = Ease.Linear)
        {
            return DoTweenAnimation.ToUGUIShowInt(_target, _start, _end, _time, _ease);
        }
        public static DoTweenAnimation ToUGUIShowNumber(GameObject _target, float _start, float _end, float _time, int _showLimit, Ease _ease = Ease.Linear)
        {
            return DoTweenAnimation.ToUGUIShowNumber(_target, _start, _end, _time, _showLimit, _ease);
        }
        public static DoTweenAnimation ToUGUIColor(GameObject _target, Color _start, Color _end, float _time, Ease _ease = Ease.Linear)
        {
            return DoTweenAnimation.ToUGUIColor(_target, _start, _end, _time, _ease);
        }
        public static DoTweenAnimation ToAlpha(GameObject _target, float _start, float _end, float _time, Ease _ease = Ease.Linear)
        {
            return DoTweenAnimation.ToAlpha(_target, _start, _end, _time, _ease);
        }
    }
}