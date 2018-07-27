using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameFramework
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Image))]
    public class UGUIFlash : MonoBase
    {
        [Serializable]
        public enum flashType
        {
            Single,
            Loop,
            Pingpong,
        }

        public flashType type;
        public float totalTime;
        public float delay;
        public bool useNativeSize = false;
        public bool autoPlayOnEnable = false;
        public bool autoDisableOnEnd = false;
        public bool disableWhenClose = true;

        public Sprite[] res;

        private Image image;
        private bool isRunning = false;
        private float spaceTime = 0;
        private int pos = -1;
        private bool added = true;

        private void Awake()
        {
            image = GetComponent<Image>();
        }
		private void Start()
		{
			spaceTime = totalTime / (res.Length + 1);
		}
        private void OnEnable()
        {
            if (autoPlayOnEnable)
            {
                Play();
            }
        }
        private void OnDisable()
        {
            if (disableWhenClose)
            {
                Stop();
            }
        }
        private void RunningDelay ()
        {
            Invoke("RunningRes", delay);
        }
        private void RunningRes()
        {
            if (added) pos++;
            else pos--;

            if (pos >= res.Length)
            {
                switch (type)
                {
                    case flashType.Single:
                        Stop();
                        return;
                    case flashType.Loop:
                        pos = 0;
                        break;
                    case flashType.Pingpong:
                        pos = res.Length - 2;
                        added = !added;
                        break;
                }
            }
            //这种只能是pingpong了
            if (pos < 0)
            {
                pos = 1;
                added = !added;
            }

            image.sprite = res[pos];
            if (useNativeSize)
            {
                image.SetNativeSize();
            }
            Invoke("RunningRes", spaceTime);
        }


        public void Play ()
        {
            isRunning = true;
            pos = 0;
            gameObject.SetActive(true);
            RunningDelay();
        }
        public void Stop ()
        {
            isRunning = false;
            pos = 0;
            if (autoDisableOnEnd) gameObject.SetActive(false);
        }
        public void Pause ()
        {
            isRunning = false;
        }
        public void Continue()
        {
            isRunning = true;
        }
    }
}
