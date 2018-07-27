using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

namespace GameFramework
{
    public class DoTweenAnimation : MonoBase
    {
        private Tween tween = null;
        [SerializeField]
        private DoTweenAnimationData data = null;
        private System.Action callback;

        public float length
        {
            get
            {
                return data.time;
            }
        }
        public float lengthWithDelay
        {
            get
            {
                return data.time + data.delay;
            }
        }

        public Image image
        {
            get { return GetComponent<Image>(); }
        }
        public Text text
        {
            get { return GetComponent<Text>(); }
        }
        public Graphic graphic
        {
            get { return GetComponent<Graphic>(); }
        }
        public RectTransform rectTransform
        {
            get { return GetComponent<RectTransform>(); }
        }
        public CanvasGroup canvasGroup
        {
            get { return GetComponent<CanvasGroup>(); }
        }

        private void CreateTween ()
        {
            if (data != null)
            {
                tween = data.GetTween(this);
                tween.OnComplete(OnComponent);
            }
        }
        private void OnEnable()
        {
            if (data != null && data.time > 0)
            {
                CreateTween();
                if (data.autoPlayer) Play();
                else Pause();
            }
        }
        private void OnDisable()
        {
            Stop();
        }
        private void OnComponent ()
        {
            if (callback != null)
            {
                callback();
            }
            if (data.autoDestroy)
            {
                Destroy(gameObject);
            }
            else
            {
                if (data.autoDisable) enabled = false;
                if (data.autoClose) gameObject.SetActive(false);
            }
        }
        public void Clear()
        {
            Destroy(this);
        }
        public void SetCallback (System.Action _ac)
        {
            if (_ac == null) return;
            if (tween == null) CreateTween();
            if (tween == null) return;
            tween.OnComplete(OnComponent);
            callback = _ac;
        }
        public void Play ()
        {
            if (tween == null) CreateTween();
            if (tween == null) return;
            tween.PlayForward();
        }
        public void Stop()
        {
            if (tween == null) return;
            tween.Kill();
            tween = null;
        }
        public void Pause()
        {
            if (tween == null) return;
            tween.Pause();
        }
        public void ReStart(bool _includeDelay)
        {
            if (tween == null) return;
            tween.Restart(_includeDelay);
        }
        public DoTweenAnimation SetTween (Tween _tween)
        {
            tween = _tween;
            return this;
        }
        
        public DoTweenAnimationData GetData ()
        {
            if (data == null) data = new DoTweenAnimationData();
            return data;
        }
        public void SetData (DoTweenAnimationData _data)
        {
            data = _data;
        }

        public static DoTweenAnimation ToLocalRotation(GameObject _target, Vector3 _end, float _time, Ease _ease = Ease.Linear)
        {
            if (Utility.isNull(_target)) return null;
            Tween _tween = DOTween.To(() => _target.transform.localRotation, (_pos) => _target.transform.localRotation = _pos, _end, _time).SetEase(_ease);
            return _target.AddComponent<DoTweenAnimation>().SetTween(_tween);
        }
        public static DoTweenAnimation ToRotation(GameObject _target, Vector3 _end, float _time, Ease _ease = Ease.Linear)
        {
            if (Utility.isNull(_target)) return null;
            Tween _tween = DOTween.To(() => _target.transform.rotation, (_pos) => _target.transform.rotation = _pos, _end, _time).SetEase(_ease);
            return _target.AddComponent<DoTweenAnimation>().SetTween(_tween);
        }
        public static DoTweenAnimation ToLocalEulerAngles(GameObject _target, Vector3 _end, float _time, Ease _ease = Ease.Linear)
        {
            if (Utility.isNull(_target)) return null;
            Tween _tween = DOTween.To(() => _target.transform.localEulerAngles, (_pos) => _target.transform.localEulerAngles = _pos, _end, _time).SetEase(_ease);
            return _target.AddComponent<DoTweenAnimation>().SetTween(_tween);
        }
        public static DoTweenAnimation ToEulerAngles(GameObject _target, Vector3 _end, float _time, Ease _ease = Ease.Linear)
        {
            if (Utility.isNull(_target)) return null;
            Tween _tween = DOTween.To(() => _target.transform.eulerAngles, (_pos) => _target.transform.eulerAngles = _pos, _end, _time).SetEase(_ease);
            return _target.AddComponent<DoTweenAnimation>().SetTween(_tween);
        }
        public static DoTweenAnimation ToScale(GameObject _target, Vector3 _end, float _time, Ease _ease = Ease.Linear)
        {
            if (Utility.isNull(_target)) return null;
            Tween _tween = DOTween.To(() => _target.transform.localScale, (_pos) => _target.transform.localScale = _pos, _end, _time).SetEase(_ease);
            return _target.AddComponent<DoTweenAnimation>().SetTween(_tween);
        }
        public static DoTweenAnimation ToUGUIScale(GameObject _target, Vector2 _end, float _time, Ease _ease = Ease.Linear)
        {
            if (Utility.isNull(_target)) return null;
            RectTransform _rect = _target.GetComponent<RectTransform>();
            if (Utility.isNull(_rect)) return null;
            Tween _tween = DOTween.To(() => _rect.sizeDelta, (_p) => _rect.sizeDelta = _p, _end, _time).SetEase(_ease);
            return _target.AddComponent<DoTweenAnimation>().SetTween(_tween);
        }
        public static DoTweenAnimation ToMove (GameObject _target, Vector3 _end, float _time, Ease _ease = Ease.Linear)
        {
            if (Utility.isNull(_target)) return null;
            Tween _tween = DOTween.To(()=> _target.transform.position, (_pos)=> _target.transform.position = _pos, _end, _time).SetEase(_ease);
            return _target.AddComponent<DoTweenAnimation>().SetTween(_tween);
        }
        public static DoTweenAnimation ToMoveLocal(GameObject _target, Vector3 _end, float _time, Ease _ease = Ease.Linear)
        {
            if (Utility.isNull(_target)) return null;
            Tween _tween = DOTween.To(() => _target.transform.localPosition, (_pos) => _target.transform.localPosition = _pos, _end, _time).SetEase(_ease);
            return _target.AddComponent<DoTweenAnimation>().SetTween(_tween);
        }
        public static DoTweenAnimation ToMoveUGUI (GameObject _target, Vector2 _end, float _time, Ease _ease = Ease.Linear)
        {
            if (Utility.isNull(_target)) return null;
            RectTransform _rect = _target.GetComponent<RectTransform>();
            if (Utility.isNull(_rect)) return null;
            Tween _tween = DOTween.To(() => _rect.anchoredPosition, (_pos) => _rect.anchoredPosition = _pos, _end, _time).SetEase(_ease);
            return _target.AddComponent<DoTweenAnimation>().SetTween(_tween);
        }
        public static DoTweenAnimation ToUGUIShowInt(GameObject _target, int _start, int _end, float _time, Ease _ease = Ease.Linear)
        {
            if (Utility.isNull(_target)) return null;
            Text _uiShow = _target.GetComponent<Text>();
            if (Utility.isNull(_uiShow)) return null;
            _uiShow.text = _start.ToString();
            Tween _tween = DOTween.To(() => int.Parse(_uiShow.text), (_c) => _uiShow.text = _c.ToString(), _end, _time).SetEase(_ease);
            return _target.AddComponent<DoTweenAnimation>().SetTween(_tween);
        }
        public static DoTweenAnimation ToUGUIShowNumber(GameObject _target, float _start, float _end, float _time, int _showLimit, Ease _ease = Ease.Linear)
        {
            if (Utility.isNull(_target)) return null;
            Text _uiShow = _target.GetComponent<Text>();
            if (Utility.isNull(_uiShow)) return null;
            _uiShow.text = _start.ToString();
            Tween _tween = DOTween.To(() => int.Parse(_uiShow.text), (_c) => _uiShow.text = _c.ToString(Utility.MergeString("F", _showLimit)), _end, _time).SetEase(_ease);
            return _target.AddComponent<DoTweenAnimation>().SetTween(_tween);
        }
        public static DoTweenAnimation ToUGUIColor(GameObject _target, Color _start, Color _end, float _time, Ease _ease = Ease.Linear)
        {
            if (Utility.isNull(_target)) return null;
            Graphic _uiShow = _target.GetComponent<Graphic>();
            if (Utility.isNull(_uiShow)) return null;
            _uiShow.color = _start;
            Tween _tween = DOTween.To(() => _uiShow.color, (_c) => _uiShow.color = _c, _end, _time).SetEase(_ease);
            return _target.AddComponent<DoTweenAnimation>().SetTween(_tween);
        }
        public static DoTweenAnimation ToFilledImage (GameObject _target, float _start, float _end, float _time, Ease _ease = Ease.Linear)
        {
            if (Utility.isNull(_target)) return null;
            Image _image = _target.GetComponent<Image>();
            if (Utility.isNull(_image)) return null;
            Tween _tween = DOTween.To(() => _image.fillAmount, (_c) => _image.fillAmount = _c, _end, _time).SetEase(_ease);
            return _target.AddComponent<DoTweenAnimation>().SetTween(_tween);
        }
        public static DoTweenAnimation ToAlpha(GameObject _target, float _start, float _end, float _time, Ease _ease = Ease.Linear)
        {
            if (Utility.isNull(_target)) return null;
            _start = Mathf.Clamp01(_start);
            _end = Mathf.Clamp01(_end);
            CanvasGroup _cg = _target.GetComponent<CanvasGroup>();
            if (_cg == null)
            {
                Graphic _uiShow = _target.GetComponent<Graphic>();
                if (_uiShow == null) return null;
                else
                {
                    Color _cs = new Color (_uiShow.color.r, _uiShow.color.g, _uiShow.color.b, _start);
                    Color _ce = new Color(_uiShow.color.r, _uiShow.color.g, _uiShow.color.b, _end);
                    return ToUGUIColor(_target, _cs, _ce, _time, _ease);
                }
            }
            else
            {
                _cg.alpha = _start;
                Tween _tween = DOTween.To(() => _cg.alpha, (_c) => _cg.alpha = _c, _end, _time).SetEase(_ease);
                return _target.AddComponent<DoTweenAnimation>().SetTween(_tween);
            }
        }
    }
}