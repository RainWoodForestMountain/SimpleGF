using UnityEngine;

namespace GameFramework
{
    public class GameObjectPool : IQueue
    {
        private Dictionary<string, System.Collections.Generic.Queue<GameObject>> queues;
        private Transform root;

        public GameObjectPool()
        {
            GameObject _temp = new GameObject();
            _temp.SetActive(false);
            Object.DontDestroyOnLoad(_temp);
            root = _temp.transform;
            root.name = "GameObjectPool";
            queues = new Dictionary<string, System.Collections.Generic.Queue<GameObject>>();
        }
        public void Destroy()
        {
            Object.Destroy(root.gameObject);
            queues.Clear();
            queues = null;
        }

        public T Pop<T>(string _key) where T : Object
        {
            if (!queues.ContainsKey(_key))
            {
                return null;
            }
            if (queues[_key].Count <= 0)
            {
                return null;
            }
            return queues[_key].Dequeue() as T;
        }

        public void Push<T>(string _key, T _t) where T : Object
        {
            if (_t == null) return;
            GameObject _go = _t as GameObject;

            if (!queues.ContainsKey (_key))
            {
                queues.Add(_key, new System.Collections.Generic.Queue<GameObject>());
            }
            queues[_key].Enqueue(_go);

            _go.transform.parent = root;
        }
    }
}