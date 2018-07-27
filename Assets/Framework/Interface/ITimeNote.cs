namespace GameFramework
{
	public interface ITimeNote
	{
		void TryWorking (float _time);
		bool IsComplete ();
		bool IsIDEqual (long _id);
		long GetID ();
        /// <summary>
        /// ����һ���ֵ䣬�Լ�ʱ�����иı�
        /// </summary>
        void Change(Dictionary<string, string> _dic);
	}
}
