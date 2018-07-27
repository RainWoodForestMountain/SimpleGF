using UnityEngine;

namespace GameFramework
{
	public class GameObjectLayoutGridGroup : MonoBehaviour
	{
		public enum LayoutAlignmentXAxis
		{
			Left,
			Right,
		}

		public enum LayoutAlignmentYAxis
		{
			Top,
			Bottom,
		}

		// 布局方式
		public LayoutAlignmentXAxis ChildAlignmentXAxis = LayoutAlignmentXAxis.Left;
		public LayoutAlignmentYAxis ChildAlignmentYAxis = LayoutAlignmentYAxis.Top;

		// 是否开启反向布局
		public bool EnableXAxisInverse = false;
		public bool EnableYAxisInverse = false;

		public bool EnableZAxisInverse = false;

		// 布局元素限制数量
		public int XAxisLayoutElementCount = 0;
		public int YAxisLayoutElementCount = 0;

		// 间隔
		public float XAxisSpace = 0;
		public float YAxisSpace = 0;
		public float ZAxisSpace = 0;
		
		public Vector3 LayoutSize
		{
			get {
				Vector3 size = last;
				size.Scale(transform.lossyScale);
				return size;
			}
		}

		protected bool IsLimitXAxis
		{
			get { return XAxisLayoutElementCount > 0; }
		}

		protected bool IsLimitYAxis
		{
			get { return XAxisLayoutElementCount > 0 && YAxisLayoutElementCount > 0; }
		}

		Vector3 last;

		[ContextMenu("Refresh")]
		public void Refresh()
		{
			// 初始化
			int xcount = 0, ycount = 0, zcount = 0;
			last = Vector3.zero;

			List<Transform> trans_list = new List<Transform>();
			List<GameObjectLayoutElement> elem_list = new List<GameObjectLayoutElement>();
			for (int i = 0; i < transform.childCount; i++)
			{
				var _t = transform.GetChild(i);
				var _elem = _t.GetComponent<GameObjectLayoutElement>();
				//忽略隐藏或者没有GameObjectLayoutElement组件的物体
				if (!_t.gameObject.activeSelf || !_t.gameObject.activeInHierarchy || _elem == null)
				{
					continue;
				}

				trans_list.Add(_t);
				elem_list.Add(_elem);
			}

			if (IsLimitYAxis)
			{
				RefreshZAxisBlock(trans_list, elem_list, 0, trans_list.Count / (XAxisLayoutElementCount * YAxisLayoutElementCount),
					trans_list.Count % (XAxisLayoutElementCount * YAxisLayoutElementCount));
			}
			else if (IsLimitXAxis)
			{
				RefreshYAxisBlock(trans_list, elem_list, 0, trans_list.Count / XAxisLayoutElementCount,
					trans_list.Count % XAxisLayoutElementCount);
			}
			else
			{
				RefreshXAxisBlock(trans_list, elem_list, 0, trans_list.Count);
			}
		}

		protected void RefreshZAxisBlock(List<Transform> trans_list, List<GameObjectLayoutElement> elem_list, int begin,
			int blockCount, int remainLen)
		{
			if (begin + blockCount * XAxisLayoutElementCount * YAxisLayoutElementCount + remainLen > trans_list.Count)
			{
				return;
			}

			last.z = 0;
			for (int i = 0; i < blockCount; i++)
			{
				int index = begin + i * XAxisLayoutElementCount * YAxisLayoutElementCount;
				if (EnableZAxisInverse)
				{
					index = begin + (blockCount - i - 1) * XAxisLayoutElementCount * YAxisLayoutElementCount;
				}

				var _t = trans_list[index];
				var _elem = elem_list[index];
				RefreshYAxisBlock(trans_list, elem_list, index, YAxisLayoutElementCount, 0);
				last.z += (_elem.LayoutSize.z + ZAxisSpace) / transform.lossyScale.z;
			}

			int _begin = blockCount * XAxisLayoutElementCount * YAxisLayoutElementCount;
			int remain = trans_list.Count - _begin;
			RefreshYAxisBlock(trans_list, elem_list, _begin, remain / XAxisLayoutElementCount, remain % XAxisLayoutElementCount);
		}

		protected void RefreshYAxisBlock(List<Transform> trans_list, List<GameObjectLayoutElement> elem_list, int begin,
			int blockCount, int remainLen)
		{
			if (begin + blockCount * XAxisLayoutElementCount + remainLen > trans_list.Count ||
			    (blockCount == 0 && remainLen == 0))
			{
				return;
			}

			last.y = 0;
			float offsety = 0;
			if (ChildAlignmentYAxis == LayoutAlignmentYAxis.Bottom && IsLimitYAxis && blockCount != YAxisLayoutElementCount)
			{
				offsety = elem_list[begin].LayoutSize.y *
				          (YAxisLayoutElementCount - (Mathf.CeilToInt(remainLen / (float) XAxisLayoutElementCount) + blockCount)) /
				          transform.lossyScale.y;
			}

			for (int i = 0; i < blockCount; i++)
			{
				int index = begin + i * XAxisLayoutElementCount;
				if (EnableYAxisInverse)
				{
					index = begin + (blockCount - i - 1) * XAxisLayoutElementCount;
				}

				var _t = trans_list[index];
				var _elem = elem_list[index];
				last.y += offsety;
				RefreshXAxisBlock(trans_list, elem_list, index, XAxisLayoutElementCount);
				last.y -= offsety;
				last.y += (_elem.LayoutSize.y + YAxisSpace) / transform.lossyScale.y;
			}

			if (remainLen > 0)
			{
				last.y += offsety;
				RefreshXAxisBlock(trans_list, elem_list, begin + blockCount * XAxisLayoutElementCount, remainLen);
				last.y -= offsety;
			}
		}

		protected void RefreshXAxisBlock(List<Transform> trans_list, List<GameObjectLayoutElement> elem_list,
			int begin, int blockCount)
		{
			if (begin + blockCount > trans_list.Count)
			{
				return;
			}

			last.x = 0;
			float offsetx = 0;
			if (ChildAlignmentXAxis == LayoutAlignmentXAxis.Right && IsLimitXAxis && blockCount > 0)
			{
				offsetx = elem_list[begin].LayoutSize.x *
				          (XAxisLayoutElementCount - blockCount) / transform.lossyScale.x;
			}

			if (trans_list.Count - begin < blockCount)
			{
				blockCount = trans_list.Count - begin;
			}

			for (int i = 0; i < blockCount; i++)
			{
				int index = begin + i;
				if (EnableXAxisInverse)
				{
					index = begin + blockCount - i - 1;
				}

				var _t = trans_list[index];
				var _elem = elem_list[index];
				last.x += offsetx;
				_t.localPosition = last + Vector3.zero;
				last.x -= offsetx;
				last.x += (_elem.LayoutSize.x + XAxisSpace) / transform.lossyScale.x;
			}
		}
	}
}