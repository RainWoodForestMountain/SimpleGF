using System;

namespace GameFramework
{
	public class List<TValue> : System.Collections.Generic.List<TValue>
	{
        public List() : base() { }
        public List(System.Collections.Generic.IEnumerable<TValue> collection) : base(collection) { }
    }
}