namespace GameFramework
{
    public class Slider : UnityEngine.UI.Slider
    {
        private System.Action<float> listener;
        private new void Awake()
        {
            onValueChanged.AddListener(OnValueChanged);
        }
        private void OnValueChanged (float _v)
        {
            if (listener != null) listener(_v);
        }
        public void SetOnValueChangedListener (System.Action<float> _lis)
        {
            listener = _lis;
        }
    }
}
