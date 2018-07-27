using System.Collections.Generic;

namespace GameFramework
{
	public class Dictionary<TKey, TValue> : System.Collections.Generic.Dictionary<TKey, TValue>
    {
        public new void Add (TKey _key, TValue _value)
		{
			if (base.ContainsKey(_key))
			{
				base[_key] = _value;
			}
			else
			{
				base.Add (_key, _value);
			}
		}
        public new void Remove (TKey _key)
        {
            if (base.ContainsKey(_key))
            {
                base.Remove(_key);
            }
        }

        public new TValue this[TKey key]
        {
            get
            {
                if (base.ContainsKey(key)) return base[key];
                else return default(TValue);
            }
            set
            {
                this.Add(key, value);
            }
        }

        public List<TValue> ToList()
        {
            List<TValue> _list = new List<TValue>();
            foreach (var _kvp in this)
            {
                _list.Add(_kvp.Value);
            }
            return _list;
        }
    }
}