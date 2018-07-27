using UnityEngine;

namespace GameFramework
{
    public interface IQueue : INeedDestroy
    {
        void Push<T>(string _key, T _t) where T : Object;
        T Pop<T>(string _key) where T : Object;
    }
}