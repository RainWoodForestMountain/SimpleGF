namespace GameFramework
{
    public interface IAutoAnimation
    {
        void Play(System.Action _cb);
        void OnEnd();
    }
}