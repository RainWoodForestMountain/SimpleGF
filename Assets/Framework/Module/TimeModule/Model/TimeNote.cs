using System;
using UnityEngine;

namespace GameFramework
{
	/// <summary>
	/// 时间节点链：按顺序执行某些事件，并需要指定每一个事件之间的时间间隔，如果时间间隔的数量小于事件，则会使用末尾的时间补足
	/// </summary>
	internal class TimeNoteChain : ITimeNote
	{
		/// <summary>
		/// 上一次执行了时间节点的时间点，初始化记录创建时间
		/// </summary>
		private float lastWorkingTime;
		/// <summary>
		/// 当前循环的次数，只要onEnd执行一次（不存在也会被计算）则加1
		/// </summary>
		private int alreadyLoopCount;
		/// <summary>
		/// 运行时唯一id标志
		/// </summary>
		private long runningID;

		private float[] loopSpaceTime;
		private Action[] worker;

		/// <summary>
		/// 是否可执行。为了提高效率，时间由外部传入
		/// </summary>
		public void TryWorking (float _time)
		{
			if (IsComplete ())
			{
				return;
			}
			if (_time - lastWorkingTime >= GetSpaceTime(alreadyLoopCount))
			{
				lastWorkingTime = _time;
				if (worker[alreadyLoopCount] != null)
				{
					worker[alreadyLoopCount] ();
				}
				alreadyLoopCount++;
			}
		}
		/// <summary>
		/// 是否已经完成循环
		/// </summary>
		public bool IsComplete ()
		{
			if (alreadyLoopCount >= worker.Length)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 检查id
		/// </summary>
		public bool IsIDEqual (long _id)
		{
			return runningID == _id;
		}
		public long GetID ()
		{
			return runningID;
		}

		private float GetSpaceTime (int _pos)
		{
			_pos = _pos < loopSpaceTime.Length ? _pos : loopSpaceTime.Length - 1;
			return loopSpaceTime[_pos];
		}

        public void Change(Dictionary<string, string> _dic)
        {
        }

        public TimeNoteChain (long _id, Action[] _worker, float[] _loopSpaceTime, float _delay)
		{
            runningID = _id;
			lastWorkingTime = Time.time + _delay;
			alreadyLoopCount = 0;
			loopSpaceTime = _loopSpaceTime;
			worker = _worker;
		}
	}
	/// <summary>
	/// 时间节点：单次或则循环执行某一事件
	/// </summary>
	internal class TimeNote : ITimeNote
	{
		/// <summary>
		/// 循环的最大次数，小于等于0表示无限次数
		/// </summary>
		private int loopCount;
		/// <summary>
		/// 当前循环的次数，只要onEnd执行一次（不存在也会被计算）则加1
		/// </summary>
		private int alreadyLoopCount;
		/// <summary>
		/// 每次循环的时间，最小时间为一帧
		/// </summary>
		private float loopSpace;
		/// <summary>
		/// 执行功能，每一次循环结束的时候执行一次。循环结束的最后一次也会被执行
		/// </summary>
		private Action onEnd;
		/// <summary>
		/// 运行时唯一id标志
		/// </summary>
		private long runningID;
		/// <summary>
		/// 上一次执行了时间节点的时间点，初始化记录创建时间
		/// </summary>
		private float lastWorkingTime;

		/// <summary>
		/// 是否可执行。为了提高效率，时间由外部传入
		/// </summary>
		public void TryWorking (float _time)
		{
			if (IsComplete ())
			{
				return;
			}
			if (_time - lastWorkingTime >= loopSpace)
			{
				lastWorkingTime = _time;
				alreadyLoopCount++;
				if (onEnd != null)
				{
					onEnd ();
				}
			}
		}
		/// <summary>
		/// 是否已经完成循环
		/// </summary>
		public bool IsComplete ()
		{
			if (loopCount > 0)
			{
				return alreadyLoopCount >= loopCount;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 检查id
		/// </summary>
		public bool IsIDEqual (long _id)
		{
			return runningID == _id;
		}
		public long GetID ()
		{
			return runningID;
		}

        public void Change(Dictionary<string, string> _dic)
        {
            try
            {
                if (_dic.ContainsKey("loopSpace"))
                {
                    loopSpace = float.Parse(_dic["loopSpace"]);
                }
                if (_dic.ContainsKey("loopCount"))
                {
                    loopCount = int.Parse(_dic["loopCount"]);
                }
            }
            catch
            {
                Utility.LogError("修改计时器失败！计时器id = ", runningID);
            }
        }

        public TimeNote (long _id, Action _end, int _loopCount, float _loopSpace, float _delay)
		{
            runningID = _id;
            alreadyLoopCount = 0;
			lastWorkingTime = (Time.time - _loopSpace) + _delay;
			loopCount = _loopCount;
			loopSpace = _loopSpace;
			onEnd = _end;
		}
	}
}