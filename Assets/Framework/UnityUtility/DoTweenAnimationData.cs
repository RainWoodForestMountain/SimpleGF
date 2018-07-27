using UnityEngine;

using DG.Tweening;

namespace GameFramework
{
    [System.Serializable]
    public class DoTweenAnimationData
    {
        public DoTweenAnis anis = DoTweenAnis.No;
        public DoTweenAnisLoop loop = DoTweenAnisLoop.No;

        public bool openEditor = false;
        public bool autoPlayer = false;
        public bool alreadySetStart = false;
        public bool alreadySetEnd = false;
        public bool autoDestroy = false;
        public bool autoDisable = true;
        public bool autoClose = false;

        public Vector3 startVector3;
        public System.Collections.Generic.List<Vector3> pathVector3;
        public Vector2 startVector2;
        public System.Collections.Generic.List<Vector2> pathVector2;
        public float startFloat;
        public System.Collections.Generic.List<float> pathNumber;
        public Color startColor;
        public System.Collections.Generic.List<Color> pathColor;

        public Ease ease = Ease.Linear;
        public float delay;
        public float time;
        public int limit;

        public Tween GetTween (DoTweenAnimation _animation)
        {
            Sequence _sequence = null;
            float _oneTime = 0;
            if (_animation == null) return _sequence;
            switch (anis)
            {
                case DoTweenAnis.ToMove:
                    _animation.transform.position = startVector3;
                    _sequence = DOTween.Sequence();
                    _sequence.Append(_animation.transform.DOPath(pathVector3.ToArray(), time, PathType.CatmullRom).SetEase(ease));
                    break;
                case DoTweenAnis.ToLocalMove:
                    _animation.transform.localPosition = startVector3;
                    _sequence = DOTween.Sequence();
                    _oneTime = time / pathVector3.Count;
                    _sequence.Append(_animation.transform.DOLocalPath(pathVector3.ToArray(), time, PathType.CatmullRom).SetEase(ease));
                    break;
                case DoTweenAnis.ToEulerAngles:
                    _animation.transform.eulerAngles = startVector3;
                    _sequence = DOTween.Sequence();
                    _oneTime = time / pathVector3.Count;
                    for (int i = 0; i < pathVector3.Count; i++)
                    {
                        _sequence.Append(_animation.transform.DORotate(pathVector3[i], _oneTime).SetEase(ease));
                    }
                    break;
                case DoTweenAnis.ToLocalEulerAngles:
                    _animation.transform.localEulerAngles = startVector3;
                    _sequence = DOTween.Sequence();
                    _oneTime = time / pathVector3.Count;
                    for (int i = 0; i < pathVector3.Count; i++)
                    {
                        _sequence.Append(_animation.transform.DOLocalRotate(pathVector3[i], _oneTime).SetEase(ease));
                    }
                    break;
                case DoTweenAnis.ToScale:
                    _animation.transform.localScale = startVector3;
                    _sequence = DOTween.Sequence();
                    _oneTime = time / pathVector3.Count;
                    for (int i = 0; i < pathVector3.Count; i++)
                    {
                        _sequence.Append(_animation.transform.DOScale(pathVector3[i], _oneTime).SetEase(ease));
                    }
                    break;
                case DoTweenAnis.ToUGUIScale:
                    if (_animation.rectTransform)
                    {
                        _animation.rectTransform.sizeDelta = startVector2;
                        _sequence = DOTween.Sequence();
                        _oneTime = time / pathVector2.Count;
                        for (int i = 0; i < pathVector2.Count; i++)
                        {
                            _sequence.Append(DOTween.To(() => _animation.rectTransform.sizeDelta, (_v) => _animation.rectTransform.sizeDelta = _v, pathVector2[i], _oneTime).SetEase(ease));
                        }
                    }
                    break;
                case DoTweenAnis.ToMoveUGUI:
                    if (_animation.rectTransform)
                    {
                        _animation.rectTransform.anchoredPosition3D = startVector3;
                        _sequence = DOTween.Sequence();
                        _oneTime = time / pathVector3.Count;
                        for (int i = 0; i < pathVector3.Count; i++)
                        {
                            _sequence.Append(DOTween.To(()=>_animation.rectTransform.anchoredPosition3D, (_v)=> _animation.rectTransform.anchoredPosition3D = _v, pathVector3[i], _oneTime).SetEase(ease));
                        }
                    }
                    break;
                case DoTweenAnis.ToUGUIShowInt:
                    if (_animation.text)
                    {
                        _animation.text.text = startFloat.ToString();
                        _sequence = DOTween.Sequence();
                        _oneTime = time / pathNumber.Count;
                        for (int i = 0; i < pathNumber.Count; i++)
                        {
                            _sequence.Append(DOTween.To(() => int.Parse(_animation.text.text), (_v) => _animation.text.text = ((int)_v).ToString(), pathNumber[i], _oneTime).SetEase(ease));
                        }
                    }
                    break;
                case DoTweenAnis.ToUGUIShowNumber:
                    if (_animation.text)
                    {
                        _animation.text.text = startFloat.ToString("F" + limit);
                        _sequence = DOTween.Sequence();
                        _oneTime = time / pathNumber.Count;
                        for (int i = 0; i < pathNumber.Count; i++)
                        {
                            _sequence.Append(DOTween.To(() => float.Parse(_animation.text.text), (_v) => _animation.text.text = _v.ToString("F" + limit), pathNumber[i], _oneTime).SetEase(ease));
                        }
                    }
                    break;
                case DoTweenAnis.ToUGUIColor:
                    if (_animation.graphic)
                    {
                        Color _c = _animation.graphic.color;
                        _animation.graphic.color = startColor;
                        _sequence = DOTween.Sequence();
                        _oneTime = time / pathColor.Count;
                        for (int i = 0; i < pathColor.Count; i++)
                        {
                            _sequence.Append(DOTween.To(() => _animation.graphic.color, (_v) => _animation.graphic.color = _v, pathColor[i], _oneTime).SetEase(ease));
                        }
                    }
                    break;
                case DoTweenAnis.ToFilledImage:
                    if (_animation.image)
                    {
                        _animation.image.fillAmount = startFloat;
                        _sequence = DOTween.Sequence();
                        _oneTime = time / pathNumber.Count;
                        for (int i = 0; i < pathNumber.Count; i++)
                        {
                            _sequence.Append(DOTween.To(() => _animation.image.fillAmount, (_v) => _animation.image.fillAmount = _v, pathNumber[i], _oneTime).SetEase(ease));
                        }
                    }
                    break;
                case DoTweenAnis.ToAlpha:
                    if (_animation.graphic)
                    {
                        Color _c = _animation.graphic.color;
                        _animation.graphic.color = new Color(_c.r, _c.g, _c.b, startFloat);
                        _sequence = DOTween.Sequence();
                        _oneTime = time / pathNumber.Count;
                        for (int i = 0; i < pathNumber.Count; i++)
                        {
                            _sequence.Append(DOTween.To(() => _animation.graphic.color, (_v) => _animation.graphic.color = _v, new Color(_c.r, _c.g, _c.b, pathNumber[i]), _oneTime).SetEase(ease));
                        }
                    }
                    if (_animation.canvasGroup)
                    {
                        _animation.canvasGroup.alpha = startFloat;
                        _sequence = DOTween.Sequence();
                        _oneTime = time / pathNumber.Count;
                        for (int i = 0; i < pathNumber.Count; i++)
                        {
                            _sequence.Append(DOTween.To(() => _animation.canvasGroup.alpha, (_v) => _animation.canvasGroup.alpha = _v, pathNumber[i], _oneTime).SetEase(ease));
                        }
                    }
                    break;
                default:
                    break;
            }
            
            if (_sequence != null)
            {
                _sequence.SetEase(ease);
                _sequence.SetDelay(delay);
            }
            if (loop != DoTweenAnisLoop.No)
            {
                _sequence.SetLoops(-1, (LoopType)((int)loop));
            }

            return _sequence;
        }
    }
}
