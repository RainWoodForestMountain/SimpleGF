namespace GameFramework
{
    public interface IUILayer : ICanStart
    {
        void ChangeAlpha(float _a, int _layer);
        void AddObjectToLayer(UnityEngine.GameObject _obj, int _layer);
        void RemoveObjectFromLayer(UnityEngine.GameObject _obj, int _layer);
        void Refresh(int _layer = int.MinValue);
    }
}