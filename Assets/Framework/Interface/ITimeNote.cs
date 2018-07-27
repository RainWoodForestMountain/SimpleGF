namespace GameFramework
{
	public interface ITimeNote
	{
		void TryWorking (float _time);
		bool IsComplete ();
		bool IsIDEqual (long _id);
		long GetID ();
        /// <summary>
        /// 传入一个字典，对计时器进行改变
        /// </summary>
        void Change(Dictionary<string, string> _dic);
	}
}
