namespace GameFramework
{
    public interface IAdaptation
    {
        int scaleType { set; }
        void Refresh(float _width, float _heigth);
    }
}
