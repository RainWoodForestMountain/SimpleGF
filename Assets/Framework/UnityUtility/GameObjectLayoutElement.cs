using UnityEngine;

namespace GameFramework
{
	public class GameObjectLayoutElement : MonoBehaviour
	{
		public Vector3 LayoutSize;

		[ContextMenu("PerfaceSize")]
		public void PrefactSize()
		{
			MeshRenderer mesh = null;
			var layout = transform.GetComponent<GameObjectLayoutGridGroup>();
			if (layout == null)
			{
				if (mesh == null)
				{
					mesh = transform.GetComponentInChildren<MeshRenderer>();
				}

				if (mesh != null)
				{
					LayoutSize = Quaternion.Inverse(transform.localRotation) * mesh.bounds.size;
					LayoutSize = transform.rotation * LayoutSize;
					LayoutSize = new Vector3(Mathf.Abs(LayoutSize.x), Mathf.Abs(LayoutSize.y), Mathf.Abs(LayoutSize.z));
					LayoutSize.Scale(transform.localScale);
				}
			}
			else
			{
				layout.Refresh();
				LayoutSize = layout.LayoutSize;
			}
		}
	}
}