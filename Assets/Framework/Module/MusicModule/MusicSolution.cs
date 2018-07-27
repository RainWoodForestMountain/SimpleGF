using UnityEngine;
using DG.Tweening;

namespace GameFramework
{
    public class MusicSolution
    {
        private GameObject autoListener;
        private AudioSource bgm;
        private AudioClip nextBgm;
        private List<AudioSource> audios;

        public void Init()
        {
            autoListener = new GameObject("GameFramework.MusicModule");
            Object.DontDestroyOnLoad(autoListener);
            autoListener.AddComponent<AudioListener>();

            audios = new List<AudioSource>();

            GameObject _bgm = new GameObject("bgm");
            UtilityUnity.SetParent(_bgm, autoListener);
            bgm = _bgm.AddComponent<AudioSource>();
            bgm.loop = true;

            Refresh();
        }
        public void Destroy()
        {
            UtilityUnity.DestroyGameObject(autoListener);
        }

        public void Activate()
        {
            if (bgm) bgm.mute = false;
            for (int i = 0; i < audios.Count; i++)
            {
                audios[i].mute = false;
            }
        }

        public void Sleep()
        {
            if (bgm) bgm.mute = true;
            for (int i = 0; i < audios.Count; i++)
            {
                audios[i].mute = true;
            }
        }

        public void PlayBGM (AudioClip _bgm)
        {
            if (bgm.clip == null)
            {
                bgm.clip = _bgm;
                bgm.Play();
                Refresh();
                return;
            }
            nextBgm = _bgm;
            StartChangeBGM();
        }
        private void StartChangeBGM ()
        {
            Tween _tween = DOTween.To(()=>bgm.volume, (_v)=>bgm.volume = _v, 0.4f, 1);
            _tween.SetEase(Ease.Linear);
            _tween.OnComplete(OnStartPlayerBGM);
            _tween.Play();
        }
        private void OnStartPlayerBGM ()
        {
            bgm.volume = 1;
            bgm.clip = nextBgm;
            bgm.Play();
            Refresh();
        }
        public void PlayAudio(AudioClip _audio)
        {
            AudioSource _as = null;
            GameObject _new = PoolModule.instance.Pop<GameObject>("AUDIO_PLAYER");
            if (_new == null)
            {
                _new = new GameObject(_audio.name);
                _as = _new.AddComponent<AudioSource>();
            }
            else
            {
                _new.SetActive(true);
                _as = _new.GetComponent<AudioSource>();
            }
            UtilityUnity.SetParent(_new, autoListener);
            _as.clip = _audio;
            _as.mute = !PersistenceData.GetPrefsDataBool(CommonKey.SWITCH_AUDIO, true);
            _as.Play();
            Refresh();
            audios.Add(_as);
        }

        public void Refresh()
        {
            bgm.mute = !PersistenceData.GetPrefsDataBool(CommonKey.SWITCH_MUSIC, true);
            bool _audio = !PersistenceData.GetPrefsDataBool(CommonKey.SWITCH_AUDIO, true);
            for (int i = 0; i < audios.Count; i++)
            {
                audios[i].mute = _audio;
                if (audios[i].isPlaying) continue;

                audios[i].clip = null;
                PoolModule.instance.Push("AUDIO_PLAYER", audios[i].gameObject);
                //UtilityUnity.DestroyGameObject(audios[i].gameObject);
                audios.RemoveAt(i);
                i--;
            }
        }
    }
}