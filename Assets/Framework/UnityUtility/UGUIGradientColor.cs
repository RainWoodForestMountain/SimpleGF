using UnityEngine;
using UnityEngine.UI;

namespace GameFramework
{
    public class UGUIGradientColor : BaseMeshEffect
    {
        [SerializeField]
        private Color32 topColor = Color.white;

        [SerializeField]
        private Color32 bottomColor = Color.white;

        public override void ModifyMesh(VertexHelper _vh)
        {
            if (!IsActive())
            {
                return;
            }

            List<UIVertex> _vertexList = new List<UIVertex>();
            _vh.GetUIVertexStream(_vertexList);
            int _count = _vertexList.Count;
            if (_count > 0)
            {
                ApplyGradient(_vertexList, 0, _count);
            }
            _vh.Clear();
            _vh.AddUIVertexTriangleStream(_vertexList);
        }
        private void ApplyGradient(List<UIVertex> _vertexList, int _start, int _end)
        {
            float _bottomY = _vertexList[0].position.y;
            float _topY = _vertexList[0].position.y;
            for (int i = _start; i < _end; ++i)
            {
                float y = _vertexList[i].position.y;
                if (y > _topY)
                {
                    _topY = y;
                }
                else if (y < _bottomY)
                {
                    _bottomY = y;
                }
            }

            float _uiElementHeight = _topY - _bottomY;
            for (int i = _start; i < _end; ++i)
            {
                UIVertex _uiVertex = _vertexList[i];
                _uiVertex.color = Color32.Lerp(bottomColor, topColor, (_uiVertex.position.y - _bottomY) / _uiElementHeight);
                _vertexList[i] = _uiVertex;
            }
        }
    }
}