using UnityEngine;

namespace GameFramework
{
    public class AutoAnimation_Base : MonoBase, IAutoAnimation
    {
        protected System.Action callback;

        public virtual void OnEnd()
        {
            gameObject.SetActive(false);
            if (callback != null) callback();
        }

        public virtual void Play(System.Action _cb)
        {
            callback = _cb;
            gameObject.SetActive(true);
        }
    }
}
