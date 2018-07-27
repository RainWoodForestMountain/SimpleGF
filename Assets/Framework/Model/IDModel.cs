using System;

namespace GameFramework
{
	public class IDModel
	{
		public IDModel (string _name)
		{
			insName = _name;
			curId = long.MinValue;
		}
		public IDModel ()
		{
			insName = string.Empty;
			curId = long.MinValue;
		}

		private long curId;
		private string insName;

		public string GetID
		{
			get 
			{
				string _id = Utility.MergeString (insName, curId);
				curId++;
				return _id;
			}
		}
		public long GetLongId
		{
			get
			{
				long _id = curId;
				curId++;
				return _id;
			}
		}
	}
}