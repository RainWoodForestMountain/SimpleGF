using System;
using UnityEngine;

namespace GameFramework
{
	public class MonoBase : MonoBehaviour
	{
		private Transform trans;
		public new Transform transform
		{
			get 
			{
				if (trans == null)
				{
					trans = base.transform;
				}
				return trans;
			}
		}
		public Transform parent
		{
			get
			{
				return transform.parent;
			}
			set
			{ 
				transform.parent = value;
			}
		}
        public Vector3 position
        {
            get
            {
                return transform.position;
            }
            set
            {
                transform.position = value;
            }
        }
        public Vector3 eulerAngles
        {
            get
            {
                return transform.eulerAngles;
            }
            set
            {
                transform.eulerAngles = value;
            }
        }
        public Vector3 localPosition
        {
            get
            {
                return transform.localPosition;
            }
            set
            {
                transform.localPosition = value;
            }
        }
        public Vector3 localEulerAngles
        {
            get
            {
                return transform.localEulerAngles;
            }
            set
            {
                transform.localEulerAngles = value;
            }
        }
        public Vector3 localScale
        {
            get
            {
                return transform.localScale;
            }
            set
            {
                transform.localScale = value;
            }
        }
        public Quaternion rotation
        {
            get
            {
                return transform.rotation;
            }
            set
            {
                transform.rotation = value;
            }
        }
        public Quaternion localRotation
        {
            get
            {
                return transform.localRotation;
            }
            set
            {
                transform.localRotation = value;
            }
        }

        public event Action onAwakeEvent;
        public event Action onStartEvent;
        public event Action onEnableEvent;
		public event Action onDisableEvent;
		public event Action onUpdateEvent;
        public event Action onDestroyEvent;

        private void Awake ()
		{
			if (onAwakeEvent != null) 
			{
				onAwakeEvent ();
			}
		}
        private void Start()
        {
            if (onStartEvent != null)
            {
                onStartEvent();
            }
        }
        private void OnEnable()
        {
            if (onEnableEvent != null)
            {
                onEnableEvent();
            }
        }
        private void OnDisable()
        {
            if (onDisableEvent != null)
            {
                onDisableEvent();
            }
        }
        private void Update()
        {
            if (onUpdateEvent != null)
            {
                onUpdateEvent();
            }
        }
        private void OnDestroy()
        {
            if (onDestroyEvent != null)
            {
                onDestroyEvent();
            }
        }
    }
}