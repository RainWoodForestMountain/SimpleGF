using System;
using UnityEngine;
using DG.Tweening;

namespace GameFramework
{
    [Serializable]
    public class ChildMove
    {
        public GameObject child;
        public Transform start;
        public Transform target;
        public Ease ease;

        public bool Check ()
        {
            return child != null && start != null && target != null;
        }
    }

    public class AutoAnimation_ChildsMove : AutoAnimation_Base
    {
        [SerializeField]
        private float time = 0.5f;
        [SerializeField]
        private bool pingpeng = true;
        [SerializeField]
        private ChildMove[] childs;

        private bool onIn = false;

        public override void OnEnd()
        {
            if (!onIn) base.OnEnd();
        }

        public override void Play(Action _cb)
        {
            base.Play(_cb);

            if (pingpeng) onIn = !onIn;

            Transform _s, _t;
            for (int i = 0; i < childs.Length; i++)
            {
                if (!childs[i].Check()) continue;
                if (onIn)
                {
                    _s = childs[i].start;
                    _t = childs[i].target;
                }
                else
                {
                    _s = childs[i].target;
                    _t = childs[i].start;
                }
                Transform _child = childs[i].child.transform;
                _child.position = _s.position;
                Tween _tween = DOTween.To(()=>_child.position, (_pos)=>_child.position = _pos, _t.position, time);
                _tween.SetEase(childs[i].ease);
                _tween.Play();
            }

            Invoke("OnEnd", time);
        }
    }
}
