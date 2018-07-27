using UnityEngine;
using UnityEngine.UI;

namespace GameFramework
{
    class UILayerObject : System.IComparable
    {
        private string name;
        internal int layer { get; private set; }

        internal GameObject gameObject { get; private set; }
        internal Transform rectTransform { get; private set; }
        private float scale;

        private Canvas canvas;
        private CanvasGroup canvasGroup;
        private Transform mask;
        private Transform cache;
        private GameObject lastShow;

        internal UILayerObject(string _name, int _layer, float _scale, Transform _root)
        {
            name = _name;
            layer = _layer;
            scale = _scale;

            CreateGameObject(_root);
        }
        /// <summary>
        /// 创建根
        /// </summary>
        private void CreateGameObject(Transform _root)
        {
            gameObject = GameObject.Instantiate(Resources.Load<GameObject>("base/Layer"), _root);
            rectTransform = gameObject.transform.Find("UI");
            mask = rectTransform.Find("mask");
            canvas = gameObject.GetComponent<Canvas>();
            canvasGroup = gameObject.GetComponent<CanvasGroup>();

            gameObject.name = name;
            //canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.overrideSorting = true;
            canvas.sortingOrder = layer;

            canvasGroup.alpha = 1;

            //两种不同的现实方式，单一显示和重叠显示
            //单一显示会有一个非显示缓存
            //同时单一显示会删掉mask，消息层也会删掉mask
            if (layer <= (int)UILayers.Page || layer == (int)UILayers.Message || layer == (int)UILayers.NoMaskWindow)
            {
                GameObject.Destroy(mask.gameObject);
                cache = new GameObject().transform;
                cache.name = "cache";
                cache.SetParent(gameObject.transform);
                cache.transform.localPosition = Vector3.zero;
                cache.transform.localScale = Vector3.one;
                cache.gameObject.SetActive(false);
            }
        }

        internal bool Equals(string _name)
        {
            return name.Equals(_name);
        }
        internal bool Equals(int _layer)
        {
            return layer == _layer;
        }
        /// <summary>
        /// 改变组的透明度
        /// </summary>
        internal void ChangeAlpha(float _a)
        {
            canvasGroup.alpha = _a;
        }
        //新添加的一律放顶层
        internal void AddChild (GameObject _obj)
        {
            IAutoAnimation _autoAnimation;

            //页面层级以下会自动关闭上一次的显示
            if (lastShow && layer <= (int)UILayers.Page)
            {
                _autoAnimation = lastShow.GetComponent<IAutoAnimation>();
                if (_autoAnimation == null)
                {
                    lastShow.SetActive(false);
                }
                else
                {
                    _autoAnimation.Play(null);
                }
            }

            _obj.transform.SetParent(rectTransform);
            _obj.transform.localPosition = Vector3.zero;
            _obj.transform.localScale = Vector3.one;
            _obj.transform.SetAsLastSibling();
            //适配
            IAdaptation[] _adaptation = UtilityUnity.GetAllComponents<IAdaptation>(_obj);
            if (_adaptation != null)
            {
                for (int i = 0; i < _adaptation.Length; i++)
                {
                    _adaptation[i].scaleType = 0;
                    _adaptation[i].Refresh(UGUILayerSolution.canvasScaler.referenceResolution.x, UGUILayerSolution.canvasScaler.referenceResolution.y);
                }
            }
            //动画
            _autoAnimation = _obj.GetComponent<IAutoAnimation>();
            if (_autoAnimation == null)
            {
                _obj.SetActive(true);
                Refresh();
            }
            else
            {
                _autoAnimation.Play(Refresh);
            }
            lastShow = _obj;
        }
        internal void RemoveChild(GameObject _obj)
        {
            IAutoAnimation _autoAnimation = _obj.GetComponent<IAutoAnimation>();
            if (_autoAnimation == null)
            {
                _obj.SetActive(false);
                Refresh();
            }
            else
            {
                _autoAnimation.Play(Refresh);
            }
        }
        /// <summary>
        /// 刷新操作
        /// </summary>
        internal void Refresh()
        {
            if (layer <= (int)UILayers.Page)
            {
                RefreshSingle();
            }
            else
            {
                RefreshOverlap();
            }
        }
        /// <summary>
        /// 单一显示
        /// </summary>
        private void RefreshSingle ()
        {
            int _cc = rectTransform.childCount;
            Transform _t = null;
            Transform _show = null;
            //没有子物体则不工作
            if (_cc > 0)
            {
                //对所有页面进行核查，如果是关闭状态，压入缓存
                for (int i = _cc - 1; i >= 0; i--)
                {
                    _t = rectTransform.GetChild(i);
                    if (_t.gameObject.activeSelf)
                    {
                        _show = _t;
                    }
                    else
                    {
                        _t.SetParent(cache);
                    }
                }
            }
            //没有界面是处在显示状态的，则从拿出一个界面显示
            if (_show == null && cache.childCount > 0)
            {
                _show = cache.GetChild(0);
                lastShow = null;
                AddChild(_show.gameObject);
            }
            if (_show) _show.SetAsLastSibling();
        }
        /// <summary>
        /// 刷新重叠
        /// </summary>
        private void RefreshOverlap ()
        {
            int _cc = rectTransform.childCount;
            if (_cc == 1)
            {
                if (mask) mask.gameObject.SetActive(false);
                return;
            }

            int _ac = -1;
            for (int i = 0; i < _cc; i++)
            {
                if (mask && rectTransform.GetChild(i).Equals(mask)) continue;
                if (rectTransform.GetChild(i).gameObject.activeSelf) _ac = i;
            }
            if (_ac < 0)
            {
                if (mask) mask.gameObject.SetActive(false);
                return;
            }
            //设置mask为已经显示的子物体的后一位
            if (mask) mask.SetSiblingIndex(_ac == 0 ? _ac : _ac - 1);
            if (mask) mask.gameObject.SetActive(true);
        }
        /// <summary>
        /// 排序比较
        /// </summary>
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            UILayerObject _other = (UILayerObject)obj;
            return this.layer > _other.layer ? 1 : -1;
        }
    }
    internal class UGUILayerSolution : IUILayer
    {
        internal Canvas canvas { get; private set; }
        internal static CanvasScaler canvasScaler { get; private set; }
        private Dictionary<int, UILayerObject> layersDic;

        public void Start()
        {
            layersDic = new Dictionary<int, UILayerObject>();
            CreateRootCanvas();
            CreateBaseLayer();
        }
        public void Destroy()
        {
            Object.Destroy(canvas.gameObject);
            layersDic.Clear();
            layersDic = null;
        }
        /// <summary>
        /// 添加物体到层级
        /// </summary>
        public void AddObjectToLayer(GameObject _obj, int _layer)
        {
            GetOne(_layer).AddChild(_obj);
        }
        public void RemoveObjectFromLayer(GameObject _obj, int _layer)
        {
            GetOne(_layer).RemoveChild(_obj);
        }
        public void ChangeAlpha(float _a, int _layer)
        {
            GetOne(_layer).ChangeAlpha(_a);
        }
        public void Refresh(int _layer = int.MinValue)
        {
            List<UILayerObject> _list = new List<UILayerObject>();
            if (_layer >= 0)
            {
                _list.Add (GetOne(_layer));
            }
            else
            {
                _list = layersDic.ToList();
            }
            for (int i = 0; i < _list.Count; i++)
            {
                if (_list[i] == null) continue;
                _list[i].Refresh();
            }
        }

        private void CreateRootCanvas()
        {
            //这里使用默认资源，因为不会有变化
            GameObject _temp = GameObject.Instantiate(Resources.Load<GameObject>("base/Canvas"));
            GameObject.DontDestroyOnLoad(_temp);
            canvas = _temp.GetComponent<Canvas>();
            canvasScaler = _temp.GetComponent<CanvasScaler>();
        }

        private void CreateBaseLayer()
        {
            System.Array _a = System.Enum.GetValues(typeof(UILayers));
            foreach (UILayers _one in _a)
            {
                CreateOne(_one.ToString(), (int)_one);
            }
        }
        private UILayerObject CreateOne(string _name, int _one)
        {
            //注意，这里如果存在的话，不会再次生成
            if (!layersDic.ContainsKey(_one))
            {
                layersDic.Add(_one, new UILayerObject(_name, _one, canvas.scaleFactor, canvas.transform));
            }
            return layersDic[_one];
        }
        private UILayerObject GetOne(int _layer)
        {
            if (layersDic.ContainsKey(_layer))
            {
                return layersDic[_layer];
            }
            Utility.Log("找不到层级为", _layer, "的层级！将会自动创建名称为Layer", _layer, "的层级！");
            CreateOne(Utility.MergeString("Layer", _layer), _layer);
            //刷新顺序
            Sort();
            return layersDic[_layer];
        }
        private void Sort ()
        {
            List<UILayerObject> _list = layersDic.ToList();
            _list.Sort();
            for (int i = 0; i < _list.Count; i++)
            {
                _list[i].rectTransform.SetAsFirstSibling();
            }
        }
    }
}