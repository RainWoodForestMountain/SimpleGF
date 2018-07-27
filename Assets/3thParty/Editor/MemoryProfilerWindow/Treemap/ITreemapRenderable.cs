using UnityEngine;

namespace Assets.Editor.Treemap
{
    interface ITreemapRenderable
    {
        Color GetColor();
        Rect GetPosition();
        string GetLabel();
    }
}
