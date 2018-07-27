using UnityEngine;

namespace GameFramework
{
	public class ObjectList : MonoBehaviour
	{
		public enum Direction
		{
			horizontal,
			vertical,
		}

		public enum Alignment
		{
			Left,
			Right,
		}

		[Tooltip("方向")] public Direction direction;
		[Tooltip("每一组的数量")] public int oneGroupCount = 1;

		public Alignment ChildAlignment = Alignment.Left;

		public float sizeX = 0;
		public float sizeY = 0;

		internal float currentX = 0;
		internal float currentY = 0;

		[ContextMenu("刷新")]
		public void Refresh()
		{
			currentX = 0;
			currentY = 0;
			float _lastSizeX = 0;
			float _lastSizeY = 0;
			int _length = this.transform.childCount;
			if (_length == 0)
			{
				currentX = sizeX;
				currentY = sizeY;
				return;
			}

			ObjectListChild _child = null;
			ObjectList _list = null;
			int _count = -1;
			if (ChildAlignment == Alignment.Left)
			{
				for (int i = 0; i < _length; i++)
				{
					_count++;
					Transform _t = this.transform.GetChild(i);
					//忽略隐藏
					if (!_t.gameObject.activeSelf || !_t.gameObject.activeInHierarchy)
					{
						continue;
					}

					if (_count == 0)
					{
						_t.transform.localPosition = Vector3.zero;

						_lastSizeX = sizeX;
						_lastSizeY = sizeY;
						_child = _t.GetComponent<ObjectListChild>();
						if (_child != null)
						{
							_lastSizeX = _child.sizeX;
							_lastSizeY = _child.sizeY;
						}

						_list = _t.GetComponent<ObjectList>();
						if (_list != null)
						{
							_list.Refresh();
							_lastSizeX = _list.currentX;
							_lastSizeY = _list.currentY;
						}

						continue;
					}

					int _zu = _count % oneGroupCount;

					if (_zu == 0)
					{
						switch (direction)
						{
							case Direction.horizontal:
								currentY = 0;
								currentX += _lastSizeX;
								break;
							case Direction.vertical:
								currentX = 0;
								currentY += _lastSizeY;
								break;
						}
					}
					else
					{
						switch (direction)
						{
							case Direction.horizontal:
								currentY += _lastSizeY;
								break;
							case Direction.vertical:
								currentX += _lastSizeX;
								break;
						}
					}
					

					_lastSizeX = sizeX;
					_lastSizeY = sizeY;
					_child = _t.GetComponent<ObjectListChild>();
					if (_child != null)
					{
						_lastSizeX = _child.sizeX;
						_lastSizeY = _child.sizeY;
					}

					_list = _t.GetComponent<ObjectList>();
					if (_list != null)
					{
						_list.Refresh();
						_lastSizeX = _list.currentX;
						_lastSizeY = _list.currentY;
					}

					_t.localPosition = new Vector3(currentX, currentY, 0);
				}

				switch (direction)
				{
					case Direction.horizontal:
						currentX += _lastSizeX;
						break;
					case Direction.vertical:
						currentY += _lastSizeY;
						break;
				}
			}
			else if (ChildAlignment == Alignment.Right)
			{
				float totalx = 0;
				float totaly = 0;
				for (int i = 0; i < _length; i++)
				{
					Transform _t = this.transform.GetChild(i);
					//忽略隐藏
					if (!_t.gameObject.activeSelf || !_t.gameObject.activeInHierarchy)
					{
						continue;
					}
					switch (direction)
					{
						case Direction.horizontal:
							totalx += _lastSizeY;
							break;
						case Direction.vertical:
							totaly += _lastSizeX;
							break;
					}
				}
				for (int i = _length - 1; i >= 0; --i)
				{
					_count++;
					Transform _t = this.transform.GetChild(i);
					//忽略隐藏
					if (!_t.gameObject.activeSelf || !_t.gameObject.activeInHierarchy)
					{
						continue;
					}

					if (_count == 0)
					{
						_t.transform.localPosition = Vector3.zero;

						_lastSizeX = sizeX;
						_lastSizeY = sizeY;
						_child = _t.GetComponent<ObjectListChild>();
						if (_child != null)
						{
							_lastSizeX = _child.sizeX;
							_lastSizeY = _child.sizeY;
						}

						_list = _t.GetComponent<ObjectList>();
						if (_list != null)
						{
							_list.Refresh();
							_lastSizeX = _list.currentX;
							_lastSizeY = _list.currentY;
						}

						continue;
					}

					int _zu = _count % oneGroupCount;

					if (_zu == 0)
					{
						switch (direction)
						{
							case Direction.horizontal:
								currentY = 0;
								currentX -= _lastSizeX;
								break;
							case Direction.vertical:
								currentX = 0;
								currentY -= _lastSizeY;
								break;
						}
					}
					else
					{
						switch (direction)
						{
							case Direction.horizontal:
								currentY -= _lastSizeY;
								break;
							case Direction.vertical:
								currentX -= _lastSizeX;
								break;
						}
					}

					_lastSizeX = sizeX;
					_lastSizeY = sizeY;
					_child = _t.GetComponent<ObjectListChild>();
					if (_child != null)
					{
						_lastSizeX = _child.sizeX;
						_lastSizeY = _child.sizeY;
					}

					_list = _t.GetComponent<ObjectList>();
					if (_list != null)
					{
						_list.Refresh();
						_lastSizeX = _list.currentX;
						_lastSizeY = _list.currentY;
					}

					_t.localPosition = new Vector3(currentX, currentY, 0);
				}

				switch (direction)
				{
					case Direction.horizontal:
						currentX -= _lastSizeX;
						break;
					case Direction.vertical:
						currentY -= _lastSizeY;
						break;
				}
			}
		}
	}
}