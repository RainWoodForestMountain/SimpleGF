using UnityEngine;
using System;
using System.Collections;

namespace GameFramework
{
	public class TimeModule : ModuleBase, IModule
	{
		public static TimeModule instance
		{
			get 
			{
				return ModuleController.instance.GetModule<TimeModule> ();
			}
		}
		private List<ITimeNote> timeNotes;
		private MonoBase mono;
        private IDModel idmodel;

		public override void Init (long _id)
		{
			base.Init (_id);
            idmodel = new IDModel();
            timeNotes = new List<ITimeNote>();
			GameObject _temp = new GameObject ();
			UnityEngine.Object.DontDestroyOnLoad (_temp);
			mono = _temp.AddComponent<MonoBase> ();
            mono.onUpdateEvent += Update;
            _temp.name = "GameFramework.TimeModule";
		}
		public override void Destroy ()
		{
			timeNotes.Clear ();
			mono.StopAllCoroutines ();
			UnityEngine.Object.Destroy (mono.gameObject);
		}
		public override void Activate() 
		{
			base.Activate ();
			Time.timeScale = 1;
		}
		public override void Sleep() 
		{
			base.Sleep ();
			Time.timeScale = 0;
		}
		public void SetTimeSpeed (float _speed)
		{
			Time.timeScale = _speed;
		}


		public long Register (Action _end)
		{
			return Register (_end, 1, 0);
		}
		public long Register (Action _end, int _loopCount)
		{
			return Register (_end, _loopCount, 0);
		}
		public long Register (Action _end, float _loopSpace)
		{
			return Register (_end, 1, _loopSpace);
		}
		public long Register (Action _end, int _loopCount, float _loopSpace)
		{
			return Register (_end, _loopCount, _loopSpace, 0);
		}
		public long Register (Action _end, int _loopCount, float _loopSpace, float _delay)
		{
            long _id = idmodel.GetLongId;
            TimeNote _new = new TimeNote (_id, _end, _loopCount, _loopSpace, _delay);
			timeNotes.Add (_new);
            return _id;
        }

		public long RegisterChain (Action[] _end, float[] _loopSpace)
		{
			return RegisterChain (_end, _loopSpace, 0);
		}
		public long RegisterChain (Action[] _end, float[] _loopSpace, float _delay)
        {
            long _id = idmodel.GetLongId;
            TimeNoteChain _new = new TimeNoteChain (_id, _end, _loopSpace, _delay);
			timeNotes.Add (_new);
            return _id;
        }

		public void RemoveTimeNodeByID (long _id)
		{
			for (int i = 0; i < timeNotes.Count; i++)
			{
				if (timeNotes[i].IsIDEqual (_id))
				{
					timeNotes.RemoveAt (i);
					i--;
				}
			}
		}

		private void Update ()
		{
			if (!isRunning) return;

			float _time = Time.time;
			for (int i = 0; i < timeNotes.Count; i++)
			{
				if (timeNotes[i].IsComplete ())
				{
					timeNotes.RemoveAt (i);
					i--;
					continue;
				}
				timeNotes[i].TryWorking (_time);
			}
		}

		public void RunIEnumerator (IEnumerator _enumerator)
		{
			if (!isRunning) return;

			mono.StartCoroutine (_enumerator);
		}

        public void ChangedTimerSpecaTime (long _id, float _speca)
        {
            for (int i = 0; i < timeNotes.Count; i++)
            {
                if (timeNotes[i].IsIDEqual(_id))
                {
                    Dictionary<string, string> _temp = new Dictionary<string, string>();
                    _temp.Add("loopSpace", _speca.ToString());
                    timeNotes[i].Change(_temp);
                }
            }
        }
        public void ChangedTimerLoopCount(long _id, int _count)
        {
            for (int i = 0; i < timeNotes.Count; i++)
            {
                if (timeNotes[i].IsIDEqual(_id))
                {
                    Dictionary<string, string> _temp = new Dictionary<string, string>();
                    _temp.Add("loopCount", _count.ToString());
                    timeNotes[i].Change(_temp);
                }
            }
        }
    }
}
