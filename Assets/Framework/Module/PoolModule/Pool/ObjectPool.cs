using UnityEngine;

namespace GameFramework
{
    public class ObjectPool : IQueue
    {
        private Dictionary<string, System.Collections.Generic.Queue<Object>> queues;

        public ObjectPool()
        {
            queues = new Dictionary<string, System.Collections.Generic.Queue<Object>>();
        }
        public void Destroy()
        {
            queues.Clear();
            queues = null;
        }

        public T Pop<T>(string _key) where T : Object
        {
            if (!queues.ContainsKey(_key))
            {
                return null;
            }
            return queues[_key].Dequeue() as T;
        }

        public void Push<T>(string _key, T _t) where T : Object
        {
            if (_t == null) return;

            if (!queues.ContainsKey(_key))
            {
                queues.Add(_key, new System.Collections.Generic.Queue<Object>());
            }
            queues[_key].Enqueue(_t);
        }
    }
}