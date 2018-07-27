namespace GameFramework
{
    public class MusicModule : ModuleBase
    {
        private MusicSolution solution;

        public override void Init(long _id)
        {
            base.Init(_id);
            solution = new MusicSolution();
            solution.Init();

            MessageModule.instance.AddListener(MessageType.OnMusicStateChanged, OnMusicStateChanged);
            MessageModule.instance.AddListener(MessageType.PlayBGM, OnPlayerBGM);
            MessageModule.instance.AddListener(MessageType.PlayAudio, OnPlayerAudio);
        }
        public override void Destroy()
        {
            base.Destroy();
            solution.Destroy();

            MessageModule.instance.RemoveListener(MessageType.OnMusicStateChanged, OnMusicStateChanged);
            MessageModule.instance.RemoveListener(MessageType.PlayBGM, OnPlayerBGM);
            MessageModule.instance.RemoveListener(MessageType.PlayAudio, OnPlayerAudio);
        }
        public override void Activate()
        {
            base.Activate();
            solution.Activate();
        }

        public override void Sleep()
        {
            base.Sleep();
            solution.Sleep();
        }

        private void OnMusicStateChanged (Message _msg)
        {
            solution.Refresh();
        }
        private void OnPlayerBGM(Message _msg)
        {
            MessageBody_UnityObject _uo = _msg.body as MessageBody_UnityObject;
            if (_uo == null) return;
            UnityEngine.AudioClip _clip = _uo.content as UnityEngine.AudioClip;
            solution.PlayBGM(_clip);
        }
        private void OnPlayerAudio(Message _msg)
        {
            MessageBody_UnityObject _uo = _msg.body as MessageBody_UnityObject;
            if (_uo == null) return;
            UnityEngine.AudioClip _clip = _uo.content as UnityEngine.AudioClip;
            if (Utility.isNull(_clip)) return;
            solution.PlayAudio(_clip);
        }
    }
}