using System;
using UnityEngine;

namespace GameFramework
{
    public class PoolModule : ModuleBase
    {
        public static PoolModule instance
        {
            get
            {
                return ModuleController.instance.GetModule<PoolModule>();
            }
        }

        private Dictionary<Type, IQueue> pools;

        public override void Init(long _id)
        {
            base.Init(_id);
            pools = new Dictionary<Type, IQueue>();
        }
        public override void Destroy()
        {
            base.Destroy();
            foreach (var _kvp in pools)
            {
                _kvp.Value.Destroy();
            }
            pools.Clear();
            pools = null;
        }

        public T Pop<T> (string _key) where T : UnityEngine.Object
        {
            return CheckQueue(typeof(T)).Pop<T>(_key);
        }
        public void Push<T>(string _key, T _t) where T : UnityEngine.Object
        {
            CheckQueue(typeof(T)).Push(_key, _t);
        }

        private IQueue CheckQueue (Type _type)
        {
            if (Equals (_type, typeof (GameObject)))
            {
                if (!pools.ContainsKey(_type))
                {
                    pools.Add(_type, new GameObjectPool());
                }
                return pools[_type];
            }
            if (!pools.ContainsKey(_type))
            {
                _type = typeof(UnityEngine.Object);
                pools.Add(_type, new ObjectPool());
                return pools[_type];
            }
            return null;
        }

        private bool Equals(Type _sou, Type _tar)
        {
            return _sou.Equals(_tar) || _sou.IsSubclassOf(_tar);
        }
    }
}