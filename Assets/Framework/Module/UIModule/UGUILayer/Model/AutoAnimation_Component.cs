using UnityEngine;

namespace GameFramework
{
    public class AutoAnimation_Component : AutoAnimation_Base, IAutoAnimation
    {
        private Animator animator;
        private new Animation animation;

        public override void OnEnd()
        {
            if (animator != null)
            {
                AnimatorStateInfo _animatorInfo;
                _animatorInfo = animator.GetCurrentAnimatorStateInfo(0);
                if (_animatorInfo.normalizedTime >= 1)
                {
                    gameObject.SetActive(false);
                    if (callback != null) callback();
                }
                return;
            }
            if (animation != null)
            {
                if (!animation.isPlaying)
                {
                    gameObject.SetActive(false);
                    if (callback != null) callback();
                }
                return;
            }
        }

        public override void Play(System.Action _cb)
        {
            base.Play(_cb);

            if (animator == null) animator = GetComponent<Animator>();
            if (animator != null)
            {
                animator.Play("start");
                return;
            }

            if (animation == null) animation = GetComponent<Animation>();
            if (animation != null)
            {
                animation.Play();
                return;
            }
        }
        protected void Update()
        {
            OnEnd();
        }
    }
}
