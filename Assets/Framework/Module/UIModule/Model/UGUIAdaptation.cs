using UnityEngine;
using UnityEngine.UI;

namespace GameFramework
{
    [ExecuteInEditMode]
    public class UGUIAdaptation : MonoBase, IAdaptation
    {
        [SerializeField]
        private UIAnchor anchor = UIAnchor.Center;
        private RectTransform rect;
        private RectTransform rectTransform
        {
            get
            {
                if (rect == null) rect = GetComponent<RectTransform>();
                return rect;
            }
        }
        /// <summary>
        /// 依赖方式： 0-高依赖 其他-宽依赖
        /// </summary>
        public int scaleType { get; set; }

        private float width = 0;
        private float height = 0;

        private void Update()
        {
            Refresh();
        }
        private void OnEnable()
        {
            Refresh();
        }
        private void OnApplicationFocus()
        {
            Refresh();
        }

        public void Refresh(float _width, float _height)
        {
            width = _width;
            height = _height;
            Refresh();
        }
        private void Refresh()
        {
            if (rectTransform == null) return;
            switch (anchor)
            {
                case UIAnchor.Left:
                    rectTransform.anchorMin = new Vector2(0, 0.5f);
                    rectTransform.anchorMax = new Vector2(0, 0.5f);
                    rectTransform.pivot = new Vector2(0, 0.5f);
                    break;
                case UIAnchor.Right:
                    rectTransform.anchorMin = new Vector2(1, 0.5f);
                    rectTransform.anchorMax = new Vector2(1, 0.5f);
                    rectTransform.pivot = new Vector2(1, 0.5f);
                    break;
                case UIAnchor.Top:
                    rectTransform.anchorMin = new Vector2(0.5f, 1);
                    rectTransform.anchorMax = new Vector2(0.5f, 1);
                    rectTransform.pivot = new Vector2(0.5f, 1);
                    break;
                case UIAnchor.Bottom:
                    rectTransform.anchorMin = new Vector2(0.5f, 0);
                    rectTransform.anchorMax = new Vector2(0.5f, 0);
                    rectTransform.pivot = new Vector2(0.5f, 0);
                    break;
                case UIAnchor.LeftTop:
                    rectTransform.anchorMin = new Vector2(0, 1);
                    rectTransform.anchorMax = new Vector2(0, 1);
                    rectTransform.pivot = new Vector2(0, 1);
                    break;
                case UIAnchor.LeftBottom:
                    rectTransform.anchorMin = new Vector2(0, 0);
                    rectTransform.anchorMax = new Vector2(0, 0);
                    rectTransform.pivot = new Vector2(0, 1);
                    break;
                case UIAnchor.RightTop:
                    rectTransform.anchorMin = new Vector2(1, 1);
                    rectTransform.anchorMax = new Vector2(1, 1);
                    rectTransform.pivot = new Vector2(1, 0);
                    break;
                case UIAnchor.RightBottom:
                    rectTransform.anchorMin = new Vector2(1, 0);
                    rectTransform.anchorMax = new Vector2(1, 0);
                    rectTransform.pivot = new Vector2(1, 0);
                    break;
                case UIAnchor.Center:
                    rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                    rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                    rectTransform.pivot = new Vector2(0.5f, 0.5f);
                    break;
                case UIAnchor.Full:
                    rectTransform.anchorMin = new Vector2(0, 0);
                    rectTransform.anchorMax = new Vector2(1, 1);
                    rectTransform.pivot = new Vector2(0.5f, 0.5f);
                    rectTransform.sizeDelta = Vector3.zero;
                    rectTransform.anchoredPosition = Vector3.zero;
                    rectTransform.localScale = Vector3.one;
                    //平铺不做缩放处理
                    return;
                default:
                    return;
            }
            //保证子物体不会被误偏移，将根物体的UIsize设为0
            rectTransform.sizeDelta = Vector3.zero;
            rectTransform.anchoredPosition = Vector3.zero;

            if (width > 0 && height > 0)
            {
                //高依赖：宽度增加不做缩放处理（高度相对未变），宽度变窄做处理
                if (scaleType == 0)
                {
                    float _lx = height / width;
                    float _current = (float)Screen.height / (float)Screen.width;
                    if (_current > _lx)
                    {
                        //获取理想比例下相对现实宽度需要的高度值
                        float _needHeight = _lx * Screen.width;
                        //计算缩放比例
                        float _needb = _needHeight / Screen.height;

                        rectTransform.localScale = Vector3.one * _needb;
                    }
                    else
                    {
                        rectTransform.localScale = Vector3.one;
                    }
                }
                //宽依赖：高度增加不做缩放处理（宽度相对未变），高度变窄做处理
                else
                {
                    float _lx = width / height;
                    float _current = (float)Screen.width / (float)Screen.height;
                    if (_current > _lx)
                    {
                        //获取理想比例下相对现实高度需要的宽度值
                        float _needWidth = _lx * Screen.height;
                        //计算缩放比例
                        float _needb = _needWidth / Screen.width;

                        rectTransform.localScale = Vector3.one * _needb;
                    }
                    else
                    {
                        rectTransform.localScale = Vector3.one;
                    }
                }
            }
            else
            {
                rectTransform.localScale = Vector3.one;
            }
        }
    }
}
